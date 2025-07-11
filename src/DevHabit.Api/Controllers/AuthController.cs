﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevHabit.Api.Controllers;

using Database;
using DTOs.Auth;
using DTOs.Users;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public sealed class AuthController(
    UserManager<IdentityUser> userManager,
    ApplicationIdentityDbContext identityDbContext,
    ApplicationDbContext applicationDbContext) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        await using IDbContextTransaction transaction = await identityDbContext.Database.BeginTransactionAsync();
        applicationDbContext.Database.SetDbConnection(identityDbContext.Database.GetDbConnection());
        await applicationDbContext.Database.UseTransactionAsync(transaction.GetDbTransaction());
        
        var identityUser = new IdentityUser()
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.Name
        };

        IdentityResult identityResult =  await userManager.CreateAsync(identityUser, registerUserDto.Password);

        if (!identityResult.Succeeded)
        {
            var extensions = new Dictionary<string, object?>
            {
                {
                    "errors",
                    identityResult.Errors.ToDictionary(e => e.Code, e => e.Description)
                }
            };
            
            return Problem(
                detail:"Unable to register user",
                statusCode:StatusCodes.Status400BadRequest,
                extensions: extensions);
        }

        User user = registerUserDto.ToEntity();
        user.IdentityId = identityUser.Id;

        applicationDbContext.Users.Add(user);
        await applicationDbContext.SaveChangesAsync();

        await transaction.CommitAsync();
        
        return Ok(user.Id);
    }
}

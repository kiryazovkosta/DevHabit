using DevHabit.Api.Database;
using DevHabit.Api.DTOs.Tags;
using DevHabit.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/tags")]
[ApiController]
public sealed class TagsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TagsCollectionDto>> GetTags()
    {
        List<TagDto> tags = await dbContext
            .Tags
            .Select(TagQueries.ProjectToDto())
            .ToListAsync();
        var result = new TagsCollectionDto()
        {
            Data = tags
        };
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(string id)
    {
        TagDto tag = await dbContext
            .Tags
            .Where(t => t.Id == id)
            .Select(TagQueries.ProjectToDto())
            .FirstOrDefaultAsync();
        if (tag is null)
        {
            return NotFound();
        }
        
        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateTag(CreateTagDto createTagDto)
    {
        Tag tag = createTagDto.ToEntity();
        if (await dbContext.Tags.AnyAsync(t => t.Name == createTagDto.Name))
        {
            return Conflict($"Tag with {tag.Name} already exists");
        }
        
        await dbContext.Tags.AddAsync(tag);
        await dbContext.SaveChangesAsync();
        TagDto dto = tag.ToDto();
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTag(string id, [FromBody]UpdateTagDto updateTagDto)
    {
        Tag? tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        if (tag is null)
        {
            return NotFound();
        }
        
        tag.UpdateFromDto(updateTagDto);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag(string id)
    {
        Tag? tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        if (tag is null)
        {
            return NotFound();
        }
        
        dbContext.Tags.Remove(tag);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}

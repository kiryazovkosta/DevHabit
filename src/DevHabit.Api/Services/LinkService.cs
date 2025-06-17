using DevHabit.Api.DTOs.Common;

namespace DevHabit.Api.Services;

public sealed class LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
{
    public LinkDto Create(
        string endpointName,
        string rel,
        string method,
        object? values = null,
        string? controller = null)
    {
        HttpContext httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new InvalidOperationException("HttpContext is null. Ensure the IHttpContextAccessor is properly configured.");
        }

        string? href = linkGenerator.GetUriByAction(httpContext, endpointName, controller, values);
        if (string.IsNullOrEmpty(href))
        {
            throw new InvalidOperationException($"Unable to generate link for endpoint '{endpointName}'.");
        }

        return new LinkDto
        {
            Href = href, 
            Rel = rel, 
            Method = method
        };
    }
}

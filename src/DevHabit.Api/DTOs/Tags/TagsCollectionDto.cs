namespace DevHabit.Api.DTOs.Tags;

using Common;
using Newtonsoft.Json;

public sealed class TagsCollectionDto : ICollectionResponse<TagDto>, ILinksResponse
{
    public List<TagDto> Items { get; init; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public List<LinkDto> Links { get; set; }
}

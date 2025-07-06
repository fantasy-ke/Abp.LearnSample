namespace BlogSample.Tags;

public class GetPopularTagsInput
{
    public int ResultCount { get; set; } = 10;

    public int? MinimumPostCount { get; set; }
}
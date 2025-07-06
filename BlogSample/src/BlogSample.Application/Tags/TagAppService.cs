using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSample.Tags;

public class TagAppService : BlogSampleAppService, ITagAppService
{
    private readonly ITagRepository _tagRepository;

    public TagAppService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<List<TagDto>> GetPopularTags(Guid blogId, GetPopularTagsInput input)
    {
        var postTags = (await _tagRepository.GetListAsync(blogId)).OrderByDescending(t => t.UsageCount)
            .WhereIf(input.MinimumPostCount != null, t => t.UsageCount >= input.MinimumPostCount)
            .Take(input.ResultCount).ToList();

        return [..ObjectMapper.Map<List<Tag>, List<TagDto>>(postTags)];
    }
}
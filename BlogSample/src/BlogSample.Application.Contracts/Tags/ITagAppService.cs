using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BlogSample.Tags;

public interface ITagAppService : IApplicationService
{
    Task<List<TagDto>> GetPopularTags(Guid blogId, GetPopularTagsInput input);

}
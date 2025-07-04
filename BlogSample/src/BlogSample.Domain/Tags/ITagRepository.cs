using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BlogSample.Tags;

public interface ITagRepository : IRepository<Tag, Guid>
{
    Task<List<Tag>> GetListAsync(Guid blogId);

    Task<Tag> FindByNameAsync(Guid blogId, string name);

    Task<Tag> GetByNameAsync(Guid blogId, string name);
    
    Task<List<Tag>> GetListAsync(IEnumerable<Guid> ids);

    Task DecreaseUsageCountOfTagsAsync(List<Guid> id);

}
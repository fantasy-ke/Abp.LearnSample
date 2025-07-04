using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BlogSample.Blogs;

public interface IBlogRepository: IRepository<Blog,Guid>
{
    Task<Blog> FindByShortNameAsync(string shortName);
}
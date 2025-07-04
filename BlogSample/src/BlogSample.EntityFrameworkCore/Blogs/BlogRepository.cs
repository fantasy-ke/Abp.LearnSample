using System;
using System.Threading;
using System.Threading.Tasks;
using BlogSample.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BlogSample.Blogs;

public class BlogRepository: EfCoreRepository<BlogSampleDbContext, Blog, Guid>, IBlogRepository
{
    public BlogRepository(IDbContextProvider<BlogSampleDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
        
    }

    public async Task<Blog> FindByShortNameAsync(string shortName)
    {
        var dbset = await GetDbSetAsync();
        
        return await dbset.FirstOrDefaultAsync(p => p.ShortName == shortName);
    }
}
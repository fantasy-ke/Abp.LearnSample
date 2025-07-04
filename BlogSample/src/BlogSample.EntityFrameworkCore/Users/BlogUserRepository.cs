using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSample.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace BlogSample.Users;

public class BlogUserRepository: EfCoreUserRepositoryBase<BlogSampleDbContext, BlogUser>, IBlogUserRepository
{
    public BlogUserRepository(IDbContextProvider<BlogSampleDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<List<BlogUser>> GetUsersAsync(int maxCount, string filter)
    {
        return await DbSet.WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.UserName.Contains(filter)).Take(maxCount).ToListAsync();
    }
}
    
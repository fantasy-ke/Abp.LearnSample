using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSample.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BlogSample.Posts;

public class PostRepository : EfCoreRepository<BlogSampleDbContext, Post, Guid>, IPostRepository
{
    public PostRepository(IDbContextProvider<BlogSampleDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Post>> GetPostsByBlogId(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(p => p.BlogId == id).OrderByDescending(p => p.CreationTime)
            .ToListAsync();
    }

    public async Task<bool> IsPostUrlInUseAsync(Guid blogId, string url, Guid? excludingPostId = null)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet.Where(p => blogId == p.BlogId && p.Url == url);

        if (excludingPostId != null)
        {
            query = query.Where(p => excludingPostId != p.Id);
        }

        return await query.AnyAsync();
    }

    public async Task<Post> GetPostByUrl(Guid blogId, string url)
    {
        var dbSet = await GetDbSetAsync();
        var post = await dbSet.FirstOrDefaultAsync(p => p.BlogId == blogId && p.Url == url);

        if (post == null)
        {
            throw new EntityNotFoundException(typeof(Post), nameof(post));
        }

        return post;
    }

    public async Task<List<Post>> GetOrderedList(Guid blogId, bool descending = false)
    {
        var dbSet = await GetDbSetAsync();
        if (!descending)
        {
            return await dbSet.Where(x => x.BlogId == blogId).OrderByDescending(x => x.CreationTime)
                .ToListAsync();
        }
        else
        {
            return await dbSet.Where(x => x.BlogId == blogId).OrderBy(x => x.CreationTime)
                .ToListAsync();
        }
    }

    public override async Task<IQueryable<Post>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).Include(x => x.Tags);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSample.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BlogSample.Comments;

public class CommentRepository:EfCoreRepository<BlogSampleDbContext, Comment, Guid>,ICommentRepository
{
    public CommentRepository(IDbContextProvider<BlogSampleDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Comment>> GetListOfPostAsync(Guid postId)
    {
        return await (await GetDbSetAsync())
            .Where(a => a.PostId == postId)
            .OrderBy(a => a.CreationTime)
            .ToListAsync();
    }

    public async Task<int> GetCommentCountOfPostAsync(Guid postId)
    {
        return await (await GetDbSetAsync())
            .CountAsync(a => a.PostId == postId);
    }

    public async Task<List<Comment>> GetRepliesOfComment(Guid id)
    {
        return await (await GetDbSetAsync())
            .Where(a => a.RepliedCommentId == id).ToListAsync();
    }

    public async Task DeleteOfPost(Guid id)
    {
        var recordsToDelete = await (await GetDbSetAsync()).Where(pt => pt.PostId == id).ToListAsync();
        (await GetDbSetAsync()).RemoveRange(recordsToDelete);
    }
}
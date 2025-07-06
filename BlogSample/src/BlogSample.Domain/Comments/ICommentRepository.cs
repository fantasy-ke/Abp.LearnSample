using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BlogSample.Comments;

public interface ICommentRepository: IRepository<Comment, Guid>
{
    /// <summary>
    /// 根据文章Id 获取评论
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<List<Comment>> GetListOfPostAsync(Guid postId);
    /// <summary>
    /// 根据文章Id 获取评论数量
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<int> GetCommentCountOfPostAsync(Guid postId);
    /// <summary>
    /// 根据评论Id 下面的子获取评论
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<Comment>> GetRepliesOfComment(Guid id);

    Task DeleteOfPost(Guid id);
}
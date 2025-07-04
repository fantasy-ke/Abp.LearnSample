using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BlogSample.Posts;

public interface IPostRepository : IRepository<Post, Guid>
{
    Task<List<Post>> GetPostsByBlogId(Guid id);

    Task<bool> IsPostUrlInUseAsync(Guid blogId, string url, Guid? excludingPostId = null);

    Task<Post> GetPostByUrl(Guid blogId, string url);

    Task<List<Post>> GetOrderedList(Guid blogId, bool descending = false);
}
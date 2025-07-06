using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace BlogSample.Posts;

public class PostCacheInvalidator: ILocalEventHandler<PostChangedEvent>, ITransientDependency
{
    protected IDistributedCache<List<PostCacheItem>> Cache { get; }
    public PostCacheInvalidator(IDistributedCache<List<PostCacheItem>> cache)
    {
        Cache = cache;
    }

    
    public async Task HandleEventAsync(PostChangedEvent eventData)
    {
        await Cache.RemoveAsync(eventData.BlogId.ToString());
    }
}
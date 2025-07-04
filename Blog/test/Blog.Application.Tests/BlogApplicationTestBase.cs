using Volo.Abp.Modularity;

namespace Blog;

public abstract class BlogApplicationTestBase<TStartupModule> : BlogTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

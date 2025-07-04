using Volo.Abp.Modularity;

namespace BlogSample;

public abstract class BlogSampleApplicationTestBase<TStartupModule> : BlogSampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

using Volo.Abp.Modularity;

namespace BlogSample;

/* Inherit from this class for your domain layer tests. */
public abstract class BlogSampleDomainTestBase<TStartupModule> : BlogSampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

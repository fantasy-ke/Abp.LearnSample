using Volo.Abp.Modularity;

namespace BlogSample;

[DependsOn(
    typeof(BlogSampleDomainModule),
    typeof(BlogSampleTestBaseModule)
)]
public class BlogSampleDomainTestModule : AbpModule
{

}

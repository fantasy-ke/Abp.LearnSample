using Volo.Abp.Modularity;

namespace BlogSample;

[DependsOn(
    typeof(BlogSampleApplicationModule),
    typeof(BlogSampleDomainTestModule)
)]
public class BlogSampleApplicationTestModule : AbpModule
{

}

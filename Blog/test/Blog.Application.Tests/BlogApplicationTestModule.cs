using Volo.Abp.Modularity;

namespace Blog;

[DependsOn(
    typeof(BlogApplicationModule),
    typeof(BlogDomainTestModule)
)]
public class BlogApplicationTestModule : AbpModule
{

}

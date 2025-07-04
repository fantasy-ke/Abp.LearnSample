using Volo.Abp.Modularity;

namespace Blog;

[DependsOn(
    typeof(BlogDomainModule),
    typeof(BlogTestBaseModule)
)]
public class BlogDomainTestModule : AbpModule
{

}

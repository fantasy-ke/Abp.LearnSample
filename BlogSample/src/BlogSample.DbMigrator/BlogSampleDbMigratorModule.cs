using BlogSample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BlogSample.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BlogSampleEntityFrameworkCoreModule),
    typeof(BlogSampleApplicationContractsModule)
)]
public class BlogSampleDbMigratorModule : AbpModule
{
}

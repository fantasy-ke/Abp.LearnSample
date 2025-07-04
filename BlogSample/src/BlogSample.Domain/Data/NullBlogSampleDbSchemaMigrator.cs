using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BlogSample.Data;

/* This is used if database provider does't define
 * IBlogSampleDbSchemaMigrator implementation.
 */
public class NullBlogSampleDbSchemaMigrator : IBlogSampleDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

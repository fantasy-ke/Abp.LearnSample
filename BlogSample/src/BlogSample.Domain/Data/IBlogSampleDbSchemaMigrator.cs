using System.Threading.Tasks;

namespace BlogSample.Data;

public interface IBlogSampleDbSchemaMigrator
{
    Task MigrateAsync();
}

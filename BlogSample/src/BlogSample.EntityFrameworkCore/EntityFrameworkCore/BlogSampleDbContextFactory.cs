using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogSample.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class BlogSampleDbContextFactory : IDesignTimeDbContextFactory<BlogSampleDbContext>
{
    public BlogSampleDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        BlogSampleEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<BlogSampleDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new BlogSampleDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BlogSample.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}

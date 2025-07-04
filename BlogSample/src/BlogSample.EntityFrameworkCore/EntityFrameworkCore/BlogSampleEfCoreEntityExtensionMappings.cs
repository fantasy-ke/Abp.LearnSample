using BlogSample.Blogs;
using BlogSample.Comments;
using BlogSample.Posts;
using BlogSample.Tags;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Threading;

namespace BlogSample.EntityFrameworkCore;

public static class BlogSampleEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        BlogSampleGlobalFeatureConfigurator.Configure();
        BlogSampleModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            /* You can configure extra properties for the
             * entities defined in the modules used by your application.
             *
             * This class can be used to map these extra properties to table fields in the database.
             *
             * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
             * USE BlogSampleModuleExtensionConfigurator CLASS (in the Domain.Shared project)
             * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
             *
             * Example: Map a property to a table field:

                 ObjectExtensionManager.Instance
                     .MapEfCoreProperty<IdentityUser, string>(
                         "MyProperty",
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(128);
                         }
                     );

             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
             */
        });
    }

    public static void ConfigureBlogSampleCore(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        if (builder.IsTenantOnlyDatabase())
        {
            return;
        }


        builder.Entity<Blog>(b =>
        {
            b.ToTable(BlogSampleConsts.DbTablePrefix + "Blogs", BlogSampleConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(BlogConsts.MaxNameLength)
                .HasColumnName(nameof(Blog.Name));
            b.Property(x => x.ShortName).IsRequired().HasMaxLength(BlogConsts.MaxShortNameLength);
            b.Property(x => x.Description).IsRequired(false).HasMaxLength(BlogConsts.MaxDescriptionLength);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<Post>(b =>
        {
            b.ToTable(BlogSampleConsts.DbTablePrefix + "Posts", BlogSampleConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.BlogId).HasColumnName(nameof(Post.BlogId));
            b.Property(x => x.Title).IsRequired().HasMaxLength(PostConsts.MaxTitleLength)
                .HasColumnName(nameof(Post.Title));
            b.Property(x => x.CoverImage).IsRequired().HasColumnName(nameof(Post.CoverImage));
            b.Property(x => x.Url).IsRequired().HasMaxLength(PostConsts.MaxUrlLength).HasColumnName(nameof(Post.Url));
            b.Property(x => x.Content).IsRequired(false).HasMaxLength(PostConsts.MaxContentLength)
                .HasColumnName(nameof(Post.Content));
            b.Property(x => x.Description).IsRequired(false).HasMaxLength(PostConsts.MaxDescriptionLength)
                .HasColumnName(nameof(Post.Description));

            b.OwnsMany(p => p.Tags, pd =>
            {
                pd.ToTable(BlogSampleConsts.DbTablePrefix + "PostTags", BlogSampleConsts.DbSchema);

                pd.Property(x => x.TagId).HasColumnName(nameof(PostTag.TagId));
            });

            b.HasOne<Blog>().WithMany().IsRequired().HasForeignKey(p => p.BlogId);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<Tag>(b =>
        {
            b.ToTable(BlogSampleConsts.DbTablePrefix + "Tags", BlogSampleConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength).HasColumnName(nameof(Tag.Name));
            b.Property(x => x.Description).HasMaxLength(TagConsts.MaxDescriptionLength)
                .HasColumnName(nameof(Tag.Description));
            b.Property(x => x.UsageCount).HasColumnName(nameof(Tag.UsageCount));

            b.ApplyObjectExtensionMappings();
        });


        builder.Entity<Comment>(b =>
        {
            b.ToTable(BlogSampleConsts.DbTablePrefix + "Comments", BlogSampleConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Text).IsRequired().HasMaxLength(CommentConsts.MaxTextLength)
                .HasColumnName(nameof(Comment.Text));
            b.Property(x => x.RepliedCommentId).HasColumnName(nameof(Comment.RepliedCommentId));
            b.Property(x => x.PostId).IsRequired().HasColumnName(nameof(Comment.PostId));

            b.HasOne<Comment>().WithMany().HasForeignKey(p => p.RepliedCommentId);
            b.HasOne<Post>().WithMany().IsRequired().HasForeignKey(p => p.PostId);

            b.ApplyObjectExtensionMappings();
        });


        builder.TryConfigureObjectExtensions<BlogSampleDbContext>();
    }
}
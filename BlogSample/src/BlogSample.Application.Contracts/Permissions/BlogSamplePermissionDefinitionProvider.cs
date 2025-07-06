using BlogSample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace BlogSample.Permissions;

public class BlogSamplePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BlogSamplePermissions.GroupName);

        var blogs = myGroup.AddPermission(BlogSamplePermissions.Blogs.Default, L("Permission:Blogs"));
        blogs.AddChild(BlogSamplePermissions.Blogs.Management, L("Permission:Management"));
        blogs.AddChild(BlogSamplePermissions.Blogs.Update, L("Permission:Edit"));
        blogs.AddChild(BlogSamplePermissions.Blogs.Delete, L("Permission:Delete"));
        blogs.AddChild(BlogSamplePermissions.Blogs.Create, L("Permission:Create"));
        blogs.AddChild(BlogSamplePermissions.Blogs.ClearCache, L("Permission:ClearCache"));

        var posts = myGroup.AddPermission(BlogSamplePermissions.Posts.Default, L("Permission:Posts"));
        posts.AddChild(BlogSamplePermissions.Posts.Update, L("Permission:Edit"));
        posts.AddChild(BlogSamplePermissions.Posts.Delete, L("Permission:Delete"));
        posts.AddChild(BlogSamplePermissions.Posts.Create, L("Permission:Create"));

        var tags = myGroup.AddPermission(BlogSamplePermissions.Tags.Default, L("Permission:Tags"));
        tags.AddChild(BlogSamplePermissions.Tags.Update, L("Permission:Edit"));
        tags.AddChild(BlogSamplePermissions.Tags.Delete, L("Permission:Delete"));
        tags.AddChild(BlogSamplePermissions.Tags.Create, L("Permission:Create"));

        var comments = myGroup.AddPermission(BlogSamplePermissions.Comments.Default, L("Permission:Comments"));
        comments.AddChild(BlogSamplePermissions.Comments.Update, L("Permission:Edit"));
        comments.AddChild(BlogSamplePermissions.Comments.Delete, L("Permission:Delete"));
        comments.AddChild(BlogSamplePermissions.Comments.Create, L("Permission:Create"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BlogSamplePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BlogSampleResource>(name);
    }
}

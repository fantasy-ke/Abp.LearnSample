using BlogSample.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace BlogSample;

[DependsOn(
    typeof(BlogSampleDomainModule),
    typeof(BlogSampleApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class BlogSampleApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BlogSampleApplicationModule>();
        });
        
        // 注册
        Configure<AuthorizationOptions>(options =>
        {
            options.AddPolicy("BloggingUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
            options.AddPolicy("BloggingDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
        });

        context.Services.AddSingleton<IAuthorizationHandler, CommentAuthorizationHandler>();
    }
}

using Microsoft.Extensions.Localization;
using BlogSample.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BlogSample;

[Dependency(ReplaceServices = true)]
public class BlogSampleBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BlogSampleResource> _localizer;

    public BlogSampleBrandingProvider(IStringLocalizer<BlogSampleResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

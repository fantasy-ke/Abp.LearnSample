using BlogSample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BlogSample.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BlogSampleController : AbpControllerBase
{
    protected BlogSampleController()
    {
        LocalizationResource = typeof(BlogSampleResource);
    }
}

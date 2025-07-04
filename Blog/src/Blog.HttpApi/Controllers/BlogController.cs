using Blog.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Blog.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BlogController : AbpControllerBase
{
    protected BlogController()
    {
        LocalizationResource = typeof(BlogResource);
    }
}

using BlogSample.Localization;
using Volo.Abp.Application.Services;

namespace BlogSample;

/* Inherit your application services from this class.
 */
public abstract class BlogSampleAppService : ApplicationService
{
    protected BlogSampleAppService()
    {
        LocalizationResource = typeof(BlogSampleResource);
    }
}

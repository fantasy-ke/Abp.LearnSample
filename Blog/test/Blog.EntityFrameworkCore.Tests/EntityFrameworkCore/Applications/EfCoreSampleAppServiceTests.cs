using Blog.Samples;
using Xunit;

namespace Blog.EntityFrameworkCore.Applications;

[Collection(BlogTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BlogEntityFrameworkCoreTestModule>
{

}

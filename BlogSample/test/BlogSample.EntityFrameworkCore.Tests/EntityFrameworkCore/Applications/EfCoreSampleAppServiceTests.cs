using BlogSample.Samples;
using Xunit;

namespace BlogSample.EntityFrameworkCore.Applications;

[Collection(BlogSampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BlogSampleEntityFrameworkCoreTestModule>
{

}

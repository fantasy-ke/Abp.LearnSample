using BlogSample.Samples;
using Xunit;

namespace BlogSample.EntityFrameworkCore.Domains;

[Collection(BlogSampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BlogSampleEntityFrameworkCoreTestModule>
{

}

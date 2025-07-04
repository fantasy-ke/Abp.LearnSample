using Blog.Samples;
using Xunit;

namespace Blog.EntityFrameworkCore.Domains;

[Collection(BlogTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BlogEntityFrameworkCoreTestModule>
{

}

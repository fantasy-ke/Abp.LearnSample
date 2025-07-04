using Xunit;

namespace BlogSample.EntityFrameworkCore;

[CollectionDefinition(BlogSampleTestConsts.CollectionDefinitionName)]
public class BlogSampleEntityFrameworkCoreCollection : ICollectionFixture<BlogSampleEntityFrameworkCoreFixture>
{

}

using Xunit;

namespace Blog.EntityFrameworkCore;

[CollectionDefinition(BlogTestConsts.CollectionDefinitionName)]
public class BlogEntityFrameworkCoreCollection : ICollectionFixture<BlogEntityFrameworkCoreFixture>
{

}

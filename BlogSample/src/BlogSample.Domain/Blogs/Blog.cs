using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BlogSample.Blogs;

public class Blog: FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
}
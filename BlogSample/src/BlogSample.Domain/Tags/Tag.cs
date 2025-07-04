using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BlogSample.Tags;

public class Tag:FullAuditedAggregateRoot<Guid>
{
    public  Guid BlogId { get; protected set; }

    public  string Name { get; protected set; }

    public  string Description { get; protected set; }

    public  int UsageCount { get; protected internal set; }


    protected Tag()
    {

    }

    public Tag(Guid id, Guid blogId, string name, int usageCount = 0, string description = null)
    {
        Id = id;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        BlogId = blogId;
        Description = description;
        UsageCount = usageCount;
    }

    public  void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
    }

    public  void IncreaseUsageCount(int number = 1)
    {
        UsageCount += number;
    }

    public  void DecreaseUsageCount(int number = 1)
    {
        if (UsageCount <= 0)
        {
            return;
        }

        if (UsageCount - number <= 0)
        {
            UsageCount = 0;
            return;
        }

        UsageCount -= number;
    }

    public  void SetDescription(string description)
    {
        Description = description;
    }
}
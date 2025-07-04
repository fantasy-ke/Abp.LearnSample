using System;
using Volo.Abp.Application.Dtos;

namespace BlogSample.Blogs;

public class BlogDto: FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
}
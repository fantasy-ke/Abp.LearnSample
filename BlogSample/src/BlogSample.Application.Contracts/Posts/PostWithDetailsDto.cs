using System;
using System.Collections.Generic;
using BlogSample.Tags;
using Volo.Abp.Application.Dtos;

namespace BlogSample.Posts;

public class PostWithDetailsDto : FullAuditedEntityDto<Guid>
{
    public Guid BlogId { get; set; }

    public string Title { get; set; }

    public string CoverImage { get; set; }

    public string Url { get; set; }

    public string Content { get; set; }

    public string Description { get; set; }

    public int ReadCount { get; set; }

    public int CommentCount { get; set; }

    public BlogUserDto Writer { get; set; }

    public List<TagDto> Tags { get; set; }
}
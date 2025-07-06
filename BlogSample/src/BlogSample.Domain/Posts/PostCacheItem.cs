﻿using System;
using System.Collections.Generic;
using BlogSample.Tags;
using Volo.Abp.Auditing;

namespace BlogSample.Posts;

[Serializable]
public class PostCacheItem : ICreationAuditedObject
{
    public Guid Id { get; set; }

    public Guid BlogId { get; set; }

    public string Title { get; set; }

    public string CoverImage { get; set; }

    public string Url { get; set; }

    public string Content { get; set; }

    public string Description { get; set; }

    public int ReadCount { get; set; }

    public int CommentCount { get; set; }

    public List<Tag> Tags { get; set; }
    
    public DateTime CreationTime { get; }
    
    public Guid? CreatorId { get; }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BlogSample.Posts;

public class Post: FullAuditedAggregateRoot<Guid>
{
    public Guid BlogId { get; protected set; }

    public string Url { get; protected set; }

    public string CoverImage { get; set; }

    public string Title { get; protected set; }

    public string Content { get; set; }

    public string Description { get; set; }

    public int ReadCount { get; protected set; }

    public Collection<PostTag> Tags { get; protected set; }
    
    protected Post()
    {

    }

    public Post(Guid id, Guid blogId,string title,string coverImage,string url)
    {
        Id = id;
        BlogId = blogId;
        Title = Check.NotNullOrWhiteSpace(title, nameof(title));
        Url = Check.NotNullOrWhiteSpace(url, nameof(url));
        CoverImage = Check.NotNullOrWhiteSpace(coverImage, nameof(coverImage));
        Tags = new Collection<PostTag>();
    }

    public virtual Post IncreaseReadCount()
    {
        ReadCount++;
        return this;
    }

    public virtual Post SetTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title));
        return this;
    }

    public virtual Post SetUrl(string url)
    {
        Url = Check.NotNullOrWhiteSpace(url, nameof(url));
        return this;
    }

    public virtual void AddTag(Guid tagId)
    {
        Tags.Add(new PostTag(tagId));
    }

    public virtual void RemoveTag(Guid tagId)
    {
        Tags.RemoveAll(t => t.TagId == tagId);
    }
}
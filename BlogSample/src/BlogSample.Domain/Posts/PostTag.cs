using System;

namespace BlogSample.Posts;

public class PostTag
{
    public virtual Guid TagId { get; init; }  //主键

    protected PostTag()
    {

    }

    public PostTag(Guid tagId)
    {
        TagId = tagId;
    }
}
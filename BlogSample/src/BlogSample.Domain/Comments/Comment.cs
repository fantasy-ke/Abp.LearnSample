using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BlogSample.Comments;

public class Comment : FullAuditedAggregateRoot<Guid>
{
    public  Guid PostId { get; protected set; }

    public  Guid? RepliedCommentId { get; protected set; }

    public  string Text { get; protected set; }

    protected Comment()
    {

    }

    public Comment(Guid id, Guid postId, Guid? repliedCommentId, string text)
    {
        Id = id;
        PostId = postId;
        RepliedCommentId = repliedCommentId;
        Text = Check.NotNullOrWhiteSpace(text, nameof(text));
    }

    public void SetText(string text)
    {
        Text = Check.NotNullOrWhiteSpace(text, nameof(text));
    }
}
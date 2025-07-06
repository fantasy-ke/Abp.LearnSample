using System;
using BlogSample.Posts;
using Volo.Abp.Application.Dtos;

namespace BlogSample.Comments;

public class CommentWithDetailsDto : FullAuditedEntityDto<Guid>
{
    public Guid? RepliedCommentId { get; set; }

    public string Text { get; set; }

    public BlogUserDto Writer { get; set; }
}
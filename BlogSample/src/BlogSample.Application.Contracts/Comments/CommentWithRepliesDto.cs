using System.Collections.Generic;

namespace BlogSample.Comments;

public class CommentWithRepliesDto
{
    public CommentWithDetailsDto Comment { get; set; }

    public List<CommentWithDetailsDto> Replies { get; set; } = new List<CommentWithDetailsDto>();
}
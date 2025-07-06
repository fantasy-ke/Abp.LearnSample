using System.ComponentModel.DataAnnotations;

namespace BlogSample.Comments;

public class UpdateCommentDto
{
    [Required]
    public string Text { get; set; }
}
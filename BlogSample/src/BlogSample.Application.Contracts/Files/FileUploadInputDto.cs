using System.ComponentModel.DataAnnotations;

namespace BlogSample.Files;

public class FileUploadInputDto
{
    [Required]
    public byte[] Bytes { get; set; }

    [Required]
    public string Name { get; set; }
}
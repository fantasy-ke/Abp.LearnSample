namespace BlogSample.Models.Files;

public class FileUploadResult
{
    public string FileUrl { get; set; }

    public FileUploadResult(string fileUrl)
    {
        FileUrl = fileUrl;
    }
}
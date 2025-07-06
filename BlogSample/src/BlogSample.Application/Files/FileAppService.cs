using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.Validation;

namespace BlogSample.Files;

public class FileAppService: BlogSampleAppService, IFileAppService
{
    private readonly IBlobContainer _blobContainer;

    public FileAppService(
        IBlobContainer blobContainer)
    {
        _blobContainer = blobContainer;
    }
    
    public virtual async Task<RawFileDto> GetAsync(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        return new RawFileDto
        {
            Bytes = await _blobContainer.GetAllBytesAsync(name)
        };
    }

    public virtual async Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input)
    {
        if (input.Bytes.IsNullOrEmpty())
        {
            ThrowValidationException("上传文件为空!", "Bytes");
        }

        if (input.Bytes.Length > BlogWebConsts.FileUploading.MaxFileSize)
        {
            throw new UserFriendlyException($"文件大小超出上限 ({BlogWebConsts.FileUploading.MaxFileSizeAsMegabytes} MB)!");
        }

        if (!ImageFormatHelper.IsValidImage(input.Bytes, FileUploadConsts.AllowedImageUploadFormats))
        {
            throw new UserFriendlyException("无效的图片格式!");
        }

        var uniqueFileName = GenerateUniqueFileName(Path.GetExtension(input.Name));

        await _blobContainer.SaveAsync(uniqueFileName, input.Bytes);

        return new FileUploadOutputDto
        {
            Name = uniqueFileName,
            WebUrl = "/api/blog/files/www/" + uniqueFileName
        };
    }
    
    
    private static void ThrowValidationException(string message, string memberName)
    {
        throw new AbpValidationException(message,
            new List<ValidationResult>
            {
                new ValidationResult(message, new[] {memberName})
            });
    }

    protected virtual string GenerateUniqueFileName(string extension, string prefix = null, string postfix = null)
    {
        return prefix + GuidGenerator.Create().ToString("N") + postfix + extension;
    }
}
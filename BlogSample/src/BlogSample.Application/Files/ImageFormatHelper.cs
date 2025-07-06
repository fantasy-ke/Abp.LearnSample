using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using JetBrains.Annotations;

namespace BlogSample.Files;

public class ImageFormatHelper
{
    private static ImageFormat GetImageRawFormat(byte[] fileBytes)
    {
        using var memoryStream = new MemoryStream(fileBytes);
        return Image.FromStream(memoryStream).RawFormat;
    }

    public static bool IsValidImage(byte[] fileBytes,ICollection<ImageFormat> validFormats)
    {
        var imageFormat = GetImageRawFormat(fileBytes);
        return validFormats.Contains(imageFormat);
    }
}
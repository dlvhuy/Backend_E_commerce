
using System.IO;

namespace Ecommerce_BE.Shared.Files
{
  public class FileService : IFileService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileService(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public string DeleteFile(IFormFile file, string existingFilePath)
    {
      if (File.Exists(existingFilePath))
      {
        File.Delete(existingFilePath);
      }

      return UploadFileAsync(file);
    }

    public bool IsExist(string path)
    {
      return File.Exists(path);
    }

    public string UploadFileAsync(IFormFile file)
    {

      var _filePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");

      var fileName = Path.GetFileName(file.FileName);
      fileName = String.Concat(Guid.NewGuid().ToString(), file.FileName);
      var filePath = Path.Combine(_filePath, fileName);

      // Lưu tệp vào thư mục
      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        file.CopyTo(stream);
      }

      var result = $"/image/{fileName}";

      return result;
    }

    public string SetCoverImage(string pathImage)
    {
      return string.Format("{0}://{1}{2}{3}", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host, _httpContextAccessor.HttpContext.Request.PathBase, pathImage);
    }
  }
}

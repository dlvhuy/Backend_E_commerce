namespace Ecommerce_BE.Shared.Files
{
  public interface IFileService
  {
    bool IsExist(string path);
    string UploadFileAsync(IFormFile file);

    string DeleteFile(IFormFile file,string filePath);

    string SetCoverImage(string pathImage);

  }
}

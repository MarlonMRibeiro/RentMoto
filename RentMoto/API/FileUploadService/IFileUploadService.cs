namespace API.FileUploadService
{
    public interface IFileUploadService
    {
        string UploadFile(IFormFile formFile);
    }
}

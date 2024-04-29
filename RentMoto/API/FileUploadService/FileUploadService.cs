using System;

namespace API.FileUploadService
{
    public class FileUploadService : IFileUploadService
    {
        public string UploadFile(IFormFile formFile)
        {

            string addres = @"wwwroot\Images";

            if (!Directory.Exists(addres))
            {
                Directory.CreateDirectory(addres);
            }

            var filePath = Path.Combine(addres, formFile.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);

            formFile.CopyToAsync(fileStream);

            return filePath;
        }

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(string path, IFormFile file=null)
        {
            // 1. get path
            string folderPath = Path.Combine("wwwroot", path);

            // 2. get file name
            string fileName = $"{Guid.NewGuid().ToString()}" + "_" + file.FileName;

            // 3. combine folder path and file name
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine(path, fileName).Replace("\\", "/"); // Use forward slashes for URL
        }
    }
}

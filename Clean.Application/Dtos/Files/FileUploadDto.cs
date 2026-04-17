using Microsoft.AspNetCore.Http;

namespace Clean.Application.Dtos.Files
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
    }
}

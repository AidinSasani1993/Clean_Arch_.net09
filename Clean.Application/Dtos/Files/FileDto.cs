namespace Clean.Application.Dtos.Files
{
    public class FileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

using SimpleVideoCompressor.Models;
using SimpleVideoCompressor.Services;

namespace SimpleVideoCompressor.Controllers
{
    public class MainWindowViewModel
    {
        public string? FilePathNameUri { get; set; }
        public string? DirectFileName { get; set; }
        public string? UploadPathUri { get; set; }

        public string? FileUploadPathUri { get; set; }
        public string? CompressedVideoFileName { get; set; }

        public async Task StartCompression()
        {
            File file = new(FilePathNameUri, DirectFileName);
            CompressedVideoFileName = VideoCompressorService.GenerateFileName();
            FileUploadPathUri = await VideoCompressorService.CompressMedia_H265_HEVC(file, UploadPathUri, CompressedVideoFileName);
        }
    }
}

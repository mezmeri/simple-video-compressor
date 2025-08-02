using SimpleVideoCompressor.Models;
using SimpleVideoCompressor.Utility;

namespace SimpleVideoCompressor.Controllers
{
    public class MainWindowController
    {
        public string? FilePathNameUri { get; set; }
        public string? DirectFileName { get; set; }
        public string? UploadPathUri { get; set; }

        /// <summary>
        /// The path to the compressed file.
        /// </summary>
        public string? FileUploadPathUri { get; set; }
        public string? CompressedVideoFileName { get; set; }

        public async Task StartCompression()
        {
            File file = new(FilePathNameUri, DirectFileName);
            CompressedVideoFileName = VideoCompressor.GenerateFileName();
            FileUploadPathUri = await VideoCompressor.CompressMedia_H265_HEVC(file, UploadPathUri, CompressedVideoFileName);
        }
    }
}

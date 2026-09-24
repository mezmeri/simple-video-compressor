using SimpleVideoCompressor.Models;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SimpleVideoCompressor.Services
{
    public class VideoCompressorService
    {
        private static SemaphoreSlim? _sempahore;

        public async Task CompressFiles(IEnumerable<VideoFile> videoFiles)
        {
            _sempahore = new SemaphoreSlim(3, 3);

            // Making sure the folder "Clips" exists.
            if (!Directory.Exists(Constants.FilePaths.VideosFolder))
            {
                Directory.CreateDirectory(Constants.FilePaths.VideosFolder);
            }


            foreach (VideoFile file in videoFiles)
            {
                await _sempahore.WaitAsync();

                _ = Task.Run(async() =>
                {
                    try
                    {
                        file.VideoStatus = VideoStatus.Rendering;
                        using (Process process = new Process())
                        {
                            string? ffmpegPath = Path.Combine(Environment.CurrentDirectory, "Resources", "Rendering", "ffmpeg", "ffmpeg.exe");
                            process.StartInfo.FileName = ffmpegPath.Replace("\\", "/");

                            process.StartInfo.Arguments = $"-i \"{file.FilePath}\" -c:v hevc \"{Constants.FilePaths.VideosFolder}/{file.FileName}.mp4\"";

                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;

                            process.Start();
                            await process.WaitForExitAsync();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        _sempahore.Release();
                        file.VideoStatus = VideoStatus.Rendered;
                    }
                });
            }

        }

        public static async Task<string> CompressMedia_H265_HEVC(IEnumerable<VideoFile> videoFiles, string uploadPathUri, string outputFileName)
        {
            uploadPathUri = uploadPathUri.Replace("\\", "/");

            try
            {
                using (Process process = new Process())
                {
                    string? ffmpegPath = Path.Combine(Environment.CurrentDirectory, "Resources", "Rendering", "ffmpeg", "ffmpeg.exe");
                    process.StartInfo.FileName = ffmpegPath.Replace("\\", "/");

                    //process.StartInfo.Arguments = $"-i \"{file.FilePath}\" -c:v hevc {uploadPathUri}/{outputFileName}.mp4";

                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.UseShellExecute = false;

                    process.Start();
                    await process.WaitForExitAsync();

                    if (process.ExitCode == 0)
                    {
                        return uploadPathUri;
                    } else
                    {
                        return null;
                    }
                }
            }
            catch (SystemException e)
            {
                throw new SystemException(e.Message);
            }
        }
    }
}

using System.Diagnostics;
using System.IO;

namespace SimpleVideoCompressor.Utility
{
    public class VideoCompressor
    {
        public static string GenerateFileName()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static async Task<string> CompressMedia_H265_HEVC(Models.File file, string uploadPathUri, string outputFileName)
        {
            uploadPathUri = uploadPathUri.Replace("\\", "/");
            try
            {
                using (Process process = new Process())
                {
                    string? ffmpegPath = Path.Combine(Environment.CurrentDirectory, "Resources", "Rendering", "ffmpeg", "ffmpeg.exe");
                    process.StartInfo.FileName = ffmpegPath.Replace("\\", "/");

                    process.StartInfo.Arguments = $"-i \"{file.PathNameUri}/{file.DirectName}\" -c:v hevc {uploadPathUri}/{outputFileName}.mp4";

                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.UseShellExecute = false;

                    process.Start();
                    await process.WaitForExitAsync();

                    if(process.ExitCode == 0)
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

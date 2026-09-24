using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVideoCompressor
{
    public static class Constants
    {
        public static class FilePaths
        {
            public static string VideosFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Clips");
        }
    }
}

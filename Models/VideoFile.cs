using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleVideoCompressor.Models
{
    public enum VideoStatus
    {
        Ready,
        Rendering,
        Rendered,
        Failed
    }

    public class VideoFile : INotifyPropertyChanged
    {
        public string FileName { get; set; }
        public string FileSizeBefore { get; set; }
        public string FileSizeAfter { get; set; }

        private VideoStatus _videoStatus = VideoStatus.Ready;
        public VideoStatus VideoStatus
        {
            get => _videoStatus;
            set
            {
                _videoStatus = value;
                OnPropertyChanged(nameof(VideoStatus));
            }
        }
        public string FilePath { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

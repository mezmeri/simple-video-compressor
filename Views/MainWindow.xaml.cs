using SimpleVideoCompressor.Models;
using SimpleVideoCompressor.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SimpleVideoCompressor
{
    public partial class MainWindow : Window
    {
        private readonly VideoCompressorService _videoCompressorService;
        public ObservableCollection<VideoFile> VideoFiles { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            VideoListView.ItemsSource = VideoFiles;
            _videoCompressorService = new VideoCompressorService();
        }

        private async void Button_CompressAll(object sender, RoutedEventArgs e)
        {
            if (VideoFiles.Count == 0)
            {
                MessageBox.Show($"Cannot compress anything because you haven't added any files.",
                        "Video files missing",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
            } else
            {
                await _videoCompressorService.CompressFiles(VideoFiles);
            }
        }

        private void VideoListView_Drop(object sender, DragEventArgs e)
        {
            string[] allowedExtensions = { ".mp4", ".mov", ".avi", ".wmv" };
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    if (allowedExtensions.Contains(fileInfo.Extension.ToLower()))
                    {
                        VideoFiles.Add(new VideoFile
                        {
                            FileName = fileInfo.Name,
                            FileSizeBefore = $"{fileInfo.Length / (1024 * 1024)} MB",
                            FilePath = fileInfo.FullName,
                            VideoStatus = VideoStatus.Ready,
                        });
                    }
                }
            }
        }

        private void VideoListView_KeyUp_DeleteFileFromList(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                var selectedItemInList = VideoListView.SelectedItems;
                var videoFiles = selectedItemInList.Cast<VideoFile>().ToList();

                foreach (VideoFile videoFile in videoFiles)
                {
                    VideoFiles.Remove(videoFile);
                }
            }
        }

        private void Button_Click_OpenVideosFolder(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Constants.FilePaths.VideosFolder))
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = Constants.FilePaths.VideosFolder,
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            } else
            {
                MessageBox.Show($"The video folder has not been created yet because you haven't rendered any videos.",
                        "Directory missing",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
            }
        }
    }
}

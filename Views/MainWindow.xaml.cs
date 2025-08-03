using Microsoft.Win32;
using SimpleVideoCompressor.Controllers;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SimpleVideoCompressor
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new();
            DataContext = _viewModel;
        }

        private void btn_UploadedFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                textblock_UserFile.Text = dialog.SafeFileName;
                _viewModel.FilePathNameUri = System.IO.Path.GetDirectoryName(dialog.FileName);
                _viewModel.DirectFileName = dialog.SafeFileName;
            }
        }

        private void btn_FilePathUser_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dialog = new OpenFolderDialog();

            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                textblock_UserFilePath.Text = dialog.FolderName;
                _viewModel.UploadPathUri = dialog.FolderName;
            }
        }

        private async void btn_StartCompression_Click(object sender, RoutedEventArgs e)
        {
            btn_FilePathUser.IsEnabled = false;
            btn_FileUploadUser.IsEnabled = false;
            btn_StartCompression.IsEnabled = false;

            try
            {
                await _viewModel.StartCompression();
                string compressedVideoPath = Path.Combine(_viewModel.UploadPathUri, _viewModel.CompressedVideoFileName);
                string fullPath = Path.GetFullPath(compressedVideoPath);
                Process.Start("explorer.exe", $"/select,\"{fullPath}.mp4\"");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Something went wrong. Exception log: {ex.Message}");
                throw;
            }
            finally
            {
                btn_FilePathUser.IsEnabled = true;
                btn_FileUploadUser.IsEnabled = true;
                btn_StartCompression.IsEnabled = true;
                textblock_UserFile.Text = "";
                textblock_UserFilePath.Text = "";
            }

        }
    }
}

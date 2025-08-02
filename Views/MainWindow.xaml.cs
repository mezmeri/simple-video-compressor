using Microsoft.Win32;
using SimpleVideoCompressor.Controllers;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SimpleVideoCompressor
{
    public partial class MainWindow : Window
    {
        private MainWindowController controller;
        public MainWindow()
        {
            InitializeComponent();
            controller = new();
            DataContext = controller;
        }

        private void btn_UploadedFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                textblock_UserFile.Text = dialog.SafeFileName;
                controller.FilePathNameUri = System.IO.Path.GetDirectoryName(dialog.FileName);
                controller.DirectFileName = dialog.SafeFileName;
            }
        }

        private void btn_FilePathUser_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dialog = new OpenFolderDialog();

            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                textblock_UserFilePath.Text = dialog.FolderName;
                controller.UploadPathUri = dialog.FolderName;
            }
        }

        private async void btn_StartCompression_Click(object sender, RoutedEventArgs e)
        {
            btn_FilePathUser.IsEnabled = false;
            btn_FileUploadUser.IsEnabled = false;
            btn_StartCompression.IsEnabled = false;

            // Wait for the thing to finish
            await controller.StartCompression();

            string compressedVideoPath = Path.Combine(controller.UploadPathUri, controller.CompressedVideoFileName);

            if (File.Exists(compressedVideoPath))
            {
                Process.Start("explorer.exe", "/select, \"" + compressedVideoPath + "\"");

                btn_FilePathUser.IsEnabled = true;
                btn_FileUploadUser.IsEnabled = true;
                btn_StartCompression.IsEnabled = true;

                textblock_UserFile.Text = "";
                textblock_UserFilePath.Text = "";
            } else
            {
                MessageBox.Show("An error occured.");
            }
        }
    }
}

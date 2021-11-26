using FileUploader.CustomControl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileUploader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


            

       

        private void FileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = System.IO.Path.GetFileName(files[0]);

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = System.IO.Path.GetFileName(files[i]);
                    FileInfo fileInfo = new FileInfo(files[i]);
                    UploadingFilesList.Items.Add(new fileDetail()
                    {
                        FileName = filename,
                        FileSize = string.Format("{0} {1}", (fileInfo.Length / 1.094e+6).ToString("0.0"), "Mb"),
                        UploadProgress = 100
                    });
                }


            }
        }

        private void BtnChoose_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true };
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                string[] files = openFileDialog.FileNames;


                for (int i = 0; i < files.Length; i++)
                {
                    string filename = System.IO.Path.GetFileName(files[i]);
                    FileInfo fileInfo = new FileInfo(files[i]);
                    UploadingFilesList.Items.Add(new fileDetail()
                    {
                        FileName = filename,
                        FileSize = string.Format("{0} {1}", (fileInfo.Length / 1.094e+6).ToString("0.0"), "Mb"),
                        UploadProgress = 100
                    });
                    ConverterToAudio(@"C:\Users\62427\Desktop\Bot\" + filename);
                }
            }
        }


        void ConverterToAudio(string path)
        {
            string AudioName = path.Replace("mp4", "mp3");
            try
            {
                var ConvertVideo = new NReco.VideoConverter.FFMpegConverter();
                ConvertVideo.ConvertMedia(path, AudioName, "mp3");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

// https://www.youtube.com/watch?v=eEa_Fl3ZguA
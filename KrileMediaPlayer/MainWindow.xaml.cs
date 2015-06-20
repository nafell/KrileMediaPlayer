using System;
using System.Collections.Generic;
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

using System.Net;

namespace KrileMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string StartupArgs { get; set; }

        public MainWindow(string startupargs)
        {
            InitializeComponent();

            this.StartupArgs = startupargs;

            this.Title = StartupArgs;

            showaw();
        }

        private async void showaw()
        {
            await Task.Run(() => FetchImage());
        }

        #region "download"
        private async void FetchImage()
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(StartupArgs);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadComplete);
            await Task.Run(() => client.DownloadDataAsync(uri));

        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            //UI thread Invoker
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
            {
                this.prgress.Value = e.ProgressPercentage;
            }));
        }

        private void DownloadComplete(object sender, DownloadDataCompletedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
            {
                media.Source = ByteArrayToBI(e.Result);
                prgress.Visibility = Visibility.Hidden;
            }));
        }

        public BitmapImage ByteArrayToBI(byte[] pink)
        {
            using (var ms = new System.IO.MemoryStream(pink))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        #endregion


        private void bye(MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Application.Current.Shutdown();
            }
        }

        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bye(e);
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bye(e);
        }
    }
}

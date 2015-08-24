using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using System.Net;

namespace KrileMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Gets Previouse Window State
        /// </summary>
        public WindowState PreviouseWindowState { get; private set; }

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
        
        /// <summary>
        /// Occures on layout change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            PreviouseWindowState = WindowState;
        }

        /// <summary>
        /// Activates Window <para/>
        /// Example for this: http://blogs.microsoft.co.il/blogs/maxim/archive/2009/12/24/daily-tip-how-to-activate-minimized-window-form.aspx
        /// </summary>
        /// <param name="restoreIfMinimized">if [true] restore prev. win. state</param>
        /// <returns></returns>
        public bool Activate(bool restoreIfMinimized)
        {
            if (restoreIfMinimized && WindowState == WindowState.Minimized)
            {
                WindowState = PreviouseWindowState == WindowState.Normal
                                        ? WindowState.Normal : WindowState.Maximized;
            }
            return Activate();
        }

        /// <summary>
        /// Appends the args.
        /// </summary>
        /// <param name="arg">The args.</param>
        public void AppendArgs(string arg)
        {
            Console.WriteLine($"apd: {arg}");
        }
    }
}

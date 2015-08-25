using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
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

        public MainWindow(string StartupArgument)
        {
            InitializeComponent();

            this.Title = StartupArgument;

            OpenImage(StartupArgument);
        }

        #region "download"
        private void OpenImage(string URL)
        {
            ImageTab pagecontent = new ImageTab();
            Tabs.Items.Add(pagecontent);
            Tabs.SelectedItem = pagecontent;
            pagecontent.Header = System.IO.Path.GetFileName(URL);
            if (URL.EndsWith(":orig"))
            {
                pagecontent.Header = System.IO.Path.GetFileName(URL.Replace(":orig",""));
            }

            WebClient dlClient = new WebClient();
            var bytes = dlClient.DownloadDataTaskAsync(URL);


            dlClient.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
            {
                pagecontent.Progress.Value = e.ProgressPercentage;
            };

            dlClient.DownloadDataCompleted += delegate (object sender, DownloadDataCompletedEventArgs e)
            {
                try
                {
                    pagecontent.Media.Source = ByteArrayToBI(e.Result);
                    pagecontent.Progress.Visibility = Visibility.Hidden;
                }
                catch
                {; }
            };
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
                if (Tabs.Items.Count == 1)
                {
                    Application.Current.Shutdown();
                }
                Tabs.Items.Remove(Tabs.SelectedItem);
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
            OpenImage(arg);
        }
    }
}

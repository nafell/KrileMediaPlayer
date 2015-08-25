using System;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Text.RegularExpressions;

namespace KrileMediaPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainWindow mwindow;
        public string[] SupportedExtentions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };
        public string[] TwiterMediaResolusions = { ":orig", ":large", ":medium", ":small" };

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // register single instance app. and check for existence of other process
            if (!ApplicationInstanceManager.CreateSingleInstance(
                    Assembly.GetExecutingAssembly().GetName().Name,
                    SingleInstanceCallback))
                return; // exit, if same app. is running

            base.OnStartup(e);
            
            string URL = null;
            if (WinterWrapUp(e.Args[0], ref URL) == true)
            {
                mwindow = new MainWindow(URL);
                mwindow.Show();
            }

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Activated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            var win = MainWindow as MainWindow;
            if (win == null) return;

            // add 1st args
            string[] arglines = Environment.GetCommandLineArgs();
            if (arglines.Length == 0) return;
            string URL = null;
            if (WinterWrapUp(arglines[0], ref URL) == true)
            {
                Console.WriteLine($"OnAtv: {URL}");
                win.AppendArgs(URL);
            }
        }

        /// <summary>
        /// Single instance callback handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SingleInstanceApplication.InstanceCallbackEventArgs"/> instance containing the event data.</param>
        private void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (args == null || Dispatcher == null) return;
            Action<bool> d = (bool x) =>
            {
                var win = MainWindow as MainWindow;
                if (win == null) return;

                if (args.CommandLineArgs.Length == 0) return;
                string URL = null;
                if (WinterWrapUp(args.CommandLineArgs[1], ref URL) == true)
                {
                    Console.WriteLine($"Clb: {URL}");
                    win.AppendArgs(URL);
                    win.Activate(x);
                }
            };
            Dispatcher.Invoke(d, true);
        }

        private bool WinterWrapUp(string url, ref string URL)
        {
            string ext = System.IO.Path.GetExtension(url);
            if (url.EndsWith(":orig")) //Twitterの原寸画像のsuffix
            {
                URL = url;
                return true;
            }
            else if (Regex.IsMatch(url, @"http://gyazo\.com/.+")) //gyazoを画像直リンクしないcunt向け
            {
                URL = Regex.Replace(url, @"http://gyazo\.com/(?<urid>.+)", @"http://i.gyazo.com/${urid}.png");
                return true;
            }
            else if (SupportedExtentions.Contains(ext)) //その他普通の画像
            {
                URL = url;
                return true;
            }
            else                    //対応してないのでブラウザにたらい回し
            {
                URL = url;
                return false;
            }
        }


    }
}

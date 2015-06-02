using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

        private void SummerSunCelebration(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0) return;
            string URL;
            if (WinterWrapUp(e.Args[0], out URL) == true)
            {
                mwindow = new MainWindow(URL);
                mwindow.Show();
            }
            else
            {
                System.Diagnostics.Process.Start(URL);
                Application.Current.Shutdown();
            }
        }

        private bool WinterWrapUp(string url, out string URL)
        {
            string ext = System.IO.Path.GetExtension(url);

            if (url.EndsWith(":orig")) //Twitterの原寸画像のsuffix
                { URL = url;
                  return true; }
            if (Regex.IsMatch(url, @"http://gyazo\.com/.+")) //gyazoを画像直リンクしないcunt向け
                { URL = Regex.Replace(url, @"http://gyazo\.com/(?<urid>.+)", @"http://i.gyazo.com/${urid}.png");
                  return true; }
            if (SupportedExtentions.Contains(ext))
                { URL = url;
                  return true; }
            else
                { URL = url;
                  return false; }
        }
    }
}

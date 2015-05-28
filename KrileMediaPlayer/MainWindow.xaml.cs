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

namespace KrileMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string[] startupargs)
        {
            InitializeComponent();

            this.StartupArgs = startupargs;

            showag();
        }

        private void showag()
        {
            media.Source = new BitmapImage(new Uri(StartupArgs[0]));
            this.Title = StartupArgs[0];
        }

        public string[] StartupArgs { get; set; }
    }
}

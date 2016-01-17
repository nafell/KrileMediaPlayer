using System;
using System.Windows;

namespace KrileMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Gets Previouse Window State
        /// </summary>
        public WindowState PreviouseWindowState { get; private set; }
        public PagesViewModel Pages { get; set; } = new PagesViewModel();

        public MainWindow(string startupArgument)
        {
            InitializeComponent();

            DataContext = Pages;

            Pages.OpenImage(startupArgument);
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
            Pages.OpenImage(arg);
        }
    }
}

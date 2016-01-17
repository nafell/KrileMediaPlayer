using System.Windows;
using System.Windows.Input;

namespace KrileMediaPlayer.Pages
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView
    {
        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
            "CloseCommand", typeof (ICommand), typeof (ImageView), new PropertyMetadata(default(ICommand)));

        public ICommand CloseCommand
        {
            get { return (ICommand) GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly DependencyProperty CopyUrlCommandProperty = DependencyProperty.Register(
            "CopyUrlCommand", typeof (ICommand), typeof (ImageView), new PropertyMetadata(default(ICommand)));

        public ICommand CopyUrlCommand
        {
            get { return (ICommand) GetValue(CopyUrlCommandProperty); }
            set { SetValue(CopyUrlCommandProperty, value); }
        }

        public ImageView()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                CloseCommand.Execute(DataContext);
            }
        }
    }
}

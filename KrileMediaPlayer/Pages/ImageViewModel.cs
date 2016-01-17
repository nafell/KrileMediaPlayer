using System;
using System.Net;
using System.Windows.Media.Imaging;
using KrileMediaPlayer.ClassSuplies;

namespace KrileMediaPlayer.Pages
{
    public class ImageViewModel : ObservableObject, IPageViewModel
    {
        private int _initialFetchPercentage;
        private BitmapImage _image;
        private string _title;
        private string _url;
        private bool _isSelected;

        public int InitialFetchPercentage
        {
            get { return _initialFetchPercentage; }
            set
            {
                if (value == _initialFetchPercentage)
                    return;

                _initialFetchPercentage = value;
                OnPropertyChanged(nameof(InitialFetchPercentage));
            }
        }

        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                if (value.Equals(_image))
                    return;

                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title)
                    return;

                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Url
        {
            get { return _url; }
            set
            {
                if (value == _url)
                    return;

                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public ImageViewModel(string url)
        {
            Fetch(url);

            Url = url;

            if (url.EndsWith(":orig"))
                url = System.IO.Path.GetFileName(url.Replace(":orig", ""));
            Title = System.IO.Path.GetFileName(url);
        }

        private async void Fetch(string url)
        {
            var client = new WebClient();

            client.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
            {
                InitialFetchPercentage = e.ProgressPercentage;
            };

            var resp = await client.DownloadDataTaskAsync(url);
            Image = ByteArrayToBitmapImage(resp);
        }

        private static BitmapImage ByteArrayToBitmapImage(byte[] pink)
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
    }
}

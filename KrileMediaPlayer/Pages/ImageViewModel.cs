using System;
using System.Net;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using KrileMediaPlayer.ClassSuplies;
using System.IO;
using Microsoft.Win32;

namespace KrileMediaPlayer.Pages
{
    public class ImageViewModel : ObservableObject, IPageViewModel
    {
        private int _initialFetchPercentage;
        private BitmapImage _image;
        private string _title;
        private string _url;
        private string _statusError;
        private bool _isSelected;
        private ICommand _saveImageCommand;

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

        public string StatusError
        {
            get { return _statusError; }
            set
            {
                if (value == _statusError)
                    return;

                _statusError = value;
                OnPropertyChanged(nameof(StatusError));
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

        public ICommand SaveImageCommand
        {
            get
            {
                return _saveImageCommand ?? (_saveImageCommand = new RelayCommand(
                    p =>
                    {
                        var vm = (ImageViewModel) p;
                        BitmapEncoder encoder;
                        var ext = Path.GetExtension(vm.Title);
                        if (ext == ".png")
                            encoder = new PngBitmapEncoder();
                        else if (ext == ".gif")
                            encoder = new GifBitmapEncoder();
                        else
                            encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(vm.Image));

                        var dialog = new SaveFileDialog();
                        dialog.FileName = vm.Title;
                        dialog.DefaultExt = ext;
                        dialog.Filter = $"|*{ext}";
                        if (dialog.ShowDialog() == true)
                        {
                            using (var fileStream = new FileStream(dialog.FileName, FileMode.Create))
                            {
                                encoder.Save(fileStream);
                            }
                        }
                    },
                    p => p is ImageViewModel));

            }
        }

        public ImageViewModel(string url)
        {
            Fetch(url);

            Url = url;

            if (url.EndsWith(":orig"))
                url = Path.GetFileName(url.Replace(":orig", ""));
            Title = Path.GetFileName(url);
        }

        private async void Fetch(string url)
        {
            try
            {
                var fetcher = new MediaRequestor();
                var resp = await fetcher.FetchAsyncWithProgressCallBack(url, p => { InitialFetchPercentage = p; });
                Image = ByteArrayToBitmapImage(resp);
            }
            catch (WebException ex)
                when (ex.Response is HttpWebResponse)
            {
                StatusError = $"{(int)((HttpWebResponse)ex.Response).StatusCode} {((HttpWebResponse)ex.Response).StatusDescription}";
            }
            catch (Exception ex)
            {
                StatusError = ex.ToString();
            }
        }

        private static BitmapImage ByteArrayToBitmapImage(byte[] pink)
        {
            using (var ms = new MemoryStream(pink))
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

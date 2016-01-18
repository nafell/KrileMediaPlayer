using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using KrileMediaPlayer.ClassSuplies;
using KrileMediaPlayer.Pages;

namespace KrileMediaPlayer
{
    public class PagesViewModel :ObservableObject
    {
        private IPageViewModel _currentPageViewModel;
        private ObservableCollection<IPageViewModel> _pages;

        private ICommand _changePageCommand;

        private ICommand _closeCommand;
        private ICommand _copyUrlCommand;

        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand = _changePageCommand ??
                                            new RelayCommand(
                                                p => ChangePage((IPageViewModel) p),
                                                p => p is IPageViewModel);
            }
        }
        private void ChangePage(IPageViewModel viewmodel)
        {
            if (!Pages.Contains(viewmodel))
                Pages.Add(viewmodel);

            for (var i = 0; i <= Pages.Count - 1; i++)
            {
                Pages[i].IsSelected = false;
            }

            CurrentPageViewModel = Pages.FirstOrDefault(vm => vm == viewmodel);
            Pages[Pages.IndexOf(viewmodel)].IsSelected = true;
        }

        public ObservableCollection<IPageViewModel> Pages
        {
            get
            {
                if(_pages == null)
                    _pages = new ObservableCollection<IPageViewModel>();

                return _pages;
            }
            set
            {
                if (value == _pages)
                    return;

                _pages = value;
                OnPropertyChanged(nameof(Pages));
            }
        }

        public void OpenImage(string url)
        {
            var newViewModel = new ImageViewModel(url);
            Pages.Add(newViewModel);
            ChangePage(newViewModel);
        }

        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set
            {
                if (value == _currentPageViewModel)
                    return;

                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand = _closeCommand ??
                                       new RelayCommand(
                                           p => ClosePage((IPageViewModel) p),
                                           p => p is IPageViewModel);
            }
        }
        private void ClosePage(IPageViewModel viewmodel)
        {
            Pages.Remove(viewmodel);
            if (Pages.Count == 0)
            {
                Application.Current.Shutdown();
                return;
            }
            ChangePage(Pages[Pages.Count - 1]);
        }

        public ICommand CopyUrlCommand
        {
            get
            {
                return _copyUrlCommand = _copyUrlCommand ??
                                         new RelayCommand(
                                             p => CopyUrl((string) p),
                                             p => p is string);
            }
        }
        private void CopyUrl(string url)
        {
            Clipboard.SetText(url);
        }
    }
}

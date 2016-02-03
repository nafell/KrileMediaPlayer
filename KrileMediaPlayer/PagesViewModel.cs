using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KrileMediaPlayer.ClassSuplies;
using KrileMediaPlayer.Pages;

namespace KrileMediaPlayer
{
    public class PagesViewModel :ObservableObject
    {
        #region fields

        private IPageViewModel _currentPageViewModel;
        private ObservableCollection<IPageViewModel> _pages;

        private ICommand _changePageCommand;

        private ICommand _closeCommand;
        private ICommand _copyUrlCommand;

        #endregion

        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand = _changePageCommand ??
                                            new RelayCommand(
                                                p =>
                                                {
                                                    var viewmodel = (IPageViewModel) p; 
                                                    if (!Pages.Contains(viewmodel))
                                                        Pages.Add(viewmodel);

                                                    foreach (IPageViewModel pg in Pages)
                                                    {
                                                        pg.IsSelected = false;
                                                    }
                                                    
                                                    CurrentPageViewModel = Pages[Pages.IndexOf(viewmodel)];
                                                    CurrentPageViewModel.IsSelected = true;
                                                },
                                                p => p is IPageViewModel);
            }
        }

        public ObservableCollection<IPageViewModel> Pages
        {
            get
            {
                return _pages = _pages ?? new ObservableCollection<IPageViewModel>();
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
            ChangePageCommand.Execute(newViewModel);
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
                                           p =>
                                           {
                                               Pages.Remove((IPageViewModel)p);
                                               if (Pages.Count == 0)
                                               {
                                                   Application.Current.Shutdown();
                                                   return;
                                               }
                                               ChangePageCommand.Execute(Pages[Pages.Count - 1]);
                                           },
                                           p => p is IPageViewModel);
            }
        }

        public ICommand CopyUrlCommand
        {
            get
            {
                return _copyUrlCommand = _copyUrlCommand ??
                                         new RelayCommand(
                                             p => Clipboard.SetText((string) p),
                                             p => p is string);
            }
        }
    }
}

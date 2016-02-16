using System.Windows;
using KrileMediaPlayer.ClassSuplies;

namespace KrileMediaPlayer.Config
{
    public class MainWindowConfigModel : ObservableObject
    {
        #region fields

        private double _mainWindowTop = 50;
        private double _mainWindowLeft = 50;
        private double _mainWindowWidth = 1280;
        private double _mainWindowHeight = 720;
        private WindowState _mainWindowState;

        #endregion

        #region property accessors

        public double MainWindowTop
        {
            get { return _mainWindowTop; }
            set
            {
                if (value.Equals(_mainWindowTop))
                    return;

                _mainWindowTop = value;
                OnPropertyChanged(nameof(MainWindowTop));
            }
        }

        public double MainWindowLeft
        {
            get { return _mainWindowLeft; }
            set
            {
                if (value.Equals(_mainWindowLeft))
                    return;

                _mainWindowLeft = value;
                OnPropertyChanged(nameof(MainWindowLeft));
            }
        }

        public double MainWindowWidth
        {
            get { return _mainWindowWidth; }
            set
            {
                if (value.Equals(_mainWindowWidth))
                    return;

                _mainWindowWidth = value;
                OnPropertyChanged(nameof(MainWindowWidth));
            }
        }

        public double MainWindowHeight
        {
            get { return _mainWindowHeight; }
            set
            {
                if (value.Equals(_mainWindowHeight))
                    return;

                _mainWindowHeight = value;
                OnPropertyChanged(nameof(MainWindowHeight));
            }
        }

        public WindowState MainWindowState
        {
            get { return _mainWindowState; }
            set
            {
                if (value.Equals(_mainWindowState))
                    return;

                _mainWindowState = value;
                OnPropertyChanged(nameof(MainWindowState));
            }
        }

        #endregion
    }
}

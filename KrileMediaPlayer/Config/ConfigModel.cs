using KrileMediaPlayer.ClassSuplies;

namespace KrileMediaPlayer.Config
{
    public class ConfigModel : ObservableObject
    {
        #region Fields

        private MainWindowConfigModel _mainWindowConfig;

        #endregion

        #region Property Accessors

        public MainWindowConfigModel MainWindowConfig
        {
            get { return _mainWindowConfig = _mainWindowConfig ?? new MainWindowConfigModel(); }
            set
            {
                if (value == _mainWindowConfig)
                    return;

                _mainWindowConfig = value;
                OnPropertyChanged(nameof(MainWindowConfig));
            }
        }

        #endregion
    }
}

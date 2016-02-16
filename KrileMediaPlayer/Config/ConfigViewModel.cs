using System.IO;
using KrileMediaPlayer.ClassSuplies;

namespace KrileMediaPlayer.Config
{
    public class ConfigViewModel : ObservableObject
    {
        #region Fields

        private readonly string _configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config.xml");

        private ConfigModel _configModel;

        #endregion

        #region Property Accessors

        public ConfigModel Model
        {
            get { return _configModel = _configModel ?? new ConfigModel(); }
            set
            {
                if (value == _configModel)
                    return;

                _configModel = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        #endregion

        public ConfigViewModel()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            var xmlSerializer = new XmlSerializer<ConfigModel>();
            if (!File.Exists(_configFilePath))
                return;
            Model = xmlSerializer.DeserializeFromFile(_configFilePath);
        }

        public void SaveConfig()
        {
            var xmlSerializer = new XmlSerializer<ConfigModel>();
            xmlSerializer.SerializeToFile(Model, _configFilePath);
        }
    }
}

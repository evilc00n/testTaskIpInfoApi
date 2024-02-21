using IpInfo.Domain.Interfaces;

namespace IpInfo.Dal
{
    public class ConnectinAdressConfig : IConnectionAdressConfig
    {
        private bool _isValueSet = false;
        private string _сonnectionString;

        //данный объект будет синглтоном, который инициализируется в другом файле
        //поэтому, чтобы строка подключение не изменялась делается способ задания финального значения
        public string ConnectionString 
        { 
            get { return _сonnectionString;} 
            set
            {
                if (!_isValueSet)
                {
                    _сonnectionString = value;
                    _isValueSet = true;
                }
                else
                {
                    throw new InvalidOperationException("Значение уже установлено.");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;

namespace Calculo_Comisiones_Operadores
{
    public class ConfigDataBase
    {
        private string _server = "localhost";
        private string _database = "comisiones_report";
        private string _user = "root";
        private string _pass = string.Empty;
        private string _config = HttpContext.Current.Server.MapPath("~") + "config.txt";

        public ConfigDataBase()
        {
            ReadConfig();
        }

        private void ReadConfig()
        {
            var _fileStream = new FileStream(_config, FileMode.Open, FileAccess.Read);
            string _line = string.Empty;

            using (var _streamReader = new StreamReader(_fileStream, Encoding.UTF8))
            {
                while ((_line = _streamReader.ReadLine()) != null) break;
            }

            if (_line != string.Empty && _line != "")
            {
                string[] _auxString = _line.Split(',');

                for (int i = 0; i < _auxString.Length; i++)
                {
                    if (i == 0)
                        server = _auxString[i];
                    if (i == 1)
                        database = _auxString[i];
                    if (i == 2)
                        pass = _auxString[i];
                    if (i == 3)
                        user = _auxString[i];
                }
            }
        }

        public String server
        {
            get { return _server; }
            set { _server = value; }
        }

        public String database
        {
            get { return _database; }
            set { _database = value; }
        }

        public String user
        {
            get { return _user; }
            set { _user = value; }
        }

        public String pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
    }
}
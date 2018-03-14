using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class Color
    {
        private int _intId;
        private string _strName;
        private TimeSpan _timeHora;
        private string _strPrioridad;
        private decimal _decTarifa;

        public int intID
        {
            get { return _intId; }
            set { _intId = value; }
        }
        public string strName
        {
            get { return _strName; }
            set { _strName = value; }
        }

        public TimeSpan timeHora
        {
            get { return _timeHora; }
            set { _timeHora = value; }
        }

        public string strPrioridad
        {
            get { return _strPrioridad; }
            set { _strPrioridad = value; }
        }

        public decimal decTarfica
        {
            get { return _decTarifa; }
            set { _decTarifa = value; }
        }
    }
}
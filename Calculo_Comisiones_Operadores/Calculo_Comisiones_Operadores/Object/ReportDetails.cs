using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class ReportDetails
    {
        private string _strnameOperador;
        private string _strNameUnidad;
        private DateTime _dateTravel;
        private int _intKilOut;
        private int _intKilIn;
        private TimeSpan _timeOut;
        private TimeSpan _timeIn;
        private List<Cliente> _listCliente;//Consulta de Cliente con Color,
        private string _strTipoTravel;
        private TimeSpan _timeTotal;
        private int _intKilTotal;

        public string strNameUnidad
        {
            get { return _strNameUnidad; }
            set { _strNameUnidad = value; }
        }

        public string strNameOperador
        {
            get { return _strnameOperador; }
            set { _strnameOperador = value; }
        }

        public DateTime dateTravel
        {
            get { return _dateTravel; }
            set { _dateTravel = value; }
        }

        public int intKilOut
        {
            get { return _intKilOut; }
            set { _intKilOut = value; }
        }

        public int intKilIn
        {
            get { return _intKilIn; }
            set { _intKilIn = value; }
        }

        public TimeSpan timeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }

        public TimeSpan timeIn
        {
            get { return _timeIn; }
            set { _timeIn = value; }
        }

        public List<Cliente> listCliente
        {
            get { return _listCliente; }
            set { _listCliente = value; }
        }

        public string strTipoTravel
        {
            get { return _strTipoTravel; }
            set { _strTipoTravel = value; }
        }

        public TimeSpan timeTotal
        {
            get { return _timeTotal; }
            set { _timeTotal = value; }
        }

        public int intKilTotal
        {
            get { return _intKilTotal; }
            set { _intKilTotal = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class ReportSummary
    {
        private string _strName;
        private decimal _floatComisiones;
        private int _intTotalViajes;
        private int _intTotalEntregas;
        private List<ClientePreferencial> _listClientePreferenciales;
        private decimal _floatViajes;
        private decimal _floatEntregas;

        public string strName
        {
            get { return _strName; }
            set { _strName = value; }
        }

        public decimal floatComisiones
        {
            get { return _floatComisiones; }
            set { _floatComisiones = value; }
        }

        public int intTotalViajes
        {
            get { return _intTotalViajes; }
            set { _intTotalViajes = value; }
        }

        public int intTotalEntregas
        {
            get { return _intTotalEntregas; }
            set { _intTotalEntregas = value; }
        }

        public List<ClientePreferencial> listClientePreferencial
        {
            get { return _listClientePreferenciales; }
            set { _listClientePreferenciales = value; }
        }

        public decimal floatViajes
        {
            get { return _floatViajes; }
            set { _floatViajes = value; }
        }

        public decimal floatEntregas
        {
            get { return _floatEntregas; }
            set { _floatEntregas = value; }
        }
    }
}
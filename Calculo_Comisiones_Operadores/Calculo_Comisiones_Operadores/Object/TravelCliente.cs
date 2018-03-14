using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class TravelCliente
    {
        private int _intId;
        private int _intTravelId;
        private int _intClienteId;
        private DateTime _dateUp;
        private DateTime _dateMod;
        private string _strFactura;
        private string _strUser;

        public int intId
        {
            get { return _intId; }
            set { _intId = value; }
        }

        public int intTravelId
        {
            get { return _intTravelId; }
            set { _intTravelId = value; }
        }

        public int intClienteId
        {
            get { return _intClienteId; }
            set { _intClienteId = value; }
        }

        public string strFactura
        {
            get { return _strFactura; }
            set { _strFactura = value; }
        }
        
        public DateTime dateUp
        {
            get { return _dateUp; }
            set { _dateUp = value; }
        }

        public DateTime dateMod
        {
            get { return _dateMod; }
            set { _dateMod = value; }
        }

        public string strUser
        {
            get { return _strUser; }
            set { _strUser = value; }
        }
    }
}
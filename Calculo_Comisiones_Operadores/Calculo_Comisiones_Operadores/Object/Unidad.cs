using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class Unidad
    {
        private int _intId;
        private string _strName;
        private string _strModelo;
        private string _strPlaca;
        private int _intStatus;

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
        public string strModelo
        {
            get { return _strModelo; }
            set { _strModelo = value; }
        }
        public string strPlaca
        {
            get { return _strPlaca; }
            set { _strPlaca = value; }
        }
        public int intStatus
        {
            get { return _intStatus; }
            set { _intStatus = value; }
        }
    }
}
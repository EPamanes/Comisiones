using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class ClientePreferencial
    {
        private int _intId;
        private string _strNombre;
        private int _intTotal;

        public int intId
        {
            get { return _intId; }
            set { _intId = value; }
        }

        public string strNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        public int intTotal
        {
            get { return _intTotal; }
            set { _intTotal = value; }
        }
    }
}
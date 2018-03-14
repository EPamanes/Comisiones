using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class Cliente
    {
        private int _intId;
        private string _strName;
        private int _intStatus;
        private int _intColorId;
        private string _strTipo;
        private int _intRule1;
        private int _intRule2;

        public int intId
        {
            get { return _intId; }
            set { _intId = value; }
        }

        public string strName
        {
            get { return _strName; }
            set { _strName = value; }
        }

        public int intStatus
        {
            get { return _intStatus; }
            set { _intStatus = value; }
        }

        public int intColorId
        {
            get { return _intColorId; }
            set { _intColorId = value; }
        }

        public string strTipo
        {
            get { return _strTipo; }
            set { _strTipo = value; }
        }

        public int intRule1
        {
            get { return _intRule1; }
            set { _intRule1 = value; }
        }

        public int intRule2
        {
            get { return _intRule2; }
            set { _intRule2 = value; }
        }
    }
}
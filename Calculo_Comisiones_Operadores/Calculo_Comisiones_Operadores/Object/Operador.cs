using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class Operador
    {
        private int _intId;
        private string _strName;
        private string _strLastName;
        private string _strMLastName;
        private string _strSexo;
        private int _intAge;
        private DateTime _dateBirthdate;
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

        public string strLastName
        {
            get { return _strLastName; }
            set { _strLastName = value; }
        }
        public string strMlastName
        {
            get { return _strMLastName; }
            set { _strMLastName = value; }
        }
        public string strSexo
        {
            get { return _strSexo; }
            set { _strSexo = value; }
        }
        public int intAge
        {
            get { return _intAge; }
            set { _intAge = value; }
        }
        public DateTime dateBirthdate
        {
            get { return _dateBirthdate; }
            set { _dateBirthdate = value; }
        }
        public int intStatus
        {
            get { return _intStatus; }
            set { _intStatus = value; }
        }
    }
}
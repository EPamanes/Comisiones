using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class User
    {
        private int _intID;
        private string _strName;
        private string _strLastName;
        private string _strMlastName;
        private string _strUser;
        private string _strPassword;
        private int _intStatus;
        private string _strTipo;

        public int intID
        {
            get { return _intID; }
            set { _intID = value; }
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
            get { return _strMlastName; }
            set { _strMlastName = value; }
        }

        public string strUser
        {
            get { return _strUser; }
            set { _strUser = value; }
        }

        public string strPassword
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }
        
        public int intStatus
        {
            get { return _intStatus; }
            set { _intStatus = value; }
        }

        public string strTipo
        {
            get { return _strTipo; }
            set { _strTipo = value; }
        }
    }
}
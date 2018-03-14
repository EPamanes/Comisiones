using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class Upload
    {
        private int _intId;
        private string _strName;
        private string _strType;
        private int _intSize;
        private byte[] _bFile;
        private string _strExt;
        private string _strUser;
        private DateTime _dateUp;

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

        public string strType
        {
            get { return _strType; }
            set { _strType = value; }
        }

        public int intSize
        {
            get { return _intSize; }
            set { _intSize = value; }
        }

        public byte[] bFile
        {
            get { return _bFile; }
            set { _bFile = value; }
        }

        public string strUser
        {
            get { return _strUser; }
            set { _strUser = value; }
        }

        public string strExt
        {
            get { return _strExt; }
            set { _strExt = value; }
        }

        public DateTime dateUp
        {
            get { return _dateUp; }
            set { _dateUp = value; }
        }
    }
}
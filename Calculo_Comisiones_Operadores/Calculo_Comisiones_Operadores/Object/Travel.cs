using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class Travel
    {
        private int _intId;
        private int _intKilometrajeOut;
        private int _intKilometrajeIn;
        private string _intTipo;
        private int _intControl;
        private DateTime _dateOut;
        private DateTime _dateIn;
        private int _intUnidadId;
        private int _intOperadorId;
        private DateTime _dateUp;
        private DateTime _dateMod;
        private string _strUser;

        public int intId
        {
            get { return _intId; }
            set { _intId = value; }
        }

        public int intUnidadId
        {
            get { return _intUnidadId; }
            set { _intUnidadId = value; }
        }

        public int intOperadorId
        {
            get { return _intOperadorId; }
            set { _intOperadorId = value; }
        }

        public int intKilometrajeOut
        {
            get { return _intKilometrajeOut; }
            set { _intKilometrajeOut = value; }
        }

        public int intKilometrajeIn
        {
            get { return _intKilometrajeIn; }
            set { _intKilometrajeIn = value; }
        }

        public string intTipo
        {
            get { return _intTipo; }
            set { _intTipo = value; }
        }

        public int intControl
        {
            get { return _intControl; }
            set { _intControl = value; }
        }

        public DateTime dateOut
        {
            get { return _dateOut; }
            set { _dateOut = value; }
        }

        public DateTime dateIn
        {
            get { return _dateIn; }
            set { _dateIn = value; }
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
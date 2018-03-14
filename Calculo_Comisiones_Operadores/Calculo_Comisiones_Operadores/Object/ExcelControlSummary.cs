using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores.Object
{
    public class ExcelControlSummary
    {
        private int _intRowStart = 1;
        private int _intRowEnd = 52;
        private int _intRowMiddle = 27;

        private int _intRowData = 1;
        private int _intRowDataAux = 1;
        private int _intRowCliP = 14;
        private int _intRowCliPAux = 14;

        private int _intCountWS = 1;
        private int _intLogStart = 1;
        private int _intLogEnd = 4;
        private float _floatLeftLogo = 0;
        private float _floatTopLogo = 0;
        private int _intRowDateIO = 10;
        private int _intRowResult = 17;

        public int intRowStart
        {
            get { return _intRowStart; }
            set { _intRowStart = value; }
        }

        public int intRowEnd
        {
            get { return _intRowEnd; }
            set { _intRowEnd = value; }
        }

        public int intRowMiddle
        {
            get { return _intRowMiddle; }
            set { _intRowMiddle = value; }
        }

        public int intRowData
        {
            get { return _intRowData; }
            set { _intRowData = value; }
        }

        public int intRowDataAux
        {
            get { return _intRowDataAux; }
            set { _intRowDataAux = value; }
        }

        public int intRowCliP
        {
            get { return _intRowCliP; }
            set { _intRowCliP = value; }
        }

        public int intRowCliPAux
        {
            get { return _intRowCliPAux; }
            set { _intRowCliPAux = value; }
        }

        public int intCountWS
        {
            get { return _intCountWS; }
            set { _intCountWS = value; }
        }

        public int intLogStart
        {
            get { return _intLogStart; }
            set { _intLogStart = value; }
        }

        public int intLogEnd
        {
            get { return _intLogEnd; }
            set { _intLogEnd = value; }
        }

        public float floatLeftLogo
        {
            get { return _floatLeftLogo; }
            set { _floatLeftLogo = value; }
        }

        public float floatTopLogo
        {
            get { return _floatTopLogo; }
            set { _floatTopLogo = value; }
        }

        public int intRowDateIO
        {
            get { return _intRowDateIO; }
            set { _intRowDateIO = value; }
        }

        public int intRowResult
        {
            get { return _intRowResult; }
            set { _intRowResult = value; }
        }
    }
}
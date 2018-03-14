using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using Core = Microsoft.Office.Core;

namespace Calculo_Comisiones_Operadores.Object
{
    public class ExcelCreate
    {
        private Excel._Application _objXLM;
        private Excel._Workbook _objWB;
        private Excel._Worksheet _objWS;
        private Excel.Range _objRN;

        public Excel._Application objXLM
        {
            get { return _objXLM; }
            set { _objXLM = value; }
        }

        public Excel._Workbook objWB
        {
            get { return _objWB; }
            set { _objWB = value; }
        }

        public Excel._Worksheet objWS
        {
            get { return _objWS; }
            set { _objWS = value; }
        }

        public Excel.Range objRN
        {
            get { return _objRN; }
            set { _objRN = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Core = Microsoft.Office.Core;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Calculo_Comisiones_Operadores.Object
{
    public class ExcelDesignSummary
    {
        ExcelCreate _objExcelCreate = new ExcelCreate();
        private object _objMissValue = Missing.Value;
        public string _strLogoPath;
        ExcelControlSummary _objExcelControl = new ExcelControlSummary();

        public ExcelDesignSummary()
        {
            _objExcelCreate.objXLM = new Excel.Application();
            _objExcelCreate.objWB = _objExcelCreate.objXLM.Workbooks.Add(_objMissValue);
            _objExcelCreate.objWS = _objExcelCreate.objWB.Worksheets[1];

            _objExcelCreate.objWS.PageSetup.TopMargin = 20;//20
            _objExcelCreate.objWS.PageSetup.BottomMargin = 10;
            _objExcelCreate.objWS.PageSetup.LeftMargin = 30.1;
            _objExcelCreate.objWS.PageSetup.RightMargin = 0;
            _objExcelCreate.objWS.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;
            _objExcelCreate.objWS.get_Range("A1", "AD1").EntireColumn.ColumnWidth = 2.5;
        }

        public void ExcelBodyPage(int intPapers)
        {

            for (int i = 0; i < intPapers; i++)
            {
                //Insertar Logo
                _objExcelCreate.objRN = (Excel.Range)_objExcelCreate.objWS.Cells[_objExcelControl.intRowStart, 1];
                _objExcelControl.floatLeftLogo = (float)_objExcelCreate.objRN.Left;
                _objExcelControl.floatTopLogo = (float)_objExcelCreate.objRN.Top;
                _objExcelCreate.objWS.Shapes.AddPicture(HttpContext.Current.Server.MapPath("~/Imagenes/") + "insert-logo-here.png", Core.MsoTriState.msoFalse, Core.MsoTriState.msoCTrue, _objExcelControl.floatLeftLogo + 2, _objExcelControl.floatTopLogo + 2, 152, 57).PictureFormat.ColorType = Core.MsoPictureColorType.msoPictureAutomatic;
                //Insertar Header Text
                _objExcelCreate.objWS.get_Range("K" + _objExcelControl.intRowStart, "AD" + _objExcelControl.intRowStart).Merge(false);
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("K" + _objExcelControl.intRowStart, "AD" + _objExcelControl.intRowStart);
                _objExcelCreate.objRN.FormulaR1C1 = "GYBSACO";
                AlignmentText(5, 3, "Arial", 12, true);
                ////Insertar Formato Text
                _objExcelCreate.objWS.get_Range("K" + (_objExcelControl.intRowStart + 1), "AD" + (_objExcelControl.intRowStart + 1)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 1, 11] = "FORMATO: REPORTE RESUMIDO";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 1, 11];
                AlignmentText(2, 2, "Arial", 8, true);
                ////Insertar Area
                _objExcelCreate.objWS.get_Range("K" + (_objExcelControl.intRowStart + 2), "T" + (_objExcelControl.intRowStart + 2)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 2, 11] = "AREA: NÓMINA";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 2, 11];
                AlignmentText(2, 2, "Arial", 8, true);
                ////Insertar Emision
                _objExcelCreate.objWS.get_Range("U" + (_objExcelControl.intRowStart + 2), "AD" + (_objExcelControl.intRowStart + 2)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 2, 21] = "EMISION: " + DateTime.Now.ToString("yyyy-MM-dd");
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 2, 21];
                AlignmentText(2, 2, "Arial", 8, true);

                //Insertar Hora 
                _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intRowEnd, "J" + _objExcelControl.intRowEnd).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowEnd, 1] = "HORA DE ELABORACIÓN: " + DateTime.Now.ToString("HH:mm:ss");
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowEnd, 1];
                AlignmentText(2, 2, "Arial", 8, false);
                //Insertar Numero de hoja
                _objExcelCreate.objWS.get_Range("AA" + _objExcelControl.intRowEnd, "AD" + _objExcelControl.intRowEnd).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowEnd, 27] = "HOJA: " + _objExcelControl.intCountWS.ToString("##") + " DE " + intPapers.ToString("##");
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowEnd, 27];
                AlignmentText(2, 2, "Arial", 7, false);
                _objExcelControl.intCountWS++;

                //Borde Hoja
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intRowStart, "AD" + _objExcelControl.intRowEnd);
                BorderTop(2d);
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intRowStart, "A" + _objExcelControl.intRowEnd);
                BorderLeft(2d);
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("AD" + _objExcelControl.intRowStart, "AD" + _objExcelControl.intRowEnd);
                BorderRight(2d);
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intRowEnd, "AD" + _objExcelControl.intRowEnd);
                BorderTop(2d);
                BorderBottom(2d);
                //Borde Logo
                _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intLogStart, "I" + _objExcelControl.intLogEnd).Merge(false);
                _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intLogStart, "I" + _objExcelControl.intLogEnd).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                //Borde Header Text
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("J" + _objExcelControl.intLogStart, "AD" + _objExcelControl.intLogStart);
                BorderBottom(2d);
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("J" + (_objExcelControl.intLogEnd - 1), "AD" + (_objExcelControl.intLogEnd - 1));
                BorderTop(2d);
                BorderBottom(2d);
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("J" + _objExcelControl.intLogEnd, "AD" + _objExcelControl.intLogEnd);
                BorderBottom(2d);
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("T" + (_objExcelControl.intLogEnd - 1), "T" + _objExcelControl.intLogEnd);
                BorderRight(2d);
                //Borde Mitad Hoja
                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intRowMiddle, "AD" + _objExcelControl.intRowMiddle);
                BorderTop(2d);

                //Variables de control
                _objExcelControl.intRowStart += 52;
                _objExcelControl.intRowEnd += 52;
                _objExcelControl.intLogStart += 52;
                _objExcelControl.intLogEnd += 52;
                _objExcelControl.intRowMiddle += 52;
            }
        }

        public void ExcelBodyData(List<ReportSummary> listReportSummary, DateTime dateStart, DateTime dateEnd)
        {
            int _intCount = 0;

            foreach (ReportSummary _objReportSummary in listReportSummary)
            {
                //Insertar Operador
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 5), "P" + (_objExcelControl.intRowData + 5)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2] = _objReportSummary.strName;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2];
                AlignmentText(2, 2, "Arial", 11, true);
                //Insertar Fechas
                _objExcelCreate.objWS.get_Range("R" + (_objExcelControl.intRowData + 5), "W" + (_objExcelControl.intRowData + 5)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 18] = "DEL:   " + dateStart.ToString("dd-MM-yyyy");
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 18];
                AlignmentText(2, 2, "Arial", 11, true);
                //
                _objExcelCreate.objWS.get_Range("Y" + (_objExcelControl.intRowData + 5), "AD" + (_objExcelControl.intRowData + 5)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 25] = " AL:   " + dateEnd.ToString("dd-MM-yyyy");
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 25];
                AlignmentText(2, 2, "Arial", 11, true);
                //Insertar Comision
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 7), "L" + (_objExcelControl.intRowData + 7)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 7, 2] = "Comisión: $ " + _objReportSummary.floatComisiones.ToString();
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 7, 2];
                AlignmentText(2, 2, "Arial", 11, true);
                //Insertar Cantidad de Viajes
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 9), "H" + (_objExcelControl.intRowData + 9)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 2] = "Cantidad de Viajes:";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 2];
                AlignmentText(2, 2, "Arial", 11, true);
                //Total de Viajes
                _objExcelCreate.objWS.get_Range("J" + (_objExcelControl.intRowData + 9), "L" + (_objExcelControl.intRowData + 9)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 10] = _objReportSummary.intTotalViajes;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 10];
                AlignmentText(2, 2, "Arial", 11, true);
                //Insertar % de Viaje
                _objExcelCreate.objWS.get_Range("N" + (_objExcelControl.intRowData + 9), "N" + (_objExcelControl.intRowData + 9)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 14] = "%";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 14];
                AlignmentText(2, 2, "Arial", 11, true);
                //% de Viajes
                _objExcelCreate.objWS.get_Range("O" + (_objExcelControl.intRowData + 9), "Q" + (_objExcelControl.intRowData + 9)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 15] = _objReportSummary.floatViajes;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 9, 15];
                AlignmentText(2, 2, "Arial", 11, true);
                //Insertar Cantidad de Entregas
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 10), "H" + (_objExcelControl.intRowData + 10)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 2] = "Cantidad de Entregas:";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 2];
                //Total de Entregas
                _objExcelCreate.objWS.get_Range("J" + (_objExcelControl.intRowData + 10), "L" + (_objExcelControl.intRowData + 10)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 10] = _objReportSummary.intTotalEntregas;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 10];
                AlignmentText(2, 2, "Arial", 11, true);
                //Insertar % de Entrega
                _objExcelCreate.objWS.get_Range("N" + (_objExcelControl.intRowData + 10), "N" + (_objExcelControl.intRowData + 10)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 14] = "%";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 14];
                AlignmentText(2, 2, "Arial", 11, true);
                //% de Entregas
                _objExcelCreate.objWS.get_Range("O" + (_objExcelControl.intRowData + 10), "Q" + (_objExcelControl.intRowData + 10)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 15] = _objReportSummary.floatEntregas;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 15];
                AlignmentText(2, 2, "Arial", 11, true);
                //Insertar Clientes Especiales
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 12), "H" + (_objExcelControl.intRowData + 12)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 12, 2] = "Clientes Especiales";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 12, 2];

                //Ciclo para insertar clientes especiales 
                foreach (ClientePreferencial _objClientePre in _objReportSummary.listClientePreferencial)
                {
                    //Insertar Cliente
                    _objExcelCreate.objWS.get_Range("C" + (_objExcelControl.intRowCliP), "H" + (_objExcelControl.intRowCliP)).Merge(false);
                    _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 3] = "* " + _objClientePre.strNombre + ":";
                    _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 3];
                    //Insertar Total
                    _objExcelCreate.objWS.get_Range("J" + (_objExcelControl.intRowCliP), "L" + (_objExcelControl.intRowCliP)).Merge(false);
                    _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 10] = _objClientePre.intTotal;
                    _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 10];
                    AlignmentText(2, 2, "Arial", 11, true);

                    _objExcelControl.intRowCliP += 1;
                }

                _intCount++;

                if (_intCount < 2)
                {
                    _objExcelControl.intRowCliP = _objExcelControl.intRowCliPAux;
                    _objExcelControl.intRowCliP += (23 * _intCount);//23
                    //_objExcelControl.intRowCliP += 23;
                    _objExcelControl.intRowData += 23;
                }
                else
                {
                    _objExcelControl.intRowCliPAux += 52;
                    _objExcelControl.intRowCliP = _objExcelControl.intRowCliPAux;
                    _objExcelControl.intRowDataAux += 52;
                    _objExcelControl.intRowData = _objExcelControl.intRowDataAux;
                    _intCount = 0;
                }
            }
        }

        public void SaveExcel(ref string strFileName, DateTime dateStart, DateTime dateEnd, string strSession)
        {
            strFileName = strSession[0] + dateStart.ToString("dd") + dateEnd.ToString("dd") + DateTime.Now.ToString("ss") + "_SCCO_REPORTE_RESUMEN_" + DateTime.Now.ToString("yyyyMMdd");
            _objExcelCreate.objXLM.DisplayAlerts = false;
            _objExcelCreate.objXLM.AskToUpdateLinks = false;

            _objExcelCreate.objWB.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, HttpContext.Current.Server.MapPath("~/FileTemporal/") + strFileName, Excel.XlFixedFormatQuality.xlQualityStandard);

            //if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SCCO_REO") == false)
            //    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ZKTeco_Report");

            //string _strPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ZKTeco_Report";
            //string _strFile = "zkteco_report_" + strSaveName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            //_objExcelCreate.objWB.SaveAs(_strPath + "\\" + _strFile);

            _objExcelCreate.objWB.Close();
            _objExcelCreate.objXLM.Quit();

            ReleaseObject(_objExcelCreate.objXLM);
            ReleaseObject(_objExcelCreate.objWB);
            ReleaseObject(_objExcelCreate.objWS);
        }

        private void AlignmentText(int intVertical, int intHorizontal, string strFont, int intSize, bool flagBold)
        {
            _objExcelCreate.objRN.Font.Name = strFont;
            _objExcelCreate.objRN.Font.Size = intSize;
            _objExcelCreate.objRN.Font.Bold = flagBold;
            _objExcelCreate.objRN.HorizontalAlignment = intHorizontal;
            _objExcelCreate.objRN.VerticalAlignment = intVertical;
        }

        private void BorderTop(double doubleWeight)
        {
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = doubleWeight;
        }
        private void BorderBottom(double doubleWeight)
        {
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = doubleWeight;
        }
        private void BorderLeft(double doubleWeight)
        {
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = doubleWeight;
        }
        private void BorderRight(double doubleWeight)
        {
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            _objExcelCreate.objRN.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = doubleWeight;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

    }
}
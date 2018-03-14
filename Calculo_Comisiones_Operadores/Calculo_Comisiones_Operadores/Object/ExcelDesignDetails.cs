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
using Calculo_Comisiones_Operadores.ObjectSQL;

namespace Calculo_Comisiones_Operadores.Object
{
    public class ExcelDesignDetails
    {
        ExcelCreate _objExcelCreate = new ExcelCreate();
        private object _objMissValue = Missing.Value;
        public string _strLogoPath;
        ExcelControlDetails _objExcelControl = new ExcelControlDetails();

        public ExcelDesignDetails()
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
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowStart + 1, 11] = "FORMATO: REPORTE DETALLADO";
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
                //_objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + _objExcelControl.intRowMiddle, "AD" + _objExcelControl.intRowMiddle);
                //BorderTop(2d);

                //Variables de control
                _objExcelControl.intRowStart += 52;
                _objExcelControl.intRowEnd += 52;
                _objExcelControl.intLogStart += 52;
                _objExcelControl.intLogEnd += 52;
                _objExcelControl.intRowMiddle += 52;
            }
        }

        public void ExcelBodyData(List<ReportDetails> listReportDetail, DateTime dateStart, DateTime dateEnd)
        {
            int _intCount = 0;
            string _strNameOperador = string.Empty;

            foreach (ReportDetails _objReportDetails in listReportDetail)
            {
                if (_strNameOperador == string.Empty)
                {
                    _strNameOperador = _objReportDetails.strNameOperador;

                    //Insertar Operador
                    _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 5), "P" + (_objExcelControl.intRowData + 5)).Merge(false);
                    _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2] = _objReportDetails.strNameOperador;
                    _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2];
                    AlignmentText(2, 2, "Arial", 12, true);

                    _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + (_objExcelControl.intRowData + 6), "AD" + (_objExcelControl.intRowData + 6));
                    BorderBottom(2d);
                }
                else if (_strNameOperador == _objReportDetails.strNameOperador)
                {
                    if (_intCount == 0)
                    {
                        //Insertar Operador
                        _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 5), "P" + (_objExcelControl.intRowData + 5)).Merge(false);
                        _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2] = _objReportDetails.strNameOperador;
                        _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2];
                        AlignmentText(2, 2, "Arial", 12, true);

                        _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + (_objExcelControl.intRowData + 6), "AD" + (_objExcelControl.intRowData + 6));
                        BorderBottom(2d);
                    }
                }
                else
                {
                    _strNameOperador = _objReportDetails.strNameOperador;

                    if (_intCount != 0)
                    {
                        _objExcelControl.intRowCliPAux += 52;
                        _objExcelControl.intRowCliP = _objExcelControl.intRowCliPAux;
                        _objExcelControl.intRowDataAux += 52;
                        _objExcelControl.intRowData = _objExcelControl.intRowDataAux;
                        _intCount = 0;
                    }

                    //Insertar Operador
                    _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 5), "P" + (_objExcelControl.intRowData + 5)).Merge(false);
                    _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2] = _objReportDetails.strNameOperador;
                    _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2];
                    AlignmentText(2, 2, "Arial", 12, true);

                    _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + (_objExcelControl.intRowData + 6), "AD" + (_objExcelControl.intRowData + 6));
                    BorderBottom(2d);
                }


                ////Insertar Operador
                //_objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 5), "P" + (_objExcelControl.intRowData + 5)).Merge(false);
                //_objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2] = _objReportSummary.strName;
                //_objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 5, 2];
                //AlignmentText(2, 2, "Arial", 11, true);

                //Insertar Unidad //Corregir
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 8), "L" + (_objExcelControl.intRowData + 8)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 8, 2] = "Unidad: " + _objReportDetails.strNameUnidad;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 8, 2];
                AlignmentText(2, 2, "Arial", 11, true);

                //Insertar Fechas //Corregir
                _objExcelCreate.objWS.get_Range("W" + (_objExcelControl.intRowData + 8), "AC" + (_objExcelControl.intRowData + 8)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 8, 23] = "Fecha: " + _objReportDetails.dateTravel.ToString("dd-MM-yyyy");
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 8, 23];
                AlignmentText(2, 2, "Arial", 11, true);
                ////

                //Insertar Kil Out //Corregir
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 10), "M" + (_objExcelControl.intRowData + 10)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 2] = "Kilometraje de Salida: " + _objReportDetails.intKilOut;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 2];
                AlignmentText(2, 2, "Arial", 11, true);
                ////

                //Insertar Kil In //Corregir
                _objExcelCreate.objWS.get_Range("Q" + (_objExcelControl.intRowData + 10), "AC" + (_objExcelControl.intRowData + 10)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 17] = "Kilometraje de Entrada: " + _objReportDetails.intKilIn;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 10, 17];
                AlignmentText(2, 2, "Arial", 11, true);
                ////

                //Insertar Kil Total //Corregir
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 12), "O" + (_objExcelControl.intRowData + 12)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 12, 2] = "Kilometros Recorridos: " + _objReportDetails.intKilTotal;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 12, 2];
                AlignmentText(2, 2, "Arial", 11, true);
                ////

                //Insertar Tipo de Viaje //Corregir
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 14), "J" + (_objExcelControl.intRowData + 14)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 14, 2] = "Tipo de Viaje: " + _objReportDetails.strTipoTravel;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 14, 2];
                AlignmentText(2, 2, "Arial", 11, true);
                ////

                //Insertar Hora de Salida //Corregir
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 16), "H" + (_objExcelControl.intRowData + 16)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 16, 2] = "Hora de Salida: " + _objReportDetails.timeOut;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 16, 2];
                AlignmentText(2, 2, "Arial", 11, true);
                ////

                //Insertar Hora de Entrada //Corregir
                _objExcelCreate.objWS.get_Range("J" + (_objExcelControl.intRowData + 16), "Q" + (_objExcelControl.intRowData + 16)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 16, 10] = "Hora de Entrada: " + _objReportDetails.timeIn;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 16, 10];
                AlignmentText(2, 2, "Arial", 11, true);
                ////

                //Insertar Tiempo Total //Corregir
                _objExcelCreate.objWS.get_Range("T" + (_objExcelControl.intRowData + 16), "AB" + (_objExcelControl.intRowData + 16)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 16, 20] = "Tiempo Total: " + _objReportDetails.timeTotal;
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 16, 20];
                AlignmentText(2, 2, "Arial", 11, true);
                ////


                //Insertar Cliente //Corregir
                _objExcelCreate.objWS.get_Range("B" + (_objExcelControl.intRowData + 18), "D" + (_objExcelControl.intRowData + 18)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 18, 2] = "Clientes:";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 18, 2];
                AlignmentText(2, 2, "Arial", 10, true);
                ////

                //Insertar Color y Hora //Corregir
                _objExcelCreate.objWS.get_Range("K" + (_objExcelControl.intRowData + 18), "O" + (_objExcelControl.intRowData + 18)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 18, 11] = "Color & Tiempo:";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 18, 11];
                AlignmentText(2, 2, "Arial", 10, true);
                ////

                //Insertar Tarifa //Corregir
                _objExcelCreate.objWS.get_Range("V" + (_objExcelControl.intRowData + 18), "W" + (_objExcelControl.intRowData + 18)).Merge(false);
                _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 18, 22] = "Tarifa:";
                _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowData + 18, 22];
                AlignmentText(2, 2, "Arial", 10, true);
                ////


                ////Ciclo para insertar clientes //Error con salto de clientes dinamicos.
                foreach (Cliente _objCliente in _objReportDetails.listCliente)
                {
                    //Insertar Cliente
                    _objExcelCreate.objWS.get_Range("D" + (_objExcelControl.intRowCliP), "J" + (_objExcelControl.intRowCliP)).Merge(false);
                    _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 4] = "* " + _objCliente.strName;
                    _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 4];
                    AlignmentText(2, 2, "Arial", 10, false);

                    //Insertar Tiempo y Color
                    Color _objColor = ColorSQL.SelectColorObject(_objCliente.intColorId);

                    _objExcelCreate.objWS.get_Range("N" + (_objExcelControl.intRowCliP), "U" + (_objExcelControl.intRowCliP)).Merge(false);
                    _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 14] = "* " + _objColor.strName + " - " + _objColor.timeHora;
                    _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 14];
                    AlignmentText(2, 2, "Arial", 10, false);

                    _objExcelCreate.objWS.get_Range("X" + (_objExcelControl.intRowCliP), "AC" + (_objExcelControl.intRowCliP)).Merge(false);
                    _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 24] = "* " + _objColor.decTarfica;
                    _objExcelCreate.objRN = _objExcelCreate.objWS.Cells[_objExcelControl.intRowCliP, 24];
                    AlignmentText(2, 2, "Arial", 10, false);

                    _objExcelControl.intRowCliP += 1;
                }

                _objExcelCreate.objRN = _objExcelCreate.objWS.get_Range("A" + (_objExcelControl.intRowData + 21), "AD" + (_objExcelControl.intRowData + 21));
                BorderBottom(2d);

                _intCount++;

                if (_intCount < 3)
                {
                    _objExcelControl.intRowCliP = _objExcelControl.intRowCliPAux;
                    _objExcelControl.intRowCliP += (15 * _intCount);//23
                    _objExcelControl.intRowData += 15;//23
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
            strFileName = strSession[0] + dateStart.ToString("dd") + dateEnd.ToString("dd") + DateTime.Now.ToString("ss") + "_SCCO_REPORTE_DETALLADO_" + DateTime.Now.ToString("yyyyMMdd");
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
using Calculo_Comisiones_Operadores.Object;
using Calculo_Comisiones_Operadores.ObjectSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculo_Comisiones_Operadores
{
    public partial class ComisionesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Session["scco_user"].ToString() == "" || Session["scco_user"].ToString() == null) || Session["scco_tipo"].ToString() != "Administrador")
                    Response.Redirect("Default.aspx");
                //ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnDown);
            }
            DataLoadDGV();
        }

        protected void btnCreate_Click(object sender, ImageClickEventArgs e)
        {
            DateTime _dateStart;
            DateTime _dateEnd;
            bool _flagSummary;
            bool _flagDetail;

            if ((txtDateStart.Text == "" || txtDateStart.Text == string.Empty) || ((txtDateEnd.Text == "" || txtDateEnd.Text == string.Empty)))
            {
                string script = "alert(\"Error, Es necesario seleccionar las fecha para la generación del reporte.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }
            else
            {
                _dateStart = Convert.ToDateTime(txtDateStart.Text);
                _dateEnd = Convert.ToDateTime(txtDateEnd.Text);
            }

            if (_dateEnd.Date < _dateStart.Date)
            {
                string script = "alert(\"Error, La fecha final no puede ser menor que la fecha de inicio.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }

            _flagSummary = ckbSummary.Checked;
            _flagDetail = ckbDetail.Checked;

            if (_flagDetail == false && _flagSummary == false)
            {
                string script = "alert(\"Error, Es necesario seleccionar un tipo de reporte a generar.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }

            if (ckbSummary.Checked == true)
                ReportSummary(_dateStart, _dateEnd);

            if (ckbDetail.Checked == true)
                ReportDetails(_dateStart, _dateEnd);

            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        private void ReportSummary(DateTime dateStart, DateTime dateEnd)
        {
            List<Travel> _listTotalOperadores = TravelSQL.SelectTravelTotalOperador(dateStart, dateEnd);
            List<Travel> _listTravelOperadores = TravelSQL.SelectTravelTotalTravel(dateStart, dateEnd);
            int _intTotalDelivers = TotalTravelDeliver(_listTravelOperadores);//Cantidad Total de Entregas
            int _intTotalOperadores = _listTotalOperadores.Count;//Cantidad Total de Operadores
            int _intTotalTravel = _listTravelOperadores.Count;//Cantidad Total de Viajes
            List<ReportSummary> _listReportSummary = new List<ReportSummary>();

            if (_intTotalTravel > 0)
            {
                foreach (Travel _objTravelOperador in _listTotalOperadores)
                {
                    IEnumerable<Travel> _listIETravelOperador = _listTravelOperadores.Where(lto => lto.intOperadorId == _objTravelOperador.intOperadorId).OrderBy(lto => lto.dateOut);
                    Operador _objOperador = OperadorSQL.SelectOperadorObject(_objTravelOperador.intOperadorId);
                    int _intTotalTravelxOperador = _listIETravelOperador.ToList().Count; //Total de Viajes del Operador
                    int _intTotalDeliver = 0; //Cantidad de Entregas por Viaje 
                    decimal _floatComisiones = 0; //Total de Comisiones

                    List<ClientePreferencial> _listClientePreferencial = ListClientePreferencial(ClienteSQL.SelectClientesPreferenciales());//Clientes Preferenciales                    

                    foreach (Travel _objTravelCliente in _listIETravelOperador.ToList())
                    {
                        List<TravelCliente> _listTravelCliente = TravelSQL.SelectTravelCliente(_objTravelCliente.intId);
                        _intTotalDeliver += _listTravelCliente.Count;
                        TotalClientePreferencial(ref _listClientePreferencial, _listTravelCliente);

                        _floatComisiones += CalculateComisionxTravel(_listTravelCliente);
                    }

                    //Almacenar Informacion dentro de Lista De Report Summary
                    ReportSummary _objReportSummary = new ReportSummary();
                    _objReportSummary.strName = _objOperador.strName + " " + _objOperador.strLastName + " " + _objOperador.strMlastName;
                    _objReportSummary.floatComisiones = _floatComisiones;
                    _objReportSummary.intTotalViajes = _intTotalTravelxOperador;
                    _objReportSummary.intTotalEntregas = _intTotalDeliver;
                    _objReportSummary.listClientePreferencial = _listClientePreferencial;
                    _objReportSummary.floatViajes = (_intTotalTravelxOperador * 100) / _intTotalTravel;
                    _objReportSummary.floatEntregas = (_intTotalDeliver * 100) / _intTotalDelivers;
                    _listReportSummary.Add(_objReportSummary);
                }
                //Enviar a Metodo para proceso de crear Excel.
                double _floatPaper = (_intTotalOperadores / 2);
                int _intPaper = Convert.ToInt32(Math.Round(_floatPaper, 0));

                ExcelDesignSummary _objExcel = new ExcelDesignSummary();
                _objExcel.ExcelBodyPage(_intPaper + 1);
                _objExcel.ExcelBodyData(_listReportSummary, dateStart, dateEnd);

                string _strFileName = string.Empty;
                _objExcel.SaveExcel(ref _strFileName, dateStart, dateEnd, Session["scco_user"].ToString());

                UploadFile(_strFileName);
            }
        }

        private void UploadFile(string strFileName)
        {
            Upload _objUpload = new Upload();
            FileStream _fs = new FileStream(HttpContext.Current.Server.MapPath("~/FileTemporal/" + strFileName + ".pdf"), FileMode.Open, FileAccess.Read);
            byte[] _byte = new byte[_fs.Length];
            _objUpload.intSize = (int)_fs.Length;
            _fs.Read(_byte, 0, (int)_fs.Length);
            _fs.Close();
            _objUpload.bFile = _byte;
            _objUpload.strName = strFileName;
            _objUpload.strType = "Resumen";
            _objUpload.strUser = Session["scco_user"].ToString();
            _objUpload.dateUp = DateTime.Now;
            _objUpload.strExt = ".pdf";

            UploadSQL.InsertFileUpload(_objUpload);
        }

        private int TotalTravelDeliver(List<Travel> listTravelOperadores)
        {
            int _intTotalDelivers = 0;

            foreach (Travel _objTravel in listTravelOperadores)
            {
                List<TravelCliente> _listTravelCliente = TravelSQL.SelectTravelCliente(_objTravel.intId);
                _intTotalDelivers += _listTravelCliente.Count;
            }

            return _intTotalDelivers;
        }

        private List<ClientePreferencial> ListClientePreferencial(List<Cliente> listCliente)
        {
            List<ClientePreferencial> _listClienteP = new List<ClientePreferencial>();

            foreach (Cliente _objCliente in listCliente)
            {
                ClientePreferencial _objClienteP = new ClientePreferencial();
                _objClienteP.intId = _objCliente.intId;
                _objClienteP.strNombre = _objCliente.strName;
                _objClienteP.intTotal = 0;
                _listClienteP.Add(_objClienteP);
            }

            return _listClienteP;
        }

        private void TotalClientePreferencial(ref List<ClientePreferencial> listClientePreferencial, List<TravelCliente> listTravelCliente)
        {
            foreach (TravelCliente _objTravelCliente in listTravelCliente)
            {
                foreach (ClientePreferencial _objCliente in listClientePreferencial)
                {
                    if (_objCliente.intId == _objTravelCliente.intClienteId)
                        _objCliente.intTotal += 1;
                }
            }
        }

        private decimal CalculateComisionxTravel(List<TravelCliente> listTravelCliente)
        {
            List<Color> _listColor = new List<Color>();
            decimal _floatMax = 0;
            decimal _floatComision = 0;

            foreach (TravelCliente _objTravelCliente in listTravelCliente)
            {
                _listColor.Add(ColorSQL.SelectColorTravel(_objTravelCliente.intClienteId));
                //Crear Query que al momento de buscar el cliente, obtenga el Precio y color.
            }

            _floatMax = _listColor.Max(lc => lc.decTarfica);

            for (int i = 1; i <= _listColor.Count; i++)
            {
                if (i == 1)
                    _floatComision += _floatMax;
                else
                    _floatComision += (_floatComision / 2);
            }

            //Ciclo de calcule comision si son mas de dos. tomar la mitad del mayor color.

            return _floatComision;
        }

        private void DataLoadDGV()
        {
            DataTable _dtTable = UploadSQL.SelectFiles();
            _dtTable.Columns.Add("scco_date", typeof(System.String));

            foreach (DataRow _dtRow in _dtTable.Rows)
            {
                _dtRow["scco_date"] = (Convert.ToDateTime(_dtRow["scco_date_up"].ToString())).ToString("yyyy-MM-dd HH:mm:ss");
            }

            dgvComisiones.DataSource = _dtTable;
            dgvComisiones.DataBind();
        }

        protected void dgvComisiones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvComisiones.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void dgvComisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int _intIndex = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.Equals("deleteRecords"))
            {
                DeleteRecord(_intIndex);
            }

            if (e.CommandName.Equals("editRecord"))
            {
                DownloadFile(_intIndex);
            }
        }

        private void DeleteRecord(int intIndex)
        {
            deleteID.Value = dgvComisiones.DataKeys[intIndex].Value.ToString();

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#viewModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UploadSQL.DeleteFile(deleteID.Value);

            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#viewModal').modal('hide');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void DownloadFile(int intIndex)
        {
            //string _strId = dgvComisiones.DataKeys[intIndex].Value.ToString();
            DownId.Value = dgvComisiones.DataKeys[intIndex].Value.ToString();
            Upload _objUpload = UploadSQL.SelectFile(DownId.Value.ToString());
            lbFile.Text = _objUpload.strName;

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#updateModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "UpdateModalScript", _strBuilder.ToString(), false);

            //Upload _objUpload = UploadSQL.SelectFile(_strId);
            //byte[] _bytes = _objUpload.bFile;
            //string _strName = _objUpload.strName + _objUpload.strExt;

            //Response.Clear();
            ////Response.ClearHeaders();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "Application/pdf";
            ////Response.AppendHeader("Content-Type", "Application/octet-stream");            
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + _strName);
            ////Response.AppendHeader("Content-Length", _objUpload.intSize.ToString());
            //Response.BinaryWrite(_bytes);
            //Response.TransmitFile(Server.MapPath("~/FileTemporal/" + _strName));
            //Response.Flush();
            //Response.End();
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            Upload _objUpload = UploadSQL.SelectFile(DownId.Value.ToString());
            byte[] _bytes = _objUpload.bFile;
            string _strName = _objUpload.strName + _objUpload.strExt;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + _strName);
            Response.BinaryWrite(_bytes);
            Response.Flush();
            Response.End();

            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#updateModal').modal('hide');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this, GetType(), "UpdateModalScript", _strBuilder.ToString(), false);
        }

        private void ReportDetails(DateTime dateStart, DateTime dateEnd)
        {
            List<Travel> _listTotalOperadores = TravelSQL.SelectTravelTotalOperador(dateStart, dateEnd);
            List<Travel> _listTravelOperadores = TravelSQL.SelectTravelTotalTravel(dateStart, dateEnd);
            //int _intTotalDelivers = TotalTravelDeliver(_listTravelOperadores);//Cantidad Total de Entregas
            //int _intTotalOperadores = _listTotalOperadores.Count;//Cantidad Total de Operadores
            int _intTotalTravel = _listTravelOperadores.Count;//Cantidad Total de Viajes
            List<ReportDetails> _listReportDetails = new List<ReportDetails>();
            int _intTotalsTravelCliente = 0;

            if (_intTotalTravel > 0)
            {
                foreach (Travel _objTravelOperador in _listTotalOperadores)
                {
                    IEnumerable<Travel> _listIETravelOperador = _listTravelOperadores.Where(lto => lto.intOperadorId == _objTravelOperador.intOperadorId).OrderBy(lto => lto.dateOut);
                    Operador _objOperador = OperadorSQL.SelectOperadorObject(_objTravelOperador.intOperadorId);
                    List<Cliente> _listCliente = new List<Cliente>();

                    //int _intTotalTravelxOperador = _listIETravelOperador.ToList().Count; //Total de Viajes del Operador
                    //int _intTotalDeliver = 0; //Cantidad de Entregas por Viaje 
                    //decimal _floatComisiones = 0; //Total de Comisiones
                    //List<ClientePreferencial> _listClientePreferencial = ListClientePreferencial(ClienteSQL.SelectClientesPreferenciales());//Clientes Preferenciales   
                    List<ReportDetails> _listReportDetailsAux = new List<ReportDetails>();

                    foreach (Travel _objTravelCliente in _listIETravelOperador.ToList())//Viaje x Cliente
                    {
                        List<TravelCliente> _listTravelCliente = TravelSQL.SelectTravelCliente(_objTravelCliente.intId);
                        //_intTotalDeliver += _listTravelCliente.Count;
                        //TotalClientePreferencial(ref _listClientePreferencial, _listTravelCliente);
                        //_floatComisiones += CalculateComisionxTravel(_listTravelCliente);
                        _listCliente = ClienteSQL.SelectClientesTravel(_listTravelCliente);
                        Unidad _objUnidad = UnidadSQL.SelectUnidadObject(_objTravelCliente.intUnidadId);

                        ReportDetails _objReportDetails = new ReportDetails();
                        _objReportDetails.strNameOperador = _objOperador.strName + " " + _objOperador.strLastName + " " + _objOperador.strMlastName;
                        _objReportDetails.strNameUnidad = _objUnidad.strName;
                        _objReportDetails.strTipoTravel = _objTravelCliente.intTipo;
                        _objReportDetails.dateTravel = _objTravelCliente.dateOut;
                        _objReportDetails.intKilOut = _objTravelCliente.intKilometrajeOut;
                        _objReportDetails.intKilIn = _objTravelCliente.intKilometrajeIn;
                        _objReportDetails.intKilTotal = _objTravelCliente.intKilometrajeIn - _objTravelCliente.intKilometrajeOut;
                        _objReportDetails.timeOut = TimeSpan.Parse(_objTravelCliente.dateOut.ToString("HH:mm:ss"));
                        _objReportDetails.timeIn = TimeSpan.Parse(_objTravelCliente.dateIn.ToString("HH:mm:ss"));
                        _objReportDetails.timeTotal = _objReportDetails.timeIn - _objReportDetails.timeOut;
                        _objReportDetails.listCliente = _listCliente;
                        _listReportDetails.Add(_objReportDetails);
                        _listReportDetailsAux.Add(_objReportDetails);
                    }
                    
                    double _floatPaper = (_listReportDetailsAux.Count / 3);

                    if (_floatPaper == 0)
                        _floatPaper = 1;

                    _intTotalsTravelCliente += Convert.ToInt32(Math.Round(_floatPaper, 0));
                }

                //Enviar a Metodo para proceso de crear Excel.
                //double _floatPaper = (_intTotalOperadores / 2);
                int _intPaper = _intTotalsTravelCliente;//Convert.ToInt32(Math.Round(_floatPaper, 0));

                ExcelDesignDetails _objExcel = new ExcelDesignDetails();
                _objExcel.ExcelBodyPage(_intPaper);
                _objExcel.ExcelBodyData(_listReportDetails, dateStart, dateEnd);

                string _strFileName = string.Empty;
                _objExcel.SaveExcel(ref _strFileName, dateStart, dateEnd, Session["scco_user"].ToString());

                UploadFile(_strFileName);
            }
        }
    }
}
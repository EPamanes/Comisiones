using Calculo_Comisiones_Operadores.Object;
using Calculo_Comisiones_Operadores.ObjectSQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculo_Comisiones_Operadores
{
    public partial class TravelAdmin : System.Web.UI.Page
    {
        List<Operador> _listOperadorAux = new List<Operador>();
        List<Unidad> _listUnidadAux = new List<Unidad>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Session["scco_user"].ToString() == "" || Session["scco_user"].ToString() == null) || Session["scco_tipo"].ToString() != "Administrador")
                    Response.Redirect("Default.aspx");
            }
            DataLoadDGV();
            ListTipoSearch();
            ListStatusSearch();
        }

        private void DataLoadDGV()
        {
            DataTable _dtTable = TravelSQL.SelectTravels();

            _dtTable.Columns.Add("scco_control_name", typeof(System.String));
            _dtTable.Columns.Add("scco_unidad_name", typeof(System.String));
            _dtTable.Columns.Add("scco_operador_name", typeof(System.String));
            _dtTable.Columns.Add("scco_date_out_str", typeof(System.String));
            _dtTable.Columns.Add("scco_date_in_str", typeof(System.String));

            foreach (DataRow _dtRow in _dtTable.Rows)
            {
                _dtRow["scco_control_name"] = (Convert.ToInt32(_dtRow["scco_control"].ToString()) == 0) ? "Iniciado" : "Finalizado";
                _dtRow["scco_unidad_name"] = SearchUnidad(Convert.ToInt32(_dtRow["scco_unidad_id"].ToString()));
                _dtRow["scco_operador_name"] = SearchUnidad(Convert.ToInt32(_dtRow["scco_operador_id"].ToString()));
                _dtRow["scco_date_out_str"] = (Convert.ToDateTime(_dtRow["scco_date_out"].ToString())).ToString("yyyy-MM-dd HH:mm:ss");

                try
                {
                    _dtRow["scco_date_in_str"] = (Convert.ToDateTime(_dtRow["scco_date_in"].ToString())).ToString("yyyy-MM-dd HH:mm:ss");
                }

                catch
                {
                    _dtRow["scco_date_in_str"] = string.Empty;
                }

            }

            dgvTravel.DataSource = _dtTable;
            dgvTravel.DataBind();
        }

        private string SearchUnidad(int intUnidadId)
        {
            Unidad _objUnidad = UnidadSQL.SelectUnidadObject(intUnidadId);
            return _objUnidad.strName;
        }

        private string SearchOperador(int intOperadorId)
        {
            Operador _objOperador = OperadorSQL.SelectOperadorObject(intOperadorId);
            return _objOperador.strName;// + _objOperador.strLastName;
        }

        private void ListTipoSearch()
        {
            listTravelShearch.Items.Clear();
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "----Tipo Viaje-----", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Simple", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Doble", Value = 2 });


            listTravelShearch.DataSource = _listTipo;
            listTravelShearch.DataTextField = "Text";
            listTravelShearch.DataValueField = "Value";
            listTravelShearch.DataBind();
            listTravelShearch.SelectedValue = "0";
        }

        private void ListStatusSearch()
        {
            listStatusShearch.Items.Clear();
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-----Status-----", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Iniciado", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Finalizado", Value = 2 });


            listStatusShearch.DataSource = _listTipo;
            listStatusShearch.DataTextField = "Text";
            listStatusShearch.DataValueField = "Value";
            listStatusShearch.DataBind();
            listStatusShearch.SelectedValue = "0";
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string _strTipo = listTravelShearch.SelectedItem.Text;
            int _intStatus = Convert.ToInt32(listStatusShearch.SelectedValue);

            //if (txtSearch.Text.Trim() != "" || txtSearch.Text.Trim() != string.Empty)
            //{
            DataTable _dtSearch = TravelSQL.SelectTravelSearch(_strTipo, _intStatus);

            _dtSearch.Columns.Add("scco_control_name", typeof(System.String));
            _dtSearch.Columns.Add("scco_unidad_name", typeof(System.String));
            _dtSearch.Columns.Add("scco_operador_name", typeof(System.String));
            _dtSearch.Columns.Add("scco_date_out_str", typeof(System.String));
            _dtSearch.Columns.Add("scco_date_in_str", typeof(System.String));

            foreach (DataRow _dtRow in _dtSearch.Rows)
            {
                _dtRow["scco_control_name"] = (Convert.ToInt32(_dtRow["scco_control"].ToString()) == 0) ? "Iniciado" : "Finalizado";
                _dtRow["scco_unidad_name"] = SearchUnidad(Convert.ToInt32(_dtRow["scco_unidad_id"].ToString()));
                _dtRow["scco_operador_name"] = SearchUnidad(Convert.ToInt32(_dtRow["scco_operador_id"].ToString()));
                _dtRow["scco_date_out_str"] = (Convert.ToDateTime(_dtRow["scco_date_out"].ToString())).ToString("yyyy-MM-dd HH:mm:ss");

                try
                {
                    _dtRow["scco_date_in_str"] = (Convert.ToDateTime(_dtRow["scco_date_in"].ToString())).ToString("yyyy-MM-dd HH:mm:ss");
                }

                catch
                {
                    _dtRow["scco_date_in_str"] = string.Empty;
                }
            }

            dgvTravel.DataSource = _dtSearch;
            dgvTravel.DataBind();

            //}
            //else
            //{
            //    //DataTable _dtSearch = ClienteSQL.SelectCliente();
            //    //dgvCliente.DataSource = _dtSearch;
            //    //dgvCliente.DataBind();

            //    //DataLoadDGV();
            //}
        }

        private void CreateDGVDynamic()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add("rowid", typeof(int));
            dt.Columns.Add("cliente", typeof(string));
            dt.Columns.Add("factura", typeof(string));

            dr = dt.NewRow();
            dr["rowid"] = 1;
            //dr["cliente"] = string.Empty;
            dr["factura"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["Curtbl"] = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();

            DropDownList ddl = (DropDownList)gvDetails.Rows[0].Cells[1].FindControl("listClienteAdd");
            FillDropDownList(ddl);
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            CreateDGVDynamic();
            ClearInsert();

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#addModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AddShowModalScript", _strBuilder.ToString(), false);
        }

        protected void dgvTravel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTravel.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void dgvTravel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int _intIndex = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.Equals("deleteRecord"))
            {
                DeleteRecord(_intIndex);
            }

            if (e.CommandName.Equals("editRecord"))
            {
                UpdateRecord(_intIndex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInsert() == true)
            {
                Travel _objTravel = new Travel();
                _objTravel.intKilometrajeOut = Convert.ToInt32(txtKilOutAdd.Text.Trim());
                _objTravel.intControl = 0;
                DateTime _dateAux = Convert.ToDateTime(txtDateOutAdd.Text);
                TimeSpan _timeAux = TimeSpan.Parse(txtHoraOutAdd.Text);
                _dateAux = _dateAux.Date + _timeAux;
                _objTravel.dateOut = _dateAux;
                _objTravel.intOperadorId = Convert.ToInt32(listOperadorAdd.SelectedValue);
                _objTravel.intUnidadId = Convert.ToInt32(listUnidadadd.SelectedValue);
                _objTravel.strUser = Session["scco_user"].ToString();

                DataTable dt = (DataTable)ViewState["Curtbl"];
                List<TravelCliente> _listTravelCliente = new List<TravelCliente>();
                int _intRule1 = 0;
                int _intRule2 = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList drpListAux = (DropDownList)gvDetails.Rows[i].Cells[1].FindControl("listClienteAdd");
                    TextBox txtprice = (TextBox)gvDetails.Rows[i].Cells[2].FindControl("txtFactura");

                    TravelCliente _objTravelCliente = new TravelCliente();
                    _objTravelCliente.dateUp = DateTime.Now;
                    _objTravelCliente.strFactura = txtprice.Text;
                    _objTravelCliente.intClienteId = Convert.ToInt32(drpListAux.SelectedItem.Value);
                    _objTravelCliente.strUser = Session["scco_user"].ToString();

                    Cliente _objCliente = new Cliente();
                    _objCliente = ClienteSQL.SelectClienteObject(_objTravelCliente.intClienteId);

                    if (_objCliente.intRule1 == 1)
                        _intRule1++;
                    if (_objCliente.intRule2 == 1)
                        _intRule2++;

                    _listTravelCliente.Add(_objTravelCliente);
                }

                if (_intRule2 > 0)
                    _objTravel.intTipo = "Doble";
                else
                    _objTravel.intTipo = "Simple";

                TravelSQL.InsertTravel(_objTravel, _listTravelCliente);

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
                _strBuilder.Append(@"<script type='text/javascript'>");
                _strBuilder.Append("$('#addModal').modal('hide');");
                _strBuilder.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", _strBuilder.ToString(), false);
            }
        }

        private bool ValidateInsert()
        {
            if (listOperadorAdd.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar a el operador.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }
            //Validar que Operador no se encuentre en un viaje activo (Iniciado -> Terminado)

            if (listUnidadadd.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar la unidad del viaje.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }
            //Validar que Unidad no se encuentre en un viaje activo (Iniciado -> Terminado)

            if (txtDateOutAdd.Text.Trim() == "" || txtDateOutAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la fecha de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtHoraOutAdd.Text.Trim() == "" || txtHoraOutAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la hora de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtKilOutAdd.Text.Trim() == "" || txtKilOutAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el kilometraje de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            DataTable dt = (DataTable)ViewState["Curtbl"];
            int rowIndex = 0;
            List<string> _listCliente = new List<string>();
            List<string> _listFactura = new List<string>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList drpListAux = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("listClienteAdd");
                    TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtFactura");

                    if (Convert.ToInt32(drpListAux.SelectedItem.Value) == 0)
                    {
                        string script = "alert(\"Error, Es necesario seleccionar el cliente.\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        return false;
                    }

                    if (txtprice.Text.Trim() == string.Empty || txtprice.Text.Trim() == "")
                    {
                        string script = "alert(\"Error, Es necesario ingresar el codigo de factura del cliente.\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        return false;
                    }

                    _listCliente.Add(drpListAux.SelectedItem.Text);
                    _listFactura.Add(txtprice.Text.Trim());

                    rowIndex++;
                }
            }

            var _varAuxCliente = _listCliente.GroupBy(x => x).Count(x => x.Count() > 1);
            var _varAuxFactura = _listFactura.GroupBy(y => y).Count(y => y.Count() > 1);

            if (Convert.ToInt32(_varAuxCliente.ToString()) >= 1)
            {
                string script = "alert(\"Error, No se puede seleccionar un mismo cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (Convert.ToInt32(_varAuxFactura.ToString()) >= 1)
            {
                string script = "alert(\"Error, No se puede ingresar la misma factura a distintos cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();


                    for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
                    {
                        gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOldData();
                }
            }
        }

        protected void btnAddDGV_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        DropDownList drpListAux = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("listClienteAdd");
                        TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtFactura");
                        drCurrentRow = dt.NewRow();
                        drCurrentRow["rowid"] = i + 1;
                        dt.Rows[i - 1]["cliente"] = drpListAux.SelectedItem.Text;
                        int _intAux = Convert.ToInt32(drpListAux.SelectedItem.Value);
                        dt.Rows[i - 1]["factura"] = txtprice.Text;
                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState Value is Null");
            }
            SetOldData();
        }
        private void SetOldData()
        {
            int rowIndex = 0;
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList drpListAux = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("listClienteAdd");
                        TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtFactura");

                        FillDropDownList(drpListAux);
                        drpListAux.ClearSelection();

                        string _strClientAux = string.Empty;
                        int _intCount = 0;

                        foreach (var item in dt.Rows[i].ItemArray)
                        {
                            if (_intCount == 1)
                                _strClientAux = item.ToString();
                            _intCount++;
                        }

                        if (_strClientAux != string.Empty)
                            drpListAux.Items.FindByText(_strClientAux).Selected = true;
                        else
                            drpListAux.Items.FindByText(_strClientAux);

                        txtprice.Text = dt.Rows[i]["factura"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        private void FillDropDownList(DropDownList ddl)
        {
            ArrayList arr = GetDummyData();

            foreach (ListItem item in arr)
            {
                ddl.Items.Add(item);
            }
        }

        private ArrayList GetDummyData()
        {
            ArrayList arr = new ArrayList();
            List<Cliente> _listCliente = ClienteSQL.SelectClientes();

            foreach (Cliente _objCliente in _listCliente)
            {
                arr.Add(new ListItem(_objCliente.strName, _objCliente.intId.ToString()));
            }

            return arr;
        }


        private void ClearInsert()
        {
            txtIdAdd.Value = string.Empty;
            txtDateOutAdd.Text = string.Empty;
            txtHoraOutAdd.Text = string.Empty;
            txtKilOutAdd.Text = string.Empty;

            ListOperador();
            ListUnidad();
        }

        private void ListOperador()
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });

            List<Operador> _listOperador = new List<Operador>();
            _listOperador = OperadorSQL.SelectOperadorActive();

            List<Travel> _listTravel = TravelSQL.SelectTravelOperador("operador");

            IEnumerable<Operador> _listIEOperador;

            if (_listTravel.Count != 0)
                _listIEOperador = _listOperador.Where(lo => _listTravel.Any(lt => lt.intOperadorId != lo.intID));
            else
                _listIEOperador = _listOperador;

            foreach (Operador _objOperador in _listIEOperador) //_listOperador)
            {
                _listTipo.Add(new ComboBoxItem() { Text = _objOperador.strName, Value = _objOperador.intID });
            }

            listOperadorAdd.DataSource = _listTipo;
            listOperadorAdd.DataTextField = "Text";
            listOperadorAdd.DataValueField = "Value";
            listOperadorAdd.DataBind();
            listOperadorAdd.SelectedValue = "0";
        }

        private void ListUnidad()
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });

            List<Unidad> _listUnidad = new List<Unidad>();
            _listUnidad = UnidadSQL.SelectUnidadActive();

            List<Travel> _listTravel = TravelSQL.SelectTravelOperador("unidad");

            IEnumerable<Unidad> _listIEUnidad;

            if (_listTravel.Count != 0)
                _listIEUnidad = _listUnidad.Where(lu => _listTravel.Any(lt => lt.intUnidadId != lu.intID));
            else
                _listIEUnidad = _listUnidad;


            foreach (Unidad _objUnidad in _listIEUnidad) //_listUnidad)
            {
                _listTipo.Add(new ComboBoxItem() { Text = _objUnidad.strName, Value = _objUnidad.intID });
            }

            listUnidadadd.DataSource = _listTipo;
            listUnidadadd.DataTextField = "Text";
            listUnidadadd.DataValueField = "Value";
            listUnidadadd.DataBind();
            listUnidadadd.SelectedValue = "0";
        }

        private void DeleteRecord(int intIndex)
        {
            deleteID.Value = dgvTravel.DataKeys[intIndex].Value.ToString();
            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            TravelSQL.DeleteTravel(deleteID.Value);

            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('hide');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void UpdateRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvTravel.DataKeys[intIndex].Value.ToString());
            Travel _objtravel = TravelSQL.SelectTravelObject(_intID);

            txtIdMod.Value = _intID.ToString();
            ListUnidad(_objtravel.intUnidadId);
            ListOperador(_objtravel.intOperadorId);
            DateTime _dateAuxOut = _objtravel.dateOut;
            TimeSpan _timeAuxOut = TimeSpan.Parse(_dateAuxOut.ToString("HH:mm:ss"));
            txtDateOutMod.Text = _dateAuxOut.ToString("yyyy-MM-dd");
            txtHoraOutMod.Text = _timeAuxOut.ToString();
            txtKilOutMod.Text = _objtravel.intKilometrajeOut.ToString();
            DateTime _dateAuxIn = _objtravel.dateIn;
            TimeSpan _timeAuxIn = TimeSpan.Parse(_dateAuxIn.ToString("HH:mm:ss"));
            txtDateInMod.Text = _dateAuxIn.ToString("yyyy-MM-dd");
            txtHoraInMod.Text = _timeAuxIn.ToString();
            txtKilInMod.Text = _objtravel.intKilometrajeIn.ToString();

            ModDGVDynamic(_intID);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#editModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EditModalScript", _strBuilder.ToString(), false);
        }

        private void ListOperador(int intOperadorId)
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });

            List<Operador> _listOperador = new List<Operador>();
            _listOperador = OperadorSQL.SelectOperadorActive();

            List<Travel> _listTravel = TravelSQL.SelectTravelOperador("operador");

            IEnumerable<Operador> _listIEOperador = _listOperador.Where(lo => _listTravel.Any(lt => lt.intOperadorId != lo.intID));

            foreach (Operador _objOperador in _listIEOperador) //_listOperador)
            {
                _listTipo.Add(new ComboBoxItem() { Text = _objOperador.strName, Value = _objOperador.intID });
            }

            bool _flag = _listTipo.Any(lt => Convert.ToInt32(lt.Value.ToString()) == intOperadorId);

            if (_flag == false)
                _listTipo.Add(new ComboBoxItem() { Text = SearchOperador(intOperadorId), Value = intOperadorId });

            listOperadorMod.DataSource = _listTipo;
            listOperadorMod.DataTextField = "Text";
            listOperadorMod.DataValueField = "Value";
            listOperadorMod.DataBind();
            listOperadorMod.SelectedValue = intOperadorId.ToString();
        }

        private void ListUnidad(int intUnidadId)
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });

            List<Unidad> _listUnidad = new List<Unidad>();
            _listUnidad = UnidadSQL.SelectUnidadActive();

            List<Travel> _listTravel = TravelSQL.SelectTravelOperador("unidad");

            IEnumerable<Unidad> _listIEUnidad = _listUnidad.Where(lu => _listTravel.Any(lt => lt.intUnidadId != lu.intID));

            foreach (Unidad _objUnidad in _listIEUnidad) //_listUnidad)
            {
                _listTipo.Add(new ComboBoxItem() { Text = _objUnidad.strName, Value = _objUnidad.intID });
            }

            bool _flag = _listTipo.Any(lt => Convert.ToInt32(lt.Value.ToString()) == intUnidadId);

            if (_flag == false)
                _listTipo.Add(new ComboBoxItem() { Text = SearchUnidad(intUnidadId), Value = intUnidadId });

            listUnidadMod.DataSource = _listTipo;
            listUnidadMod.DataTextField = "Text";
            listUnidadMod.DataValueField = "Value";
            listUnidadMod.DataBind();
            listUnidadMod.SelectedValue = intUnidadId.ToString();
        }


        private void ModDGVDynamic(int intID)
        {
            List<TravelCliente> _listTravelCliente = TravelSQL.SelectTravelCliente(intID);

            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add("rowid", typeof(int));
            dt.Columns.Add("cliente", typeof(string));
            dt.Columns.Add("factura", typeof(string));

            foreach (TravelCliente _objTravelCliente in _listTravelCliente)
            {
                dr = dt.NewRow();
                dr["rowid"] = _objTravelCliente.intId;
                dr["factura"] = _objTravelCliente.strFactura;
                dt.Rows.Add(dr);

                ViewState["Curtbl"] = dt;
                dgvDetailMod.DataSource = dt;
                dgvDetailMod.DataBind();
            }

            SetOldDataMod(_listTravelCliente);
        }

        private void FillDropDownList2(DropDownList ddl)
        {
            ArrayList arr = GetDummyData2();

            foreach (ListItem item in arr)
            {
                ddl.Items.Add(item);
            }
        }

        private ArrayList GetDummyData2()
        {
            ArrayList arr = new ArrayList();
            List<Cliente> _listCliente = ClienteSQL.SelectClientes();

            foreach (Cliente _objCliente in _listCliente)
            {
                arr.Add(new ListItem(_objCliente.strName, _objCliente.intId.ToString()));
            }

            return arr;
        }

        private void SetOldDataMod(List<TravelCliente> listTravelCliente)
        {
            int rowIndex = 0;
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];

                if (dt.Rows.Count > 0)
                {
                    foreach (TravelCliente _objTravelCliente in listTravelCliente)
                    {
                        DropDownList drpListAux = (DropDownList)dgvDetailMod.Rows[rowIndex].Cells[1].FindControl("listClienteMod");
                        TextBox txtprice = (TextBox)dgvDetailMod.Rows[rowIndex].Cells[2].FindControl("txtFacturaMod");
                        Label txtrow = (Label)dgvDetailMod.Rows[rowIndex].Cells[0].FindControl("rowid");

                        FillDropDownList(drpListAux);

                        drpListAux.ClearSelection();
                        Cliente _objCliente = ClienteSQL.SelectClienteObject(_objTravelCliente.intClienteId);

                        drpListAux.Items.FindByText(_objCliente.strName).Selected = true;
                        drpListAux.Items.FindByValue(_objTravelCliente.intClienteId.ToString()).Selected = true;

                        txtprice.Text = _objTravelCliente.strFactura;
                        txtrow.Text = _objTravelCliente.intId.ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate() == true)
            {
                Travel _objTravel = new Travel();
                _objTravel.intControl = 1;//Meter Check
                _objTravel.intId = Convert.ToInt32(txtIdMod.Value);
                DateTime _dateAuxOut = Convert.ToDateTime(txtDateOutMod.Text);
                TimeSpan _timeAuxOut = TimeSpan.Parse(txtHoraOutMod.Text);
                _dateAuxOut = _dateAuxOut.Date + _timeAuxOut;
                _objTravel.dateOut = _dateAuxOut;
                _objTravel.intKilometrajeOut = Convert.ToInt32(txtKilOutMod.Text.Trim());

                DateTime _dateAuxIn = Convert.ToDateTime(txtDateInMod.Text);
                TimeSpan _timeAuxIn = TimeSpan.Parse(txtHoraInMod.Text);
                _dateAuxIn = _dateAuxIn.Date + _timeAuxIn;
                _objTravel.dateIn = _dateAuxIn;
                _objTravel.intKilometrajeIn = Convert.ToInt32(txtKilInMod.Text.Trim());

                _objTravel.intOperadorId = Convert.ToInt32(listOperadorMod.SelectedValue);
                _objTravel.intUnidadId = Convert.ToInt32(listUnidadMod.SelectedValue);
                _objTravel.strUser = Session["scco_user"].ToString();

                DataTable dt = (DataTable)ViewState["Curtbl"];
                List<TravelCliente> _listTravelCliente = new List<TravelCliente>();
                int _intRule1 = 0;
                int _intRule2 = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList drpListAux = (DropDownList)dgvDetailMod.Rows[i].Cells[1].FindControl("listClienteMod");
                    TextBox txtprice = (TextBox)dgvDetailMod.Rows[i].Cells[2].FindControl("txtFacturaMod");
                    Label txtrowid = (Label)dgvDetailMod.Rows[i].Cells[0].FindControl("rowid");

                    TravelCliente _objTravelCliente = new TravelCliente();
                    _objTravelCliente.intId = Convert.ToInt32(txtrowid.Text);
                    _objTravelCliente.dateMod = DateTime.Now;
                    _objTravelCliente.strUser = Session["scco_user"].ToString();
                    //_objTravelCliente.dateUp = DateTime.Now;
                    _objTravelCliente.strFactura = txtprice.Text;
                    _objTravelCliente.intClienteId = Convert.ToInt32(drpListAux.SelectedItem.Value);

                    Cliente _objCliente = new Cliente();
                    _objCliente = ClienteSQL.SelectClienteObject(_objTravelCliente.intClienteId);

                    if (_objCliente.intRule1 == 1)
                        _intRule1++;
                    if (_objCliente.intRule2 == 1)
                        _intRule2++;

                    _listTravelCliente.Add(_objTravelCliente);
                }

                if (_intRule2 > 0)
                    _objTravel.intTipo = "Doble";
                else
                {
                    if (_intRule1 > 0)
                    {
                        TimeSpan _timeOutR1 = TimeSpan.Parse("07:30:00");
                        TimeSpan _timeInR1 = TimeSpan.Parse("17:45:00");

                        if (_timeAuxOut <= _timeOutR1)
                        {
                            if (_timeAuxIn >= _timeInR1 || _dateAuxIn.Date > _dateAuxOut.Date)
                                _objTravel.intTipo = "Doble";
                            else
                                _objTravel.intTipo = "Simple";
                        }
                        else
                            _objTravel.intTipo = "Simple";
                    }
                    else
                        _objTravel.intTipo = "Simple";
                }

                TravelSQL.UpdateTravel(_objTravel, _listTravelCliente);

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
                _strBuilder.Append(@"<script type='text/javascript'>");
                _strBuilder.Append("$('#editModal').modal('hide');");
                _strBuilder.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", _strBuilder.ToString(), false);
            }
        }

        private bool ValidateUpdate()
        {
            if (listOperadorMod.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar a el operador.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }
            //Validar que Operador no se encuentre en un viaje activo (Iniciado -> Terminado)

            if (listUnidadMod.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar la unidad del viaje.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }
            //Validar que Unidad no se encuentre en un viaje activo (Iniciado -> Terminado)

            if (txtDateOutMod.Text.Trim() == "" || txtDateOutMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la fecha de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtHoraOutMod.Text.Trim() == "" || txtHoraOutMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la hora de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtKilOutMod.Text.Trim() == "" || txtKilOutMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el kilometraje de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtDateInMod.Text.Trim() == "" || txtDateInMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la fecha de entrada.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtHoraInMod.Text.Trim() == "" || txtHoraInMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la hora de entrada.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtKilInMod.Text.Trim() == "" || txtKilInMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el kilometraje de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            DateTime _dateAuxOut = Convert.ToDateTime(txtDateOutMod.Text);
            TimeSpan _timeAuxOut = TimeSpan.Parse(txtHoraOutMod.Text);
            DateTime _dateAuxIn = Convert.ToDateTime(txtDateInMod.Text);
            TimeSpan _timeAuxIn = TimeSpan.Parse(txtHoraInMod.Text);

            _dateAuxOut = _dateAuxOut.Date + _timeAuxOut;
            _dateAuxIn = _dateAuxIn.Date + _timeAuxIn;

            if (_dateAuxIn < _dateAuxOut)
            {
                string script = "alert(\"Error, El tiempo de entrada no puede ser menor que al tiempo de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (Convert.ToInt32(txtKilInMod.Text) <= Convert.ToInt32(txtKilOutMod.Text))
            {
                string script = "alert(\"Error, El kilometraje de entrada no puede ser menor que al kilometraje de salida.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            DataTable dt = (DataTable)ViewState["Curtbl"];
            int rowIndex = 0;
            List<string> _listCliente = new List<string>();
            List<string> _listFactura = new List<string>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList drpListAux = (DropDownList)dgvDetailMod.Rows[rowIndex].Cells[1].FindControl("listClienteMod");
                    TextBox txtprice = (TextBox)dgvDetailMod.Rows[rowIndex].Cells[2].FindControl("txtFacturaMod");

                    if (Convert.ToInt32(drpListAux.SelectedItem.Value) == 0)
                    {
                        string script = "alert(\"Error, Es necesario seleccionar el cliente.\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        return false;
                    }

                    if (txtprice.Text.Trim() == string.Empty || txtprice.Text.Trim() == "")
                    {
                        string script = "alert(\"Error, Es necesario ingresar el codigo de factura del cliente.\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        return false;
                    }

                    _listCliente.Add(drpListAux.SelectedItem.Text);
                    _listFactura.Add(txtprice.Text.Trim());

                    rowIndex++;
                }
            }

            var _varAuxCliente = _listCliente.GroupBy(x => x).Count(x => x.Count() > 1);
            var _varAuxFactura = _listFactura.GroupBy(y => y).Count(y => y.Count() > 1);

            if (Convert.ToInt32(_varAuxCliente.ToString()) >= 1)
            {
                string script = "alert(\"Error, No se puede seleccionar un mismo cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (Convert.ToInt32(_varAuxFactura.ToString()) >= 1)
            {
                string script = "alert(\"Error, No se puede ingresar la misma factura a distintos cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }

    }
}
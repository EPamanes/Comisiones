using Calculo_Comisiones_Operadores.Object;
using Calculo_Comisiones_Operadores.ObjectSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculo_Comisiones_Operadores
{
    public partial class ClienteAdmin : System.Web.UI.Page
    {      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Session["scco_user"].ToString() == "" || Session["scco_user"].ToString() == null) || Session["scco_tipo"].ToString() != "Administrador")
                    Response.Redirect("Default.aspx");
            }
            DataLoadDGV();
        }

        private void DataLoadDGV()
        {
            DataTable _dtTable = ClienteSQL.SelectCliente();

            _dtTable.Columns.Add("scco_nameColor", typeof(System.String));

            foreach (DataRow _dtRow in _dtTable.Rows)
            {
                _dtRow["scco_nameColor"] = NameColor(Convert.ToInt32(_dtRow["scco_color_id"].ToString()));
            }

            dgvCliente.DataSource = _dtTable;
            dgvCliente.DataBind();
        }

        private string NameColor(int intColor)
        {
            string _strnameColor = string.Empty;

            Color _objColor = ColorSQL.SelectColorObject(intColor);
            _strnameColor = _objColor.strName;

            return _strnameColor;
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSearch.Text.Trim() != "" || txtSearch.Text.Trim() != string.Empty)
            {
                DataTable _dtSearch = ClienteSQL.SelectClienteSearch(txtSearch.Text.Trim());

                _dtSearch.Columns.Add("scco_nameColor", typeof(System.String));

                foreach (DataRow _dtRow in _dtSearch.Rows)
                {
                    _dtRow["scco_nameColor"] = NameColor(Convert.ToInt32(_dtRow["scco_color_id"].ToString()));
                }

                dgvCliente.DataSource = _dtSearch;
                dgvCliente.DataBind();
            }
            else
            {
                //DataTable _dtSearch = ClienteSQL.SelectCliente();
                //dgvCliente.DataSource = _dtSearch;
                //dgvCliente.DataBind();

                DataLoadDGV();
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            ClearInsert();

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#addModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AddShowModalScript", _strBuilder.ToString(), false);
        }

        protected void dgvColor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCliente.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void dgvCliente_RowCommand(object sender, GridViewCommandEventArgs e)
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

            if (e.CommandName.Equals("viewRecord"))
            {
                ViewRecord(_intIndex);
            }
        }

        private void DeleteRecord(int intIndex)
        {
            deleteID.Value = dgvCliente.DataKeys[intIndex].Value.ToString();
            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void UpdateRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvCliente.DataKeys[intIndex].Value.ToString());
            Cliente _objCliente = ClienteSQL.SelectClienteObject(_intID);

            txtIdMod.Value = _intID.ToString();
            txtNameMod.Text = _objCliente.strName;
            ListTipo(_objCliente.strTipo);
            ListColor(_objCliente.intColorId);
            ckbStatusMod.Checked = (_objCliente.intStatus == 1) ? true : false;
            ckbRule1Mod.Checked = (_objCliente.intRule1 == 1) ? true : false;
            ckbRule2Mod.Checked = (_objCliente.intRule2 == 1) ? true : false;

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#editModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EditModalScript", _strBuilder.ToString(), false);
        }

        private void ViewRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvCliente.DataKeys[intIndex].Value.ToString());
            DataTable _dtCliente = ClienteSQL.SelectCliente(_intID);

            _dtCliente.Columns.Add("scco_nameColor", typeof(System.String));
            _dtCliente.Columns.Add("scco_statusString", typeof(System.String));

            foreach (DataRow _dtRow in _dtCliente.Rows)
            {
                _dtRow["scco_nameColor"] = NameColor(Convert.ToInt32(_dtRow["scco_color_id"].ToString()));
                _dtRow["scco_statusString"] = (Convert.ToInt32(_dtRow["scco_status"].ToString()) == 1) ? "Habilidato" : "Des-habilitado";
            }

            detailsView.DataSource = _dtCliente;
            detailsView.DataBind();

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#viewModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewModalScript", _strBuilder.ToString(), false);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInsert() == true)
            {
                Cliente _objCliente = new Cliente();
                //_objUser.intID = Convert.ToInt32(txtIdMod.Value);
                _objCliente.strName = txtNameAdd.Text;
                _objCliente.intStatus = (ckbStatusAdd.Checked == true) ? 1 : 0;
                _objCliente.intColorId = Convert.ToInt32(listColorAdd.SelectedValue);
                _objCliente.strTipo = listTipoAdd.SelectedItem.Text;
                _objCliente.intRule1 = (ckbRule1Add.Checked == true) ? 1 : 0;
                _objCliente.intRule2 = (ckbRule2Add.Checked == true) ? 1 : 0;

                ClienteSQL.InsertCliente(_objCliente);

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
                _strBuilder.Append(@"<script type='text/javascript'>");
                _strBuilder.Append("$('#addModal').modal('hide');");
                _strBuilder.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", _strBuilder.ToString(), false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate() == true)
            {
                Cliente _objCliente = new Cliente();
                _objCliente.intId = Convert.ToInt32(txtIdMod.Value);
                _objCliente.strName = txtNameMod.Text.Trim();
                _objCliente.intStatus = (ckbStatusMod.Checked == true) ? 1 : 0;
                _objCliente.intColorId = Convert.ToInt32(listColorMod.SelectedValue);
                _objCliente.strTipo = listTipoMod.SelectedItem.Text;
                _objCliente.intRule1 = (ckbRule1Mod.Checked == true) ? 1 : 0;
                _objCliente.intRule2 = (ckbRule2Mod.Checked == true) ? 1 : 0;

                ClienteSQL.UpdateCliente(_objCliente);

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
                _strBuilder.Append(@"<script type='text/javascript'>");
                _strBuilder.Append("$('#editModal').modal('hide');");
                _strBuilder.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", _strBuilder.ToString(), false);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClienteSQL.DeleteCliente(deleteID.Value);

            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('hide');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void ClearInsert()
        {
            txtIdAdd.Value = string.Empty;
            txtNameAdd.Text = string.Empty;
            ckbStatusAdd.Checked = false;
            ckbRule1Add.Checked = false;
            ckbRule2Add.Checked = false;
            ListTipo();
            ListColor();
        }

        private void ListTipo()
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Preferencial", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Normal", Value = 2 });

            listTipoAdd.DataSource = _listTipo;
            listTipoAdd.DataTextField = "Text";
            listTipoAdd.DataValueField = "Value";
            listTipoAdd.DataBind();
            listTipoAdd.SelectedValue = "0";
        }

        private void ListTipo(string strTipo)
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Preferencial", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Normal", Value = 2 });

            listTipoMod.DataSource = _listTipo;
            listTipoMod.DataTextField = "Text";
            listTipoMod.DataValueField = "Value";
            listTipoMod.DataBind();
            listTipoMod.SelectedValue = (strTipo == "Preferencial") ? "1" : "2";
        }

        private void ListColor()
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });

            DataTable _dtTable = ColorSQL.SelectColor();

            foreach (DataRow _dtRow in _dtTable.Rows)
            {
                _listTipo.Add(new ComboBoxItem() { Text = _dtRow["scco_name"].ToString(), Value = Convert.ToInt32(_dtRow["scco_id"].ToString()) });
            }

            listColorAdd.DataSource = _listTipo;
            listColorAdd.DataTextField = "Text";
            listColorAdd.DataValueField = "Value";
            listColorAdd.DataBind();
            listColorAdd.SelectedValue = "0";
        }

        private void ListColor(int intColorId)
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });

            DataTable _dtTable = ColorSQL.SelectColor();

            foreach (DataRow _dtRow in _dtTable.Rows)
            {
                _listTipo.Add(new ComboBoxItem() { Text = _dtRow["scco_name"].ToString(), Value = Convert.ToInt32(_dtRow["scco_id"].ToString()) });
            }

            listColorMod.DataSource = _listTipo;
            listColorMod.DataTextField = "Text";
            listColorMod.DataValueField = "Value";
            listColorMod.DataBind();
            listColorMod.SelectedValue = intColorId.ToString();
        }

        private bool ValidateInsert()
        {
            if (txtNameAdd.Text.Trim() == "" || txtNameAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre del cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listTipoAdd.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el tipo de cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listColorAdd.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el color de cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }

        private bool ValidateUpdate()
        {
            if (txtNameMod.Text.Trim() == "" || txtNameMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre del operador.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listTipoMod.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el tipo de cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listColorMod.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el color del cliente.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }
    }
}
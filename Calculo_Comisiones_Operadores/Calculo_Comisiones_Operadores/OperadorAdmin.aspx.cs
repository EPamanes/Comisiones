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
    public partial class OperadorAdmin : System.Web.UI.Page
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
            DataTable _dtTable = OperadorSQL.SelectOperador();
            _dtTable.Columns.Add("scco_namelastname", typeof(System.String));

            foreach (DataRow _dtRow in _dtTable.Rows)
            {
                _dtRow["scco_namelastname"] = _dtRow["scco_name"].ToString() + " " + _dtRow["scco_lastname"].ToString() + " " + _dtRow["scco_mlastname"].ToString();
            }

            dgvOperador.DataSource = _dtTable;
            dgvOperador.DataBind();
        }

        protected void dgvOperador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvOperador.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void dgvOperador_RowCommand(object sender, GridViewCommandEventArgs e)
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
            deleteID.Value = dgvOperador.DataKeys[intIndex].Value.ToString();
            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void UpdateRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvOperador.DataKeys[intIndex].Value.ToString());
            Operador _objOperador = OperadorSQL.SelectOperadorObject(_intID);

            txtIdMod.Value = _intID.ToString();
            txtNameMod.Text = _objOperador.strName;
            txtLastNameMod.Text = _objOperador.strLastName;
            txtMlastNameMod.Text = _objOperador.strMlastName;
            txtBirthDateMod.Text = _objOperador.dateBirthdate.ToString("yyyy-MM-dd");
            txtAgeMod.Text = _objOperador.intAge.ToString();
            ckbStatusMod.Checked = (_objOperador.intStatus == 1) ? true : false;
            ListTipo(_objOperador.strSexo);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#editModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EditModalScript", _strBuilder.ToString(), false);
        }

        private void ListTipo(string strTipo)
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Masculino", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Femenino", Value = 2 });

            listSexMod.DataSource = _listTipo;
            listSexMod.DataTextField = "Text";
            listSexMod.DataValueField = "Value";
            listSexMod.DataBind();
            listSexMod.SelectedValue = (strTipo == "Masculino") ? "1" : "2";
        }

        private void ViewRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvOperador.DataKeys[intIndex].Value.ToString());
            DataTable _dtOperador = OperadorSQL.SelectOperador(_intID);
            _dtOperador.Columns.Add("scco_lblstatus", typeof(System.String));

            foreach (DataRow _dtRow in _dtOperador.Rows)
            {
                _dtRow["scco_lblstatus"] = (_dtRow["scco_status"].ToString() == "1") ? "Activo" : "Inactivo";
            }

            detailsView.DataSource = _dtOperador;
            detailsView.DataBind();

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#viewModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewModalScript", _strBuilder.ToString(), false);
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSearch.Text.Trim() != "" || txtSearch.Text.Trim() != string.Empty)
            {
                DataTable _dtSearch = OperadorSQL.SelectOperadorSearch(txtSearch.Text.Trim());
                _dtSearch.Columns.Add("scco_namelastname", typeof(System.String));

                foreach (DataRow _dtRow in _dtSearch.Rows)
                {
                    _dtRow["scco_namelastname"] = _dtRow["scco_name"].ToString() + " " + _dtRow["scco_lastname"].ToString() + " " + _dtRow["scco_mlastname"].ToString();
                }

                dgvOperador.DataSource = _dtSearch;
                dgvOperador.DataBind();
            }
            else
            {
                DataTable _dtSearch = OperadorSQL.SelectOperador();
                _dtSearch.Columns.Add("scco_namelastname", typeof(System.String));

                foreach (DataRow _dtRow in _dtSearch.Rows)
                {
                    _dtRow["scco_namelastname"] = _dtRow["scco_name"].ToString() + " " + _dtRow["scco_lastname"].ToString() + " " + _dtRow["scco_mlastname"].ToString();
                }

                dgvOperador.DataSource = _dtSearch;
                dgvOperador.DataBind();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInsert() == true)
            {
                Operador _objOperador = new Operador();
                //_objUser.intID = Convert.ToInt32(txtIdMod.Value);
                _objOperador.strName = txtNameAdd.Text.Trim();
                _objOperador.strLastName = txtLastNameAdd.Text;
                _objOperador.strMlastName = txtMlastNameAdd.Text;
                _objOperador.dateBirthdate = Convert.ToDateTime(txtBirthDateAdd.Text);
                _objOperador.strSexo = listSexAdd.SelectedItem.Text;
                _objOperador.intStatus = (ckbStatusAdd.Checked == true) ? 1 : 0;
                _objOperador.intAge = Convert.ToInt32(txtAgeAdd.Text);

                OperadorSQL.InsertOperador(_objOperador);

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
                Operador _objOperador = new Operador();
                _objOperador.intID = Convert.ToInt32(txtIdMod.Value);
                _objOperador.strName = txtNameMod.Text.Trim();
                _objOperador.strLastName = txtLastNameMod.Text;
                _objOperador.strMlastName = txtMlastNameMod.Text;
                _objOperador.dateBirthdate = Convert.ToDateTime(txtBirthDateMod.Text);
                _objOperador.strSexo = listSexMod.SelectedItem.Text;
                _objOperador.intStatus = (ckbStatusMod.Checked == true) ? 1 : 0;
                _objOperador.intAge = Convert.ToInt32(txtAgeMod.Text);

                OperadorSQL.UpdateOperador(_objOperador);

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
            OperadorSQL.DeleteOperador(deleteID.Value);

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
            txtLastNameAdd.Text = string.Empty;
            txtMlastNameAdd.Text = string.Empty;
            txtAgeAdd.Text = string.Empty;
            txtBirthDateAdd.Text = string.Empty;
            ckbStatusAdd.Checked = false;
            ListTipo();
        }

        private void ListTipo()
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Masculino", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Femenino", Value = 2 });

            listSexAdd.DataSource = _listTipo;
            listSexAdd.DataTextField = "Text";
            listSexAdd.DataValueField = "Value";
            listSexAdd.DataBind();
            listSexAdd.SelectedValue = "0";
        }
        private bool ValidateInsert()
        {
            if (txtNameAdd.Text.Trim() == "" || txtNameAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre del operador.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtLastNameAdd.Text.Trim() == "" || txtLastNameAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el apellido paterno del operador.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listSexAdd.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el tipo de genero del operador.\");";
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

            if (txtLastNameMod.Text.Trim() == "" || txtLastNameMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el apellido paterno del operador.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listSexMod.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el genero del operador.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }
    }
}
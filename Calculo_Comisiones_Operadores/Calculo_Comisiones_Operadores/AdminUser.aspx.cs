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
    public partial class AdminUser : System.Web.UI.Page
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
            DataTable _dtTable = UserSQL.SelectUser();
            _dtTable.Columns.Add("scco_namelastname", typeof(System.String));

            foreach (DataRow _dtRow in _dtTable.Rows)
            {
                _dtRow["scco_namelastname"] = _dtRow["scco_name"].ToString() + " " + _dtRow["scco_lastname"].ToString() + " " + _dtRow["scco_mlastname"].ToString();
            }

            dgvUser.DataSource = _dtTable;
            dgvUser.DataBind();
        }

        protected void dgvUser_RowCommand(object sender, GridViewCommandEventArgs e)
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
            deleteID.Value = dgvUser.DataKeys[intIndex].Value.ToString();
            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void UpdateRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvUser.DataKeys[intIndex].Value.ToString());
            User _objUser = UserSQL.SelectUserObject(_intID);

            txtIdMod.Value = _intID.ToString();
            txtNameMod.Text = _objUser.strName;
            txtLastNameMod.Text = _objUser.strLastName;
            txtMlastNameMod.Text = _objUser.strMlastName;
            txtUserMod.Text = _objUser.strUser;
            txtPasswordMod.Attributes.Add("Value", _objUser.strPassword);
            ckbStatusMod.Checked = (_objUser.intStatus == 1) ? true : false;
            ListTipo(_objUser.strTipo);

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
            _listTipo.Add(new ComboBoxItem() { Text = "Administrador", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Usuario", Value = 2 });

            listTipoMod.DataSource = _listTipo;
            listTipoMod.DataTextField = "Text";
            listTipoMod.DataValueField = "Value";
            listTipoMod.DataBind();
            listTipoMod.SelectedValue = (strTipo == "Administrador") ? "1" : "2";
        }

        private void ViewRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvUser.DataKeys[intIndex].Value.ToString());
            DataTable _dtUser = UserSQL.SelectUser(_intID);
            _dtUser.Columns.Add("scco_lblstatus", typeof(System.String));

            foreach (DataRow _dtRow in _dtUser.Rows)
            {
                _dtRow["scco_lblstatus"] = (_dtRow["scco_status"].ToString() == "1") ? "Activo" : "Inactivo";
            }

            detailsView.DataSource = _dtUser;
            detailsView.DataBind();

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#viewModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewModalScript", _strBuilder.ToString(), false);
        }

        protected void dgvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUser.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if(txtSearch.Text.Trim() != "" || txtSearch.Text.Trim() != string.Empty)
            {
                DataTable _dtSearch = UserSQL.SelectUserSearch(txtSearch.Text.Trim());
                _dtSearch.Columns.Add("scco_namelastname", typeof(System.String));

                foreach (DataRow _dtRow in _dtSearch.Rows)
                {
                    _dtRow["scco_namelastname"] = _dtRow["scco_name"].ToString() + " " + _dtRow["scco_lastname"].ToString() + " " + _dtRow["scco_mlastname"].ToString();
                }

                dgvUser.DataSource = _dtSearch;
                dgvUser.DataBind();
            }
            else
            {
                DataTable _dtSearch = UserSQL.SelectUser();
                _dtSearch.Columns.Add("scco_namelastname", typeof(System.String));

                foreach (DataRow _dtRow in _dtSearch.Rows)
                {
                    _dtRow["scco_namelastname"] = _dtRow["scco_name"].ToString() + " " + _dtRow["scco_lastname"].ToString() + " " + _dtRow["scco_mlastname"].ToString();
                }

                dgvUser.DataSource = _dtSearch;
                dgvUser.DataBind();
            }
        }              

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate() == true)
            {
                User _objUser = new User();
                _objUser.intID = Convert.ToInt32(txtIdMod.Value);
                _objUser.strName = txtNameMod.Text.Trim();
                _objUser.strLastName = txtLastNameMod.Text;
                _objUser.strMlastName = txtMlastNameMod.Text;
                _objUser.strUser = txtUserMod.Text.Trim();
                _objUser.strTipo = listTipoMod.SelectedItem.Text;
                _objUser.intStatus = (ckbStatusMod.Checked == true) ? 1 : 0;
                _objUser.strPassword = txtPasswordMod.Text;

                UserSQL.UpdateUser(_objUser);

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
            UserSQL.DeleteUser(deleteID.Value);

            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('hide');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
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
                User _objUser = new User();
                //_objUser.intID = Convert.ToInt32(txtIdMod.Value);
                _objUser.strName = txtNameAdd.Text.Trim();
                _objUser.strLastName = txtLastNameAdd.Text;
                _objUser.strMlastName = txtMlastNameAdd.Text;
                _objUser.strUser = txtUserAdd.Text.Trim();
                _objUser.strTipo = listTipoAdd.SelectedItem.Text;
                _objUser.intStatus = (ckbStatusAdd.Checked == true) ? 1 : 0;
                _objUser.strPassword = txtPasswordAdd.Text;

                UserSQL.InsertUser(_objUser);

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
                _strBuilder.Append(@"<script type='text/javascript'>");
                _strBuilder.Append("$('#addModal').modal('hide');");
                _strBuilder.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", _strBuilder.ToString(), false);
            }
        }

        private void ClearInsert()
        {
            txtIdAdd.Value = string.Empty;
            txtNameAdd.Text = string.Empty;
            txtLastNameAdd.Text = string.Empty;
            txtMlastNameAdd.Text = string.Empty;
            txtUserAdd.Text = string.Empty;
            txtPasswordAdd.Text = string.Empty;
            ckbStatusAdd.Checked = false;
            ListTipo();
        }

        private void ListTipo()
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Administrador", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Usuario", Value = 2 });

            listTipoAdd.DataSource = _listTipo;
            listTipoAdd.DataTextField = "Text";
            listTipoAdd.DataValueField = "Value";
            listTipoAdd.DataBind();
            listTipoAdd.SelectedValue = "0";
        }

        private bool ValidateUpdate()
        {
            if (txtNameMod.Text.Trim() == "" || txtNameMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtLastNameMod.Text.Trim() == "" || txtLastNameMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el apellido paterno del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtUserMod.Text.Trim() == "" || txtUserMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el alias del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtPasswordMod.Text.Trim() == "" || txtPasswordMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el password del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listTipoMod.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el tipo de usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }

        private bool ValidateInsert()
        {
            if (txtNameAdd.Text.Trim() == "" || txtNameAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtLastNameAdd.Text.Trim() == "" || txtLastNameAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el apellido paterno del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtUserAdd.Text.Trim() == "" || txtUserAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el alias del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtPasswordAdd.Text.Trim() == "" || txtPasswordAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el password del usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listTipoAdd.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar el tipo de usuario.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }

        protected void ckbPasswordMod_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPasswordMod.Checked == true)
                txtPasswordMod.TextMode = TextBoxMode.SingleLine;
            else
                txtPasswordMod.TextMode = TextBoxMode.Password;
        }

        protected void ckbPasswordAdd_CheckedChanged(object sender, EventArgs e)
        {
            string _strPassword = txtPasswordAdd.Text;

            if (ckbPasswordAdd.Checked == true)
            {
                txtPasswordAdd.Attributes.Add("Value", _strPassword);
                txtPasswordAdd.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                txtPasswordAdd.Attributes.Add("Value", _strPassword);
                txtPasswordAdd.TextMode = TextBoxMode.Password;
            }
        }
    }
}
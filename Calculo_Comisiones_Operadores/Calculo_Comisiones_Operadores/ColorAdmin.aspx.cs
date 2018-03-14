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
    public partial class ColorAdmin : System.Web.UI.Page
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
            DataTable _dtTable = ColorSQL.SelectColor();

            dgvColor.DataSource = _dtTable;
            dgvColor.DataBind();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSearch.Text.Trim() != "" || txtSearch.Text.Trim() != string.Empty)
            {
                DataTable _dtSearch = ColorSQL.SelectColorSearch(txtSearch.Text.Trim());

                dgvColor.DataSource = _dtSearch;
                dgvColor.DataBind();
            }
            else
            {
                DataTable _dtSearch = ColorSQL.SelectColor();

                dgvColor.DataSource = _dtSearch;
                dgvColor.DataBind();
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
            dgvColor.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void dgvColor_RowCommand(object sender, GridViewCommandEventArgs e)
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
            deleteID.Value = dgvColor.DataKeys[intIndex].Value.ToString();
            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void UpdateRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvColor.DataKeys[intIndex].Value.ToString());
            Color _objColor = ColorSQL.SelectColorObject(_intID);

            txtIdMod.Value = _intID.ToString();
            txtNameMod.Text = _objColor.strName;
            txtHoraMod.Text = _objColor.timeHora.ToString();
            txtTarifaMod.Text = _objColor.decTarfica.ToString();
            ListTipo(_objColor.strPrioridad);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#editModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EditModalScript", _strBuilder.ToString(), false);
        }

        private void ViewRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvColor.DataKeys[intIndex].Value.ToString());
            DataTable _dtColor = ColorSQL.SelectColor(_intID);

            detailsView.DataSource = _dtColor;
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
                Color _objColor = new Color();
                //_objUser.intID = Convert.ToInt32(txtIdMod.Value);
                _objColor.strName = txtNameAdd.Text.Trim();
                _objColor.timeHora = TimeSpan.Parse(txtHoraAdd.Text);
                _objColor.decTarfica = Convert.ToDecimal(txtTarifaAdd.Text.Trim().Replace(",", ""));
                _objColor.strPrioridad = listPrioridadAdd.SelectedItem.Text;

                ColorSQL.InsertColor(_objColor);

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
                Color _objColor = new Color();
                _objColor.intID = Convert.ToInt32(txtIdMod.Value);
                _objColor.strName = txtNameMod.Text.Trim();
                _objColor.strPrioridad = listPrioridadMod.SelectedItem.Text;
                _objColor.timeHora = TimeSpan.Parse(txtHoraMod.Text);
                _objColor.decTarfica = Convert.ToDecimal(txtTarifaMod.Text);

                ColorSQL.UpdateColor(_objColor);

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
            ColorSQL.DeleteColor(deleteID.Value);

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
            txtHoraAdd.Text = string.Empty;
            txtTarifaAdd.Text = string.Empty;
            ListTipo();
        }

        private void ListTipo()
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Alta", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Media", Value = 2 });
            _listTipo.Add(new ComboBoxItem() { Text = "Baja", Value = 3 });

            listPrioridadAdd.DataSource = _listTipo;
            listPrioridadAdd.DataTextField = "Text";
            listPrioridadAdd.DataValueField = "Value";
            listPrioridadAdd.DataBind();
            listPrioridadAdd.SelectedValue = "0";
        }

        private void ListTipo(string strTipo)
        {
            List<ComboBoxItem> _listTipo = new List<ComboBoxItem>();
            _listTipo.Add(new ComboBoxItem() { Text = "-- Selecciona --", Value = 0 });
            _listTipo.Add(new ComboBoxItem() { Text = "Alta", Value = 1 });
            _listTipo.Add(new ComboBoxItem() { Text = "Media", Value = 2 });
            _listTipo.Add(new ComboBoxItem() { Text = "Baja", Value = 3 });

            listPrioridadMod.DataSource = _listTipo;
            listPrioridadMod.DataTextField = "Text";
            listPrioridadMod.DataValueField = "Value";
            listPrioridadMod.DataBind();
            listPrioridadMod.SelectedValue = (strTipo == "Alta") ? "1" : (strTipo == "Media") ? "2" : "3";
        }

        private bool ValidateInsert()
        {
            if (txtNameAdd.Text.Trim() == "" || txtNameAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre del color.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (txtHoraAdd.Text.Trim() == "" || txtHoraAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la hora estimada.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }
            else
            {
                if (ValidateTimeAdd() == false)
                {
                    string script = "alert(\"Error, Verificar los valores de la hora ingresada.\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    return false;
                }
            }

            if (txtTarifaAdd.Text.Trim() == "" || txtTarifaAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la tarifa del color.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }
            

            if (listPrioridadAdd.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar la prioridad del color.\");";
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

            if (txtHoraMod.Text.Trim() == "" || txtHoraMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la hora estimada del viaje.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }
            else
            {
                if(ValidateTimeMod() == false)
                {
                    string script = "alert(\"Error, Verificar los valores de la hora ingresada.\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    return false;
                }
            }

            if (txtTarifaMod.Text.Trim() == "" || txtTarifaMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar la tarifa del color.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            if (listPrioridadMod.SelectedValue == "0")
            {
                string script = "alert(\"Error, Es necesario seleccionar la prioridad del color.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }

        private bool ValidateTimeAdd()
        {
            bool _flagH = false;
            bool _flagM = false;
            bool _flagS = false;
            bool _flag = false;

            try
            {
                TimeSpan _timeAux = TimeSpan.Parse(txtHoraAdd.Text);

                if (_timeAux.Hours >= 0 || _timeAux.Hours <= 23)
                    _flagH = true;
                if (_timeAux.Minutes >= 0 || _timeAux.Minutes <= 59)
                    _flagM = true;
                if (_timeAux.Seconds >= 0 || _timeAux.Seconds <= 59)
                    _flagS = true;

                if (_flagH == true && _flagM == true && _flagS == true)
                    _flag = true;
            }
            catch
            {
                _flag = false;
            }                              

            return _flag;
        }

        private bool ValidateTimeMod()
        {
            bool _flagH = false;
            bool _flagM = false;
            bool _flagS = false;
            bool _flag = false;

            try
            {
                TimeSpan _timeAux = TimeSpan.Parse(txtHoraMod.Text);

                if (_timeAux.Hours >= 0 || _timeAux.Hours <= 23)
                    _flagH = true;
                if (_timeAux.Minutes >= 0 || _timeAux.Minutes <= 59)
                    _flagM = true;
                if (_timeAux.Seconds >= 0 || _timeAux.Seconds <= 59)
                    _flagS = true;

                if (_flagH == true && _flagM == true && _flagS == true)
                    _flag = true;
            }
            catch
            {
                _flag = false;
            }

            return _flag;
        }
    }
}
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
    public partial class UnidadAdmin : System.Web.UI.Page
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
            DataTable _dtTable = UnidadSQL.SelectUnidad();

            dgvUnidad.DataSource = _dtTable;
            dgvUnidad.DataBind();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSearch.Text.Trim() != "" || txtSearch.Text.Trim() != string.Empty)
            {
                DataTable _dtSearch = UnidadSQL.SelectUnidadSearch(txtSearch.Text.Trim());
                
                dgvUnidad.DataSource = _dtSearch;
                dgvUnidad.DataBind();
            }
            else
            {
                DataTable _dtSearch = UnidadSQL.SelectUnidad();
                
                dgvUnidad.DataSource = _dtSearch;
                dgvUnidad.DataBind();
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

        private void ClearInsert()
        {
            txtIdAdd.Value = string.Empty;
            txtNameAdd.Text = string.Empty;
            txtModeloAdd.Text = string.Empty;
            txtPlacaAdd.Text = string.Empty;;
            ckbStatusAdd.Checked = false;
        }

        protected void dgvUnidad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUnidad.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void dgvUnidad_RowCommand(object sender, GridViewCommandEventArgs e)
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
            deleteID.Value = dgvUnidad.DataKeys[intIndex].Value.ToString();
            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }

        private void UpdateRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvUnidad.DataKeys[intIndex].Value.ToString());
            Unidad _objUnidad = UnidadSQL.SelectUnidadObject(_intID);

            txtIdMod.Value = _intID.ToString();
            txtNameMod.Text = _objUnidad.strName;
            txtModeloMod.Text = _objUnidad.strModelo;
            txtPlacaMod.Text = _objUnidad.strPlaca;
            ckbStatusMod.Checked = (_objUnidad.intStatus == 1) ? true : false;

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#editModal').modal('show');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EditModalScript", _strBuilder.ToString(), false);
        }

        private void ViewRecord(int intIndex)
        {
            int _intID = Convert.ToInt32(dgvUnidad.DataKeys[intIndex].Value.ToString());
            DataTable _dtUnidad = UnidadSQL.SelectUnidad(_intID);
            _dtUnidad.Columns.Add("scco_lblstatus", typeof(System.String));

            foreach (DataRow _dtRow in _dtUnidad.Rows)
            {
                _dtRow["scco_lblstatus"] = (_dtRow["scco_status"].ToString() == "1") ? "Activo" : "Inactivo";
            }

            detailsView.DataSource = _dtUnidad;
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
                Unidad _objUnidad = new Unidad();
                //_objUser.intID = Convert.ToInt32(txtIdMod.Value);
                _objUnidad.strName = txtNameAdd.Text.Trim();
                _objUnidad.strModelo = txtModeloAdd.Text;
                _objUnidad.strPlaca = txtPlacaAdd.Text;
                _objUnidad.intStatus = (ckbStatusAdd.Checked == true) ? 1 : 0;

                UnidadSQL.InsertUnidad(_objUnidad);

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
            if (txtNameAdd.Text.Trim() == "" || txtNameAdd.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre de la unidad.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }                       

            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate() == true)
            {
                Unidad _objUnidad = new Unidad();
                _objUnidad.intID = Convert.ToInt32(txtIdMod.Value);
                _objUnidad.strName = txtNameMod.Text.Trim();
                _objUnidad.strModelo = txtModeloMod.Text;
                _objUnidad.strPlaca = txtPlacaMod.Text;
                _objUnidad.intStatus = (ckbStatusMod.Checked == true) ? 1 : 0;

                UnidadSQL.UpdateUnidad(_objUnidad);

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
            if (txtNameMod.Text.Trim() == "" || txtNameMod.Text.Trim() == string.Empty)
            {
                string script = "alert(\"Error, Es necesario ingresar el nombre de la unidad.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return false;
            }

            return true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UnidadSQL.DeleteUnidad(deleteID.Value);

            Page_Load(sender, e);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            System.Text.StringBuilder _strBuilder = new System.Text.StringBuilder();
            _strBuilder.Append(@"<script type='text/javascript'>");
            _strBuilder.Append("$('#deleteModal').modal('hide');");
            _strBuilder.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", _strBuilder.ToString(), false);
        }
    }
}
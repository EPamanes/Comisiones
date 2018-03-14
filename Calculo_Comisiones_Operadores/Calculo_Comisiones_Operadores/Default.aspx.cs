using Calculo_Comisiones_Operadores.Object;
using Calculo_Comisiones_Operadores.ObjectSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculo_Comisiones_Operadores
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<User> _listUser = UserSQL.SelectUserActive(txtUsername.Text, txtPassword.Text);
            //Seguridad
            if (_listUser.Count == 1)
            {
                foreach (User _objUser in _listUser)
                {
                    Session["scco_user"] = _objUser.strUser;
                    Session["scco_tipo"] = _objUser.strTipo;
                }

                if (Session["scco_tipo"].ToString() == "Administrador")
                    Response.Redirect("ComisionesReport.aspx");
                else
                    Response.Redirect("TravelUser.aspx");
            }
            else
            {
                string script = "alert(\"Error, Usuario o Password incorrecto, intente de nuevo por favor.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                errorLabel.Text = "Error - Usuario|Password";
            }
        }


    }
}
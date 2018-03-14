using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculo_Comisiones_Operadores
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            string _strUser = (string)Session["scco_user"];
            string _strTipo = (string)Session["scco_tipo"];

            if (!IsPostBack)
                if (_strUser == "" || _strUser == null)
                    Response.Redirect("Default.aspx");

            lk_user.Text = "Sesión (" + Session["scco_user"].ToString() + ")";

            if (_strTipo == "Administrador")
            {
                nb_UserTravel.Visible = false;
                nb_Comision.Visible = true;
                li_AdminTravel.Visible = true;
                li_AdminCliente.Visible = true;
                li_AdminUser.Visible = true;
                li_AdminOperador.Visible = true;
                li_AdminUnidad.Visible = true;
                li_AdminColor.Visible = true;
                li_About.Visible = false;
                li_Contact.Visible = false;
            }
            if (_strTipo == "Usuario")
            {
                nb_UserTravel.Visible = true;
                nb_Comision.Visible = false;
                li_AdminTravel.Visible = false;
                li_AdminCliente.Visible = false;
                li_AdminColor.Visible = false;
                li_AdminUser.Visible = false;
                li_AdminOperador.Visible = false;
                li_AdminUnidad.Visible = false;
                li_About.Visible = false;
                li_Contact.Visible = false;
            }

        }

        protected void lk_user_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}
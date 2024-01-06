using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace tp_final_nivel_3
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                Usuario user = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;
                user.Id = usuarioNegocio.InsertarNuevo(user);
                Session.Add("usuario", user);

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}
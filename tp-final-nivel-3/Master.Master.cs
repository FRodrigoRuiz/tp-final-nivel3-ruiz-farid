using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace tp_final_nivel_3
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://thumbs.dreamstime.com/b/%C3%ADcone-do-consultor-ponto-de-interroga%C3%A7%C3%A3o-vetorial-com-o-s%C3%ADmbolo-avatar-perfil-pessoa-usu%C3%A1rio-masculino-para-ajudar-assinar-168495483.jpg";

            //Hago una doble verificacion en las pantallas de Default y Detalle, para que cuando inicie sesion, ingrese al if y me cargue la foto de perfil en la barra superior y no me quede la imagen por defecto.
            if (!(Page is Login || Page is Registro || Page is Default && !Seguridad.sesionActiva(Session["usuario"]) || Page is DetalleArticulo && !Seguridad.sesionActiva(Session["usuario"])))
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                    Response.Redirect("Login.aspx", false);
                else
                {
                    Usuario user = (Usuario)Session["usuario"];
                    if(!string.IsNullOrEmpty(user.UrlImagenPerfil))
                    {
                        imgAvatar.ImageUrl = "~/Images/" + user.UrlImagenPerfil;
                    }
                }
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
using NominaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace NominaAPI.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        //private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();
        private bool IsAuthorizedUser(string username, string password)
        {
            using (var db = new Proyecto_Fin_Hibrido2Entities1())
            {
                return db.Usuario.Any(l => l.UserName.Equals(username) && l.Password.Equals(password));
            }


            // In this method we can handle our database logic here...  
            //Buscar usuario y contraseña en base de datos
        }
        //This method is used to return the User Details

        //Este deberá de ser sustituido para que devuelva el usuario
        private Usuario GetUsuario(string username, string password)
        {
            using (var db = new Proyecto_Fin_Hibrido2Entities1())
            {
                return db.Usuario.FirstOrDefault(l => l.UserName.Equals(username) && l.Password.Equals(password));
            }

        }

        //Modelo para sustituir la función autorización
        //public static Usuario GetUsuario(string username, string password)
        //{
        //    DB db = new DB();
        //    return db.Usuario.FirstOrDefault(user =>
        //        user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
        //        && user.Password == password);
        //}
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    base.OnAuthorization(actionContext);
        //}
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            bool intro = actionContext.Request.RequestUri.LocalPath == "/$metadata" || actionContext.Request.RequestUri.LocalPath == "/";
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;

                // decoding authToken we get decode value in 'Username:Password' format  
                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));
                if (authToken.Equals("null"))
                {
                    return;
                }
                // spliting decodeauthToken using ':'   
                var arrUserNameandPassword = decodeauthToken.Split(':');
                string username = arrUserNameandPassword[0];
                string password = arrUserNameandPassword[1];
                // at 0th postion of array we get username and at 1st we get password  
                if (IsAuthorizedUser(username, password) || actionContext.Request.RequestUri.LocalPath == "/$metadata" || actionContext.Request.RequestUri.LocalPath == "/")
                {
                    //Este será tipo usuario
                    var UserDetails = GetUsuario(username, password);
                    var identity = new GenericIdentity(username);
                    //Estos se usará para con los datos del usuario
                    identity.AddClaim(new Claim("Nombre", UserDetails.Nombre));
                    identity.AddClaim(new Claim("Apellido", UserDetails.Apellido));
                    identity.AddClaim(new Claim("Email", UserDetails.Email));
                    identity.AddClaim(new Claim("Usuario", UserDetails.UserName));
                    identity.AddClaim(new Claim("id", Convert.ToString(UserDetails.id)));
                    //IPrincipal principal = new GenericPrincipal(identity, UserDetails.Roles.Split(','));
                    IPrincipal principal = new GenericPrincipal(identity, UserDetails.Roles.Split(','));
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }

                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else if (!(intro))
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
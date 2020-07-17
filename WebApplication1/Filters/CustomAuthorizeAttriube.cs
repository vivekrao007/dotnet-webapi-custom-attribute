using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApplication1.Services;

namespace WebApplication1.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CustomAuthorizeAttriube: AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var UserId = actionContext.Request.Headers.Authorization?.Scheme;
            if (UserId != null)
            {
                AuthService service = new AuthService();
                string UserName = service.AuthorizeUser(Convert.ToInt32(UserId));
                actionContext.Request.Headers.Add("username", UserName);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                        "user Unauthorized to perform this request");
            }
        }
    }
}
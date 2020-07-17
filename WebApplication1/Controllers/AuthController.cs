using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(LoginModal LoginModal)
        {
            if (ModelState.IsValid)
            {
                AuthService service = new AuthService();
                User user = service.LoginUser(LoginModal);
                Response<User> result = new Response<User>() { 
                    status= true,
                    Message="login sucessfully",
                    Body= user
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}

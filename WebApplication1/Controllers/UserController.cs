using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("get/{UserId}")]
        public HttpResponseMessage GetUserById(int UserId)
        {
            UserService service = new UserService();
            User User = service.GetUserById(UserId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, User);
            return response;
        }
        [HttpGet]
        [Route("get/all")]
        public HttpResponseMessage GetAllUsers()
        {
            UserService service = new UserService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, service.GetAllUsers());
            return response;
        }
        [Filters.CustomAuthorizeAttriube()]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddUser(User UserModal)
        {
            if (ModelState.IsValid)
            {
                UserService service = new UserService();
                UserModal.CreatedBy = this.Request.Headers.GetValues("username").First();
                service.CreateUser(UserModal);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "added user details sucessfully");
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
        }
        [Filters.CustomAuthorizeAttriube()]
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage UpdateUser(User UserModal)
        {
            if (ModelState.IsValid)
            {
                UserService service = new UserService();
                UserModal.UpdatedBy = this.Request.Headers.GetValues("username").First();
                service.UpdateUser(UserModal);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "updated user details sucessfully");
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
        }
        [Filters.CustomAuthorizeAttriube()]
        [HttpDelete]
        [Route("delete/{UserId}")]
        public HttpResponseMessage DeleteUser(int UserId)
        {
            UserService service = new UserService();
            service.DeleteUser(new User 
            { 
                UserId = UserId , 
                DeletedBy = this.Request.Headers.GetValues("username").First()
            });
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "deleted user details sucessfully");
            return response;
        }
    }


}


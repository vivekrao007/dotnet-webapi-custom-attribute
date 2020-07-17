using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/technology")]
    public class TechnologyController : ApiController
    {
        [HttpGet]
        [Route("get/{TechId}")]
        public HttpResponseMessage GetTechnologyById(int TechId)
        {
            TechnologyService service = new TechnologyService();
            Technology TechObj = service.GetTechnologyById(TechId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, TechObj);
            return response;
        }
        [HttpGet]
        [Route("get/all")]
        public HttpResponseMessage GetAllTechnologies()
        {
            TechnologyService service = new TechnologyService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, service.GetAllTechnologies());
            return response;
        }
        [Filters.CustomAuthorizeAttriube()]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddTechnology(Technology TechnologyModal)
        {
            if (ModelState.IsValid)
            {
                TechnologyService service = new TechnologyService();
                TechnologyModal.CreatedBy = this.Request.Headers.GetValues("username").First();
                service.CreateTechnology(TechnologyModal);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "added technology details sucessfully");
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
        public HttpResponseMessage UpdateTechnology(Technology TechnologyModal)
        {
            if (ModelState.IsValid)
            {
                TechnologyService service = new TechnologyService();
                TechnologyModal.UpdatedBy = this.Request.Headers.GetValues("username").First();
                service.UpdateTechnology(TechnologyModal);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "updated technology details sucessfully");
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
        }
        [Filters.CustomAuthorizeAttriube()]
        [HttpDelete]
        [Route("delete/{TechId}")]
        public HttpResponseMessage DeleteTechnology(int TechId)
        {
            TechnologyService service = new TechnologyService();
            service.DeleteTechnology(new Technology
            {
                TechnologyId= TechId,
                DeletedBy= this.Request.Headers.GetValues("username").First()
            });
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "deleted technology details sucessfully");
            return response;
        }
    }
}

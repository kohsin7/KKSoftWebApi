using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KKSoftWebApi.Controllers
{
    //订制路由特性
    [RoutePrefix("api/user")]//路由前缀
    public class UserController : ApiController
    {
        [Route("Login")]//路由名称
        [HttpGet]//不使用Restful风格，只要自己制定那种类型来处理
        public string Login(string id, string pwd)
        {
            return "ok";
        }

        [Route("GetMessage")]//路由名称
        [HttpGet]
        public IHttpActionResult GetMessage()
        {
            //return Ok();
            //return NotFound();
            return InternalServerError(new Exception("wrong"));
        }
    }
}

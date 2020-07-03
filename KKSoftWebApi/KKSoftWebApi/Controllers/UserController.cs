using KKSoftWebApi.Filters;
using KKSoftWebApi.Models;
using KKSoftWebApi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace KKSoftWebApi.Controllers
{
    /*
     * 1、通过特性的方式进行jwt校验  特性与过滤器auth
     * 2、在校验过程中读取jwt中的账号信息，并存入User.Identity.Name
     * 3、可以再任意Action方法中读取User.Identity.Name属性里的数据
     */
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //订制路由特性
    [RoutePrefix("api/user")]//路由前缀
    public class UserController : ApiController
    {
        [Route("Login")]//路由名称
        [HttpPost]//不使用Restful风格，只要自己制定那种类型来处理
        public IHttpActionResult Login(LoginViewModel loginModel)
        {
            return Ok(new ResponseData()
            {
                Code = 200,
                Data = JwtTools.Encode(new Dictionary<string, object>()
                    {
                        { "LoginName", loginModel.LoginName },
                        { "UserId", 123456 }
                    })
            });
            if (ModelState.IsValid)//传上来的数据已经通过校验
            {
                return Ok(new ResponseData()
                {
                    Code = 200,
                    Data = JwtTools.Encode(new Dictionary<string, object>()
                    {
                        { "LoginName", loginModel.LoginName },
                        { "UserId", 123456 }
                    })
                });
            }
            else
            {
                return Ok(new ResponseData()
                {
                    Code = 500,
                    ErrorMessage = "账号密码有误"
                });
            }
        }

        [Route("GetUserInfo")]
        [HttpGet]
        [LoginAuthorize]
        public IHttpActionResult GetUserInfo()
        {
            return Ok(new ResponseData() { Data = ((UserIdentity)User.Identity).Id });
        }
    }
}

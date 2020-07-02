using KKSoftWebApi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KKSoftWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [Route("Login")]
        [HttpPost]
        public string Login(UserModel user)
        {
            if (user.LoginName == "kohsin" && user.LoginPwd == "123456")
            {
                return JwtTools.Encode(new Dictionary<string, object>()
                {
                    { "UserName", user.LoginName },
                    { "UserPwd", user.LoginPwd }
                }, JwtTools.key);
            }
            throw new Exception("账号有误");
        }

        [Route("GetUserInfo")]
        [HttpGet]
        public string GetUserInfo()
        {
            var userName = JwtTools.ValideLogined(ControllerContext.Request.Headers);
            return "用户资料:" + userName;
        }
    }
}

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
        public bool Login(UserModel user)
        {
            if (user.LoginName == "kohsin" && user.LoginPwd == "123456")
            {
                return true;
            }
            return false;
        }

        [Route("GetUserInfo")]
        [HttpGet]
        public string GetUserInfo(string loginName)
        {
            return loginName + "用户资料";
        }
    }
}

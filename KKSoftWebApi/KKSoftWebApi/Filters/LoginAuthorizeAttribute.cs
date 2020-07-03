using KKSoftWebApi.Models;
using KKSoftWebApi.Tools;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace KKSoftWebApi.Filters
{
    public class LoginAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple { get; }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync
            (HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            IEnumerable<string> headers;//获取request--->headers--->token
            if (actionContext.Request.Headers.TryGetValues("token", out headers))
            {
                //如果获取到了headers里的token
                var decodeResult = JwtTools.Decode(headers.First());
                var loginName = decodeResult["LoginName"].ToString();
                var loginId = 123;// (int)decodeResult["UserId"];

                (actionContext.ControllerContext.Controller as ApiController).User = new ApplicationUser(loginName, loginId);

                //return continuation();
                return await continuation();
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}
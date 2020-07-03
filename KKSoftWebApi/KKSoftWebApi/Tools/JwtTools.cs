using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace KKSoftWebApi.Tools
{
    public class JwtTools
    {
        public static string key = "q9w7eq8csd89fvs9ad8vyu98sfsad9fy";

        public static string Encode(Dictionary<string, object> payload, string key = "q9w7eq8csd89fvs9ad8vyu98sfsad9fy")
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密算法

            IJsonSerializer serializer = new JsonNetSerializer();//加密器

            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            payload.Add("timeout", DateTime.Now.AddHours(1));

            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, key);
        }

        public static Dictionary<string, object> Decode(string token, string key = "q9w7eq8csd89fvs9ad8vyu98sfsad9fy")
        {
            try
            {
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();

                IJsonSerializer serializer = new JsonNetSerializer();

                IDateTimeProvider provider = new UtcDateTimeProvider();

                IJwtValidator validator = new JwtValidator(serializer, provider);

                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, key, true);

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                if ((DateTime)result["timeout"] < DateTime.Now)
                {
                    throw new Exception("jwt已经过期，请重新登录");
                }
                result.Remove("timeout");
                return result;

            }
            catch (TokenExpiredException)
            {
                throw;
            }
            catch (SignatureVerificationException)
            {
                throw;
            }
        }

        public static Dictionary<string, object> ValideLogined(HttpRequestHeaders headers)
        {
            if (headers.GetValues("token") == null || !headers.GetValues("token").Any())
            {
                throw new Exception("请登录");
            }
            return Decode(headers.GetValues("token").First(), key);
        }
    }
}
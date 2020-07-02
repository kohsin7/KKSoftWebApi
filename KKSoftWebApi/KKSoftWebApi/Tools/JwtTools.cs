using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
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

        public static string Encode(Dictionary<string, object> payload, string key)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密算法

            IJsonSerializer serializer = new JsonNetSerializer();//加密器

            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, key);
        }

        public static string Decode(string token, string key)
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
                return json;

            }
            catch (TokenExpiredException)
            {
                return "Token has expired";
            }
            catch (SignatureVerificationException)
            {
                return "Token has invalid signature";
            }
        }

        public static string ValideLogined(HttpRequestHeaders headers)
        {
            if (headers.GetValues("token") == null || !headers.GetValues("token").Any())
            {
                throw new Exception("请登录");
            }
            return Decode(headers.GetValues("token").First(), key);
        }
    }
}
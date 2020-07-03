using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace KKSoftWebApi.Models
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
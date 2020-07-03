using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace KKSoftWebApi.Models
{
    public class ApplicationUser : IPrincipal
    {
        public ApplicationUser(string name, int id)
        {
            Identity = new UserIdentity(name, id);
        }

        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
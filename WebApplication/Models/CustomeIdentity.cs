using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApplication1.Models
{
    public class CustomeIdentity:GenericIdentity
    {
        public string  Email { get; set; }
        public string Address { get; set; }
        public int  Id { get; set; }
        public CustomeIdentity(string name) : base(name)
        {
            
        }

        public CustomeIdentity(string name, string type) : base(name, type)
        {
        }

        public CustomeIdentity(GenericIdentity identity) : base(identity)
        {
        }
    }

    public class CustomePricipal : GenericPrincipal
    {
        public CustomePricipal(IIdentity identity, string[] roles) : base(identity, roles)
        {
        }
    }
}
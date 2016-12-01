using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;



namespace DemoClaimAuthentication.extension
{
    public class ClaimTranformation: ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }
            var newprincipal =  CreateClaimsPrincipal(incomingPrincipal);
            CreateSessionSecurityTocken(newprincipal);
            return newprincipal;
        }

        private void CreateSessionSecurityTocken(ClaimsPrincipal newprincipal)
        {
            var sessionToken = new SessionSecurityToken(newprincipal,TimeSpan.FromHours(8));
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(sessionToken);
        }

        private ClaimsPrincipal CreateClaimsPrincipal(ClaimsPrincipal incomingPrincipal)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name,incomingPrincipal.Identity.Name) );
            claims.Add(new Claim(ClaimTypes.Country,"Viet Nam"));
            return new ClaimsPrincipal(new ClaimsIdentity(claims,"Custom"));
        }

    }
}
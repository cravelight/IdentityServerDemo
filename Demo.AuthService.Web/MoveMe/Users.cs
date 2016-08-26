using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace Demo.AuthService.Web.MoveMe
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser> {
            new InMemoryUser {
                Subject = "1",
                Username = "demo.user",
                Password = "Password123!",
                Claims = new List<Claim> {
                    new Claim(Constants.ClaimTypes.GivenName, "Demo"),
                    new Claim(Constants.ClaimTypes.FamilyName, "User"),
                    new Claim(Constants.ClaimTypes.Email, "demo.user@example.com"),
                    new Claim(Constants.ClaimTypes.Role, "Admin")
                }
            }
        };
        }
    }
}
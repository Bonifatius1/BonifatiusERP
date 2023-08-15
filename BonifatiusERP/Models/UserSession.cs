using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BonifatiusERP.Admin.Models
{
    public class UserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static IHttpContextAccessor _staticHttpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _staticHttpContextAccessor = httpContextAccessor;
        }

        HttpContext HttpContext => _httpContextAccessor.HttpContext;
        static HttpContext StaticHttpContext => _staticHttpContextAccessor.HttpContext;

        
       
        public Guid UserId
        {
            get
            {
                var userid = StaticHttpContext.User.Claims.FirstOrDefault(x => string.Compare(x.Type, "UserId", true) == 0);
                return userid != null ? Guid.Parse(userid.Value) : Guid.Empty;
            }
        }

        

        public static string Email
        {
            get
            {
                if (StaticHttpContext.Session.GetString("Email") == null)
                    return null;
                else
                    return StaticHttpContext.Session.GetString("Email");
            }
            set
            {
                StaticHttpContext.Session.SetString("Email", value);
            }
        }

        public string FirstName
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(x => string.Compare(x.Type, "FirstName", true) == 0)?.Value;
            }
        }

        public string LastName
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(x => string.Compare(x.Type, "LastName", true) == 0)?.Value;
            }
        }

        public string UserName
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(x => string.Compare(x.Type, "UserName", true) == 0)?.Value;
            }
        }

        public string AccessToken
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(x => string.Compare(x.Type, "AccessToken", true) == 0)?.Value;
            }
        }

       
        public static string ProfilePhoto
        {
            get
            {
                if (StaticHttpContext.Session.GetString("ProfilePhoto") == null)
                    return null;
                else
                    return StaticHttpContext.Session.GetString("ProfilePhoto");
            }
            set
            {
                StaticHttpContext.Session.SetString("ProfilePhoto", value);
            }
        }



       
       

    }
    

}

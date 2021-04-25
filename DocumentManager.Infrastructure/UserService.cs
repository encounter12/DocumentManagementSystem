using DocumentManager.Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace DocumentManager.Infrastructure
{
    public class UserService : IUserService
    {
        private string _username;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public string Username
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_username))
                {
                    _username = GetUsername();
                }

                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUsername()
        {
            string username;

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                throw new Exception("The Http context is null");
            }

            username = httpContext.Request.Headers["x-profi-username"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("No property 'username' has been set on the request header x-profi-username");
            }

            return username;
        }
    }
}

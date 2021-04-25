using DocumentManager.Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace DocumentManager.Infrastructure
{
    public class TenantService : ITenantService
    {
        private long _tenantID;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public long TenantID
        {
            get
            {
                if (_tenantID == 0)
                {
                    _tenantID = GetTenantID();
                }

                return _tenantID;
            }
            set
            {
                _tenantID = value;
            }
        }

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private long GetTenantID()
        {
            long tenantID = -1;

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                string tenantApiKey = httpContext.Request.Headers["x-api-key"].FirstOrDefault();

                if (string.IsNullOrWhiteSpace(tenantApiKey))
                {
                    throw new Exception("No property 'tenantID' has been set on the request header X-API-Key");
                }

                long tenantId;

                if (long.TryParse(tenantApiKey, out tenantId))
                {
                    tenantID = tenantId;
                }
                else
                {
                    throw new Exception("The tenant Id from request header X-API-Key cannot be parsed");
                }
            }

            return tenantID;
        }
    }
}

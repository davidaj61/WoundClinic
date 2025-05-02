using Microsoft.AspNetCore.Http;

namespace WoundClinic.Services
{
    public class HttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // دریافت مسیر درخواست
        public string GetRequestPath()
        {
            return _httpContextAccessor.HttpContext?.Request.Path.ToString();
        }

        // دریافت آدرس IP کلاینت
        public string GetClientIpAddress()
        {
            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
    }

}


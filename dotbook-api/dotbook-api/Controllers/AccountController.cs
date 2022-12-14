using System;
using System.Threading.Tasks;
using dotbook_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotbook_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<string> Register(string login, string password)
        {
            try
            {
                await _service.Register(login, password, HttpContext);
                return string.Empty;
            }
            catch(Exception ex)
            {
                Response.StatusCode = 400;
                return ex.Message;
            }
        }

        [HttpPost("login")]
        public async Task<string> Login(string login, string password)
        {
            try
            {
                await _service.Login(login, password, HttpContext);
                return string.Empty;
            }
            catch(Exception ex)
            {
                Response.StatusCode = 401;
                return ex.Message;
            }
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await _service.Logout(HttpContext);
        }

        [HttpGet("authorized")]
        public bool IsAuthorized()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
    }
}

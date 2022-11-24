using dotbook_api.DataAccess.Context;
using dotbook_api.DataAccess.TableModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotbook_api.Services
{
    public class AccountService
    {
        private readonly DotbookContext _context;

        public AccountService(DotbookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Авторизовать пользователя
        /// </summary>
        /// <param name="email">Эл. почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="httpContext">Http контекст сессии</param>
        public async Task Login(string name, string password, HttpContext httpContext)
        {
            // ValidateEmail(email);
            ValidateName(name);
            ValidatePassword(password);

            if (!_context.Users.Any(x => x.Name == name && x.Password == password))
                throw new Exception("Неверные имя пользователя и/или пароль");

            await AuthorizeAsync(name, httpContext);
        }

        /// <summary>
        /// Зарегистрировать нового пользователя. При успешной регистрации, сразу происходит авторизация
        /// </summary>
        /// <param name="name">Отображаемое имя</param>
        /// <param name="email">Эл. почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="httpContext">Http контекст сессии</param>
        public async Task Register(string name, string password, HttpContext httpContext)
        {
            // ValidateEmail(email);
            ValidateName(name);
            ValidatePassword(password);

            if (_context.Users.Any(x => x.Name == name))
                throw new Exception($"Пользователь с именем {name} уже зарегистрирован");

            var newUser = new User()
            {
                Name = name,
                Password = password
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            await AuthorizeAsync(name, httpContext);
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Асинхронно авторизовывает пользователя через куки
        /// </summary>
        /// <param name="email"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private async Task AuthorizeAsync(string name, HttpContext httpContext)
        {
            var claims = new Claim[] { new Claim(ClaimsIdentity.DefaultNameClaimType, name) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        private void ValidateEmail(string email)
        {
            var ex = new Exception("Некорректный адрес эл. почты");

            if (string.IsNullOrEmpty(email) || email.EndsWith(".")) throw ex;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email) throw ex;
            }
            catch
            {
                throw ex;
            }
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 4) throw new Exception("Имя пользователя не должно быть короче 4 символов");
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new Exception("Пароль не был указан");
            var hasLittleLetters = password.Any(x => x >= 'a' && x <= 'z' || x >= 'а' && x <= 'я' );
            var hasBigLetters = password.Any(x => x >= 'A' && x <= 'Z' || x >= 'А' || x <= 'Я');
            var hasNumbers = password.Any(x => x >= '0' && x <= '9');
            if (!hasLittleLetters || !hasBigLetters || !hasNumbers || password.Length < 6)
                throw new Exception("Пароль должен быть не менее 6 символов длиной и содержать заглавные, строчные буквы и цифры");
        }
    }
}

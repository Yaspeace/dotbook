using dotbook_api.DataAccess.Context;
using dotbook_api.DataAccess.TableModels;
using dotbook_api.Services;
using dotbook_api.Services.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace dotbook_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Добавление CORS для конфига заголовков
            services.AddCors();

            // Апи без вьюх
            services.AddControllers();

            var conn = Configuration.GetConnectionString("DefaultConnection");
            var ver = Configuration.GetValue<string>("SqlServerVersion");

            services.AddDbContext<DotbookContext>(options =>
                options.UseMySql(conn, ServerVersion.Parse(ver)));

            #region Services

            services.AddTransient<UserService>();
            services.AddSingleton<FileClient>();
            services.AddTransient<BaseCrudService<SavedFile>>();
            services.AddTransient<BaseReadService<Theme>>();
            services.AddTransient<AccountService>();
            services.AddTransient<BookService>();
            services.AddTransient<PublishService>();

            #endregion

            // Добавление аутентификации через куки и кляймы
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(opt => {
                        opt.LoginPath = "";
                        opt.Events = new CookieAuthenticationEvents
                        {
                            OnRedirectToAccessDenied = ctx =>
                            {
                                ctx.Response.StatusCode = 401;
                                return Task.CompletedTask;
                            },
                            OnRedirectToLogin = ctx =>
                            {
                                ctx.Response.StatusCode = 401;
                                return Task.CompletedTask;
                            }
                        };
                    });
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if(!env.IsDevelopment()) app.UseHsts();

            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().WithHeaders("accept", "content-type", "origin"));

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

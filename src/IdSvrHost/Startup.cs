using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdSvrHost.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using IdentityServer4.Core.Services;
using IdentityServer4.Core.Validation;
using IdSvrHost.Models;
using IdSvrHost.Services;
using Microsoft.AspNet.Identity;
using CustomGrantValidator = IdSvrHost.Extensions.CustomGrantValidator;

namespace IdSvrHost2
{
    public class Startup
    {
        private readonly IApplicationEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(IApplicationEnvironment environment)
        {
            _environment = environment;

            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            configurationBuilder.AddEnvironmentVariables();

            _configuration = configurationBuilder.Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            var cert = new X509Certificate2(Path.Combine(_environment.ApplicationBasePath, "idsrv4test.pfx"), "idsrv3test");

            var builder = services.AddIdentityServer(options =>
            {
                options.SigningCertificate = cert;
            });

            //builder.AddInMemoryClients(Clients.Get());
            //builder.AddInMemoryUsers(Users.Get());
            builder.AddInMemoryScopes(Scopes.Get());

            builder.Services.AddTransient<IRepository, MongoDbRepository>();
            builder.Services.AddTransient<IClientStore, MongoDbClientStore>();
            builder.Services.AddTransient<IProfileService, MongoDbProfileService>();
            builder.Services.AddTransient<IResourceOwnerPasswordValidator, MongoDbResourceOwnerPasswordValidator>();
            builder.Services.AddTransient<IPasswordHasher<MongoDbUser>, PasswordHasher<MongoDbUser>>();
            builder.Services.Configure<MongoDbRepositoryConfiguration>(_configuration.GetSection("MongoDbRepository"));
            builder.AddCustomGrantValidator<CustomGrantValidator>();


            // for the UI
            services
                .AddMvc()
                .AddRazorOptions(razor =>
                {
                    razor.ViewLocationExpanders.Add(new IdSvrHost.UI.CustomViewLocationExpander());
                });

            services.AddTransient<IdSvrHost.UI.Login.LoginService>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Verbose);
            loggerFactory.AddDebug(LogLevel.Verbose);

            app.UseDeveloperExceptionPage();
            app.UseIISPlatformHandler();

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

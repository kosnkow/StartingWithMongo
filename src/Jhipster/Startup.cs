using AspNetCore.Identity.MongoDbCore.Infrastructure;
using JHipsterNet.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyCompany.Infrastructure;
using MyCompany.Models;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;

[assembly: ApiController]

namespace MyCompany
{
    public class Startup {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
            .AddNhipsterModule(Configuration);

            AddDatabase(services);

            services
            .AddSecurityModule()
            .AddProblemDetailsModule()
            .AddAutoMapperModule()
            .AddWebModule()
            .AddSwaggerModule()
            .AddMvc()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider, IOptions<JHipsterSettings> jhipsterSettingsOptions)
        {
            var jhipsterSettings = jhipsterSettingsOptions.Value;
            app
                .UseApplicationSecurity(jhipsterSettings)
                .UseApplicationProblemDetails()
                .UseApplicationWeb(env)
                .UseApplicationSwagger()
                .UseApplicationIdentity(serviceProvider);
        }

        protected virtual void AddDatabase(IServiceCollection services)
        {
            var connection = Configuration.GetSection("DatabaseSettings").Get<MongoDbSettings>();

            services.AddDatabaseModule(Configuration);

            services.AddIdentity<User, Role>(options => {
                options.SignIn.RequireConfirmedEmail = true;
                options.ClaimsIdentity.UserNameClaimType = JwtRegisteredClaimNames.Sub;
            })
            .AddMongoDbStores<User, Role, string>(connection.ConnectionString, connection.DatabaseName)
            .AddSignInManager()
            .AddDefaultTokenProviders();
        }
    }
}

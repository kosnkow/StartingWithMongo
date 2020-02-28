using System;
using System.IdentityModel.Tokens.Jwt;
using JHipsterNet.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyCompany.Models;
using MyCompany.Test.Infrastructure;

namespace MyCompany.Test.Setup
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider, IOptions<JHipsterSettings> jhipsterSettingsOptions)
        {
            base.Configure(app, env, serviceProvider, jhipsterSettingsOptions);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }

        protected override void AddDatabase(IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.ClaimsIdentity.UserNameClaimType = JwtRegisteredClaimNames.Sub;
            })
            .AddMongoDbStores<User, Role, string>(TestConfiguration.ConnectionString, TestConfiguration.DatabaseName)
            .AddSignInManager()
            .AddDefaultTokenProviders();
        }
    }
}

using JHipsterNet.Pagination.Binders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyCompany.Infrastructure {
    public static class WebConfiguration {
        public static IServiceCollection AddWebModule(this IServiceCollection @this)
        {
            @this.AddHttpContextAccessor();

            @this.AddMvc();

            //https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2
            @this.AddHealthChecks();

            //TODO use AddMvcCore + config
            @this.AddControllers(options => { options.ModelBinderProviders.Insert(0, new PageableBinderProvider()); })
            .AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            return @this;
        }

        public static IApplicationBuilder UseApplicationWeb(this IApplicationBuilder @this, IHostEnvironment env)
        {
            @this.UseDefaultFiles();
            @this.UseStaticFiles();

            @this.UseRouting();
            @this.UseAuthorization();
            @this.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            @this.UseHealthChecks("/health");

            return @this;
        }
    }
}

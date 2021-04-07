using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VirtualDev.Action.ActionServer;
using VirtualDev.Database;

namespace VirtualDev.Action
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongoDb(options =>
            {
                options.ConnectionString = Configuration["Mongo:ConnectionString"];
                options.DatabaseName = Configuration["Mongo:DatabaseName"];
            });

            services.AddRasaActionServer(options =>
            {
                options.WebhookUrl = "/webhook";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRasaActionServer();
        }
    }
}

namespace MassTransitApi
{
    using System.Reflection;
    using Commands;
    using Events;
    using MassTransit;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Processors;

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
            services.AddMvc();
            
            services.AddMediator(mediator =>
            {
                mediator.AddRequestClient<CreateUser>();
                mediator.AddRequestClient<UserCreated>();
                
                mediator.AddConsumers(Assembly.GetExecutingAssembly());
            });

            services.AddMassTransit(x =>
            {
                x.UsingInMemory();
            });

            services.AddOpenApiDocument(config => config.PostProcess = d => d.Info.Title = "Sample API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
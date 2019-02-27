using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using wazi.data.models;
using wazi.data.core.master;

namespace wazi.store
{
    public class Startup
    {
        IHostingEnvironment environment = null;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            this.environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsEnvironment("Development")) {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }


            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddOptions();

            var repoconfig = Configuration.GetValue<RepositoryConfig>("RepositoryConfig");

            services.Configure<MasterRepository>(masterrepo => {
                setupMasterRepository(masterrepo);
            });

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();


            // Shows UseCors with named policy.
            app.UseCors("CorsPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }

        MasterRepository setupMasterRepository(MasterRepository masterRepository) {
            masterRepository.Setup(new RepositoryConfig {
                Connector = ConnectorType.Mongo,
                Description = "wazimerchant",
                DisplayName = "wazimerchant",
                ID = Guid.NewGuid().ToString(),
                Name = "wazimerchant",
                Servers = new List<RepositoryAddress> {
                    new RepositoryAddress { ServerName = "localhost1", PortNo = 27017 }
                },
                Server = new RepositoryAddress { ServerName = "localhost", PortNo = 27017 },
                Type = RepositoryType.master
            });



            //create repository if it does not exist...
            masterRepository.Create();
            return masterRepository;
        }
    }
}

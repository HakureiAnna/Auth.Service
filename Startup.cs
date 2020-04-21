using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Auth.Service
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
            services.AddHttpClient();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Auth API",
                    Version = "v1"
                });
            });

            services.AddControllers();

            services.AddSingleton<IMemberService>(InitializeCosmosAsync()
                .GetAwaiter()
                .GetResult());
        }

        public static async Task<MemberService> InitializeCosmosAsync()
        {
            var connStr = Environment.GetEnvironmentVariable("cosmos_connStr");
            var databaseName = Environment.GetEnvironmentVariable("cosmos_db");
            var containerName = Environment.GetEnvironmentVariable("container_member");

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(connStr);
            CosmosClient client = clientBuilder
                .WithConnectionModeDirect()
                .Build();
            var memberService = new MemberService(client, databaseName, containerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return memberService;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
            });
        }
    }
}

using IOT.ETL.IRepository.Ietl_task_info;
using IOT.ETL.IRepository.ILOGIRepository;
using IOT.ETL.IRepository.UsersIRepository;
using IOT.ETL.Repository.DataAnalysisRepository;
using IOT.ETL.IRepository.ISys_paramIRepository;
using IOT.ETL.Repository.ISys_paramRepository;
using IOT.ETL.Repository.etl_task_info;
using IOT.ETL.Repository.ILOGRepository;
using IOT.ETL.Repository.UsersRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_role;
using IOT.ETL.Repository.sys_role;
using IOT.ETL.IRepository.sys_modules;
using IOT.ETL.Repository.sys_modules;
using IOT.ETL.Common;
using IOT.ETL.IRepository.IDataAnalysisRepository;

using IOT.ETL.IRepository.Ietl_task_join_info;
using IOT.ETL.Repository.etl_task_join_info;

namespace IOT.ETL.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IOT.ETL.Api", Version = "v1" });
            });

            #region 
            services.AddSingleton<ILOGIRepository, ILOGRepository>();
            services.AddSingleton<UsersIRepository, UsersRepository>();
            services.AddSingleton<IDataAnalysisRepository, DataAnalysisRepository>();
            services.AddSingleton<ISys_paramIRepository, ISys_paramRepository>();
            services.AddSingleton<IsysroleRespoditory, sysroleRespoditory>();
            services.AddSingleton<IsysmodulesRepository, sysmodulesRepository>();
            services.AddSingleton<Ietl_task_infoRepository, etl_task_infoRepository>();
            services.AddSingleton<Ietl_task_join_infoRepository, etl_task_join_infoRepository>();
            
            #endregion

            var section = Configuration.GetSection("Redis:Default");
            string _connectionString = section.GetSection("Connection").Value;
            string _instanceName = section.GetSection("InstanceName").Value;
            int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0");
            services.AddSingleton(new RedisHelper1(_connectionString, _instanceName, _defaultDB));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddCors(options =>
            options.AddPolicy("cors",
            p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IOT.ETL.Api v1"));
            }
            app.UseCors("cors");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

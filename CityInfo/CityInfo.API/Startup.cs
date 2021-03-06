﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using CityInfo.API.Services;
using Microsoft.Extensions.Configuration;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class Startup
    {
        // This is used with the appSettings.json file.
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                // The order of which settings files is applied matter as it
                                // sets precedence. If settings appear in two files then the
                                // most later file wins.
                                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddMvcOptions(opt => opt.OutputFormatters.Add(
                        new XmlDataContractSerializerOutputFormatter()));
            //.AddJsonOptions(opt =>
            //{
            //    if (opt.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = opt.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});

#if DEBUG
            // Transient: service is created every time it's requested.
            // Scoped: service is created once per request.
            // Singleton: service is created the first time it's requested.
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

            // The add db context method only exists after the EF Core Sql Server Nuget package is added.
            var connectionString = Configuration["connectionStrings:cityInfoDbConnectionString"];
            services.AddDbContext<CityInfoContext>(opt => opt.UseSqlServer(connectionString));

            // Repositories are best used with scoped lifetime.
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                ILoggerFactory loggerFactory,
                                CityInfoContext cityInfoContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            // Initialize seed data for the db.
            cityInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            // Configure AutoMapper to map our db entities to our dtos.
            Mapper.Initialize(config =>
            {
                // AutoMapper automatically maps the properties from the source
                // object to the same properties on the destination object.
                // If the property doesn't exist then it will be ignored.
                config.CreateMap<City, CityWithoutPointsOfInterestDto>();
                config.CreateMap<City, CityDto>();
                config.CreateMap<PointOfInterest, PointOfInterestDto>();
                config.CreateMap<PointOfInterestForCreationDto, PointOfInterest>();
                config.CreateMap<PointOfInterestForUpdateDto, PointOfInterest>();
                config.CreateMap<PointOfInterest, PointOfInterestForUpdateDto>();
            });

            app.UseMvc();


        }
    }
}

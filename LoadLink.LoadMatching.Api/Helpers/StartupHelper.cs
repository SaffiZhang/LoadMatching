﻿
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.Caching;
using LoadLink.LoadMatching.Application.UserSubscription.Repository;
using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Data;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using ValidationException = FluentValidation.ValidationException;
using System.Linq;
using FluentValidation;
using LoadLink.LoadMatching.Infrastructure.Caching;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using AutoMapper;

namespace LoadLink.LoadMatching.Api.Helpers
{
    public static class StartupHelper
    {

        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // connections
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IConnectionFactory>(x => new ConnectionFactory(connectionString));

            // mapping profiles            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // repositories
            services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();

            // services
            services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            
            // local services
            services.AddScoped<IUserHelperService, UserHelperService>();

        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

        }

        public static void AddCache(this IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider().GetRequiredService<IOptionsSnapshot<AppSettings>>();

            var memoryCacheOptions = new MemoryCacheOptions();

            // clear the cache at midnight so we get fresh data
            TimeSpan expireTimespan = new TimeSpan((DateTime.Now - DateTime.Today).Ticks);

            MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expireTimespan, // will expire irrespective whether has been used or not                
            };

            services.AddSingleton<ICacheRepository<UserApiKeyQuery>, CacheRepository<UserApiKeyQuery>>(sp =>
            {
                var memoryCache = new MemoryCache(memoryCacheOptions);
                memoryCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(appSettings.Value.ServiceCacheSettings.UserApiKeySetting.SlidingExpiration));

                return new CacheRepository<UserApiKeyQuery>(memoryCache, memoryCacheEntryOptions);
            });

            // set other data that can be cache here

        }

        public static void RegisterAuthentication(this IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider().GetRequiredService<IOptionsSnapshot<AppSettings>>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = appSettings.Value.IdentityServer.AuthorityUrl;
                o.Audience = "linkup";
                o.RequireHttpsMetadata = false;
            });

        }

        public static void AllowedCors(this IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider().GetRequiredService<IOptionsSnapshot<AppSettings>>();

            var allowedCors = appSettings.Value.AllowedCorsOrigin;

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedCorsOrigin", builder =>
                {
                    builder.WithOrigins(allowedCors)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LoadLink LoadMatching API",
                    Description = "LoadMatching swagger API",
                    Contact = new OpenApiContact
                    {
                        Name = "LoadLink Development Team",
                        Email = "dev@loadlink.ca",
                        //Url = new Uri("https://www.loadlink.ca")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Restricted",
                        //Url = new Uri("https://www.loadlink.ca/privacy")
                    }
                }
                );

                options.OperationFilter<SwaggerHelper>();


                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on the textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                    { jwtSecurityScheme, Array.Empty<string>() }
                  });

            });
        }

        public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
        {
            _ = app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";

                    string errorText = null;

                    // general exception 
                    if (!(exception is ValidationException validationException))
                    {
                        // TODO: we need to come back to this for properly setting up the business validation property into a more specific error category
                        var error = new FluentValidation.Results.ValidationFailure("BusinessValidation", exception.Message);

                        // we can call some centralized logging  here

                        errorText = System.Text.Json.JsonSerializer.Serialize(new List<dynamic> { error });

                        await context.Response.WriteAsync(errorText, Encoding.UTF8);

                        return;
                    }

                    // validation exception
                    var errors = validationException.Errors.Select(err => new { err.PropertyName, err.ErrorMessage });
                    errorText = System.Text.Json.JsonSerializer.Serialize(errors);

                    await context.Response.WriteAsync(errorText, Encoding.UTF8);

                });
            });
        }

        public static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

    }
}

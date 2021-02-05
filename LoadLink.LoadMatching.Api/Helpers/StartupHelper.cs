
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
using LoadLink.LoadMatching.Application.CarrierSearch.Services;
using LoadLink.LoadMatching.Application.CarrierSearch.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.CarrierSearch;

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

            //TO DO: add the list of repositories and services here after the rewrite   <======

            // repositories
            services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
            services.AddScoped<ICarrierSearchRepository, CarrierSearchRepository>();
            
            // services
            services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            services.AddScoped<ICarrierSearchService, CarrierSearchService>();
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
            var defaultCacheSetting = appSettings.Value.ServiceCacheSettings.DefaultCacheSetting;

            // get default cache settings
            var absoluteExpiration = defaultCacheSetting.AbsoluteExpiration;
            var slidingExpiration = defaultCacheSetting.SlidingExpiration;

            // clear the cache at midnight so we get fresh data
            TimeSpan expireTimespan = new TimeSpan((DateTime.Now - DateTime.Today).Ticks);
            if (absoluteExpiration != 0)
                expireTimespan = new TimeSpan(0, absoluteExpiration, 0);

            // set absolute expiration
            MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expireTimespan, // will expire irrespective whether has been used or not                
            };

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            memoryCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpiration));

            // cached data models

            // userapikey setting
            services.AddSingleton<ICacheRepository<UserApiKeyQuery>, CacheRepository<UserApiKeyQuery>>(sp =>
            {
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
                    });

                    // add JWT Authentication
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Name = "JWT Authentication",
                        Description = "Enter JWT Bearer token **_only_**",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, new string[] { } } });
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
                          //TODO: we need to come back to this for properly setting up the business validation property into a more specific error category
                          var error = new FluentValidation.Results.ValidationFailure("BusinessValidation", exception.Message);

                          // centralized logging  here
                          Sentry.SentrySdk.CaptureEvent(new Sentry.SentryEvent(exception));

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


using AutoMapper;
using FluentValidation;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.AssignedEquipment.Repository;
using LoadLink.LoadMatching.Application.AssignedEquipment.Services;
using LoadLink.LoadMatching.Application.AssignedLoad.Repository;
using LoadLink.LoadMatching.Application.AssignedLoad.Services;
using LoadLink.LoadMatching.Application.Caching;
using LoadLink.LoadMatching.Application.City.Repository;
using LoadLink.LoadMatching.Application.City.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using LoadLink.LoadMatching.Application.UserSubscription.Repository;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Infrastructure.Caching;
using LoadLink.LoadMatching.Persistence.Data;
using LoadLink.LoadMatching.Persistence.Repositories.AssignedEquipment;
using LoadLink.LoadMatching.Persistence.Repositories.AssignedLoad;
using LoadLink.LoadMatching.Persistence.Repositories.City;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationException = FluentValidation.ValidationException;
using LoadLink.LoadMatching.Application.CarrierSearch.Services;
using LoadLink.LoadMatching.Application.CarrierSearch.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.CarrierSearch;
using LoadLink.LoadMatching.Application.EquipmentLead.Services;



using LoadLink.LoadMatching.Application.LeadCount.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.LeadCount;
using LoadLink.LoadMatching.Application.LeadCount.Services;

using LoadLink.LoadMatching.Application.DATEquipmentLead.Services;
using LoadLink.LoadMatching.Application.Contacted.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.Contacted;
using LoadLink.LoadMatching.Application.Contacted.Services;
using LoadLink.LoadMatching.Application.DATAccount.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.DATAccount;
using LoadLink.LoadMatching.Application.DATAccount.Services;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Services;

using LoadLink.LoadMatching.Application.EquipmentLiveLead.Services;

using LoadLink.LoadMatching.Application.DATLoadLiveLead.Services;
using LoadLink.LoadMatching.Application.EquipmentPosition.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.EquipmentPosition;
using LoadLink.LoadMatching.Application.EquipmentPosition.Services;
using LoadLink.LoadMatching.Application.Exclude.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.Exclude;
using LoadLink.LoadMatching.Application.Flag.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.Flag;
using LoadLink.LoadMatching.Application.Exclude.Services;
using LoadLink.LoadMatching.Application.Flag.Services;

using LoadLink.LoadMatching.Application.LoadLead.Services;
using LoadLink.LoadMatching.Application.LoadPosition.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.LoadPosition;
using LoadLink.LoadMatching.Application.LoadPosition.Services;
using LoadLink.LoadMatching.Application.Member.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.Member;
using LoadLink.LoadMatching.Application.Member.Services;
using LoadLink.LoadMatching.Application.Networks.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.Networks;
using LoadLink.LoadMatching.Application.Networks.Services;
using LoadLink.LoadMatching.Application.PDRatio.Services;
using LoadLink.LoadMatching.Application.RIRate.Services;
using LoadLink.LoadMatching.Application.VehicleSize.Services;
using LoadLink.LoadMatching.Application.RepostAll.Services;
using LoadLink.LoadMatching.Application.TemplatePosting.Services;
using LoadLink.LoadMatching.Application.USCarrierSearch.Services;
using LoadLink.LoadMatching.Application.USMemberSearch.Services;
using LoadLink.LoadMatching.Application.VehicleType.Services;
using LoadLink.LoadMatching.Application.PDRatio.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.PDRatio;
using LoadLink.LoadMatching.Persistence.Repositories.RIRate;
using LoadLink.LoadMatching.Application.RIRate.Repository;
using LoadLink.LoadMatching.Application.RepostAll.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.RepostAll;
using LoadLink.LoadMatching.Application.TemplatePosting.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.TemplatePosting;
using LoadLink.LoadMatching.Application.USCarrierSearch.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.USCarrierSearch;
using LoadLink.LoadMatching.Application.USMemberSearch.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.USMemberSearch;
using LoadLink.LoadMatching.Application.VehicleType.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleType;
using LoadLink.LoadMatching.Application.VehicleSize.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleSize;
using LoadLink.LoadMatching.Application.LoadLiveLead.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.LoadLiveLead;
using LoadLink.LoadMatching.Application.LoadLiveLead.Services;
using LoadLink.LoadMatching.Application.LoadLiveLeadLiveLead.Services;
using LoadLink.LoadMatching.Application.NetworkMembers.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.NetworkMember;
using LoadLink.LoadMatching.Application.NetworkMembers.Services;
using LoadLink.LoadMatching.Application.LoadPosting.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.LoadPosting;
using LoadLink.LoadMatching.Application.LoadPosting.Services;
using LoadLink.LoadMatching.Application.VehicleAttribute.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleAttribute;
using LoadLink.LoadMatching.Application.LegacyDeleted.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.LegacyDeleted;
using LoadLink.LoadMatching.Application.LegacyDeleted.Services;
using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleAttribute.Services;
using LoadLink.LoadMatching.Application.MemberSearch.Services;
using LoadLink.LoadMatching.Application.MemberSearch.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.MemberSearch;
using LoadLink.LoadMatching.Application.LiveLead.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.LiveLead;
using LoadLink.LoadMatching.Application.LiveLead.Services;
using LoadLink.LoadMatching.Application.LoadLead.Repository;
using LoadLink.LoadMatching.Application.DATLoadLead.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LoadLead;
using LoadLink.LoadMatching.Application.EquipmentLiveLeadLiveLead.Services;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.DATEquipmentLead;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.DATLoadLead;
using LoadLink.LoadMatching.Application.DATLoadLead.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.DatLoadLead;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Repository;
using LoadLink.LoadMatching.Application.EquipmentLead.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.EquipmentLead;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Repository;
using LoadLink.LoadMatching.Persistence.Repositories.EquipmentLiveLead;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using MediatR;
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using LoadLink.LoadMatching.Api.BackgroundTasks;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;
using Microsoft.Extensions.Hosting;
using LoadLink.LoadMatching.Application.EquipmentPosting.IntetrationEvents;
using LoadLink.LoadMatching.IntegrationEventManager;
using LoadLink.LoadMatching.RabbitMQIntegrationEventManager;


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
            services.AddScoped<IAssignedEquipmentRepository, AssignedEquipmentRepository>();
            services.AddScoped<IAssignedLoadRepository, AssignedLoadRepository>();
            services.AddScoped<ICarrierSearchRepository, CarrierSearchRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IContactedRepository, ContactedRepository>();
            services.AddScoped<IDatAccountRepository, DatAccountRepository>();
            services.AddScoped<IDatEquipmentLeadRepository, DatEquipmentLeadRepository>();
            services.AddScoped<IDatEquipmentLiveLeadRepository, DatEquipmentLiveLeadRepository>();
            services.AddScoped<IDatLoadLeadRepository, DatLoadLeadRepository>();
            services.AddScoped<IDatLoadLiveLeadRepository, DatLoadLiveLeadRepository>();
            services.AddScoped<IEquipmentLeadRepository, EquipmentLeadRepository>();
            services.AddScoped<IEquipmentLiveLeadRepository, EquipmentLiveLeadRepository>();
            services.AddScoped<IEquipmentPositionRepository, EquipmentPositionRepository>();
           
            services.AddScoped<IExcludeRepository, ExcludeRepository>();
            services.AddScoped<IFlagRepository, FlagRepository>();
            services.AddScoped<ILeadsCountRepository, LeadsCountRepository>();
            services.AddScoped<ILegacyDeletedRepository, LegacyDeletedRepository>();
            services.AddScoped<ILiveLeadRepository, LiveLeadRepository>();
            services.AddScoped<ILoadLeadRepository, LoadLeadRepository>();
            services.AddScoped<ILoadLiveLeadRepository, LoadLiveLeadRepository>();
            services.AddScoped<ILoadPositionRepository, LoadPositionRepository>();
            services.AddScoped<LoadLink.LoadMatching.Application.LoadPosting.Repository.ILoadPostingRepository, LoadPostingRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberSearchRepository, MemberSearchRepository>();
            services.AddScoped<INetworkMembersRepository, NetworkMembersRepository>();
            services.AddScoped<INetworksRepository, NetworksRepository>();
            services.AddScoped<IPDRatioRepository, PDRatioRepository>();
            services.AddScoped<IRepostAllRepository, RepostAllRepository>();
            services.AddScoped<IRIRateRepository, RIRateRepository>();
            services.AddScoped<ITemplatePostingRepository, TemplatePostingRepository>();
            services.AddScoped<IUSCarrierSearchRepository, USCarrierSearchRepository>();
            services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
            services.AddScoped<IUSMemberSearchRepository, USMemberSearchRepository>();
            services.AddScoped<IVehicleAttributeRepository, VehicleAttributeRepository>();
            services.AddScoped<IVehicleSizeRepository, VehicleSizeRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();

            // services
            services.AddScoped<IAssignedEquipmentService, AssignedEquipmentService>();
            services.AddScoped<IAssignedLoadService, AssignedLoadService>();
            services.AddScoped<ICarrierSearchService, CarrierSearchService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IContactedService, ContactedService>();
            services.AddScoped<IDatAccountService, DatAccountService>();
            services.AddScoped<IDatEquipmentLeadService, DatEquipmentLeadService>();
            services.AddScoped<IDatEquipmentLiveLeadService, DatEquipmentLiveLeadService>();
            services.AddScoped<IDatLoadLeadService, DatLoadLeadService>();
            services.AddScoped<IDatLoadLiveLeadService, DatLoadLiveLeadService>();
            services.AddScoped<IEquipmentLeadService, EquipmentLeadService>();
            services.AddScoped<IEquipmentLiveLeadService, EquipmentLiveLeadService>();
            services.AddScoped<IEquipmentPositionService, EquipmentPositionService>();
           
            services.AddScoped<IExcludeService, ExcludeService>();
            services.AddScoped<IFlagService, FlagService>();
            services.AddScoped<ILeadsCountService, LeadsCountService>();
            services.AddScoped<ILegacyDeletedService, LegacyDeletedService>();
            services.AddScoped<ILiveLeadService, LiveLeadService>();
            services.AddScoped<ILoadLeadService, LoadLeadService>();
            services.AddScoped<ILoadLiveLeadService, LoadLiveLeadService>();
            services.AddScoped<ILoadPositionService, LoadPositionService>();
            services.AddScoped<ILoadPostingService, LoadPostingService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberSearchService, MemberSearchService>();
            services.AddScoped<INetworkMembersService, NetworkMembersService>();
            services.AddScoped<INetworksService, NetworksService>();
            services.AddScoped<IPDRatioService, PDRatioService>();
            services.AddScoped<IRepostAllService, RepostAllService>();
            services.AddScoped<IRIRateService, RIRateService>();
            services.AddScoped<ITemplatePostingService, TemplatePostingService>();
            services.AddScoped<IUSCarrierSearchService, USCarrierSearchService>();
            services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            services.AddScoped<IUSMemberSearchService, USMemberSearchService>();
            services.AddScoped<IVehicleAttributeService, VehicleAttributeService>();
            services.AddScoped<IVehicleSizeService, VehicleSizeService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();

            // local services
            services.AddScoped<IUserHelperService, UserHelperService>();
            //Changes in 2021-9-21
            
            var matchingConfig = configuration.GetSection("MatchingConfig").Get<MatchingConfig>();
            services.AddSingleton<IMatchingConfig>(matchingConfig);

            var MqConfig = configuration.GetSection("MqConfig").Get<MqConfig>();
            services.AddSingleton(MqConfig);

            services.AddScoped<IEquipmentPostingRepository, EquipmentPostingRepository>();

            services.AddMediatR(typeof(PostingBase).Assembly, typeof(CreateEquipmentPostingCommandHandler).Assembly, typeof(Startup).Assembly);
            services.AddTransient<IFillNotPlatformPosting, FillingNotPlatformPosting>();

            services.AddTransient<IMatchingServiceFactory, MatchingServiceFactory>();
            services.AddScoped(typeof(IPublishIntegrationEvent<>), typeof(IntegrationEventPublisher<>));
            



        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        }

        public static void AddCache(this IServiceCollection services)
        {
            // cached data models
            // ==================
            // userapikey setting
            services.AddSingleton<ICacheRepository<UserApiKeyQuery>, CacheRepository<UserApiKeyQuery>>();

            // VehicleSize setting
            services.AddSingleton<ICacheRepository<IEnumerable<GetVehicleSizeQuery>>, CacheRepository<IEnumerable<GetVehicleSizeQuery>>>();

            // VehicleType setting
            services.AddSingleton<ICacheRepository<IEnumerable<GetVehicleTypesQuery>>, CacheRepository<IEnumerable<GetVehicleTypesQuery>>>();

            // VehicleAttribute setting
            services.AddSingleton<ICacheRepository<IEnumerable<GetVehicleAttributeQuery>>, CacheRepository<IEnumerable<GetVehicleAttributeQuery>>>();
            // ==================
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
                          Sentry.SentryId eventId;
                          eventId = Sentry.SentrySdk.CaptureEvent(new Sentry.SentryEvent(exception));

                          errorText = System.Text.Json.JsonSerializer.Serialize(new List<dynamic> { error, "EventId:" + eventId });

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

        public static void AddRedisCaching(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = services.BuildServiceProvider().GetRequiredService<IOptionsSnapshot<AppSettings>>();
            var redisConfig = appSettings.Value.RedisConfiguration;

            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = "LoadLinkCentralCaching";
                options.Configuration = $"{redisConfig.Server},abortConnect=false,password={redisConfig.Password ?? "L0@dL1nk#1"}";

            });

            AddCache(services);
        }
    }
}

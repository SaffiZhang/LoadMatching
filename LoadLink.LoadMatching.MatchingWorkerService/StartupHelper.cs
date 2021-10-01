
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


namespace LoadLink.LoadMatching.WorkerService
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

            
            
            //Changes in 2021-9-21
            
            var matchingConfig = configuration.GetSection("MatchingConfig").Get<MatchingConfig>();
            services.AddSingleton<IMatchingConfig>(matchingConfig);

            var MqConfig = configuration.GetSection("MqConfig").Get<MqConfig>();
            services.AddSingleton(MqConfig);

            services.AddScoped<IEquipmentPostingRepository, EquipmentPostingRepository>();

            services.AddMediatR(typeof(PostingBase).Assembly, typeof(CreateEquipmentPostingCommandHandler).Assembly, typeof(LoadMatchingService).Assembly);
            services.AddTransient<IFillNotPlatformPosting, FillingNotPlatformPosting>();

            services.AddTransient<IMatchingServiceFactory, MatchingServiceFactory>();
            services.AddHostedService<LoadMatchingService>();
            



        }   

    }
}

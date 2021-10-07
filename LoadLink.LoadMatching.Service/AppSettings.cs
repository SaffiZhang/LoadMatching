// ***********************************************************************
// Assembly         : LoadLink.LoadMatching.Api
// Author           : jbuenaventura
// Created          : 2021-01-02
//
// Last Modified By : jbuenaventura
// Last Modified On : 2021-01-02
// ***********************************************************************
// <copyright file="AppSettings.cs" company="LoadLink.LoadMatching.Api">
//     Copyright (c) LoadLink Technologies. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using LoadLink.LoadMatching.IntegrationEventManager;
namespace LoadLink.LoadMatching.MatchingService
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
       
        
        public MatchingConfig MatchingConfig { get; set; }
        public MqConfig MqConfig { get; set; }
    }


    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

   

}

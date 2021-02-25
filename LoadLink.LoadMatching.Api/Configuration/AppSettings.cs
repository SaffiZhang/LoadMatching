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

namespace LoadLink.LoadMatching.Api.Configuration
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public ServiceCacheSettings ServiceCacheSettings { get; set; }
        public IdentityServer IdentityServer { get; set; }
        public string[] AllowedCorsOrigin { get; set; }
        public string MileageProvider { get; set; }
        public AppSetting AppSetting { get; set; }
        public RedisConfiguration RedisConfiguration { get; set; }
    }


    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class ServiceCacheSettings
    {
        public CacheSetting DefaultCacheSetting { get; set; }
        public CacheSetting UserApiKeySetting { get; set; }
        public CacheSetting ApiDataSetting { get;set;}
    }

    public class CacheSetting
    {
        public int AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }
    }

    public class IdentityServer
    {
        public string AuthorityUrl { get; set; }
    }

    public class AppSetting
    {
        public int LeadsCap { get; set; }
        public string MileageProvider { get; set; }
    }

    public class RedisConfiguration
    {
        public string Server { get; set; }
        public string Password { get; set; }
        public int ConnectTimeout { get; set; } = 5000; // milliseconds
        public int SyncTimeout { get; set; } = 5000; // milliseconds
    }
}

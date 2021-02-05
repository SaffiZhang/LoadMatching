﻿// ***********************************************************************
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
}
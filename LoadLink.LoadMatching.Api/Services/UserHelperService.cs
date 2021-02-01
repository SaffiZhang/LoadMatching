// ***********************************************************************
// Assembly         : LoadLink.LoadMatching.Api
// Author           : jbuenaventura
// Created          : 2021-01-02
//
// Last Modified By : jbuenaventura
// Last Modified On : 2021-01-02
// ***********************************************************************
// <copyright file="IUserHelperService.cs" company="LoadLink.LoadMatching.Api">
//     Copyright (c) LoadLink Technologies. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;

namespace LoadLink.LoadMatching.Api.Services
{
    public interface IUserHelperService
    {
        int GetUserId();
        int GetAccountId();
        string GetCustCd();
        Task<bool> HasValidSubscription(string apiKey);
    }

    public class UserHelperService : IUserHelperService
    {
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelperService(IHttpContextAccessor httpContextAccessor,
                                    IUserSubscriptionService userSubscriptionService = null)
        {
            _httpContextAccessor = httpContextAccessor;
            _userSubscriptionService = userSubscriptionService;
        }

        public string GetCustCd()
        {
            if (_httpContextAccessor?.HttpContext?.User?.Identity == null)
                throw new UnauthorizedAccessException();

            var claimsIdentity = _httpContextAccessor?.HttpContext.User.Identity as ClaimsIdentity;

            if (!claimsIdentity.IsAuthenticated)
                throw new UnauthorizedAccessException();

            return Convert.ToString(claimsIdentity.FindFirst("cust_cd").Value);
        }

        public int GetUserId()
        {
            if (_httpContextAccessor?.HttpContext?.User?.Identity == null)
                throw new UnauthorizedAccessException();

            var claimsIdentity = _httpContextAccessor?.HttpContext.User.Identity as ClaimsIdentity;

            if (!claimsIdentity.IsAuthenticated)
                throw new UnauthorizedAccessException();

            return Convert.ToInt32(claimsIdentity.FindFirst("id").Value);

        }

        public int GetAccountId()
        {
            if (_httpContextAccessor?.HttpContext?.User?.Identity == null)
                throw new UnauthorizedAccessException();

            var claimsIdentity = _httpContextAccessor?.HttpContext.User.Identity as ClaimsIdentity;

            if (!claimsIdentity.IsAuthenticated)
                throw new UnauthorizedAccessException();

            return Convert.ToInt32(claimsIdentity.FindFirst("account_id").Value);
        }

        public async Task<bool> HasValidSubscription(string apiKey)
        {
            var isApiKeyValid = await _userSubscriptionService.IsValidApiKey(GetUserId(), apiKey);

            if (!isApiKeyValid)
                return false;

            return true;
        }
    }
}

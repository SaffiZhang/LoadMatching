// ***********************************************************************
// Assembly         : LoadLink.LoadMatching.Application
// Author           : jbuenaventura
// Created          : 2021-01-02
//
// Last Modified By : jbuenaventura
// Last Modified On : 2021-01-02
// ***********************************************************************
// <copyright file="ICachRepository.cs" company="LoadLink.LoadMatching.Application">
//     Copyright (c) LoadLink Technologies. All rights reserved.
// </copyright>
// <summary>Caching of data</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Caching
{
    public interface ICacheRepository<T>
    {
        Task<IList<T>> GetMany(string keyName, Expression<Func<T, bool>> expression, Func<Task<List<T>>> callback);

        Task<IList<T>> GetAll(string keyName, Func<Task<List<T>>> callback);

        Task<T> GetSingle(string keyName, Func<Task<T>> callback);
    }
}

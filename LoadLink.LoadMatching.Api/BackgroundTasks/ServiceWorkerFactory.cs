using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace LoadLink.LoadMatching.Api.BackgroundTasks
{
    public class ServiceWorkerFactory : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly MqConfig _mqConfig;
        private List<LoadMatchingService> _LoadMatchingServices;

        public ServiceWorkerFactory(IServiceProvider services, MqConfig mqConfig)
        {
            _services = services;
            _mqConfig = mqConfig;
            _LoadMatchingServices = new List<LoadMatchingService>();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var s in _LoadMatchingServices)
                  s.StopAsync(cancellationToken);
                
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
                {
                    for (int i = 0; i < _mqConfig.MqCount; i++)
                    {
                        var service = new LoadMatchingService(_services, i);
                        _LoadMatchingServices.Add(service);
                        Task.Run(() => service.StartAsync(stoppingToken));
                       
                    }
                }, stoppingToken);
        }

    }
}

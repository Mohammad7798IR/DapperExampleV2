using GettingStarted;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DapperExample.Api
{
    public class Worker : BackgroundService
    {

        private readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new Message
                {

                    Text = "Hello"

                }, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;

namespace QuartzExample
{
    public class Scheduler
    {
        private readonly ISchedulerFactory _factory;

        private static Scheduler _instance;

        public static Scheduler Instance => _instance;

        public Task<IScheduler> Current => _factory.GetScheduler();

        public Scheduler(ISchedulerFactory factory)
        {
            _factory = factory;
            if (_instance == null)
            {
                _instance = this;
            }
        }
    }
}
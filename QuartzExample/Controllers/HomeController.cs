using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;

namespace QuartzExample.Controllers
{
	public class HomeController : Controller
	{
        /**
         * Alternatively you can just inject ISchedulerFactory and use:
         * var sched = await _factory.GetScheduler(); instead of var sched = await Scheduler.Instance.Current; 
         */
        //private readonly ISchedulerFactory _factory;
        //public HomeController(ISchedulerFactory factory)
        //{
        //    _factory = factory;
        //}

        public ActionResult Index()
		{
			return View();
		}

		public async Task<ActionResult> Start()
		{
			ViewBag.Message = "Job scheduler started.";

		    // get a scheduler, start the schedular before triggers or anything else
		    var sched = await Scheduler.Instance.Current;
		    await sched.Start();

		    // create job
		    var job = JobBuilder.Create<SimpleJob>()
		        .WithIdentity("job1", "group1")
		        .Build();

		    // create trigger
		    var trigger = TriggerBuilder.Create()
		        .WithIdentity("trigger1", "group1")
		        .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever())
		        .Build();

		    // Schedule the job using the job and trigger 
		    await sched.ScheduleJob(job, trigger);

            return View();
		}

		public async Task<ActionResult> Stop()
		{
			ViewBag.Message = "Job scheduler stopped.";

		    var sched = await Scheduler.Instance.Current;
            await sched.Shutdown();

            return View();
		}
	}

    public class SimpleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Hello, Job executed");
            return Task.FromResult(0);
        }
    }
}
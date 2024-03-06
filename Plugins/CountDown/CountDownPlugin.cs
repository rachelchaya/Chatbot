using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;

namespace CountDown
{
    public class CountDownPlugin : IPluginWithScheduler
    {
        IScheduler _scheduler;

        public CountDownPlugin(IScheduler scheduler) => _scheduler = scheduler;

        public static string _Id = "count-down";
        public string Id => _Id;

        public PluginOutput Execute(PluginInput input)
        {
            int interval = 0;
            bool isInterval= int.TryParse(input.Message, out interval);
            
            if(isInterval)
            {
               _scheduler.Schedule(TimeSpan.FromSeconds(interval), Id, ""); 
                return new PluginOutput("Countdown started.");
            }
           
            return new PluginOutput("Countdown can't start! The input is incorrect");

        }

        public void OnScheduler(string data)
        {
            Console.WriteLine("Fired.");
        }
    }
}

using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;

namespace Counter
{
    public class CounterPlugin : IPlugin
    {
        public static string _Id => "counter";
        public string Id => _Id;

        public PluginOutput Execute(PluginInput input)
        {

            if(input.PersistentData != null)
            {
                var lastCount = int.Parse(input.PersistentData);
                var result = (lastCount + 1).ToString();
                return new PluginOutput(result, result);
            }
            else
            {
                return new PluginOutput("Error: Input format is not valid", "Error");
            }
        }
    }
}
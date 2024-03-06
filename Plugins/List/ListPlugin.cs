using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ListPlugin
{
    record PersistentDataStructure(List<string> List);

    public class ListPlugin : IPlugin
    {
        public static string _Id = "list";
        public string Id => _Id;

        public PluginOutput Execute(PluginInput input)
        {
            List<string> list = new();
           
            string userInputMessage = input.Message;
            
            

            if (string.IsNullOrEmpty(input.PersistentData) == false)
            {
                list = JsonSerializer.Deserialize<PersistentDataStructure>(input.PersistentData).List;
            }

            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                return new PluginOutput("List started. Enter 'Add' to add task. Enter 'Delete' to delete task. Enter 'List' to view all list. Enter 'Exit' to stop.", input.PersistentData);
            }
            else if (String.Equals(userInputMessage, "exit", StringComparison.OrdinalIgnoreCase))
            {
                input.Callbacks.EndSession();
                return new PluginOutput("List stopped.", input.PersistentData);
            }
            else if (userInputMessage.StartsWith("add", StringComparison.OrdinalIgnoreCase))
            {
                var str = userInputMessage.Substring("add".Length).Trim();
             
                     list.Add(str.ToLower());

                     var data = new PersistentDataStructure(list);

                     return new PluginOutput($"New task: {str.ToLower()}", JsonSerializer.Serialize(data));
                     
   
            }
            else if (userInputMessage.StartsWith("delete", StringComparison.OrdinalIgnoreCase))
            {
                string name = "";
                var place = userInputMessage.Substring("delete".Length).Trim();
                int placeD = -1;
                bool isPlaceToDelete = int.TryParse(place, out placeD);
                
                try 
                { 
                if(isPlaceToDelete && placeD >= 0)
                {
                   name = list[placeD];
                   list.RemoveAt(placeD);
                }
                var data = new PersistentDataStructure(list);

                return new PluginOutput($"Delete  task: {name}", JsonSerializer.Serialize(data));
                }
                catch (Exception ex)
                {
                    if(placeD==-1)
                    {
                        {
                            placeD = list.Count - 1;
                            name = list[placeD];
                            list.RemoveAt(placeD);
                        }
                        var data = new PersistentDataStructure(list);

                        return new PluginOutput($"Delete  task: {name}", JsonSerializer.Serialize(data));
                    }
                    return new PluginOutput(ex.Message);
                }
              
               
            }
            else if (String.Equals(userInputMessage, "list", StringComparison.OrdinalIgnoreCase))
            {
                string listtasks = string.Join("\r\n", list);
                return new PluginOutput($"All list tasks:\r\n{listtasks}", input.PersistentData);
            }
            else
            {
                return new PluginOutput("Error! Enter 'Add' to add task. Enter 'Delete' to delete task. Enter 'List' to view all list. Enter 'Exit' to stop.");
            }
        }
    }
}

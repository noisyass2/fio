using System;
using FIOCore;
using System.Collections.Generic;

namespace FIOApp
{
    class Program
    {
        static GApp app = new GApp();
        static void Main(string[] args)
        {
            TObj tobj = new TObj();
            

            app.Updated += onUpdate;

            while(true)
            {
                var key = Console.ReadKey();
                
            //    Console.WriteLine(key);
                if(key.Key == ConsoleKey.X) break;

                switch(key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                        // Console.WriteLine(key.Key.ToString().Replace("D",""));
                        app.SwitchTab( Int16.Parse(key.Key.ToString().Replace("D","")));
                        Console.WriteLine(app.SelectedEType);
                        break;
                    case ConsoleKey.A:
                        app.AddToQueue(app.SelectedEType,1);
                        
                        break;
                    case ConsoleKey.D:
                        app.AddProduction(app.SelectedEType,1);
                        break;
                    case ConsoleKey.S:
                        app.AddProduction(app.SelectedEType,-1);
                        break;
                }
            }            
        }
        
        static EventHandler<GenericEventArgs> onUpdate = (sender, eventArgs) =>
        {
            // Console.WriteLine(eventArgs.Message);     

            Console.Clear();
            Console.WriteLine("TAB: " + app.SelectedEType);
            Console.WriteLine(app.Inventory.ToString());
            Console.WriteLine("%:" + app.QueueProgress);
            Console.WriteLine("MC : " + app.Inventory.MinCraftable);
        };
    }
}


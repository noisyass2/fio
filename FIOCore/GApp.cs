using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace FIOCore
{
    public class GApp
    {
        public event EventHandler<GenericEventArgs> Updated;
        public event EventHandler<GenericEventArgs> OresAdded;

        public List<Etype> Etypes { get; set; }
        
        public Inventory Inventory { get; set; }

        public List<Item> CQueue { get; set; }
        
        public Etype SelectedEType { get; private set; }

        public String LastKnownMessage { get; set; }

        Timer apptimer = null;
        AutoResetEvent are = null;
        int interval = 100;
        int ctr = 0;
        public int QueueProgress { get; set; }

        public GApp()
        {
            are = new AutoResetEvent(false);
            this.Etypes = new List<Etype>(){  Etype.Iron, Etype.Coal, Etype.Copper, Etype.IronIngot,Etype.CopperIngot, Etype.Drill, Etype.Smelter, Etype.Constructor };
            this.Inventory = new Inventory();
            this.CQueue = new List<Item>();
            apptimer = new Timer(Update,are,interval,interval);            
        }

        public void Update(Object stateinfo)
        {
            // Console.WriteLine(ctr);
            // ++ctr;

            // Process queue
            if(this.CQueue.Count > 0)
            {
                var item = CQueue.FirstOrDefault();
                item.Progress += 10;
                this.QueueProgress = item.Progress;
                if(item.Progress >= 100)
                {
                    // this.Inventory.Add(item.Etype,item.Amount);
                    this.Inventory.Craft(item.Etype,item.Amount);
                    this.CQueue.Remove(item);
                    this.QueueProgress = 0;
                }
            }


            // Process Productions
            foreach (var item in this.Inventory.Items)
            {
                if(item.Production > 0)
                {
                    item.Progress += item.Speed;
                    if(item.Progress >= 100)
                    {
                        this.Inventory.Craft(item.Etype,item.Production);
                        item.Progress = 0;
                    }
                }
            }

            
            Updated?.Invoke(this, new GenericEventArgs("done"));
        }

        public void SwitchTab(int i) => this.SelectedEType = this.Etypes[i - 1];

        public void AddToQueue(Etype etype, int cnt) => this.CQueue.Add(new Item(etype, cnt, 0));

        public void AddProduction(Etype etype, int prod)
        {
            GApp gApp = this;
            var item = gApp.Inventory.Items.FirstOrDefault(p => p.Etype == etype);
            var requiredMachine = gApp.Inventory.Items.FirstOrDefault(p => p.Etype == item.Machine);
            if(requiredMachine != null && (prod > 0 ? requiredMachine.Amount > 0 : item.Production > 0))
            {
                item.Production += prod;
                requiredMachine.Amount -= prod;
            }else{
                this.LastKnownMessage = "Missing " + item.Machine;
            }
        }
    }
}
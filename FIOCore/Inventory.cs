using System;
using System.Collections.Generic;
using System.Linq;

namespace FIOCore
{
    public class Inventory
    {
        public List<Item> Items { get; set; }
        public int MinCraftable { get; set; }

        public Inventory()
        {
            this.Items = new List<Item>(){
                new Item(Etype.Stone, 0,0,null,Etype.Drill),
                new Item(Etype.Iron, 0,0,null,Etype.Drill),
                new Item(Etype.Coal, 0,0,null,Etype.Drill),
                new Item(Etype.Copper, 0,0,null,Etype.Drill),
                new Item(Etype.IronIngot,0,0, new Recipe() { Requirements = new List<Item>() { new Item(Etype.Iron, 1)} },Etype.Smelter),
                new Item(Etype.CopperIngot,0,0, new Recipe() { Requirements = new List<Item>() { new Item(Etype.Copper, 1)} },Etype.Smelter),
                new Item(Etype.Drill, 0,0,new Recipe() { Requirements = new List<Item>() { new Item(Etype.IronIngot, 5)} },Etype.Constructor),                                
                new Item(Etype.Smelter, 0,0,new Recipe() { Requirements = new List<Item>() { new Item(Etype.IronIngot, 5), new Item(Etype.CopperIngot, 5) }},Etype.Constructor),
                new Item(Etype.Constructor, 0,0,new Recipe() { Requirements = new List<Item>() { new Item(Etype.IronIngot, 10), new Item(Etype.CopperIngot, 10)} },Etype.Constructor2),
            };
        }
        public void Add(Etype item, int cnt)
        {
            Item itm = Items.FirstOrDefault(p => p.Etype == item);
            itm.Amount += cnt;
        }   
        
        public override string ToString()
        {
            return String.Join(Environment.NewLine,this.Items.Select(p => p.ToString()));
        }

        public void Craft(Etype etype, int amt)
        {
            var item = Items.FirstOrDefault(p => p.Etype == etype);
            Console.WriteLine(item.Etype);
            // Check Recipe
            if(item.Recipe != null && item.Recipe.Requirements.Count > 0){
                // Check if amount is okay.
                int minCraftable = amt;
                foreach (var ritem in item.Recipe.Requirements)
                {
                    var ramt = Items.FirstOrDefault(p => p.Etype == ritem.Etype).Amount;
                    if(ramt < ritem.Amount * amt)
                    {
                        // take minimum
                        minCraftable = (ramt / (ritem.Amount) < minCraftable) ? (ramt / (ritem.Amount)) : minCraftable ;
                
                    }
                }
                Console.WriteLine("craftable:" + minCraftable);
                this.MinCraftable = minCraftable;
                if(minCraftable > 0)
                {
                    // reduce requirements.
                    foreach (var ritem in item.Recipe.Requirements)
                    {
                        Items.FirstOrDefault(p => p.Etype == ritem.Etype).Amount -= (ritem.Amount * minCraftable);
                    }
                    // add amount
                    item.Amount += minCraftable;
                }
            }else{
                // No Recipe, just spawn one
                item.Amount += amt;
            }
        }
    }

}
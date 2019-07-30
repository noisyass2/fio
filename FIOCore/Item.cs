
using System;

namespace FIOCore
{
    public class Item
    {
        public FIOCore.Etype Etype { get; set; }
        public int Amount { get; set; }
        public int Production { get; set; }
        public int Progress { get; set; }
        public Recipe Recipe { get; set; }
        public int Speed { get; set; }
        public Item(FIOCore.Etype etype, int amt, int prod) : this(etype,  amt,  prod, null) {}
        
        public Item(FIOCore.Etype etype, int amt) : this(etype,amt,0,null,Etype.Constructor) {}

        public Item(FIOCore.Etype etype, int amt, int prod, Recipe recipe) : this(etype,amt,0,recipe,Etype.Constructor) {}

        public Item(FIOCore.Etype etype, int amt, int prod, Recipe recipe, FIOCore.Etype machine) 
        {
            this.Etype = etype;
            this.Amount = amt;
            this.Production = prod;    
            this.Recipe = recipe;      
            this.Machine = machine;
            this.Speed = 5;
        }

        public override string ToString() => this.Etype.ToString() + ":" + this.Amount.ToString() + "[" + this.Production.ToString() + "]";

        public FIOCore.Etype Machine { get; set; }
        
    }

}
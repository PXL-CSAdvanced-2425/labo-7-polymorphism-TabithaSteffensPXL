using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    internal class General : Machine
    {
        protected override float LifeSpanCostPerMinute
        {
            get
            {
                return 1;
            }
        }

        public General(string name): base(name)
        {
            base.LifeSpan = 1000;
        }
        public override void Use(int numberOfMinutes)
        {
            this.LifeSpan -= (int)(numberOfMinutes * this.LifeSpanCostPerMinute);
        }

        public override string ToString()
        {
            return $"{this.Name} {base.LifeSpanInfo()}";
        }
    }
}

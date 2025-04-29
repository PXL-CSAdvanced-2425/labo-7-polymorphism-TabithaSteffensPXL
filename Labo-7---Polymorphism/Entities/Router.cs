using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    public class Router : Machine
    {
        #region Properties

        public double WorkSpaceWidth { get; set; }

        public double WorkSpaceLength { get; set; }

        public double PricePerMinute { get; set; }
        protected override float LifeSpanCostPerMinute
        {
            get
            {
                return 50;
            }
        }
        #endregion

        #region Constructors
        public Router(string name, double length, double width, double pricePerMinute) : base(name) //ctor chaining, necessary for abstract classes
        {
            this.WorkSpaceLength = length;
            this.WorkSpaceWidth = width;
            this.PricePerMinute = pricePerMinute;
            base.LifeSpan = 25000;
        }
        #endregion

        #region Methods
        public override void Use(int numberOfMinutes)
        {
            base.LifeSpan -=  (int)(numberOfMinutes * this.LifeSpanCostPerMinute);

            
        }
        public override string ToString()
        {
            return $"'{base.Name.ToUpper()}' ({this.WorkSpaceLength}x{this.WorkSpaceWidth}) {base.LifeSpanInfo()}";
        }
        #endregion
    }
}

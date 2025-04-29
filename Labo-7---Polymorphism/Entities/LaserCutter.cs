using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    public class LaserCutter : Router
    {

        public double Accuracy { get; set; }

        public float LifeSpanCostPerMinute
        {
            get
            {
                return 1500;
            }
        }
        public LaserCutter(string name, double length, double width, double pricePerMinute, double accuracy) : base(name, length, width, pricePerMinute)
        {
            this.Accuracy = accuracy;
            base.LifeSpan = 5000;
        }

        public override void Use(int numberOfMinutes)
        {
            base.LifeSpan -= (int)((numberOfMinutes * this.LifeSpanCostPerMinute) - 100);

        }

        public override string ToString()
        {
            return $"'{base.Name.ToUpper()}'({this.WorkSpaceLength}x{this.WorkSpaceWidth})[Accuracy: {this.Accuracy}] {base.LifeSpanInfo()}";
        }
    }
}

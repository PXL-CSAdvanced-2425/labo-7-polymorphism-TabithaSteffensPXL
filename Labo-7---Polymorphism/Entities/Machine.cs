using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    public abstract class Machine
    {

        #region Properties
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _lifeSpan;

        public int LifeSpan
        {
            get { return _lifeSpan; }
            set { _lifeSpan = value; }
        }
        private float _price;

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public bool OutOfUse
        {
            get
            {
               return this.LifeSpan <= 0;
            }
        }
        protected abstract float LifeSpanCostPerMinute { get; }
        #endregion

        #region Constructors
        public Machine(string name)
        {
            this.Name = name; 
        }
        #endregion

        #region Methods
        public abstract void Use(int numberOfMinutes);

        public string LifeSpanInfo()
        {
            if (this.OutOfUse)
            {
                return "OUT OF USE";
            }
            else
            {
                return $"<lifespan: {this.LifeSpan} h>";
            }
        }
        public override string ToString()
        {
            return this.LifeSpanInfo();
        }
        #endregion
    }
}

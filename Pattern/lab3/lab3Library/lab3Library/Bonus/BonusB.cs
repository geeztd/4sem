using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Library.Bonus
{
    public class BonusB1 : IBonus
    {
        public float cost1hour { get; set; }
        public float a { get; set; }
        public BonusB1(float cost1hour, float a)
        {
            this.cost1hour = cost1hour;
            this.a = a;
        }
        public float calc(float number_hours) => (number_hours + a) * cost1hour;
    }
    public class BonusB2 : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float a { get; set; }
        public BonusB2(float cost1hour, float x, float a)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.a = a;
        }

        public float calc(float number_hours) => (number_hours + a) * cost1hour * x;
    }
    public class BonusB3 : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float a { get; set; }
        public BonusB3(float cost1hour, float x, float y, float a)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.y = y;
            this.a = a;
        }

        public float calc(float number_hours) => (number_hours + a) * cost1hour * x + y;
    }
}

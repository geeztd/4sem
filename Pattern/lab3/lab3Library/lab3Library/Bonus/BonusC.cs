using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Library.Bonus
{
    public class BonusC1 : IBonus
    {
        public float cost1hour { get; set; }
        public float a { get; set; }
        public float b { get; set; }
        public BonusC1(float cost1hour, float a, float b)
        {
            this.cost1hour = cost1hour;
            this.a = a;
            this.b = b;
        }
        public float calc(float number_hours) => (number_hours + a) * (cost1hour + b);
    }
    public class BonusC2 : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float a { get; set; }
        public float b { get; set; }
        public BonusC2(float cost1hour, float x, float a, float b)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.a = a;
            this.b = b;
        }
        public float calc(float number_hours) => (number_hours + a) * (cost1hour + b) * x;
    }
    public class BonusC3 : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float a { get; set; }
        public float b { get; set; }
        public BonusC3(float cost1hour, float x, float y, float a, float b)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
        }

        public float calc(float number_hours) => (number_hours + a) * (cost1hour + b) * x + y;
    }
}

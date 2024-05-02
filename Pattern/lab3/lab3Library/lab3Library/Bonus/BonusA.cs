using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Library.Bonus
{
    public class BonusA1 : IBonus
    {
        public float cost1hour { get; set; }
        public BonusA1(float cost1hour) => this.cost1hour = cost1hour;
        public float calc(float number_hours) => number_hours * cost1hour;
    }
    public class BonusA2 : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public BonusA2(float cost1hour, float x)
        {
            this.cost1hour = cost1hour;
            this.x = x;
        }
        public float calc(float number_hours) => number_hours * cost1hour * x;
    }
    public class BonusA3 : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public BonusA3(float cost1hour, float x, float y)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.y = y;
        }
        public float calc(float number_hours) => number_hours * cost1hour * x + y;
    }
}

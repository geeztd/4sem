using lab3Library.Bonus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Library.Factory
{
    public class FactoryL3 : IFactory
    {
        public float a { get; set; }
        public float b { get; set; }
        public FactoryL3(float a, float b)
        {
            this.a = a;
            this.b = b;
        }
        public IBonus getA(float cost1hour) => new BonusC1(cost1hour, a, b);

        public IBonus getB(float cost1hour, float x) => new BonusC2(cost1hour, x, a, b);

        public IBonus getC(float cost1hour, float x, float y) => new BonusC3(cost1hour, x, y, a, b);
    }
}

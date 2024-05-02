
using lab3Library.Bonus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Library.Factory
{
    public class FactoryL2 : IFactory
    {
        public FactoryL2(float a) => A = a;
        public float A { get; set; }
        public IBonus getA(float cost1hour) => new BonusB1(cost1hour, A);

        public IBonus getB(float cost1hour, float x) => new BonusB2(cost1hour, x, A);

        public IBonus getC(float cost1hour, float x, float y) => new BonusB3(cost1hour, x, y, A);

    }
}


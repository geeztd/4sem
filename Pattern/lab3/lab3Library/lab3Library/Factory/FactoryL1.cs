using lab3Library.Bonus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Library.Factory
{
    public class FactoryL1 : IFactory
    {
        public IBonus getA(float cost1hour) => new BonusA1(cost1hour);

        public IBonus getB(float cost1hour, float x) => new BonusA2(cost1hour, x);

        public IBonus getC(float cost1hour, float x, float y) => new BonusA3(cost1hour, x, y);


    }
}

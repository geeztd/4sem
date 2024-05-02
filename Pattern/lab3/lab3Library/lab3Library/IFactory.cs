using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Library
{
    public interface IFactory
    {
        IBonus getA(float cost1hour);
        IBonus getB(float cost1hour, float x);
        IBonus getC(float cost1hour, float x, float y);

    }
}

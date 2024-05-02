using lab3Library.Factory;
using System.Reflection.Metadata.Ecma335;

namespace lab3Library
{
    static public partial class Lec03Lib
    {
        static public IFactory GetL1() => new FactoryL1();
        static public IFactory GetL2(float a) => new FactoryL2(a);
        static public IFactory GetL3(float a, float b) => new FactoryL3(a, b);
    }
}
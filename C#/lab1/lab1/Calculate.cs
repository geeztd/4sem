using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public interface ICalc
    {
        int Oper_And(int num1, int num2);
        int Oper_Or(int num1, int num2);
        int Oper_No(int num1);
        int Oper_Xor(int num1, int num2);


    }
    internal class Calculate : ICalc
    {
        public int Oper_And(int num1, int num2)
        {
            return num1 & num2;
        }
        public int Oper_Or(int num1, int num2)
        {
            return num1 | num2;
        }

        public int Oper_No(int num1)
        {
            return ~num1;
        }

        public int Oper_Xor(int num1, int num2)
        {
            return num1 ^ num2;
        }




    }
}

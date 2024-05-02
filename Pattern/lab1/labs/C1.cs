using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class C1
    {
        private const int _prConst = 0;
        public const int pConst = 1;
        protected const int cConst = 2;

        private string _prField = "0";
        public string pField = "1";
        protected string cField = "2";

        private int PrProperty => _prConst;
        public string PProperty { get => _prField; set => pField = value; }
        protected int CProperty => cConst;

        public C1() { }
        public C1(string prF, string pF, string cF)
        {
            this._prField = prF;
            this.pField = pF;
            this.cField = cF;
        }
        public C1(C1 c)
        {
            this._prField = c._prField;
            this.pField = c.pField;
            this.cField = c.cField;
        }

        private void prMethod()
        {
            Console.WriteLine("prMethod");
        }
        public void pMethod()
        {
            Console.WriteLine("pMethod");
        }
        protected void cMethod()
        {
            Console.WriteLine("cMethod");
        }
    }

    public interface I1
    {
        public string iField { get; set; }
        public string this[int index] { get; set; }
        public void iMethod(string mes) { }
        public event Action Act;

    }
    public class C2 : C1, I1
    {
        public string iField { get; set; }
        private const int _prConst = 0;
        private int PrProperty => _prConst;

        public void iMethod(string mes)
        {
            Console.WriteLine(mes);
        }
        public event Action? Act;
        public string this[int index] { get => iField; set => iField = value; }

        //public C2(string iField, string prF, string pF, string cF) : base(prF, pF, cF)
        //{
        //    this.iField = iField;
        //}

        public C2(string iField)
        {
            this.iField = iField;
        }
        private void prMethod()
        {
            Console.WriteLine("prMethod");
        }
    }

    public class C3
    {
        public int? Field1;
        public int? Field2;

        public C3(int one, int two)
        {
            Field1 = one;
            Field2 = two;
        }
        public void MethodC3()
        {
            Console.WriteLine(Field1 + Field2);
        }
    }

    public class C4 : C3
    {
        public int Field3;
        public C4(int one, int two, int three) : base(one, two)
        {
            Field3 = three;
        }
        public void MethodC4()
        {
            Console.WriteLine(Field1 + Field2 + Field3);
        }
    }
}

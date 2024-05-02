using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Class
{
    internal class Book
    {
        public int id;
        public string name;
        public string discription;
        public string text;
        public byte[] img;

        public Book() { }
        public Book(int id, string name, string discription, string text, byte[] img)
        {
            this.id = id;
            this.name = name;
            this.discription = discription;
            this.text = text;
            this.img = img;
        }
    }
}

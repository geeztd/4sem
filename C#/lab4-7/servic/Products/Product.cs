using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace servic.Products
{
    [Serializable]
    public class Product
    {
        private string _id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

        public BitmapImage bitmapImage { get; set; }
        public string imgPath { get; set; }
        public string Creater { get; set; }
        public Category category { get; set; }
        public float Cost { get; set; }
        public Product() { }
        public Product(string name, string shortName, string description,
            string img, Category cat, float cost, string cr)
        {
            _id = Guid.NewGuid().ToString();
            Name = name;
            ShortName = shortName;
            Description = description;
            bitmapImage = new BitmapImage(new Uri(img));
            imgPath = img;
            category = cat;
            Cost = cost;
            Creater = cr;
        }

    }

    public enum Category
    {
        basketboll,
        volleyboll,
        weightlifting,
        All
    }
}


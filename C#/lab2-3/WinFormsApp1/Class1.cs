using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    using System.ComponentModel.DataAnnotations;

    public class IsIDKValid : ValidationAttribute
    {


        public IsIDKValid()
        {
            ErrorMessage = "Удк должен быть меньше 1000 и больше 0";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int str)
            {
                if (str > 0 && str < 1000)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }

    [Serializable]
    public class Book
    {
        public string Type { get; set; }
        public string Size { get; set; }
        public string Name { get; set; }
        [IsIDKValid]
        public int IDK { get; set; }
        public int Kol_list { get; set; }
        public string publishing_House { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTimeLoad { get; set; }
        public string YearCreate { get; set; }
        public List<Author> authorList { get; set; }

        public Book(string type, string size, string name, int idk, int kol_list, string pubHouse,
            DateTime dateTimeLoad, string year, List<Author> list)
        {
            Type = type;
            Size = size;
            Name = name;
            IDK = idk;
            Kol_list = kol_list;
            publishing_House = pubHouse;
            DateTimeLoad = dateTimeLoad;
            YearCreate = year;
            authorList = list;
        }
        public Book() { }
        public override string ToString()
        {
            string ret = $"Название: {Name}, ИДК: {IDK}, Тип: {Type},\n" +
                   $"Кол-во страниц: {Kol_list}, Размер: {Size}, Издательство: {publishing_House}\n" +
                   $"Год издания: {YearCreate}, Дата загрузки: {DateTimeLoad.ToShortDateString()}\n Авторы: ";
            foreach (var a in authorList)
            {
                ret += a.ToString() + "; ";
            }
            return ret;

        }

    }
    [Serializable]
    public class Author
    {
        public Guid ID { get; set; }
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "wrong length")]
        public string Name { get; set; }
        public string Contry { get; set; }

        public Author() { }
        public Author(string name, string contry)
        {
            ID = Guid.NewGuid();
            Name = name;
            Contry = contry;
        }

        public override string ToString()
        {
            return $"Имя: {Name},Страна: {Contry}";
        }

    }
}

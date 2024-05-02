using lab2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Search : Form
    {
        [Serializable]
        private class searchClass
        {
            public IEnumerable<Book> res;
            public int sort;
            public string searchComp;
            public string searchYear;
            public string searchLists;
            public searchClass(IEnumerable<Book> res, int sort, string searchComp, string searchYear, string searchLists)
            {
                this.res = res;
                this.sort = sort;
                this.searchComp = searchComp;
                this.searchYear = searchYear;
                this.searchLists = searchLists;
            }
        }


        private Form1 form;
        private List<Book> library;

        public Search(Form1 f, List<Book> lib)
        {
            InitializeComponent();
            form = f;
            library = lib;
        }

        private void Search_Load(object sender, EventArgs e)
        {
        }


        private void SearchButton_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"\d+-\d+");
            int start = 0, end = int.MaxValue;
            if (!regex.IsMatch(textBox3.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Некоректный диапозон страниц");
                return;
            }
            else if (!string.IsNullOrEmpty(textBox3.Text))
            {
                int index = textBox3.Text.IndexOf('-');
                start = int.Parse(textBox3.Text.Substring(0, index));
                end = int.Parse(textBox3.Text.Substring(index + 1));
            }

            string pubHouse = textBox1.Text.ToLower();
            string year = textBox2.Text;
            IEnumerable<Book> res;

            if (string.IsNullOrEmpty(pubHouse) && string.IsNullOrEmpty(year))
            {
                res = library.Where(x => x.Kol_list > start && x.Kol_list < end);
            }
            else if (string.IsNullOrEmpty(pubHouse) && !string.IsNullOrEmpty(year))
            {
                res = from b in library
                      where b.YearCreate.StartsWith(year) &&
                      b.Kol_list > start && b.Kol_list < end
                      select b;
            }
            else if (!string.IsNullOrEmpty(pubHouse) && string.IsNullOrEmpty(year))
            {
                res = from b in library
                      where b.publishing_House.ToLower().StartsWith(pubHouse) &&
                      b.Kol_list > start && b.Kol_list < end
                      select b;
            }
            else
            {
                res = from b in library
                      where b.publishing_House.ToLower().StartsWith(pubHouse) &&
                      b.YearCreate.ToLower().StartsWith(year) &&
                      b.Kol_list > start && b.Kol_list < end
                      select b;
            }
            form.upDateStatus("Выполнен поиск");
            form.Print(res);
            this.Close();
        }
    }
}



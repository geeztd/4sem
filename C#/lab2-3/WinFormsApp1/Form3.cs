using lab2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        private List<Author> authorList;
        private Form1 form;
        public Form3(Form1 f, List<Author> alist)
        {
            InitializeComponent();
            form = f;
            authorList = alist;
            string[] list = new string[alist.Count];
            for (int i = 0; i < alist.Count; i++)
            {

                list[i] = alist[i].ToString();
            }
            checkedListBox1.Items.AddRange(list);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = checkedListBox1.CheckedIndices;
            List<Author> list = new List<Author>();
            foreach (int i in a)
            {
                list.Add(authorList[i]);
            }
            form.addAuthorForBook(list);
            this.Close();
        }
    }
}

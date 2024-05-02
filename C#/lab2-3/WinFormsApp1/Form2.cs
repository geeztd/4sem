using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab2
{
    public partial class Form2 : Form
    {
        private Form1 form;
        private Regex reg1 = new Regex(@"^\w+\s\w+\s\w+$");
        private Regex reg2 = new Regex(@"^\w+\s\w+$");
        public Form2(Form1 f)
        {
            InitializeComponent();
            form = f;
        }

        private string[] array = { "Беларусь", "Россия", "Украина" };
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string country = array[comboBox1.TabIndex];
            comboBox1.TabIndex = 0;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(country))
            {
                MessageBox.Show("Вы не заполнили поля");
            }
            else if (!reg1.IsMatch(name) && !reg2.IsMatch(name))
            {
                MessageBox.Show("Некоректно введено фио");
            }
            else
            {
                form.AddAuthor(new Author(name, country));
                textBox2.Text = "";
            }

        }
        private void NumberText(object sender, EventArgs e)
        {
            var tbox = sender as System.Windows.Forms.TextBox;
            string newText = "";
            foreach (char c in tbox.Text)
            {
                if (Char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tbox.Text = newText;
            tbox.SelectionStart = tbox.Text.Length;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = array;
        }


    }
}


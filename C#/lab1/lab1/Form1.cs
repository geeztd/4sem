using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        private int _numSys = 10;
        private bool enable1, enable2, enable3;
        private Calculate calc = new Calculate();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.radioButton3.Checked = true;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            int lastSys = _numSys;
            if (rb.Checked)
            {
                _numSys = Convert.ToInt32(rb.Text.Remove(rb.Text.Length - 2, 2));
            }
            if (this.textBox3.Text.Length != 0)
            {
                print(Convert.ToInt32(this.textBox3.Text, lastSys));
            }
        }

        private void print(int num)
        {

            switch (_numSys)
            {
                case 2:
                    this.textBox3.Text = Convert.ToString(num, 2);
                    break;
                case 8:
                    this.textBox3.Text = Convert.ToString(num, 8);
                    break;
                case 10:
                    this.textBox3.Text = Convert.ToString(num, 10);
                    break;
                case 16:
                    this.textBox3.Text = Convert.ToString(num, 16);
                    break;
            }
        }
        private void textBox1_Change(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)) enable1 = true;
            else enable1 = false;
            textBox1.ForeColor = Color.Black;
            enable();
        }
        private void textBox2_Change(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text)) enable2 = true;
            else enable2 = false;
            textBox2.ForeColor = Color.Black;
            enable();

        }
        private void textBox3_Change(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)) enable3 = true;
            else enable3 = false;
        }
        private void enable()
        {
            if (enable1 && !enable2)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = false;

            }
            else if ((!enable1 && !enable2) || (!enable1 && enable2))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            if (enable1 || enable2 || enable3)
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }
        }
        private bool TryParseWithBase(string input, out int result, int baseValue)
        {
            try
            {
                result = Convert.ToInt32(input, baseValue);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }
        private void but_and_Click(object sender, EventArgs e)
        {
            int num1, num2;
            var b1 = TryParseWithBase(this.textBox1.Text, out num1, _numSys);
            var b2 = TryParseWithBase(this.textBox2.Text, out num2, _numSys);
            if (b1 && b2)
            {
                print(calc.Oper_And(num1, num2));
            }
            else
            {
                if (!b1) textBox1.ForeColor = Color.Red;
                if (!b2) textBox2.ForeColor = Color.Red;
                MessageBox.Show("Некоректные данные");
            }

        }

        private void but_or_Click(object sender, EventArgs e)
        {
            int num1, num2;
            var b1 = TryParseWithBase(this.textBox1.Text, out num1, _numSys);
            var b2 = TryParseWithBase(this.textBox2.Text, out num2, _numSys);
            if (b1 && b2)
            {
                print(calc.Oper_Or(num1, num2));
            }
            else
            {
                if (!b1) textBox1.ForeColor = Color.Red;
                if (!b2) textBox2.ForeColor = Color.Red;
                MessageBox.Show("Некоректные данные");
            }
        }

        private void but_no_Click(object sender, EventArgs e)
        {
            {
                int num1;
                var b1 = TryParseWithBase(this.textBox1.Text, out num1, _numSys);
                if (b1)
                {
                    print(calc.Oper_No(num1));
                }
                else
                {
                    textBox1.ForeColor = Color.Red;
                    MessageBox.Show("Некоректные данные");
                }

            }
        }



        private void but_xor_Click(object sender, EventArgs e)
        {
            {
                int num1, num2;
                var b1 = TryParseWithBase(this.textBox1.Text, out num1, _numSys);
                var b2 = TryParseWithBase(this.textBox2.Text, out num2, _numSys);
                if (b1 && b2)
                {
                    print(calc.Oper_Xor(num1, num2));
                }
                else
                {
                    if (!b1) textBox1.ForeColor = Color.Red;
                    if (!b2) textBox2.ForeColor = Color.Red;
                    MessageBox.Show("Некоректные данные");
                }

            }
        }

        private void but_clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            button5.Enabled = false;


        }
    }
}

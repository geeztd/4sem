using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinFormsApp1;


namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Radio = radioButton1.Text;
            sorttemp = library;
            timer.Tick += new System.EventHandler(timer_Tick);
            timer.Start();
            toolStripComboBox1.SelectedIndex = 0;
        }

        private IEnumerable<Book> sorttemp;
        private List<Author> authorList = new();
        private List<Author> authorsForBook = new();
        private List<Book> library = new();
        private string Radio;
        private System.Windows.Forms.Timer timer = new() { Interval = 1000 };
        private bool isUpdating = false;
        private Stack<IEnumerable<Book>> forwardStack = new Stack<IEnumerable<Book>>();
        private Stack<IEnumerable<Book>> backStack = new Stack<IEnumerable<Book>>();


        private void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();

        }

        private void Sort_Change(object sender, EventArgs e)
        {
            if (isUpdating) return;
            isUpdating = true;

            ToolStripComboBox? cb = sender as ToolStripComboBox;
            if (cb != null)
            {
                if (cb == toolStripComboBox1 && toolStripComboBox3.SelectedIndex != cb.SelectedIndex)
                {
                    toolStripComboBox3.SelectedIndex = cb.SelectedIndex;
                }
                else if (cb == toolStripComboBox3 && toolStripComboBox1.SelectedIndex != cb.SelectedIndex)
                {
                    toolStripComboBox1.SelectedIndex = cb.SelectedIndex;
                }

                if (string.IsNullOrEmpty(richTextBox2.Text))
                {
                    isUpdating = false;
                    return;
                }

                if (cb.SelectedIndex == 0)
                {
                    sorttemp = sorttemp.OrderBy(x => x.Name);
                }
                else
                {
                    sorttemp = sorttemp.OrderBy(x => x.DateTimeLoad);
                }

                Print(sorttemp);
            }

            isUpdating = false;
            upDateStatus("Выполнена сортировка");

        }

        public void upDateStatus(string mes)
        {
            toolStripStatusLabel2.Text = mes;
        }
        private void ToggleButton_Click(object sender, EventArgs e)
        {
            if (toolStrip1.Visible)
            {
                toolStrip1.Hide();
                button7.Text = "Показать";
                upDateStatus("Панель скрыта");

            }
            else
            {
                toolStrip1.Show();
                button7.Text = "Скрыть";
                upDateStatus("Панель открыта");

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            using (StreamReader sr = new("file.json"))
            {
                string? line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    library = new();
                }
                else library = JsonSerializer.Deserialize<List<Book>>(line);
            }
            toolStripStatusLabel1.Text = "Книг: " + library.Count().ToString();
            toolStripStatusLabel2.Text = string.Empty;
            radioButton1.Checked = true;
        }

        public void AddAuthor(Author a)
        {
            authorList.Add(a);
            upDateStatus("Дабавлен автор");
        }
        public void Print(IEnumerable<Book> arr)
        {
            if (arr.Count() == 0)
            {
                richTextBox2.Text = "По вашему запросу ничего не найдено";
            }
            else
            {
                if (toolStripComboBox1.SelectedIndex == 0)
                {
                    arr = arr.OrderBy(x => x.Name);
                }
                else
                {
                    arr = arr.OrderBy(x => x.DateTimeLoad);
                }
                sorttemp = arr;
                richTextBox2.Text = string.Empty;
                foreach (Book book in arr)
                {
                    richTextBox2.Text += book.ToString() + '\n';
                }
            }
            backStack.Push(arr);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Regex reg = new(@"[\w\s-]");
            int idk, kol_list;
            string name = richTextBox1.Text;
            bool Bidk = int.TryParse(textBox5.Text, out idk);
            bool Bkol_list = int.TryParse(textBox3.Text, out kol_list);
            string pubHouse = textBox6.Text;
            DateTime year = dateTimePicker1.Value;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pubHouse) ||
                !Bidk || !Bkol_list || string.IsNullOrEmpty(Radio))
            {
                MessageBox.Show("Заполните все поля");
            }
            else if (authorsForBook.Count == 0)
            {
                MessageBox.Show("Добавте автора");
            }
            else if (year > DateTime.Now)
            {
                MessageBox.Show("Выберите корректную дату");
            }
            else if (idk > 1000)
            {
                MessageBox.Show("Удк должен быть меньше 1000 и больше 0");
            }
            else if (kol_list <= 0)
            {
                MessageBox.Show("Неверное количество страниц");
            }
            else
            {
                library.Add(new Book(Radio, (kol_list * 1860 * 2).ToString(), name, idk, kol_list, pubHouse, DateTime.Now, year.Year.ToString(), new List<Author>(authorList)));
                authorsForBook.Clear();
                foreach (var item in this.Controls.OfType<System.Windows.Forms.TextBox>())
                {
                    item.Text = string.Empty;
                }
                richTextBox1.Text = string.Empty;
                toolStripStatusLabel1.Text = "Книг: " + library.Count().ToString();
            }
            upDateStatus("Добавлена книга");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (library.Count == 0) richTextBox2.Text = "Библиотека пуста";
            else Print(library);
            upDateStatus("Показаны книги");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton? rb = sender as RadioButton;
            if (rb != null)
            {
                Radio = rb.Text;
            }
            upDateStatus("Выбрана радиокнопка");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("file.json"))
            {
                string json = JsonSerializer.Serialize(library);
                sw.WriteLine(json);

            }
            upDateStatus("Библиотека сохранена");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("vbeta_0.72; Разработчик: Клецкий Владислав");
            upDateStatus("Показано о программе");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Search search = new(this, library);
            search.Show();
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {
            foreach (var item in this.Controls.OfType<System.Windows.Forms.TextBox>())
            {
                item.Text = string.Empty;
            }
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            upDateStatus("Выполнена очистка");
        }

        private void toolStripTextBox3_Click(object sender, EventArgs e)
        {
            library.Clear();
            toolStripStatusLabel1.Text = "Книг: " + library.Count().ToString();
            upDateStatus("Выполнено удаление");
        }

        private void toolStripTextBox4_Click(object sender, EventArgs e)
        {
            if (forwardStack.Count > 0)
            {
                Print(forwardStack.First());
                backStack.Push(forwardStack.First());
                forwardStack.Pop();
            }
        }

        private void toolStripTextBox5_Click(object sender, EventArgs e)
        {
            if (backStack.Count > 0)
            {
                Print(backStack.First());
                forwardStack.Push(backStack.First());
                backStack.Pop();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            using (StreamWriter sw = new("searchRes.json"))
            {
                string json = JsonSerializer.Serialize(sorttemp);
                sw.WriteLine(json);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form3 form3 = new(this, authorList);
            form3.Show();
        }
        public void addAuthorForBook(List<Author> l)
        {
            authorsForBook = l;
        }
    }
}

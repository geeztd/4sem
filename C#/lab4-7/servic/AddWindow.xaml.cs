using Microsoft.Win32;
using servic.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace servic
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public ProductStoreViewModel main;
        public string? selectedPath;
        public AddWindow(ProductStoreViewModel main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                UpdateImage(selectedFilePath);
            }
        }

        private void UpdateImage(string imagePath)
        {
            try
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
                MyImage.Source = bitmapImage;
                selectedPath = imagePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = TextName.Text;
            string shortName = TextShortName.Text;
            string creater = TextCreater.Text;
            string des = TextDesp.Text;
            float cost;
            bool bc = float.TryParse(TextCost.Text, out cost);
            int ind = ComboCat.SelectedIndex;
            Category cat;
            switch (ind)
            {
                case 0: cat = Category.basketboll; break;
                case 1: cat = Category.volleyboll; break;
                case 3: cat = Category.weightlifting; break;
                default: cat = Category.All; break;
            }
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(shortName) || string.IsNullOrEmpty(creater) ||
                string.IsNullOrEmpty(des) || string.IsNullOrEmpty(selectedPath) || !bc)
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
                main.AddProduct(new Product(name, shortName, des, selectedPath, cat, cost, creater));
                main.updateFilter();
                this.Close();
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

    }
}

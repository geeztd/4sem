using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Microsoft.Win32;
using servic.Products;

namespace servic
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public ProductStoreViewModel main;
        public Product item;
        public EditWindow(Product product, ProductStoreViewModel main)
        {
            InitializeComponent();
            DataContext = new EditViewModel(product);
            MyImage.Source = product.bitmapImage;
            this.main = main;
            item = product;
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
                item.bitmapImage = bitmapImage;
                main.Products.ResetItem(main.Products.IndexOf(item));
                main.updateFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }
        }
    }

    public class EditViewModel
    {
        public Product product { get; set; }


        public EditViewModel(Product product)
        {
            this.product = product;
        }
    }
}

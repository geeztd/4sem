using servic.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow(Product item, ProductStoreViewModel main)
        {
            InitializeComponent();
            _item = item;
            DataContext = _item;
            this.main = main;
        }
        private Product _item;
        private ProductStoreViewModel main;
        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            main.RemoveProduct(_item);
            main.updateFilter();
            this.Close();
        }
        private void EditProduct(object sender, RoutedEventArgs e)
        {
            EditWindow w = new EditWindow(_item, main);
            w.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("a");
        }
    }
}

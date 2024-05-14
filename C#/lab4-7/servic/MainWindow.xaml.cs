using servic.Products;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace servic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductStoreViewModel view;

        public MainWindow()
        {
            InitializeComponent();
            ProductStoreViewModel viewModel = new ProductStoreViewModel();
            viewModel.SetLanguage("en");
            DataContext = viewModel;
            view = viewModel;

            var projectDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
            var cursorPath = System.IO.Path.Combine(projectDir, "Languages/cursor.cur");
            var iconPath = System.IO.Path.Combine(projectDir, "Languages/icon.ico");
            Icon = new BitmapImage(new Uri(iconPath));
            Cursor = new Cursor(cursorPath);
        }


        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            var dict = new ResourceDictionary();
            dict.Source = styleBox.SelectedIndex switch
            {
                0 => new Uri("/Themes/Light.xaml", UriKind.Relative),
                1 => new Uri("/Themes/Dark.xaml", UriKind.Relative),
                _ => new Uri("/Themes/Light.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries[2] = dict;
        }
        private void OnFilterChanged(object sender, EventArgs e)
        {
            if (view != null)
                if (view.filtered != null)
                    FilterProducts();

        }

        private void FilterProducts()
        {
            if (MyComboBox != null && searchTextBox != null)
            {

                Category category = MyComboBox.SelectedIndex switch
                {
                    0 => Category.All,
                    1 => Category.basketboll,
                    2 => Category.volleyboll,
                    3 => Category.weightlifting,
                    _ => Category.All
                };
                string name = searchTextBox.Text;

                view.filtered.Clear();
                foreach (var item in view.Products)
                {
                    if ((item.category == category || MyComboBox.SelectedIndex == 0) && item.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    {
                        view.filtered.Add(item);
                    }
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lab7Window w = new();
            w.Show();
        }
    }

}
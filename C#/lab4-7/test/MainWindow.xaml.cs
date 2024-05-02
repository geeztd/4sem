using Lab_8.Class;
using Lab_8.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Lab_8.DB.DB;

namespace Lab_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<DataGrid> dataGrids = new List<DataGrid>(); // Список всех таблиц

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            MoveSelectedItem(-1, 0);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            MoveSelectedItem(1, 0);
        }

        private void MoveSelectedItem(int verticalOffset, int horizontalOffset)
        {
            // Получаем выбранный элемент в таблице
            object selectedItem = productDataGrid.SelectedItem;

            // Получаем индекс выбранной строки
            int selectedIndex = productDataGrid.SelectedIndex;

            if (selectedItem != null && selectedIndex != -1)
            {
                // Вычисляем новый индекс выбранной строки после смещения
                int newIndex = selectedIndex + verticalOffset;

                // Проверяем, чтобы новый индекс находился в пределах диапазона строк таблицы
                if (newIndex >= 0 && newIndex < productDataGrid.Items.Count)
                {
                    // Устанавливаем новый индекс выбранной строки
                    productDataGrid.SelectedIndex = newIndex;

                    // Прокручиваем таблицу к выбранной строке
                    productDataGrid.ScrollIntoView(productDataGrid.SelectedItem);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            CreateDatabaseIfNotExists();
            dataGrids.Add(productDataGrid);
            dataGrids.Add(orderDataGrid);


            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }



        private void SortOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем список пользователей и заказов.
            List<User> users = (List<User>)productDataGrid.ItemsSource;
            List<Order> orders = (List<Order>)orderDataGrid.ItemsSource;

            // Сортируем заказы по индексу пользователей.
            var sortedOrders = orders.OrderBy(order => users.FindIndex(user => user.UserID == order.UserID));

            // Обновляем источник данных таблицы заказов.
            orderDataGrid.ItemsSource = sortedOrders.ToList();
        }
        public class UserSorter
        {
            public List<Order> SortOrdersByUserId(List<Order> orders, List<User> users)
            {
                // Упорядочиваем заказы по userid
                var sortedOrders = orders.OrderBy(order => users.FindIndex(user => user.UserID == order.UserID));

                return sortedOrders.ToList();
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var userId = (int)((Button)sender).Tag;

            // Удаляем пользователя и его заказы
            Lab_8.DB.DB.DeleteOrdersByUserId(userId);
            Lab_8.DB.DB.DeleteUser(userId);


            // Обновляем данные в DataGrid
            List<User> users = (List<User>)Lab_8.DB.DB.GetAllUsers();
            productDataGrid.ItemsSource = users;

            // Сортируем и отображаем заказы
            SortAndDisplayOrders();
        }



        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            int myValue = (int)((Button)sender).Tag;

            EditUser edit = new EditUser(myValue);

            edit.Show();
        }

        private async void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingGrid.Visibility = Visibility.Visible;
            productDataGrid.Visibility = Visibility.Collapsed;
            orderDataGrid.Visibility = Visibility.Collapsed;

            List<User> users = await LoadDataAsync();
            List<Order> orders = await LoadOrdersAsync();

            LoadingGrid.Visibility = Visibility.Collapsed;
            productDataGrid.Visibility = Visibility.Visible;
            orderDataGrid.Visibility = Visibility.Visible;

            productDataGrid.ItemsSource = users;
            orderDataGrid.ItemsSource = orders;
        }
        private async Task<List<Order>> LoadOrdersAsync()
        {
            List<Order> orders = (List<Order>)Lab_8.DB.DB.GetAllOrders();
            List<User> users = (List<User>)Lab_8.DB.DB.GetAllUsers();

            // Создаем экземпляр UserSorter
            userSorter = new UserSorter();
            // Сортируем заказы по userid
            List<Order> sortedOrders = userSorter.SortOrdersByUserId(orders, users);

            return sortedOrders;
        }

        private async Task<List<User>> LoadDataAsync()
        {
            List<User> users = (List<User>)Lab_8.DB.DB.GetAllUsers();

            return users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = (List<User>)Lab_8.DB.DB.GetAllUsers();

            productDataGrid.ItemsSource = users;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddUser userWindow = new AddUser();
            userWindow.Show();
        }

        private void Button_Image(object sender, RoutedEventArgs e)
        {
            AddUser userWindow = new AddUser();
            userWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<User> users = (List<User>)Lab_8.DB.DB.GetAllUsers();

            productDataGrid.ItemsSource = users;
            SortAndDisplayOrders();
        }
        private UserSorter userSorter;

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var myValue = ((Button)sender).Tag;
            List<User> result = (List<User>)DB.DB.Sort((string)myValue);

            productDataGrid.ItemsSource = result;
            SortAndDisplayOrders();
        }
        private void SortAndDisplayOrders()
        {
            List<Order> orders = (List<Order>)Lab_8.DB.DB.GetAllOrders();
            List<User> users = (List<User>)productDataGrid.ItemsSource; // Получите пользователей из DataGrid

            if (userSorter == null)
            {
                userSorter = new UserSorter();
            }
            List<Order> sortedOrders = userSorter.SortOrdersByUserId(orders, users);

            orderDataGrid.ItemsSource = sortedOrders;
        }

    }
}
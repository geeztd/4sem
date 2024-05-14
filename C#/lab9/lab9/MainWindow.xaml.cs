using lab9.Class;
using lab9;
using System;
using System.CodeDom;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using lab9.Windows;

namespace lab9 {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            try {
                InitializeComponent();
                using (var context = new lab9.DB()) {
                    User user = new User {
                        FirstName = "Jonh",
                        LastName = "Voitov",
                        Address = "Petra 15",
                        Phone = "+3758475856",
                        Email = "Voitovvv@gmail.com"
                    };


                    // Пользователь 1
                    User user1 = new User {
                        FirstName = "Иван",
                        LastName = "Иванов",
                        Address = "ул. Ленина, 10",
                        Phone = "+375292345678",
                        Email = "ivanov@example.com"
                    };

                    context.UserRepository.Add(user1);

                    // Пользователь 2
                    User user2 = new User {
                        FirstName = "Мария",
                        LastName = "Петрова",
                        Address = "пр. Победителей, 20",
                        Phone = "+375291234567",
                        Email = "petrova@example.com"
                    };

                    context.UserRepository.Add(user2);

                    // Пользователь 3
                    User user3 = new User {
                        FirstName = "Алексей",
                        LastName = "Сидоров",
                        Address = "ул. Гагарина, 5",
                        Phone = "+375298765432",
                        Email = "sidorov@example.com"
                    };

                    context.UserRepository.Add(user3);

                    // Пользователь 4
                    User user4 = new User {
                        FirstName = "Елена",
                        LastName = "Козлова",
                        Address = "пр. Независимости, 15",
                        Phone = "+375296543210",
                        Email = "kozlova@example.com"
                    };

                    context.UserRepository.Add(user4);

                    // Пользователь 5
                    User user5 = new User {
                        FirstName = "Сергей",
                        LastName = "Игнатьев",
                        Address = "ул. Кирова, 30",
                        Phone = "+375299876543",
                        Email = "ignatyev@example.com"
                    };

                    context.UserRepository.Add(user);

                    context.SaveChanges();
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e) {
            var myValue = ((Button)sender).Tag;
            var bd = new lab9.DB();

            try {
                using (var context = new lab9.DB()) {
                    using (var transaction = context.Database.BeginTransaction()) {
                        var user = context.UserRepository.Find(myValue);

                        if (user != null) {
                            context.UserRepository.Remove(user);
                            transaction.Commit();
                            List<User> users = (List<User>)bd.UserRepository.GetAll();
                            productDataGrid.ItemsSource = users;
                        }
                        else {
                            transaction.Rollback();

                        }
                    }
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);

            }
        }

        private async void Button_DelOrder(object sender, RoutedEventArgs e) {
            var myValue = ((Button)sender).Tag;


            var bd = new lab9.DB();

            List<Orders> orders = (List<Orders>)bd.OrdersRepository.GetAll();
            dataGrid.ItemsSource = orders;
            try {
                using (var context = new lab9.DB()) {
                    var order = await context.Orders.FindAsync(myValue);

                    if (order != null) context.Orders.Remove(order);
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e) {
            int myValue = (int)((Button)sender).Tag;

            EditUser edit = new EditUser(myValue);

            edit.Show();
        }


        private async void LoadDataButton_Click(object sender, RoutedEventArgs e) {
            LoadingGrid.Visibility = Visibility.Visible;
            productDataGrid.Visibility = Visibility.Collapsed;

            List<User> data = await LoadDataAsync();

            LoadingGrid.Visibility = Visibility.Collapsed;
            productDataGrid.Visibility = Visibility.Visible;

            productDataGrid.ItemsSource = data;

            using (var context = new lab9.DB()) {
                var orders = context.Orders.Include(o => o.User).ToList();
                dataGrid.ItemsSource = orders;
            }
        }

        private async Task<List<User>> LoadDataAsync() {
            List<User> users;
            using (var context = new lab9.DB()) {
                users = (List<User>)context.Users.ToList();
            }
            return users;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            AddUser userWindow = new AddUser();
            userWindow.Show();
        }

        private void Button_Image(object sender, RoutedEventArgs e) {
            AddUser userWindow = new AddUser();
            userWindow.Show();
        }

        private void ButtonEmailSearch_Click(object sender, RoutedEventArgs e) {
            string searchText = BoxEmail.Text.ToLower();
            productDataGrid.Items.Filter = item => {
                var product = item as User;
                if (product == null) return false;

                return product.Email.ToLower().Contains(searchText);
            };
            productDataGrid.Items.Refresh();
        }
        private void ButtonNameSearch_Click(object sender, RoutedEventArgs e) {
            string searchFName = BoxFName.Text.ToLower() ?? string.Empty;
            string searchLName = BoxLName.Text.ToLower() ?? string.Empty;
            productDataGrid.Items.Filter = item => {
                var product = item as User;
                if (product == null) return false;

                return product.FirstName.ToLower().Contains(searchFName)
                && product.LastName.ToLower().Contains(searchLName);
            };
            productDataGrid.Items.Refresh();
        }
        private async void Button_Click_3(object sender, RoutedEventArgs e) {
            try {
                var myValue = ((Button)sender).Tag;
                using (var context = new lab9.DB()) {

                    List<User> result;

                    switch (myValue) {
                        case "FName":
                            result = await context.Users
                            .OrderBy(user => user.FirstName)
                            .ToListAsync();
                            break;
                        case "LName":
                            result = await context.Users
                            .OrderBy(user => user.LastName)
                            .ToListAsync();
                            break;
                        case "id":
                            result = await context.Users
                            .OrderBy(user => user.UserID)
                            .ToListAsync();
                            break;
                        case "Email":
                            result = await context.Users
                            .OrderBy(user => user.Email)
                            .ToListAsync();
                            break;
                        case "Phone":
                            result = await context.Users
                            .OrderBy(user => user.Phone)
                            .ToListAsync();
                            break;
                        case "Address":
                            result = await context.Users
                            .OrderBy(user => user.Address)
                            .ToListAsync();
                            break;
                        default:
                            result = await context.Users
                            .OrderBy(user => user.UserID)
                            .ToListAsync();
                            break;
                    }

                    productDataGrid.ItemsSource = result;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void BoxName_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(BoxFName.Text) || !string.IsNullOrEmpty(BoxLName.Text)) {
                ButtonName.IsEnabled = true;
            }
            else {
                ButtonName.IsEnabled = false;
            }
        }

        private void BoxEmail_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(BoxEmail.Text)) {
                ButtonEmail.IsEnabled = true;
            }
            else {
                ButtonEmail.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            productDataGrid.Items.Filter = null;
            productDataGrid.Items.Refresh();
        }
    }
}
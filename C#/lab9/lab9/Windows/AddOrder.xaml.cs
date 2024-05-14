using lab9.Class;
using lab9;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows;

namespace lab9.Windows {
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window {
        private int Id { get; set; }

        public AddOrder(int id) {
            InitializeComponent();

            Id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            string name = val.Text;
            if (name.Trim().Length > 0) {
                try {
                    if (!Regex.IsMatch(name, @"^[а-яА-Яa-zA-Z\s]+$")) {
                        throw new Exception("Некорректное имя заказа.");
                    }


                    using (var context = new DB()) {
                        /*User user = context.Users.Find(Id);*/
                        User user = context.UserRepository.Find(Id);
                        MessageBox.Show(Id.ToString());

                        Orders orders = new Orders() {
                            Name = name,
                            User = user
                        };

                        context.Orders.Add(orders);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                finally {
                    this.Close();
                }
            }
        }
    }
}

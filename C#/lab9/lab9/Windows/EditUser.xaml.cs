using lab9.Class;
using lab9.Windows;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;

namespace lab9.Windows {
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window {
        private int Id { get; set; }

        public EditUser(int id) {
            Id = id;
            InitializeComponent();

            try {





                using (var context = new lab9.DB()) {

                    User user = context.Users.Find(Id);
                    /*	User user = context.Users.Find(Id);*/

                    FirsName.Text = user.FirstName;
                    LastName.Text = user.LastName;
                    Phone.Text = user.Phone;
                    Email.Text = user.Email;
                    Address.Text = user.Address;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            try {
                string firstname = FirsName.Text.Trim();
                string lastname = LastName.Text.Trim();
                string address = Address.Text.Trim();
                string phone = Phone.Text.Trim();
                string email = Email.Text.Trim();


                if (!Regex.IsMatch(firstname, @"^[а-яА-Яa-zA-Z\s]+$")) {
                    throw new Exception("Некорректное имя пользователя.");
                }

                if (!Regex.IsMatch(lastname, @"^[а-яА-Яa-zA-Z\s]+$")) {
                    throw new Exception("Некорректная фамилия пользователя.");
                }

                if (!Regex.IsMatch(phone, @"^[0-9)(-+]+$")) {
                    throw new Exception("Некорректный номер телефона пользователя.");
                }

                if (!Regex.IsMatch(firstname, @"^[а-яА-Яa-zA-Z0-9)\s]+$")) {
                    throw new Exception("Некорректное имя пользователя.");
                }


                try {

                    using (var context = new lab9.DB()) {
                        /*User user = context.Users.GetById(Id);*/
                        User user = context.Users.Find(Id);
                        if (user != null) {
                            user.FirstName = firstname;
                            user.LastName = lastname;
                            user.Phone = phone;
                            user.Email = email;
                            user.Address = address;
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                this.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            AddOrder addOrder = new AddOrder(Id);
            addOrder.Show();
        }
    }
}
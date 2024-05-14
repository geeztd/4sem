using lab9.Class;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace lab9.Windows {
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>

    public partial class AddUser : Window {
        public AddUser() {
            InitializeComponent();
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


                User user = new User() {
                    FirstName = firstname,
                    LastName = lastname,
                    Address = address,
                    Phone = phone,
                    Email = email,
                };

                try {
                    using (lab9.DB context = new lab9.DB()) {
                        context.Users.Add(user);
                        context.SaveChanges();
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

        private void Button_Image(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == true) {
                try {
                    preview.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    preview.Width = 100;
                    preview.Height = 100;
                }
                catch {
                    MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
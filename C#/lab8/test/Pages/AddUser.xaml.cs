using Lab_8.Class;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lab_8.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }
        private void Button_Fill(object sender, RoutedEventArgs e)
        {
            // Заполнение данными полей
            FirstName.Text = "Ivan";
            LastName.Text = "Ivanov";
            Address.Text = "Pushkina 10";
            Phone.Text = "+71234567890kkk";
            Email.Text = "ivanov@example.com";
            UserID.Text = "8";
            OrderDate.SelectedDate = DateTime.Now;
            TotalAmount.Text = "1000";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstName.Text.Trim();
                string lastName = LastName.Text.Trim();
                string address = Address.Text.Trim();
                string phone = Phone.Text.Trim();
                string email = Email.Text.Trim();
                int userId = Convert.ToInt32(UserID.Text.Trim());

                // Получение данных о заказе из полей ввода
                DateTime orderDate = OrderDate.SelectedDate ?? DateTime.Now; // Если дата не выбрана, используйте текущую дату
                decimal totalAmount = decimal.Parse(TotalAmount.Text);



                byte[] imageData = null;
                if (preview.Source != null)
                {
                    BitmapImage bitmapImage = (BitmapImage)preview.Source;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        encoder.Save(stream);
                        imageData = stream.ToArray();
                    }
                }

                User user = new User()
                {
                    UserID = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    Phone = phone,
                    Email = email,
                    Image = imageData,
                };

                Order order = new Order()
                {
                    UserID = userId, // Подставьте ID пользователя в заказ
                    OrderDate = orderDate,
                    TotalAmount = totalAmount
                };

                // Вызов метода добавления пользователя и его заказа
                DB.DB.AddUserWithOrder(user, order);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    preview.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    preview.Width = 100;
                    preview.Height = 100;
                }
                catch
                {
                    MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

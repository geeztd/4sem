using Lab_8.Class;
using System;
using System.Windows;

namespace Lab_8.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private int Id { get; set; }

        public EditUser(int val)
        {
            User user = DB.DB.GetUserById(val);
            InitializeComponent();
            FirstName.Text = user.FirstName;
            LastName.Text = user.LastName;
            Address.Text = user.Address;
            Phone.Text = user.Phone;
            Email.Text = user.Email;
            Id = val;
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

                // Получение данных о заказе из полей ввода
                DateTime orderDate = OrderDate.SelectedDate ?? DateTime.Now; // Если дата не выбрана, используйте текущую дату
                decimal totalAmount = decimal.Parse(TotalAmount.Text);

                if (firstName.Length == 0 || lastName.Length == 0 ||
                    address.Length == 0 || phone.Length == 0 || email.Length == 0)
                {
                    throw new Exception("Error");
                }

                User user = new User()
                {
                    UserID = Id,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    Phone = phone,
                    Email = email
                };

                Order order = new Order()
                {
                    OrderID = Id, // Подставьте соответствующий идентификатор заказа
                    OrderDate = orderDate,
                    TotalAmount = totalAmount
                };

                // Вызов метода UpdateUser с обновленными данными о пользователе и заказе
                DB.DB.UpdateUser(Id, user, order);

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
    }
}
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Data.SqlClient;
using lab8.Class;

namespace lab8.DB
{
    internal static class DataBase
    {
        private static readonly string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public static SqliteConnection connection = new SqliteConnection(connectionString);

        public static void CreateDatabaseIfNotExists()
        {

            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;


            builder.InitialCatalog = "master";
            var masterConnectionString = builder.ConnectionString;


            using (var connection = new SqliteConnection(masterConnectionString))
            {
                connection.Open();


                var sql = $"SELECT COUNT(*) FROM sys.databases WHERE [name] = '{databaseName}'";
                using (var command = new SqliteCommand(sql, connection))
                {
                    var databaseExists = (int)command.ExecuteScalar() > 0;

                    if (!databaseExists)
                    {
                        sql = $"CREATE DATABASE {databaseName}";
                        using (var createCommand = new SqliteCommand(sql, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }


                        ExecuteSchemaScript(connectionString);
                    }
                }
            }
        }

        private static void ExecuteSchemaScript(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();


                var schemaScript = @"CCREATE TABLE ""Books"" (
	""book_id""	INTEGER,
	""Name""	TEXT NOT NULL,
	""Discription""	TEXT NOT NULL,
	""Text""	TEXT NOT NULL,
	""Img""	BLOB,
	PRIMARY KEY(""book_id"" AUTOINCREMENT)
);

CREATE TABLE ""Authors"" (
	""author_id""	INTEGER,
	""name""	TEXT NOT NULL,
	""secondname""	TEXT NOT NULL,
	PRIMARY KEY(""author_id"" AUTOINCREMENT)
);
CREATE TABLE ""BookAuthor"" (
	""id""	INTEGER,
	""book_id""	INTEGER,
	""author_id""	INTEGER,
	PRIMARY KEY(""id"" AUTOINCREMENT),
	FOREIGN KEY(""book_id"") REFERENCES ""Books""(""book_id"")
);";


                using (var command = new SqlCommand(schemaScript, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void OpenConnection()
        {
            if (connection == null)
            {
                connection = new SqliteConnection(connectionString);
            }
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }


        private static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        //public static void AddUserWithOrder(User user, Order order)
        //{
        //    SqliteTransaction transaction = null;
        //    try
        //    {
        //        OpenConnection();
        //        transaction = connection.BeginTransaction();

        //        // Валидация данных пользователя
        //        if (!Regex.IsMatch(user.FirstName, @"^[а-яА-Яa-zA-Z\s]+$"))
        //        {
        //            throw new Exception("Некорректное имя пользователя. Отмена транзакции...");
        //        }

        //        if (!Regex.IsMatch(user.LastName, @"^[а-яА-Яa-zA-Z\s]+$"))
        //        {
        //            throw new Exception("Некорректная фамилия пользователя. Отмена транзакции...");
        //        }

        //        if (!Regex.IsMatch(user.Phone, @"^[0-9)(-+]+$"))
        //        {
        //            throw new Exception("Некорректный номер телефона пользователя. Отмена транзакции...");
        //        }

        //        if (!Regex.IsMatch(user.UserID.ToString(), @"^\d+$"))
        //        {
        //            throw new Exception("Некорректный идентификатор пользователя. Отмена транзакции...");
        //        }

        //        // Добавление пользователя
        //        string userQuery = "INSERT INTO Users (FirstName, LastName, Email, Phone, Address, UserID, Image) VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @UserID, @Image)";
        //        using (SqliteCommand userCommand = new SqliteCommand(userQuery, connection, transaction))
        //        {
        //            userCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
        //            userCommand.Parameters.AddWithValue("@LastName", user.LastName);
        //            userCommand.Parameters.AddWithValue("@Email", user.Email);
        //            userCommand.Parameters.AddWithValue("@Phone", user.Phone);
        //            userCommand.Parameters.AddWithValue("@Address", user.Address);
        //            userCommand.Parameters.AddWithValue("@UserID", user.UserID);
        //            userCommand.Parameters.AddWithValue("@Image", user.Image);
        //            int userResult = userCommand.ExecuteNonQuery();
        //            if (userResult == 0)
        //            {
        //                throw new Exception("Не удалось добавить пользователя.");
        //            }
        //        }

        //        // Валидация данных заказа
        //        if (!Regex.IsMatch(order.TotalAmount.ToString(), @"^\d+$"))
        //        {
        //            throw new Exception("Некорректная стоимость заказа.");
        //        }

        //        // Добавление заказа
        //        string orderQuery = "INSERT INTO Orders (OrderID, UserID, OrderDate, TotalAmount) VALUES (@OrderID, @UserID, @OrderDate, @TotalAmount)";
        //        using (SqlCommand orderCommand = new SqlCommand(orderQuery, connection, transaction))
        //        {
        //            orderCommand.Parameters.AddWithValue("@OrderID", order.UserID);
        //            orderCommand.Parameters.AddWithValue("@UserID", order.UserID);
        //            orderCommand.Parameters.AddWithValue("@OrderDate", order.OrderDate);
        //            orderCommand.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
        //            int orderResult = orderCommand.ExecuteNonQuery();
        //            if (orderResult == 0)
        //            {
        //                throw new Exception("Не удалось добавить заказ.");
        //            }
        //        }

        //        // Подтверждение транзакции
        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (transaction != null)
        //        {
        //            transaction.Rollback();
        //        }
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}



        public static void AddUser(Book book)
        {
            OpenConnection();

            string query = "INSERT INTO Books (Name, Discription, Text, Img) VALUES (@Name, @Discription, @Text, @Img)";

            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", book.name);
                command.Parameters.AddWithValue("@LastName", book.discription);
                command.Parameters.AddWithValue("@Email", book.text);
                command.Parameters.AddWithValue("@Phone", book.img);

                int rowsAffected = command.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public static void DeleteBook(int bookId)
        {
            try
            {
                OpenConnection();
                string query = "DELETE FROM Books WHERE Book_Id=@ID";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookId);
                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public static void AddAuther(Author author)
        {
            SqliteTransaction transaction = null;
            try
            {
                OpenConnection();
                transaction = connection.BeginTransaction();


                string query = "INSERT INTO Authors (name, secondname) VALUES (@Name, @SecomdName)";
                using (SqliteCommand command = new SqliteCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Name", author.name);
                    command.Parameters.AddWithValue("@SecondName", author.secondname);
                    int result = command.ExecuteNonQuery();

                    if (result == 0)
                    {
                        throw new Exception("Не удалось добавить заказ.");
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }


        public static void UpdateBook(int bookId, Book book)
        {
            SqliteTransaction transaction = null;
            try
            {
                OpenConnection();
                transaction = connection.BeginTransaction();

                // Валидация данных пользователя
                if (string.IsNullOrEmpty(book.name))
                {
                    throw new Exception("Не введено имя книги. Отмена транзакции...");
                }

                if (string.IsNullOrEmpty(book.discription))
                {
                    throw new Exception("Не введено описание книги. Отмена транзакции...");
                }

                if (string.IsNullOrEmpty(book.text))
                {
                    throw new Exception("Не введено содержание книги. Отмена транзакции...");
                }

                if (book.img == null)
                {
                    throw new Exception("Не внесена обложка книги. Отмена транзакции...");
                }

                string userQuery = "UPDATE Books SET Name=@Name, Discription=@Discription, Text=@Text, Img=@Img WHERE Book_Id=@BookId";
                using (SqliteCommand userCommand = new SqliteCommand(userQuery, connection, transaction))
                {
                    userCommand.Parameters.AddWithValue("@BookId", bookId);
                    userCommand.Parameters.AddWithValue("@Name", book.name);
                    userCommand.Parameters.AddWithValue("@Discription", book.discription);
                    userCommand.Parameters.AddWithValue("@Text", book.text);
                    userCommand.Parameters.AddWithValue("@Img", book.img);
                    int userResult = userCommand.ExecuteNonQuery();
                    if (userResult == 0)
                    {
                        throw new Exception("Не удалось обновить данные пользователя.");
                    }
                }

                // Подтверждение транзакции
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public static Book GetUserById(int BookId)
        {
            try
            {
                OpenConnection();

                string query = "SELECT * FROM Books WHERE Book_id = @BookId";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", BookId);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string Name = reader.GetString(1);
                            string Discription = reader.GetString(2);
                            string Text = reader.GetString(3);
                            byte[] image = (byte[])reader["Img"];

                            Book book = new(id, Name, Discription, Text, image);

                            return book;
                        }
                        else
                        {
                            throw new Exception("Книга с указанным идентификатором не найден.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }






        public static IEnumerable<Book> Sort(string sortBy)
        {
            var users = GetAllUsers().ToList();

            switch (sortBy.ToLower())
            {
                case "id":
                    users.Sort((u1, u2) => u1.UserID.CompareTo(u2.UserID));
                    break;

                case "firstname":
                    users.Sort((u1, u2) => u1.FirstName.CompareTo(u2.FirstName));
                    break;

                case "lastname":
                    users.Sort((u1, u2) => u1.LastName.CompareTo(u2.LastName));
                    break;

                case "email":
                    users.Sort((u1, u2) => u1.Email.CompareTo(u2.Email));
                    break;

                case "phone":
                    users.Sort((u1, u2) => u1.Phone.CompareTo(u2.Phone));
                    break;

                case "address":
                    users.Sort((u1, u2) => u1.Address.CompareTo(u2.Address));
                    break;

                default:
                    throw new ArgumentException("Invalid sort parameter");
            }

            return users;
        }

        public static IEnumerable<Author> GetAllOrders()
        {
            try
            {
                OpenConnection();
                string query = "SELECT * FROM Orders";
                List<Author> orders = new List<Author>();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string orderId = reader.GetString(0);
                            string userId = reader.GetString(1);
                            DateTime orderDate = reader.GetDateTime(2);
                            decimal totalAmount = reader.GetDecimal(3);

                            Author order = new Author
                            {
                                name = orderId,
                                secondname = userId,

                            };

                            orders.Add(order);
                        }
                        reader.Close();
                        return orders;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally { CloseConnection(); }
        }
        public static IEnumerable<Book> GetAllUsers()
        {
            try
            {
                OpenConnection();
                string query = "SELECT * FROM USERS";
                List<> usBookers = new List<>();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            string email = reader.GetString(3);
                            string phone = reader.GetString(4);
                            string address = reader.GetString(5);
                            byte[] image = (byte[])reader["Image"];
                            User user = new User
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Email = email,
                                Phone = phone,
                                Address = address,
                                UserID = id,
                                Image = image
                            };

                            users.Add(user);
                        }
                        reader.Close();
                        return users;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally { CloseConnection(); }
        }




        public interface ISortStrategy
        {
            IEnumerable<User> Sort(IEnumerable<User> users);
        }

        public class SortById : ISortStrategy
        {
            public IEnumerable<User> Sort(IEnumerable<User> users)
            {
                return users.OrderBy(u => u.UserID);
            }
        }

        public class SortByFirstName : ISortStrategy
        {
            public IEnumerable<Book> Sort(IEnumerable<Book> users)
            {
                return users.OrderBy(u => u.FirstName);
            }
        }

        public class SortByLastName : ISortStrategy
        {
            public IEnumerable<User> Sort(IEnumerable<User> users)
            {
                return users.OrderBy(u => u.LastName);
            }
        }

        public class SortByEmail : ISortStrategy
        {
            public IEnumerable<User> Sort(IEnumerable<User> users)
            {
                return users.OrderBy(u => u.Email);
            }
        }

        public class SortByPhone : ISortStrategy
        {
            public IEnumerable<User> Sort(IEnumerable<User> users)
            {
                return users.OrderBy(u => u.Phone);
            }
        }

        public class SortByAddress : ISortStrategy
        {
            public IEnumerable<User> Sort(IEnumerable<User> users)
            {
                return users.OrderBy(u => u.Address);
            }
        }

        public class UserSorter
        {
            private readonly ISortStrategy _strategy;

            public UserSorter(ISortStrategy strategy)
            {
                _strategy = strategy;
            }

            public IEnumerable<User> Sort(IEnumerable<User> users)
            {
                return _strategy.Sort(users);
            }
        }
    }
}

    }
}

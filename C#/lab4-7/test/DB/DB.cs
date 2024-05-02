using Lab_8.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Data.Sqlite;
using System.Windows.Media;

namespace Lab_8.DB
{
    public static class DB
    {
        private static SqliteConnection connection;
        private static readonly string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public static void CreateDatabaseIfNotExists()
        {

            var builder = new SqliteConnectionStringBuilder(connectionString);
            var databaseName = builder.ConnectionString;


            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();


                var sql = $"SELECT COUNT(*) FROM Users";
                using (var command = new SqliteCommand(sql, connection))
                {
                    //var databaseExists = (int)command.ExecuteScalar() > 0;
                    var databaseExists = true;

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
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();


                var schemaScript = @"CREATE TABLE Users (
    UserID INTEGER PRIMARY KEY,
    FirstName Text,
    LastName Text,
    Email Text,
    Phone Text,
    Address Text,
    Image BLOB
)
CREATE TABLE Orders (
    OrderID INTEGER PRIMARY KEY,
    UserID INTTEGER,
    OrderDate Text,
    TotalAmount REAL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);";


                using (var command = new SqliteCommand(schemaScript, connection))
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

        public static void AddUserWithOrder(User user, Order order)
        {
            SqliteTransaction transaction = null;
            try
            {
                OpenConnection();
                transaction = connection.BeginTransaction();

                // Валидация данных пользователя
                if (!Regex.IsMatch(user.FirstName, @"^[а-яА-Яa-zA-Z\s]+$"))
                {
                    throw new Exception("Некорректное имя пользователя. Отмена транзакции...");
                }

                if (!Regex.IsMatch(user.LastName, @"^[а-яА-Яa-zA-Z\s]+$"))
                {
                    throw new Exception("Некорректная фамилия пользователя. Отмена транзакции...");
                }

                if (!Regex.IsMatch(user.Phone, @"^[0-9)(-+]+$"))
                {
                    throw new Exception("Некорректный номер телефона пользователя. Отмена транзакции...");
                }

                if (!Regex.IsMatch(user.UserID.ToString(), @"^\d+$"))
                {
                    throw new Exception("Некорректный идентификатор пользователя. Отмена транзакции...");
                }

                // Добавление пользователя
                string userQuery = "INSERT INTO Users (FirstName, LastName, Email, Phone, Address, UserID, Image) VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @UserID, @Image)";
                using (SqliteCommand userCommand = new SqliteCommand(userQuery, connection, transaction))
                {
                    userCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                    userCommand.Parameters.AddWithValue("@LastName", user.LastName);
                    userCommand.Parameters.AddWithValue("@Email", user.Email);
                    userCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    userCommand.Parameters.AddWithValue("@Address", user.Address);
                    userCommand.Parameters.AddWithValue("@UserID", user.UserID);
                    userCommand.Parameters.AddWithValue("@Image", user.Image);
                    int userResult = userCommand.ExecuteNonQuery();
                    if (userResult == 0)
                    {
                        throw new Exception("Не удалось добавить пользователя.");
                    }
                }

                // Валидация данных заказа
                if (!Regex.IsMatch(order.TotalAmount.ToString(), @"^\d+$"))
                {
                    throw new Exception("Некорректная стоимость заказа.");
                }

                // Добавление заказа
                string orderQuery = "INSERT INTO Orders (OrderID, UserID, OrderDate, TotalAmount) VALUES (@OrderID, @UserID, @OrderDate, @TotalAmount)";
                using (SqliteCommand orderCommand = new SqliteCommand(orderQuery, connection, transaction))
                {
                    orderCommand.Parameters.AddWithValue("@OrderID", order.UserID);
                    orderCommand.Parameters.AddWithValue("@UserID", order.UserID);
                    orderCommand.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    orderCommand.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    int orderResult = orderCommand.ExecuteNonQuery();
                    if (orderResult == 0)
                    {
                        throw new Exception("Не удалось добавить заказ.");
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



        public static void AddUser(User user)
        {
            OpenConnection();

            string query = "INSERT INTO Users (FirstName, LastName, Email, Phone, Address,UserID,Image) VALUES (@FirstName, @LastName, @Email, @Phone, @Address,@UserID,@Image)";

            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@Image", user.Image);

                int rowsAffected = command.ExecuteNonQuery();
            }

            CloseConnection();
        }
        // Класс с методом для обработки данных
        public class DataProcedure
        {
            public static string ProcessData()
            {
                return "INSERT INTO Users (FirstName, LastName, Email, Phone, Address,UserID,Image) VALUES (@FirstName, @LastName, @Email, @Phone, @Address,@UserID,@Image)";
            }
        }

// Использование в коде

        public static void AddUserWithProcedure(User user)
        {
            OpenConnection();
         
            using (SqliteCommand command = new SqliteCommand(DataProcedure.ProcessData(), connection))
            {
               
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@Image", user.Image);

                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(rowsAffected.ToString());
            }

            CloseConnection();
        }


        public static void DeleteUser(int userId)
        {
            try
            {
                OpenConnection();
                string query = "DELETE FROM USERS WHERE USERID=@ID";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", userId);
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
        public static void DeleteOrdersByUserId(int userId)
        {
            try
            {
                OpenConnection();
                string query = "DELETE FROM Orders WHERE UserID=@ID";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", userId);
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



        public static void AddOrder(Order order)
        {
            SqliteTransaction transaction = null;
            try
            {
                OpenConnection();
                transaction = connection.BeginTransaction();


                string query = "INSERT INTO Orders (OrderID, UserID, OrderDate, TotalAmount) VALUES (@OrderID, @UserID, @OrderDate, @TotalAmount)";
                using (SqliteCommand command = new SqliteCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@OrderID", order.UserID);
                    command.Parameters.AddWithValue("@UserID", order.UserID);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
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


        public static void UpdateUser(int userId, User user, Order order)
        {
            SqliteTransaction transaction = null;
            try
            {
                OpenConnection();
                transaction = connection.BeginTransaction();

                // Валидация данных пользователя
                if (!Regex.IsMatch(user.FirstName, @"^[а-яА-Яa-zA-Z\s]+$"))
                {
                    throw new Exception("Некорректное имя пользователя. Отмена транзакции...");
                }

                if (!Regex.IsMatch(user.LastName, @"^[а-яА-Яa-zA-Z\s]+$"))
                {
                    throw new Exception("Некорректная фамилия пользователя. Отмена транзакции...");
                }

                if (!Regex.IsMatch(user.Phone, @"^[0-9)(-+]+$"))
                {
                    throw new Exception("Некорректный номер телефона пользователя. Отмена транзакции...");
                }

                if (!Regex.IsMatch(user.UserID.ToString(), @"^\d+$"))
                {
                    throw new Exception("Некорректный идентификатор пользователя. Отмена транзакции...");
                }

                // Обновление данных пользователя
                string userQuery = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Address=@Address, Phone=@Phone WHERE UserID=@UserID";
                using (SqliteCommand userCommand = new SqliteCommand(userQuery, connection, transaction))
                {
                    userCommand.Parameters.AddWithValue("@UserID", userId);
                    userCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                    userCommand.Parameters.AddWithValue("@LastName", user.LastName);
                    userCommand.Parameters.AddWithValue("@Email", user.Email);
                    userCommand.Parameters.AddWithValue("@Address", user.Address);
                    userCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    int userResult = userCommand.ExecuteNonQuery();
                    if (userResult == 0)
                    {
                        throw new Exception("Не удалось обновить данные пользователя.");
                    }
                }

                // Валидация данных заказа
                if (!Regex.IsMatch(order.TotalAmount.ToString(), @"^\d+$"))
                {
                    throw new Exception("Некорректная стоимость заказа. Отмена транзакции...");
                }

                // Обновление данных о заказе
                string orderQuery = "UPDATE Orders SET OrderDate=@OrderDate, TotalAmount=@TotalAmount WHERE OrderID=@OrderID";
                using (SqliteCommand orderCommand = new SqliteCommand(orderQuery, connection, transaction))
                {
                    orderCommand.Parameters.AddWithValue("@OrderID", order.OrderID);
                    orderCommand.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    orderCommand.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    int orderResult = orderCommand.ExecuteNonQuery();
                    if (orderResult == 0)
                    {
                        throw new Exception("Не удалось обновить данные заказа.");
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

        public static User GetUserById(int userId)
        {
            try
            {
                OpenConnection();

                string query = "SELECT * FROM Users WHERE UserID = @UserID";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
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

                            return user;
                        }
                        else
                        {
                            throw new Exception("Пользователь с указанным идентификатором не найден.");
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


        public static void UpdateOrder(int orderId, Order order)
        {
            SqliteTransaction transaction = null;
            try
            {
                OpenConnection();
                transaction = connection.BeginTransaction();

                string query = "UPDATE Orders SET OrderDate=@OrderDate, TotalAmount=@TotalAmount WHERE OrderID=@OrderID";
                using (SqliteCommand command = new SqliteCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    int result = command.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Не удалось обновить данные заказа.");
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



        public static IEnumerable<User> Sort(string sortBy)
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

        public static IEnumerable<Order> GetAllOrders()
        {
            try
            {
                OpenConnection();
                string query = "SELECT * FROM Orders";
                List<Order> orders = new List<Order>();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int orderId = reader.GetInt32(0);
                            int userId = reader.GetInt32(1);
                            DateTime orderDate = reader.GetDateTime(2);
                            decimal totalAmount = reader.GetDecimal(3);

                            Order order = new Order
                            {
                                OrderID = orderId,
                                UserID = userId,
                                OrderDate = orderDate,
                                TotalAmount = totalAmount
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
        public static IEnumerable<User> GetAllUsers()
        {
            try
            {
                OpenConnection();
                string query = "SELECT * FROM USERS";
                List<User> users = new List<User>();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
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
            public IEnumerable<User> Sort(IEnumerable<User> users)
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
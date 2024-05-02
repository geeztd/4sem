using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace lab8.Class
{
    public int UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public byte[] Image { get; set; }

    public User()
    {
        UserID = 0;
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
    }

    public User(int customerID, string firstName, string lastName, string email, string phone, string address, byte[] image)
    {
        UserID = customerID;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        Image = image;
    }

    public override string ToString()
    {
        return $"CustomerID - {UserID}\n" +
               $"FirstName - {FirstName} \n" +
               $"LastName - {LastName}\n" +
               $"Email - {Email}\n" +
               $"Phone - {Phone}\n" +
               $"Address - {Address}";
    }
}
internal class Author
{
    public int id;
    public string name;
    public string secondname;

    public Author() { }
    public Author(int id, string name, string secondname)
    {
        this.id = id;
        this.name = name;
        this.secondname = secondname;
    }
}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class User
    {
        public String Username { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public User(String username,String firstname,String lastname, String email, String password)
        {
            this.Username = username;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Password = password;
        }
    }
}

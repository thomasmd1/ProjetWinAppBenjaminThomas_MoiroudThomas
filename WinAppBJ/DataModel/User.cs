using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class User
    {
        
        public String username { get; set; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String email { get; set; }
        public String password { get; set; }

        public User(String username,String firstname,String lastname, String email, String password)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
        }
    }
}

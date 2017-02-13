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
        public String secret { get; set; }
        public int stack { get; set; }
        public String tokens { get; set; }
        public int roomId { get; set; }

        public User(String username,String firstname,String lastname, String email, String password)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
        }

        public User(String email, String password , String secret)
        {
            this.email = email;
            this.password = password;
            this.secret = secret;
        }

        public User()
        {
        }
    }
}

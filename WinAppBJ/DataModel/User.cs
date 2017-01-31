using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class User
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String PictureUri { get; set; }

        public User(String n,String e)
        {
            this.Name = n;
            this.Email = e;
            this.PictureUri = "http://lorempixel.com/64/64/people/";
        }
    }
}

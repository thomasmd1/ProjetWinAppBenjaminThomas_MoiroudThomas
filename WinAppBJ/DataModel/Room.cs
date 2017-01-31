using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class Room
    {
        public String Name { get; set; }
        public int MaxUser { get; set; }
        public int ContainUser { get; set; }
        public List<User> Users { get; set; }

        public Room(String n)
        {
            this.Name = n;
            this.Users = new List<User>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Room
    {
        public List<User> Users { get; set; }
        public List<Table> Tables { get; set; }

        public Room()
        {
            this.Users = new List<User>();
            this.Tables = new List<Table>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Room
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Table> Tables { get; set; }

        //Connstructeur Room
        public Room()
        {
            this.Users = new ObservableCollection<User>();
            this.Tables = new ObservableCollection<Table>();
        }
    }
}

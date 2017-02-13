using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Table
    {
        public int id { get; set; }
        public int maxSeat { get; set; }
        public int seatAvailable { get; set; }
        public int minBet { get; set; }
        public Boolean isClose { get; set; }

        public Table()
        {

        }
    }
}

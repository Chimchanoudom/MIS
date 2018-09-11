using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestHouse
{
    public class Room
    {
        public string ID { get; set; }
        public string Floor { get; set; }
        public string RoomType { get; set; }
        public string Status { get; set; }

        public Room()
        {

        }
        public Room(string ID,string Floor,string RoomType,string Status)
        {
            this.ID = ID;
            this.Floor = Floor;
            this.RoomType = RoomType;
            this.Status = Status;
        }

        public virtual List<object> Record()
        {
            List<object> arr = new List<object>();
            arr.Add(ID);
            arr.Add(Floor);
            arr.Add(RoomType);
            arr.Add(Status);
            return arr;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model

{
    public class UsersRec : AbsRec
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string BloodType { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Username { get; set; }

        public UsersRec()
        {
            ID = 0;
            Name = string.Empty;
            Phone = string.Empty;
            Mail = string.Empty;
            Address = string.Empty;
            BloodType = string.Empty;
            Height =0;
            Weight = 0;
            Username = string.Empty;

        }

    }

}

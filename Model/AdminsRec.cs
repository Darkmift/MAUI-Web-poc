using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AdminsRec: AbsRec
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public AdminsRec() 
        {
            ID = 0;
            Name = string.Empty;
            Phone = string.Empty;
            Mail = string.Empty;
            Username = string.Empty;
        }

    }
}

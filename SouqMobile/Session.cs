using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouqMobile
{
    public class Session
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public long ID { get; set; }
        public string Name { get; set; }
        public string AdminType { get; set; }
        public string UserType { get; set; }
        public string doSelect { get; set; }
        public string doInsert { get; set; }
        public string doUpdate { get; set; }
        public string doDelete { get; set; }
        public Session()
        {
            Username = string.Empty;
            Password = string.Empty;
            Type = string.Empty;
            ID = 0;
            Name = string.Empty;
            AdminType = string.Empty;
            UserType = string.Empty;
            doSelect = string.Empty;
            doInsert = string.Empty;
            doUpdate = string.Empty;
            doDelete = string.Empty;
            doSelect = string.Empty;
        }
    }
}

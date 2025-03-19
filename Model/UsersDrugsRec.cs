using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UsersDrugsRec : AbsRec
    {
        public long UserID { get; set; }
        public string DrugCode { get; set; }
        public int Quantity { get; set; }
        public DateTime Time { get; set; }
        public int Frequent { get; set; }
        public UsersDrugsRec()
        {
            UserID = 0;
            DrugCode = string.Empty;
            Quantity = 0;
            DateTime Time = DateTime.MinValue;
            Frequent = 0;
        }
    }
}

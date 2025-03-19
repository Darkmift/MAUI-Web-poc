using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class UsersDrugsList : List<UsersDrugsRec>
    {
        public UsersDrugsList()
        {

        }

        public UsersDrugsList(IEnumerable<UsersDrugsRec> list) : base(list)
        {

        }

        public UsersDrugsList(IEnumerable<AbsRec> list) : base(list.Cast<UsersDrugsRec>().ToList())
        {

        }
    }

}


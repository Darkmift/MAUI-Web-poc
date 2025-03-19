using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class UsersList : List<UsersRec>
    {
        public UsersList()
        {

        }

        public UsersList(IEnumerable<UsersRec> list) : base(list)
        {

        }

        public UsersList(IEnumerable<AbsRec> list) : base(list.Cast<UsersRec>().ToList())
        {

        }
    }

}


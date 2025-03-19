using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class PasswordsList : List<PasswordsRec>
    {
        public PasswordsList()
        {

        }

        public PasswordsList(IEnumerable<PasswordsRec> list) : base(list)
        {

        }

        public PasswordsList(IEnumerable<AbsRec> list) : base(list.Cast<PasswordsRec>().ToList())
        {

        }
    }

}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   
        public class AdminsList : List<AdminsRec>
        {
            public AdminsList()
            {

            }

            public AdminsList(IEnumerable<AdminsRec> list) : base(list)
            {

            }

            public AdminsList(IEnumerable<AbsRec> list) : base(list.Cast<AdminsRec>().ToList())
            {

            }
        }

    }

 
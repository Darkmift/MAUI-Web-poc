using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class DrugsList : List<DrugsRec>
    {
        public DrugsList()
        {

        }

        public DrugsList(IEnumerable<DrugsRec> list) : base(list)
        {

        }

        public DrugsList(IEnumerable<AbsRec> list) : base(list.Cast<DrugsRec>().ToList())
        {

        }
    }

}





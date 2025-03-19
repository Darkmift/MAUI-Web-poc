using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class DrugTypesList : List<DrugTypesRec>
    {
        public DrugTypesList()
        {

        }

        public DrugTypesList(IEnumerable<DrugTypesRec> list) : base(list)
        {

        }

        public DrugTypesList(IEnumerable<AbsRec> list) : base(list.Cast<DrugTypesRec>().ToList())
        {

        }
    }

}


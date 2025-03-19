using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DrugTypesRec : AbsRec
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public DrugTypesRec()
        {
            Code = string.Empty;
            Name = string.Empty;

        }
    }
}

    


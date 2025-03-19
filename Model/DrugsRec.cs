using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DrugsRec : AbsRec
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public string DrugType { get; set; }
        public DrugsRec()
        {
            Code = string.Empty;
            Name = string.Empty;
            DrugType = string.Empty;

        }
    }

}

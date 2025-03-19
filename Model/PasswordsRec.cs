using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   
    
        public class PasswordsRec : AbsRec
        {
          
            public string Username { get; set; }
            public string Password { get; set; }
            public string Type { get; set; }
            public PasswordsRec()
            {
                Username = string.Empty;
                Password = string.Empty;
                Type = string.Empty;
        }

        }
    }


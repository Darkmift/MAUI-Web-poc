using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class General
    {
        public static string message;

        public const string TYPE_ADMIN = "A";
        public const string TYPE_USER = "U";

        public const string ERR_CONNECT_DB = "Cannot Connect To Database";
        public const string ERR_WRONG_USERNAME = "Wrong Username/Password";
        public const string ERR_ADMIN_NOT_FOUND = "Cannot Find Admin";
        public const string ERR_USER_NOT_FOUND = "Cannot Find User";
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace SouqMobile
{
    public class General
    {
        public static Session session = new Session();
        public static string TITLE = "Welcome to the Souq";
        public static string ERR_CANT_CONNET_SERVER = "Cannot Connect to WCF Server";
        public static string ERR_USER_NOT_ALLOWED = "Users are not allowd \nOnly Admins ";
        public static string ERR_MISSING = "Missing/Wrong";
        public static string ERR_MISSING_Number = "Missing/Wrong Number";
        public static string ERR_MISSING_Date = "Missing/Wrong Date";
        public static string ERR_MISSING_Phone = "Missing/Wrong Phone";
        public static string ERR_MISSING_Mail = "Missing/Wrong Mail";
        public static string ERR_MISSING_Username = "Missing/Wrong Username";
        public static string ERR_MISSING_Password = "Missing/Wrong Password";
        public static string ERR_EXIST_Password = "Password Exists in System";
        public static bool IsEmpty(string strField)
        {
            if (strField.Trim() == string.Empty)
                return true;
            return false;
        }
        public static bool IsEmpty(int intField)
        {
            if (intField < 0)
                return true;
            return false;
        }
        public static bool IsEmpty(Picker ddlField)
        {
            if (ddlField.SelectedIndex < 1)
                return true;
            return false;
        }
        public static bool IsEmpty(RadioButton radioField)
        {
            return true;
        }
        public static bool IsValidDate(string strDate)
        {
            DateTime temp;
            if (DateTime.TryParse(strDate, out temp))
                return true;
            return false;
        }
        public static bool IsValidNumberInteger(string strNumber)
        {
            int tempInt;
            long tempLong;
            if (int.TryParse(strNumber, out tempInt))
                return true;
            if (long.TryParse(strNumber, out tempLong))
                return true;
            return false;
        }
        public static bool IsValidNumberReal(string strNumber)
        {
            double tempDouble;
            if (double.TryParse(strNumber, out tempDouble))
                return true;
            return false;
        }
        public static bool IsValidMail(string strMail)
        {
            if (strMail.Trim() == string.Empty) return true;
            var ValidMail = new EmailAddressAttribute();
            return ValidMail.IsValid(strMail); //true
        }
        public static bool IsValidPhone(string strPhone)
        {
            if (strPhone.Trim() == string.Empty)
            {
                Console.WriteLine("Phone number is empty.");
                return true;
            }

            string Phone = strPhone;
            Phone = Phone.Replace(" ", "");
            Phone = Phone.Replace("-", "");

            Console.WriteLine($"Processed phone number: {Phone}");

            bool isValid = Regex.Match(Phone, @"^([0-9]{9})$").Success ||
                           Regex.Match(Phone, @"^(\+[0-9]{11})$").Success ||
                           Regex.Match(Phone, @"^([0-9]{10})$").Success ||
                           Regex.Match(Phone, @"^(\+[0-9]{12})$").Success;

            Console.WriteLine($"Is valid: {isValid}");
            return isValid;
        }
        public static bool IsValidPassword(string strPassword)
        {
            //Latin chars + Arabic digits
            //return Regex.Match(strPassword, @"^[A-Za-z0-9]+$").Success;
            //+ Special chars
            return Regex.Match(strPassword, @"^[A-Za-z0-9_\~\!\@\#\$\%\^\&\*\-\.]+$").Success;
        }
    }
}
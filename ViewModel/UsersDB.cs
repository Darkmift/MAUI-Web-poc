using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace ViewModel
{
    public class UsersDB : AbsDB
    {
        public UsersRec user;
        private string SQLSelectByUsername;

        public UsersDB()
        {
            user = new UsersRec();
            InitValues();
        }
        //-----Private---------------------------------------------
        private void InitValues()
        {
            SQLInsert = "Insert Into Users " +
                         "(uName, uPhone, uMail, uAddress, uBloodType, uHeight, uWeight, uUsername) " +
                         "Values (@Name, @Phone, @Mail, @Address, @BloodType, @Height, @Weight, @Username) ";

            SQLUpdate = "Update Users Set " +
                                    "uName = @Name, " +
                                    "uPhone = @Phone, " +
                                    "uMail = @Mail, " +
                                    "uAddress = @Address, " +
                                    "uBloodType = @BloodType, " +
                                    "uHeight = @Height, " +
                                    "uWeight = @Weight, " +
                                    "uUsername = @Username " +
                                    "Where uId = @ID ";

            SQLDelete = "Delete From Users " +
                                    "Where uId = @ID ";

            SQLSelect = "Select * From Users " +
                                    "Where uId = @ID ";

            SQLSelectAll = "SELECT * From Users " +
                            "Order By uName, uId ";

            SQLSelectByUsername = "SELECT * From Users " +
                                  "Where uUsername = @Username ";

        }
        protected override void SetParameters(string Oper)
        {
            DB.ParametersClear();

            if (Oper == DB.doDelete ||
                Oper == DB.doSelect)
            {
                DB.ParameterAdd("@ID", user.ID.ToString());
            }

            if (Oper == DB.doInsert ||
                Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Name", user.Name);
                DB.ParameterAdd("@Phone", user.Phone);
                DB.ParameterAdd("@Mail", user.Mail);
                DB.ParameterAdd("@Address", user.Address);
                DB.ParameterAdd("@BloodType", user.BloodType);
                DB.ParameterAdd("@Height", user.Height.ToString());
                DB.ParameterAdd("@Weight", user.Weight.ToString());
                DB.ParameterAdd("@Username", user.Username);
            }

            if (Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@ID", user.ID.ToString());
            }

        }
        protected override AbsRec MoveDBToRecord(DataRow dbRow)
        {
            return MoveDBToRecord(dbRow, user);
        }
        private AbsRec MoveDBToRecord(DataRow dbRow, UsersRec user)
        {
            //user.ID = Convert.ToInt64(dbRow["uId"].ToString());
            user.Name = dbRow["uName"].ToString();
            user.Phone = dbRow["uPhone"].ToString();
            user.Mail = dbRow["uMail"].ToString();
            user.Address = dbRow["uAddress"].ToString();
            user.BloodType = dbRow["uBloodType"].ToString();
            user.Height = Convert.ToInt32(dbRow["uHeight"].ToString());
            user.Weight = Convert.ToInt32(dbRow["uWeight"].ToString());
            user.Username = dbRow["uUsername"].ToString();
            return user;
        }
        public override void MoveValuesToRecord(AbsRec userRec)
        {
            //user.ID = ((UsersRec)userRec).ID;
            user.Name = ((UsersRec)userRec).Name;
            user.Phone = ((UsersRec)userRec).Phone;
            user.Mail = ((UsersRec)userRec).Mail;
            user.Username = ((UsersRec)userRec).Username;
        }

        //-----Local--------------------------------------------
        public UsersList SelectAll()
        {
            UsersList UsersList = new UsersList();
            string SQL = SQLSelectAll;
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return new UsersList();
            }
            foreach (DataRow dbRow in DB.dbDataTable.Rows)
            {
                UsersRec user = new UsersRec();
                MoveDBToRecord(dbRow, user);
                UsersList.Add(user);
            }
            return UsersList;
        }
        public UsersRec SelectByUsername(string Username)
        {
            string SQL = SQLSelectByUsername;
            ErrorClear();
            DB.ParametersClear();
            DB.ParameterAdd("@Username", Username);
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return null;
            }
            MoveDBToRecord(DB.dbDataTable.Rows[0]);

            return user;
        }
    }
}

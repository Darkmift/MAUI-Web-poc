using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace ViewModel
{
    public class PasswordsDB : AbsDB
    {
        public PasswordsRec password;
        private string SQLSelectByUsernameAndPassword;

        public PasswordsDB()
        {
            password = new PasswordsRec();
            InitValues();
        }
        //-----Private---------------------------------------------
        private void InitValues()
        {
            SQLInsert = "Insert Into Passwords " +
                         "(pUsername, pPassword, pType) " +
                         "Values (@Username, @Password, @Type) ";

            SQLUpdate = "Update Passwords Set " +
                                    "pPassword = @Password, " +
                                    "pType = @Type " +
                                    "Where paUsername = @Username ";

            SQLDelete = "Delete From Passwords " +
                                    "Where pUsername = @Username ";

            SQLSelect = "Select * From Passwords " +
                                    "Where pUsername = @Username ";

            SQLSelectAll = "SELECT * From Passwords " +
                            "Order By pPassword, pUsername ";

            SQLSelectByUsernameAndPassword =
                            "Select * From Passwords " +
                            "Where pUsername = @Username " +
                            "And pPassword = @Password ";
        }
        protected override void SetParameters(string Oper)
        {
            DB.ParametersClear();

            if (Oper == DB.doDelete ||
                Oper == DB.doSelect ||
                Oper == DB.doInsert)
            {
                DB.ParameterAdd("@Username", password.Username.ToString());
            }

            if (Oper == DB.doInsert ||
                Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Username", password.Username.ToString());
                DB.ParameterAdd("@Password", password.Password);
                DB.ParameterAdd("@Type", password.Type);
            }

            if (Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Username", password.Username.ToString());
            }

        }
        protected override AbsRec MoveDBToRecord(DataRow dbRow)
        {
            return MoveDBToRecord(dbRow, password);
        }
        private AbsRec MoveDBToRecord(DataRow dbRow, PasswordsRec password)
        {
            password.Username = dbRow["pUsername"].ToString();
            password.Password = dbRow["pPassword"].ToString();
            password.Type = dbRow["pType"].ToString();
            return password;
        }
        public override void MoveValuesToRecord(AbsRec passwordRec)
        {
            password.Username = ((PasswordsRec)passwordRec).Username;
            password.Password = ((PasswordsRec)passwordRec).Password;
            password.Type = ((PasswordsRec)passwordRec).Type;
        }

        //-----Local--------------------------------------------
        public PasswordsList SelectAll()
        {
            PasswordsList PasswordsList = new PasswordsList();
            string SQL = SQLSelectAll;
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return new PasswordsList();
            }
            foreach (DataRow dbRow in DB.dbDataTable.Rows)
            {
                PasswordsRec password = new PasswordsRec();
                MoveDBToRecord(dbRow, password);
                PasswordsList.Add(password);
            }
            return PasswordsList;
        }
        public PasswordsRec SelectByUsernameAndPassword(string Username, string Password)
        {
            string SQL = SQLSelectByUsernameAndPassword;
            ErrorClear();
            DB.ParametersClear();
            DB.ParameterAdd("@Username", Username);
            DB.ParameterAdd("@Password", Password);
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return null;
            }
            MoveDBToRecord(DB.dbDataTable.Rows[0]);

            return password;
        }
    }
}
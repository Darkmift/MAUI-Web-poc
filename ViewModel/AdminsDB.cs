using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace ViewModel
{
    public class AdminsDB:AbsDB
    {
        public AdminsRec admin;
        private string SQLSelectByUsername;

        public AdminsDB()
        {
            admin = new AdminsRec();
            InitValues();
        }
        //-----Private---------------------------------------------
        private void InitValues()
        {
            SQLInsert = "Insert Into Admins " +
                         "(aName, aPhone, aMail, aUsername) " +
                         "Values (@Name, @Phone, @Mail, @Username) ";

            SQLUpdate = "Update Admins Set " +
                                    "aName = @Name, " +
                                    "aPhone = @Phone, " +
                                    "aMail = @Mail, " +
                                    "aUsername = @Username " +
                                    "Where adID = @ID ";

            SQLDelete = "Delete From Admins " +
                                    "Where aID = @ID ";

            SQLSelect = "Select * From Admins " +
                                    "Where aID = @ID ";

            SQLSelectAll = "SELECT * From Admins " +
                            "Order By aName, aID ";

            SQLSelectByUsername = "SELECT * From Admins " +
                                  "Where aUsername = @Username ";

        }
        protected override void SetParameters(string Oper)
        {
            DB.ParametersClear();

            if (Oper == DB.doDelete ||
                Oper == DB.doSelect)
            {
                DB.ParameterAdd("@ID", admin.ID.ToString());
            }

            if (Oper == DB.doInsert ||
                Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Name", admin.Name);
                DB.ParameterAdd("@Phone", admin.Phone);
                DB.ParameterAdd("@Mail", admin.Mail);
                DB.ParameterAdd("@Username", admin.Username);
            }

            if (Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@ID", admin.ID.ToString());
            }

        }
        protected override AbsRec MoveDBToRecord(DataRow dbRow)
        {
            return MoveDBToRecord (dbRow, admin);
        }
        private AbsRec MoveDBToRecord(DataRow dbRow, AdminsRec admin)
        {
            admin.ID = Convert.ToInt64(dbRow["aID"].ToString());
            admin.Name = dbRow["aName"].ToString();
            admin.Phone = dbRow["aPhone"].ToString();
            admin.Mail = dbRow["aMail"].ToString();
            admin.Username = dbRow["aUsername"].ToString();
            return admin;
        }
        public override void MoveValuesToRecord(AbsRec adminRec)
        {
            admin.ID = ((AdminsRec)adminRec).ID;
            admin.Name = ((AdminsRec)adminRec).Name;
            admin.Phone = ((AdminsRec)adminRec).Phone;
            admin.Mail = ((AdminsRec)adminRec).Mail;
            admin.Username = ((AdminsRec)adminRec).Username;
        }

        //-----Local--------------------------------------------
        public AdminsList SelectAll()
        {
            AdminsList adminsList = new AdminsList();
            string SQL = SQLSelectAll;
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return new AdminsList();
            }
            foreach (DataRow dbRow in DB.dbDataTable.Rows)
            {
                AdminsRec admin = new AdminsRec();
                MoveDBToRecord(dbRow, admin);
                adminsList.Add(admin);
            }
            return adminsList;
        }
        public AdminsRec SelectByUsername(string Username)
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

            return admin;
        }
    }
}

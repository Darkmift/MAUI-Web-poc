using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace ViewModel
{
    public class UsersDrugsDB : AbsDB
    {
        public UsersDrugsRec userdrug;

        public UsersDrugsDB()
        {
            userdrug = new UsersDrugsRec();
            InitValues();
        }
        //-----Private---------------------------------------------
        private void InitValues()
        {
            SQLInsert = "Insert Into UsersDrugs " +
                         "(udUserID, udDrugCode, udQuantity, udTime, udFrequent) " +
                         "Values (@UserID, @DrugCode, @Quantity, @Time, @Frequent) ";

            SQLUpdate = "Update UsersDrugs Set " +
                                    "udQuantity = @Quantity, " +
                                    "udTime = @Time, " +
                                    "udFrequent = @Frequent " +
                                    "Where udUserID = @UserID " +
                                    "And udDrugCode = @DrugCode";

            SQLDelete = "Delete From UsersDrugs " +
                                    "Where udUserID = @UserID " +
                                    "And udDrugCode = @DrugCode";

            SQLSelect = "Select * From UsersDrugs " +
                                     "Where udUserID = @UserID " +
                                    "And udDrugCode = @DrugCode";

            SQLSelectAll = "SELECT * From UsersDrugs " +
                            "Order By udUserID, udDrugCode ";

         
        }
        protected override void SetParameters(string Oper)
        {
            DB.ParametersClear();

            if (Oper == DB.doDelete ||
                Oper == DB.doSelect ||
                Oper == DB.doInsert)
            {
                DB.ParameterAdd("@UserID", userdrug.UserID.ToString());
                DB.ParameterAdd("@DrugCode", userdrug.DrugCode.ToString());
            }

            if (Oper == DB.doInsert ||
                Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Quantity", userdrug.Quantity.ToString());
                DB.ParameterAdd("@Time", userdrug.Time.ToString("yyyy-MM-dd"));
                DB.ParameterAdd("@Frequent", userdrug.Frequent.ToString());
            }

            if (Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@UserID", userdrug.UserID.ToString());
                DB.ParameterAdd("@DrugCode", userdrug.DrugCode.ToString());
            }

        }
        protected override AbsRec MoveDBToRecord(DataRow dbRow)
        {
            return MoveDBToRecord(dbRow, userdrug);
        }
        private AbsRec MoveDBToRecord(DataRow dbRow, UsersDrugsRec userdrug)
        {
            userdrug.UserID = Convert.ToInt32(dbRow["udUserID"].ToString());
            userdrug.DrugCode = dbRow["udDrugCode"].ToString();
            userdrug.Quantity = Convert.ToInt32(dbRow["udQuantity"].ToString());
            userdrug.Time = Convert.ToDateTime(dbRow["udTime"].ToString());
            userdrug.Frequent = Convert.ToInt32(dbRow["udFrequent"].ToString());
            return userdrug;
        }
        public override void MoveValuesToRecord(AbsRec userdrugRec)
        {
            userdrug.UserID = ((UsersDrugsRec)userdrugRec).UserID;
            userdrug.DrugCode = ((UsersDrugsRec)userdrugRec).DrugCode;
            userdrug.Quantity = ((UsersDrugsRec)userdrugRec).Quantity;
            userdrug.Time = ((UsersDrugsRec)userdrugRec).Time;
            userdrug.Frequent = ((UsersDrugsRec)userdrugRec).Frequent;
        }

        //-----Local--------------------------------------------
        public UsersDrugsList SelectAll()
        {
            UsersDrugsList UsersDrugsList = new UsersDrugsList();
            string SQL = SQLSelectAll;
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return new UsersDrugsList();
            }
            foreach (DataRow dbRow in DB.dbDataTable.Rows)
            {
                UsersDrugsRec userdrug = new UsersDrugsRec();
                MoveDBToRecord(dbRow, userdrug);
                UsersDrugsList.Add(userdrug);
            }
            return UsersDrugsList;
        }
     }
}

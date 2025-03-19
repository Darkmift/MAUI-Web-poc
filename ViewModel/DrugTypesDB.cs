using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace ViewModel
{
    public class DrugTypesDB : AbsDB
    {
        public DrugTypesRec drugtype;

        public DrugTypesDB()
        {
            drugtype = new DrugTypesRec();
            InitValues();
        }
        //-----Private---------------------------------------------
        private void InitValues()
        {
            SQLInsert = "Insert Into DrugTypes " +
                         "(dtCode, dtName) " +
                         "Values (@Code, @Name) ";

            SQLUpdate = "Update DrugTypes Set " +
                                    "dtName = @Name " +
                                    "Where dtCode = @Code ";

            SQLDelete = "Delete From DrugTypes " +
                                    "Where dtCode = @Code ";

            SQLSelect = "Select * From DrugTypes " +
                                    "Where dtCode = @Code ";

            SQLSelectAll = "SELECT * From DrugTypes " +
                            "Order By dtName, dtCode ";

        }
        protected override void SetParameters(string Oper)
        {
            DB.ParametersClear();

            if (Oper == DB.doDelete ||
                Oper == DB.doSelect ||
                Oper == DB.doInsert)
            {
                DB.ParameterAdd("@Code", drugtype.Code.ToString());
            }

            if (Oper == DB.doInsert ||
                Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Name", drugtype.Name);
            }

            if (Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Code", drugtype.Code.ToString());
            }

        }
        protected override AbsRec MoveDBToRecord(DataRow dbRow)
        {
            return MoveDBToRecord(dbRow, drugtype);
        }
        private AbsRec MoveDBToRecord(DataRow dbRow, DrugTypesRec drugtype)
        {
            drugtype.Code = dbRow["dtCode"].ToString();
            drugtype.Name = dbRow["dtName"].ToString();
            return drugtype;
        }
        public override void MoveValuesToRecord(AbsRec drugtypeRec)
        {
            drugtype.Code = ((DrugTypesRec)drugtypeRec).Code;
            drugtype.Name = ((DrugTypesRec)drugtypeRec).Name;
        }

        //-----Local--------------------------------------------
        public DrugTypesList SelectAll()
        {
            DrugTypesList DrugTypesList = new DrugTypesList();
            string SQL = SQLSelectAll;
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return new DrugTypesList();
            }
            foreach (DataRow dbRow in DB.dbDataTable.Rows)
            {
                DrugTypesRec drugtype = new DrugTypesRec();
                MoveDBToRecord(dbRow, drugtype);
                DrugTypesList.Add(drugtype);
            }
            return DrugTypesList;
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace ViewModel
{
    public class DrugsDB : AbsDB
    {
        public DrugsRec drug;

        public DrugsDB()
        {
            drug = new DrugsRec();
            InitValues();
        }
        //-----Private---------------------------------------------
        private void InitValues()
        {
            SQLInsert = "Insert Into Drugs " +
                         "(dCode, dName, dDrugType) " +
                         "Values (@Code, @Name, @DrugType) ";

            SQLUpdate = "Update Drugs Set " +
                                    "dName = @Name, " +
                                    "dDrugType = @DrugType " +
                                    "Where DCode = @Code ";

            SQLDelete = "Delete From Drugs " +
                                    "Where DCode = @Code ";

            SQLSelect = "Select * From Drugs " +
                                    "Where DCode = @Code ";

            SQLSelectAll = "SELECT * From Drugs " +
                            "Order By dName, DCode ";

        }
        protected override void SetParameters(string Oper)
        {
            DB.ParametersClear();

            if (Oper == DB.doDelete ||
                Oper == DB.doSelect ||
                Oper == DB.doInsert)
            {
                DB.ParameterAdd("@Code", drug.Code.ToString());
            }

            if (Oper == DB.doInsert ||
                Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Name", drug.Name);
                DB.ParameterAdd("@DrugType", drug.DrugType);
            }

            if (Oper == DB.doUpdate)
            {
                DB.ParameterAdd("@Code", drug.Code.ToString());
            }

        }
        protected override AbsRec MoveDBToRecord(DataRow dbRow)
        {
            return MoveDBToRecord(dbRow, drug);
        }
        private AbsRec MoveDBToRecord(DataRow dbRow, DrugsRec admin)
        {
            drug.Code = dbRow["DCode"].ToString();
            drug.Name = dbRow["dName"].ToString();
            drug.DrugType = dbRow["dDrugType"].ToString();
            return drug;
        }
        public override void MoveValuesToRecord(AbsRec drugRec)
        {
            drug.Code = ((DrugsRec)drugRec).Code;
            drug.Name = ((DrugsRec)drugRec).Name;
            drug.DrugType = ((DrugsRec)drugRec).DrugType;
        }

        //-----Local--------------------------------------------
        public DrugsList SelectAll()
        {
            DrugsList drugsList = new DrugsList();
            string SQL = SQLSelectAll;
            if (!DB.Select(SQL))
            {
                priErrorMessage = DB.Error;
                priErrorNo = -1;
                return new DrugsList();
            }
            foreach (DataRow dbRow in DB.dbDataTable.Rows)
            {
                DrugsRec drug = new DrugsRec();
                MoveDBToRecord(dbRow, drug);
                drugsList.Add(drug);
            }
            return drugsList;
        }
    }
}

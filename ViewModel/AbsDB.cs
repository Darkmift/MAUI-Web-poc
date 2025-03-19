using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace ViewModel
{
    public abstract class AbsDB
    {
        protected int priErrorNo;
        protected string priErrorMessage;

        protected string SQLInsert;
        protected string SQLUpdate;
        protected string SQLDelete;
        protected string SQLSelect;
        protected string SQLSelectAll;

        public void ErrorClear()
        {
            priErrorNo = 0;
            priErrorMessage = "";
        }
        public int ErrorNo
        {
            get { return this.priErrorNo; }
        }
        public string ErrorMessage
        {
            get { return this.priErrorMessage; }
        }
        protected abstract void SetParameters(string Oper);
        protected abstract AbsRec MoveDBToRecord(DataRow dbRow);
        public abstract void MoveValuesToRecord(AbsRec record);

        public virtual AbsRec Select(AbsRec record)
        {
            try
            {
                string SQL = SQLSelect;
                ErrorClear();
                MoveValuesToRecord(record);
                SetParameters(DB.doSelect);
                if (!DB.Select(SQL))
                {
                    priErrorMessage = DB.Error;
                    priErrorNo = -1;
                    return null;
                }
                return MoveDBToRecord(DB.dbDataTable.Rows[0]);
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return null;
            }

        }
        public virtual bool Insert(AbsRec record)
        {
            try
            {
                string SQL = SQLInsert;
                ErrorClear();
                MoveValuesToRecord(record);
                SetParameters(DB.doInsert);
                if (!DB.Insert(SQL))
                {
                    priErrorMessage = DB.Error;
                    priErrorNo = -1;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public virtual bool Update(AbsRec record)
        {
            try
            {
                string SQL = SQLUpdate;
                ErrorClear();
                MoveValuesToRecord(record);
                SetParameters(DB.doUpdate);
                if (!DB.Update(SQL))
                {
                    priErrorMessage = DB.Error;
                    priErrorNo = -1;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
        public virtual bool Delete(AbsRec record)
        {
            try
            {
                string SQL = SQLDelete;
                ErrorClear();
                MoveValuesToRecord(record);
                SetParameters(DB.doDelete);
                if (!DB.Delete(SQL))
                {
                    priErrorMessage = DB.Error;
                    priErrorNo = -1;
                    return false;
                }
                return true; 
            }
            catch (Exception ex)
            {
                General.message = ex.Message;
                return false;
            }
        }
    }
}

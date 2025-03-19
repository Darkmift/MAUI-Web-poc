using System;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class DB
    {
        public const string doSelect = "Select";
        public const string doInsert = "Insert";
        public const string doUpdate = "Update";
        public const string doDelete = "Delete";
        public const string doText = "Text";

        public static string Error;
        //public static DataSet dbDataSet;
        public static DataTable dbDataTable;

        public static OleDbConnection dbConnection;
        public static OleDbCommand dbCommand;
        public static OleDbDataAdapter dbAdapter;
        public static OleDbDataReader dbReader = null;
        public static OleDbTransaction dbTransaction = null;

        public static bool OpenDatabase()
        {
            /*
             *   <add key="Provider" value= "Microsoft.ACE.OLEDB.12.0"/>
             *   <add key="Path" value="C:\Teach\csit_Lessons\03-שרת-לקוח\Progs\SouqProject" />
             *   <add key="Database" value="Souq.accdb" />
             */
            string dbProvider = "Microsoft.ACE.OLEDB.12.0";
            string dbPath = @"C:\Project12\Database";
            string dbDatabase = dbPath + @"\" + "Project12.accdb";
            string ConnString = "Provider=" + dbProvider + ";Data Source=" + dbDatabase;

            dbConnection = new OleDbConnection(ConnString);
            Error = "";
            try
            {
                dbConnection.Open();

                dbCommand = new OleDbCommand
                {
                    Connection = dbConnection
                };
                dbAdapter = new OleDbDataAdapter();
                ParametersClear();
                return true;
            }
            catch (Exception e)
            {
                Error = "Can't find Data Base " + dbDatabase + ": " + e.Message;
                return false;
            }
        }

        public static void CloseDatabase()
        {
            if (dbConnection.State == ConnectionState.Open)
                dbConnection.Close();
        }

        public static void BeginTransaction()
        {
            dbTransaction = dbConnection.BeginTransaction();
            dbCommand.Transaction = dbTransaction;
        }

        public static void CommitTransaction()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Commit();
                dbTransaction = null;
                dbCommand.Transaction = null;
            }
        }
        public static void RollbackTransaction()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Rollback();
                dbTransaction = null;
                dbCommand.Transaction = null;
            }
        }
        public static bool Select(string strSQL)
        {
            return OpenDataSet(doSelect, strSQL);
        }
        public static bool Insert(string strSQL)
        {
            return OpenDataSet(doInsert, strSQL);
        }
        public static bool Update(string strSQL)
        {
            return OpenDataSet(doUpdate, strSQL);
        }
        public static bool Delete(string strSQL)
        {
            return OpenDataSet(doDelete, strSQL);
        }
        public static long GetLastKey()
        {
            long LastKey = 0;
            string SQL = "Select @@Identity As LastKey ";
            if (Select(SQL))
            {
                //LastKey = Convert.ToInt64(dbDataSet.Tables[0].Rows[0]["LastKey"].ToString());
                LastKey = Convert.ToInt64(dbDataTable.Rows[0]["LastKey"].ToString());
            }

            return LastKey;
        }
        private static bool OpenDataSet(string Oper, string strSQL)
        {
            //dbDataSet = new DataSet();
            dbDataTable = new DataTable();

            try
            {
                if (strSQL.ToUpper().IndexOf("SELECT ") == 0 ||
                    strSQL.ToUpper().IndexOf("INSERT ") == 0 ||
                    strSQL.ToUpper().IndexOf("UPDATE ") == 0 ||
                    strSQL.ToUpper().IndexOf("DELETE ") == 0)
                    dbCommand.CommandType = CommandType.Text;
                else
                    dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.CommandText = strSQL;

                switch (Oper)
                {
                    case doInsert:
                        dbAdapter.InsertCommand = dbCommand;
                        break;
                    case doUpdate:
                        dbAdapter.UpdateCommand = dbCommand;
                        break;
                    case doDelete:
                        dbAdapter.DeleteCommand = dbCommand;
                        break;
                    default:
                        dbAdapter.SelectCommand = dbCommand;
                        break;
                }

                if (Oper == doSelect)
                {
                    dbAdapter.Fill(dbDataTable);
                    if (dbDataTable.Rows.Count == 0)
                    {
                        Error = "No Row Found";
                        dbDataTable = null;
                        return false;
                    }

                }
                else
                    dbCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }

        public static void ParametersClear()
        {
            if (dbCommand != null)
            {
                dbCommand.Parameters.Clear();
            }
        }
        public static void ParameterAdd(string parName, string parValue)
        {
            dbCommand.Parameters.AddWithValue(parName, parValue);
        }

        public static void ParameterAddBinary(string parName, byte[] parValue)
        {
            if (parValue == null)
                dbCommand.Parameters.AddWithValue(parName, "0");
            else
                dbCommand.Parameters.AddWithValue(parName, parValue);
        }
    }
}

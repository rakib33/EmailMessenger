/*
 * Copyright(c) 2019, Rakibul Islam all rights reserved.
 * Description  : JU PMSCS project EmailMessenger
 * Author       : Rakibul Islam
 * Modification :
 * Date         By              Description
 * ----------   --------------- ------------------------------------------------------
 * 16/02/2019   Rakibul Islam            Redesigned the whole structure. Started
 * 17/02/2019   Rakibul Islam            Continnued
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmEntity
{
    //https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/capitalization-conventions
    //✓ DO use PascalCasing for all public member, type, and namespace names consisting of multiple words.
    //✓ DO use camelCasing for parameter names.
    //use PascalCasing for class names and method names. eg ServerConnect,GetTableFieldList
    public class SqlServerConnect :IServerConnect {

        private string _hostName { set; get; }
        private string _userName { set; get; }
        private string _password { set; get; }
        private string _databaseName { get; set; }
        private string _tableName { get; set; }

        private string _dbProvider { get; set; }
        private SqlConnection _sqlConnection = null;

        //The camelCasing convention, used only for parameter names, 
        //capitalizes the first character of each word except the first word

        public SqlServerConnect() { }
        public SqlServerConnect(string serverHostName,string serverUserName,string serverPassword,string databaseName,string tableName) {
            _hostName = serverHostName;
            _userName = serverUserName;
            _password = serverPassword;
            _databaseName = databaseName;
            _tableName = tableName;
           
        }

        public SqlServerConnect() { 
         // to close opened connection
            if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();
        }
        public virtual List<Field> GetSingleTableFieldList(){
            List<Field> tableFieldList = null;
            try
            {
                string connectionString = @"Data Source=" + _hostName + ";Initial Catalog=" + _databaseName + ";User ID=" + _userName + ";Password=" + _password;

                using (_sqlConnection = new SqlConnection(connectionString))
                {

                    if (_sqlConnection.State == ConnectionState.Closed)
                        _sqlConnection.Open();
                    string[] restrictions = new string[4] { null, null, _tableName, null };

                    tableFieldList = _sqlConnection.GetSchema("Columns", restrictions).AsEnumerable().Select(
                                  s => new Field()
                                  {
                                      FieldName = s.Field<String>("Column_Name"),
                                      FieldType = s.Field<String>("Data_Type")
                                  }).ToList();

                    _sqlConnection.Close();

                }
                #region Approch2
                ////string sql = "select * from " + TableName + " WHERE 1 = 0";
                ////DataTable tblSchema;
                ////using (SqlCommand cmd = cnn.CreateCommand())
                ////{
                ////    cmd.CommandText = sql;
                ////    cmd.CommandType = CommandType.Text;

                ////    using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                ////    {
                ////        tblSchema = rdr.GetSchemaTable();
                ////    }
                ////    cnn.Close();
                ////}

                ////int numColumns = tblSchema.Columns.Count;
                ////foreach (DataRow dr in tblSchema.Rows)
                ////{
                ////    string column= dr["ColumnName"]+" "+ dr["DataType"];
                ////}
                #endregion
                
            }
            catch (Exception ex) {
                throw ex;
            }

            return tableFieldList;
        }

        public  List<BaseField> GetTableFieldList()
        {
            List<BaseField> tableFieldList = null;
            try
            {
                string connectionString = @"Data Source=" + _hostName + ";Initial Catalog=" + _databaseName + ";User ID=" + _userName + ";Password=" + _password;

                using (_sqlConnection = new SqlConnection(connectionString))
                {

                    if (_sqlConnection.State == ConnectionState.Closed)
                        _sqlConnection.Open();
                    string[] restrictions = new string[4] { null, null, _tableName, null };
         
                    tableFieldList = _sqlConnection.GetSchema("Columns", restrictions).AsEnumerable().Select(
                                  s => new BaseField()
                                  {
                                      FieldName = s.Field<String>("Column_Name"),
                                      FieldType = s.Field<String>("Data_Type")
                                  }).ToList();

                    _sqlConnection.Close();

                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tableFieldList;
        }

        public List<Database> GetDatabaseList() {

            List<Database> dbNameList = new List<Database>();         

            try
            {
            
                var connectionString = string.Format("Data Source={0};User ID={1};Password={2};", _hostName, _userName, _password);
                DataTable databases = null;
                List<string> dbList = new List<string>();
                using (_sqlConnection = new SqlConnection(connectionString))
                {
                    _sqlConnection.Open();
                    databases = _sqlConnection.GetSchema("Databases");
                    dbList = databases.AsEnumerable().Select(s => s.Field<String>("Database_Name")).ToList();
                    //Now get All table
                    foreach(var dbName in dbList){
                        _databaseName = dbName;
                        var databaseObj = new Database();
                            databaseObj.DatabaseName = _databaseName;
                            databaseObj.TableList = GetTableList();
                            dbNameList.Add(databaseObj);
                    }
                    _sqlConnection.Close();
                }

                dbNameList = dbNameList.Where(t => t.TableList.Count() > 0).ToList();
          
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dbNameList;
        
        }

        public List<BaseTable> GetTableList() {
            try
            {
                if (_databaseName != null)
                {
                    List<BaseTable> tableList = new List<BaseTable>();

                    string connetionString = @"Data Source=" + _hostName + ";Initial Catalog=" + _databaseName + ";User ID=" + _userName + ";Password=" + _password;
                    _sqlConnection = new SqlConnection(connetionString);
                    
                    if (_sqlConnection.State == ConnectionState.Closed)
                        _sqlConnection.Open();

                    //List<string> table = _sqlConnection.GetSchema("Tables").AsEnumerable().Select(s => s.Field<string>("Table_Name")).ToList();

                    tableList = _sqlConnection.GetSchema("Tables").AsEnumerable().Select(
                           s => new BaseTable()
                           {
                               TableName = s.Field<String>("Table_Name"),
                              // FieldList = GetTableFieldList()
                           }).ToList();

                    _sqlConnection.Close();

                    //ResultList<T> clone = DeepClone(original);
                    return tableList;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { 
             
            }
        }

        #region BLockCode
        //public static T DeepClone<T>(T obj)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        var formatter = new BinaryFormatter();
        //        formatter.Serialize(ms, obj);
        //        ms.Position = 0;
        //        return (T)formatter.Deserialize(ms);
        //    }
        //}

        //public static T DeepClone<T>(this T obj)
        //{
        //    return (T)DeepClone((Object)obj);
        //}

        //get Database Table and Field all 
        //public List<Database> GetDatabaseList(List<string> database, string HostNameOrIP, string DataBaseUser, string UserPassword)
        //{
        //    List<Database> dbList = new List<Database>();

        //    Database databaseObj = new Database();
        //    BaseTable tableObj = new BaseTable();
        //    Field fieldObj = new Field(); ;
        //    //get all database name
        //    try
        //    {
        //        if (database != null)
        //        {
        //            foreach (var dbname in database)  //DataRow row in database.Rows// when DataTable database
        //            {

        //                string DatabaseName = dbname;
        //                List<BaseTable> tableList = new List<BaseTable>();

        //                #region get all table name

        //                string connetionString = @"Data Source=" + HostNameOrIP + ";Initial Catalog=" + DatabaseName + ";User ID=" + DataBaseUser + ";Password=" + UserPassword;
        //                SqlConnection cnn = new SqlConnection(connetionString);
        //                cnn.Open();
        //                List<string> table = cnn.GetSchema("Tables").AsEnumerable().Select(s => s.Field<String>("Table_Name")).ToList(); ;

        //                foreach (var TableName in table)
        //                {

        //                    // string TableName = rowTable; //.ItemArray[2].ToString();

        //                    //#region GetTableColumnName


        //                    if (cnn.State == ConnectionState.Closed)
        //                        cnn.Open();
        //                    string[] restrictions = new string[4] { null, null, TableName, null };

        //                    ////we don't get some specific table field such Tempdb
        //                    ////get all column in a line
        //                    List<Field> tableFieldList = cnn.GetSchema("Columns", restrictions).AsEnumerable().Select(
        //                                  s => new Field()
        //                                  {
        //                                        FieldName = s.Field<String>("Column_Name"),
        //                                      FieldType = s.Field<String>("Data_Type")
        //                                  }).ToList();
        //                    //List<Field> newField = new List<Field>();
        //                    //try
        //                    //{
        //                    //    DataTable tableColumn = cnn.GetSchema("Columns", restrictions);


        //                    //    foreach (DataRow dr in tableColumn.Rows)
        //                    //    {
        //                    //        // string column = dr.ItemArray[3].ToString() + " type:" + dr.ItemArray[7].ToString();
        //                    //        fieldObj = new Field();
        //                    //        fieldObj.FieldName = dr.ItemArray[3].ToString();
        //                    //        fieldObj.FieldType = dr.ItemArray[7].ToString();

        //                    //        newField.Add(fieldObj);
        //                    //    }
        //                    //}
        //                    //catch (Exception) { }

        //                    ////create table instance
        //                    tableObj = new BaseTable();
        //                    tableObj.TableName = TableName;
        //                    tableObj.FieldList = tableFieldList;

        //                    tableList.Add(tableObj);

        //                    #region Approch2
        //                    ////string sql = "select * from " + TableName + " WHERE 1 = 0";
        //                    ////DataTable tblSchema;
        //                    ////using (SqlCommand cmd = cnn.CreateCommand())
        //                    ////{
        //                    ////    cmd.CommandText = sql;
        //                    ////    cmd.CommandType = CommandType.Text;

        //                    ////    using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
        //                    ////    {
        //                    ////        tblSchema = rdr.GetSchemaTable();
        //                    ////    }
        //                    ////    cnn.Close();
        //                    ////}

        //                    ////int numColumns = tblSchema.Columns.Count;
        //                    ////foreach (DataRow dr in tblSchema.Rows)
        //                    ////{
        //                    ////    string column= dr["ColumnName"]+" "+ dr["DataType"];
        //                    ////}
        //                    #endregion

        //                    //#endregion



        //                }

        //                cnn.Close();

        //                #endregion

        //                databaseObj = new Database();
        //                databaseObj.DatabaseName = DatabaseName;
        //                databaseObj.TableList = tableList;

        //                dbList.Add(databaseObj);
        //                // break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return dbList;
        //    //get all database name
        //}

        //https://stackoverflow.com/questions/13703193/how-to-get-list-of-all-database-from-sql-server-in-a-combobox-using-c-net
        //https://stackoverflow.com/questions/453683/list-of-dbconnection-getschema-collection-names
        #endregion
    }


}
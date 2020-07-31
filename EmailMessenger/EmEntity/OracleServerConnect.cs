/*
 * Copyright(c) 2019, Rakibul Islam all rights reserved.
 * Description  : JU PMSCS project EmailMessenger
 * Author       : Rakibul Islam
 * Modification :
 * Date         By              Description
 * ----------   --------------- ------------------------------------------------------
 * 25/02/2019   Rakibul Islam            Started
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace EmEntity
{
   public class OracleServerConnect:IServerConnect
    {
        private string _hostName { set; get; }
        private string _userName { set; get; }
        private string _password { set; get; }
        private string _databaseName { get; set; }
        private string _tableName { get; set; }

        private OracleConnection _oracleConnection = null;

            //OracleConnection connection = new OracleConnection();
            //connection.ConnectionString = "User Id=<username>;Password=<password>;Data Source=<data source>"; //Data Source Format -> //IP_HOST:PORT/SERVICE_NAME e.g. //127.0.0.1:1521/Service_Name
            //connection.Open();
            //Console.WriteLine("Connected to Oracle" + connection.ServerVersion);           
  

        //The camelCasing convention, used only for parameter names, 
        //capitalizes the first character of each word except the first word

        public OracleServerConnect(string serverHostName, string serverUserName, string serverPassword, string databaseName, string tableName)
        {
            _hostName = serverHostName;
            _userName = serverUserName;
            _password = serverPassword;
            _databaseName = databaseName;
            _tableName = tableName;
        }

        ~OracleServerConnect() { 
         // to close opened connection
            if (_oracleConnection != null && _oracleConnection.State == ConnectionState.Open)
                _oracleConnection.Close();
        }
        public List<Field> GetSingleTableFieldList()
        {
            throw new NotImplementedException();
        }

        public List<Database> GetDatabaseList()
        {
            throw new NotImplementedException();
        }
    }
}

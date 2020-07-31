using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmEntity;
using EmailMessenger.Models;
using EmailMessenger.App_Code;

namespace EmailMessenger.App_Code
{
    public class ServerDataManager
    {
        private string _connectionName { set; get; }
        private string _hostName { set; get; }
        private string _userName { set; get; }
        private string _password { set; get; }
        private string _databaseName { get; set; }
        private string _tableName { get; set; }
        
        DBServerProvider _dbServerProvider;

        public ServerDataManager() { 
        }
        public ServerDataManager(ServerConnectVariable serverConnect,DBServerProvider dbServerProvider) {
          
            _connectionName = serverConnect.ConnectionName;
            _hostName       = serverConnect.HostNameOrIP;
            _userName       = serverConnect.ServerUserName;
            _password       = serverConnect.UserPassword;
            _databaseName   = serverConnect.DatabaseName;
            _tableName      = serverConnect.TableName;

            // _dbServerProvider = new DBServerProvider();
            _dbServerProvider = dbServerProvider;
        }

       
        public  void GetSingleTableFieldList(){

            string message;
            List<Field> getFiledList=null;
            //switch (_dbServerProvider) {
            //    case DBServerProvider.MS_SQL:{
            //        getSqlSingleTableFieldList(out getFiledList,out message);
            //        break;
            //    } //break;
            //    case DBServerProvider.MY_SQL:{  break;}
            //    case DBServerProvider.Oracle: { break; }
            //    default: { message = "Not Matched"; };
            
            //}

           // return getFiledList;
        }

        public void GetGroupList(out List<Group> getGroupList, out string message)
        {
            getGroupList = null;
            message = null;
            if (_dbServerProvider == DBServerProvider.MS_SQL){
                getSqlGroupList(out getGroupList,out message);
            }
            else if (_dbServerProvider == DBServerProvider.Oracle) { 
            
            }

        }

        public void GetDatabaseServerList(out List<EM_ServerConnect> getDbServerList,out string message) {
            getDbServerList = null;
            message = null;
            if (_dbServerProvider == DBServerProvider.MS_SQL)
            {
                getSqlDbServerList(out getDbServerList, out message);
            }
            else if (_dbServerProvider == DBServerProvider.Oracle)
            {

            }        
        }

        public void GetProjectList(out List<Project> getProjectList, out string message)
        {
            getProjectList = null;
            message = null;
            if (_dbServerProvider == DBServerProvider.MS_SQL)
            {
                getSqlProjectList(out getProjectList, out message);
            }
            else if (_dbServerProvider == DBServerProvider.Oracle)
            {

            }
        }

        public void GetTemplateList(out List<MailTemplate> getTemplateList, out string message)
        {
            getTemplateList = null;
            message = null;
            if (_dbServerProvider == DBServerProvider.MS_SQL)
            {
                getSqlTemplateList(out getTemplateList, out message);
            }
            else if (_dbServerProvider == DBServerProvider.Oracle)
            {

            }
        }

        public void SaveTemplateData(ref MailTemplate saveTemplateData,out MailTemplate getMailTemplate, out string message) {
            getMailTemplate = null;
            message = null;
            if (_dbServerProvider == DBServerProvider.MS_SQL)
            {
                saveSqlTemplateData(ref saveTemplateData,out getMailTemplate, out message);
            }
            else if (_dbServerProvider == DBServerProvider.Oracle)
            {

            }
        }

        #region MS_SQL Server

        /*The Out parameter is used when a method returns multiple values.
        When a parameter passes with the Out keyword/parameter in the method,
        then that method works with the same variable value that is passed in the method call.
        */
        private void getSqlSingleTableFieldList(out List<Field> getFiledList, out string message)
        {
            getFiledList = null;
            message = null;
            try
            {
                IServerConnect _sqlServerInterface = new SqlServerConnect(_hostName, _userName, _password, _databaseName, _tableName);
                getFiledList = _sqlServerInterface.GetSingleTableFieldList();
                message = Message.Message_OK;
            }
            catch (Exception ex) {
                message = ex.Message;
            }
          
        }

        private void getSqlDbServerList(out List<EM_ServerConnect> getDbServerList, out string message)
        {
            getDbServerList = null;
            message = null;
            try
            {
                IRepository<EM_ServerConnect, int> newRepo = new Repository<EM_ServerConnect, SqlDbContext, int>(new SqlDbContext());
                getDbServerList = newRepo.FindAll();
                message = Message.Message_OK;           
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        
        }
        private void getSqlGroupList(out List<Group> getGroupList,out string message) {
            getGroupList = null;
            message = null;
            try
            {
               // IServerConnect _sqlServerInterface = new SqlServerConnect(_hostName, _userName, _password, _databaseName, _tableName);     
                IRepository<Group, int> newRepo = new Repository<Group, SqlDbContext, int>(new SqlDbContext());
                getGroupList = newRepo.FindAll();
                message = Message.Message_OK;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        private void getSqlProjectList(out List<Project> getProjectList, out string message)
        {
            getProjectList = null;
            message = null;
            try
            {
                IRepository<Project, int> newRepo = new Repository<Project, SqlDbContext, int>(new SqlDbContext());
                getProjectList = newRepo.FindAll();
                message = Message.Message_OK;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

        }

        private void getSqlTemplateList(out List<MailTemplate> getTemplateList, out string message)
        {
            getTemplateList = null;
            message = null;
            try
            {
                IRepository<MailTemplate, int> newRepo = new Repository<MailTemplate, SqlDbContext, int>(new SqlDbContext());
                getTemplateList = newRepo.FindAll();
                message = Message.Message_OK;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

        }

        private void saveSqlTemplateData(ref MailTemplate saveTemplateData, out MailTemplate getMailTemplate, out string message)
        {
            getMailTemplate = null;
            message = null;
            try
            {
                IRepository<MailTemplate, int> newRepo = new Repository<MailTemplate, SqlDbContext, int>(new SqlDbContext());
                getMailTemplate = newRepo.Create(saveTemplateData);
                message = Message.Message_OK;
            }
            catch (Exception ex)
            {              
                message = ex.Message == Message.Message_SqlInnerException ? ex.InnerException.Message : ex.Message;
            }
        }

        #endregion

        #region Oracle
        private void getOracleGroupList()
        {

        }
        #endregion
    }
}
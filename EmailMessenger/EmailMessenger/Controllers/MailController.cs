using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

using EmEntity;
using EmailMessenger.App_Code;
using EmailMessenger.Models;
using System.Net;

namespace EmailMessenger.Controllers
{
    public class MailController : Controller
    {
        SqlDbContext db = new SqlDbContext();
        //ServerDataAccess dataAccess = new ServerDataAccess();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MailPanel()
        {
            //test
             Mail<DecryptData> obj = new Mail<DecryptData>("123");

            //
            ViewBag.Data = "Server List";
            return View();
        }


        public ActionResult MailTemplate()
        {
            return View();
        }

        #region DatabaseServerConnectivity

        [HttpPost]
        public ActionResult SaveDatabase(string connectionName, string hostNameOrIp, string serverUserName, string userPassword, string databaseName, string tableName, DBServerProvider databaseProvider) //FormCollection form
        {
            string message = Message.Message_OK;

            ServerConnectVariable _serverConnectVar = new ServerConnectVariable();

            _serverConnectVar.ConnectionName = connectionName;
            _serverConnectVar.HostNameOrIP = hostNameOrIp;
            _serverConnectVar.ServerUserName = serverUserName;
            _serverConnectVar.UserPassword = userPassword;
            _serverConnectVar.DatabaseName = databaseName;
            _serverConnectVar.TableName = tableName;
            _serverConnectVar.DatabaseProvider = databaseProvider.ToString();

            //_serverConnectVar.ConnectionName = form["ConnectionName"].ToString();
            //_serverConnectVar.HostNameOrIP = form["HostNameOrIP"].ToString();
            //_serverConnectVar.DataBaseUser = form["DataBaseUser"].ToString();
            //_serverConnectVar.UserPassword = form["UserPassword"].ToString();

            try
            {

                IServerConnect _sqlServerInterface = new SqlServerConnect(
                                                         _serverConnectVar.HostNameOrIP,
                                                         _serverConnectVar.ServerUserName,
                                                         _serverConnectVar.UserPassword,
                                                         _serverConnectVar.DatabaseName,
                                                         _serverConnectVar.TableName
                                                         );

                List<Field> getFiledList = _sqlServerInterface.GetSingleTableFieldList();

                EM_ServerConnect obj = new EM_ServerConnect(
                                                           _serverConnectVar.ConnectionName,
                                                           _serverConnectVar.HostNameOrIP,
                                                           _serverConnectVar.ServerUserName,
                                                           _serverConnectVar.UserPassword,
                                                           _serverConnectVar.DatabaseName,
                                                           _serverConnectVar.TableName,
                                                           _serverConnectVar.DatabaseProvider);


                IRepository<EM_ServerConnect, int> newRepo = new Repository<EM_ServerConnect, SqlDbContext, int>(new SqlDbContext());
                var result = newRepo.Create(obj);
                //db.EM_ServerConnect.Add(obj);
                //db.SaveChanges();
                message = Message.Message_OK;

                return Json(new { TableFieldList = getFiledList, ServerInfo = _serverConnectVar, message }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { ServerInfo = _serverConnectVar, message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDbServerList()
        {

            string message = null;
            List<EM_ServerConnect> data = new List<EM_ServerConnect>();

            try
            {
                ServerDataManager dataAccess = new ServerDataManager(new ServerConnectVariable(), DBServerProvider.MS_SQL);
                dataAccess.GetDatabaseServerList(out data, out message);
                //create dummy data
                //message = Message.Message_OK;
                data.Add(new EM_ServerConnect
                {
                    Id = 0,
                    ConnectionName = "New Connect",
                    DatabaseName = "db name",
                    TableName = "table Name",
                    ServerUserName = "sa",
                    ServerHostNameIP = "130",


                });
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }


            return Json(new { message, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTableField(string[] fieldList) //FormCollection form
        {
            return Json(new { ok = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServerInfo()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetDatabaseList(string hostNameOrIP, string dataBaseUser, string userPassword, DBServerProvider databaseProvider)
        {

            List<Database> dbNameList = new List<Database>();

            string message = "";

            try
            {

                IServerConnect _sqlServerInterface = new SqlServerConnect(hostNameOrIP, dataBaseUser, userPassword, null, null);
                dbNameList = _sqlServerInterface.GetDatabaseList();

                message = Message.Message_OK;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { dbNameList, message }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region GROUP
        public ActionResult GetGroupList()
        {
            List<Group> getGroupList = new List<Group>();
            string message = null;
            ServerDataManager dataAccess = new ServerDataManager(new ServerConnectVariable(), DBServerProvider.MS_SQL);
            dataAccess.GetGroupList(out getGroupList, out message);
            //create dummy data
            //message = Message.Message_OK;
            //getGroupList.Add(new Group
            //{
            //     Id=0,
            //     ServerConnectId=1,
            //     ConditionQuery="",
            //     Description="test",
            //     GroupName="test group",
            //     Size=10                 

            //});
            return Json(new { data = getGroupList, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Project
        public ActionResult GetProjectList()
        {
            List<Project> getProjectList = new List<Project>();
            string message = null;
            ServerDataManager dataAccess = new ServerDataManager(new ServerConnectVariable(), DBServerProvider.MS_SQL);
            dataAccess.GetProjectList(out getProjectList, out message);
            //create dummy data
            message = Message.Message_OK;
            getProjectList.Add(new Project
            {
                Id = 0,
                ProjectName = "test",
                Status = true,
                ProjectTemplatePath = "path",
                StartTime = DateTime.Today,
                TimeInterval = 12,
                IntervalOption = "Days",
                ExpiredTime = DateTime.Today.AddDays(30)

            });
            return Json(new { data = getProjectList, message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProject(string ProjectName, string ProjectStatus, DateTime StartTime, DateTime ExpiredTime, int TimeInterval, string IntervalOption) //FormCollection form
        {
            string message = null;
                      

            try
            {

                Project model = new Project();
                model.ProjectName = ProjectName;
                model.Status = true;
                model.StartTime = StartTime;
                model.ExpiredTime = ExpiredTime;
                model.TimeInterval = TimeInterval;
                model.IntervalOption = IntervalOption;

                IRepository<Project, int> newRepo = new Repository<Project, SqlDbContext, int>(new SqlDbContext());
                var result = newRepo.Create(model);
                //db.EM_ServerConnect.Add(obj);
                //db.SaveChanges();
                message = Message.Message_OK;

                return Json(new { message }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        //if (file != null && file.ContentLength > 0)  
        //try 
        //{  
        //    string path = Path.Combine(Server.MapPath("~/Images"),  
        //                               Path.GetFileName(file.FileName));  
        //    file.SaveAs(path);  
        //    ViewBag.Message = "File uploaded successfully";  
        //}  
        //catch (Exception ex)  
        //{  
        //    ViewBag.Message = "ERROR:" + ex.Message.ToString();  
        //}  
        #region SendMail
        [HttpPost]
        public ActionResult SendMail(FormCollection form, HttpPostedFileBase[] file)
        {

            string FromName = form["from_name"].ToString();
            string FromEmail = form["email"].ToString();
            string FromPassword = form["from_password"].ToString();
            string Subject = form["subject"].ToString();
            string ToEmail = form["to_email"].ToString();
            string MessageBody = form["comment"].ToString();
            string Hosting = form["hosting"].ToString();

            string message = "";

            String[] createParam = new String[] { FromName, "Dept. of CSE", "jahangirnagor University" };
            MessageBody = CallTemplate(createParam, "WelcomeMailTemplate.html");

            if (SendingMail(Hosting, FromEmail, FromPassword, ToEmail, Subject, MessageBody, null, file))
            {
                message = "mail send success.";
            }
            else
            {
                message = "mail send failed!";
            }

            return RedirectToAction("MailPanel");
        }

        public bool SendingMail(string Hosting, string FromMail, string FromPassword, string toUser, string subject, string Body, string CC, HttpPostedFileBase[] files)
        {
            try
            {

                if (Hosting == "yes")
                {
                    #region SENDMAIL FROM HOST

                    MailMessage message = new MailMessage();
                    MailAddress fromAddress = new MailAddress(FromMail);
                    message.From = fromAddress;
                    message.IsBodyHtml = true;

                    //If Successfull send message both user and support
                    if (toUser != null && !string.IsNullOrEmpty(toUser))
                    {
                        message.To.Add(toUser);
                        if (CC != null && !string.IsNullOrEmpty(CC))
                        {
                            message.CC.Add(CC);
                            //  message.Bcc.Add("email-address");
                        }
                    }
                    else  //if failed then send mail only support
                    {
                        //if error
                        message.To.Add(CC);
                        //  message.Bcc.Add("");
                    }

                    message.Subject = subject;
                    message.Body = Body;
                    SmtpClient client = new SmtpClient("localhost");
                    client.Timeout = 30000;
                    client.Credentials = new System.Net.NetworkCredential(FromMail, FromPassword);
                    client.Send(message);


                    #endregion
                }
                else
                {
                    #region SEND MAIL FROM LOCAL SERVER

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(FromMail);

                    if (toUser != null && !string.IsNullOrEmpty(toUser))
                    {
                        mail.To.Add(toUser);
                        if (CC != null && !string.IsNullOrEmpty(CC))
                        {
                            mail.CC.Add(CC);

                        }
                    }
                    else
                    {
                        //if error
                        mail.To.Add(CC);

                    }


                    mail.Subject = subject;
                    mail.Priority = MailPriority.High;// added 9/9/2016
                    mail.Body = Body;

                    mail.IsBodyHtml = true;
                    SmtpServer.Port = 587;
                    SmtpServer.Timeout = 30000;

                    SmtpServer.Credentials = new System.Net.NetworkCredential(FromMail, FromPassword);
                    SmtpServer.EnableSsl = true;

                    //attachment
                    foreach (var file in files)
                    {
                        //Attachment attachment = new Attachment(file.InputStream, file.FileName);
                        //mail.Attachments.Add(attachment);
                    }


                    SmtpServer.Send(mail);

                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                //Console.WriteLine(ex.ToString());
            }
            return false;
        }


        public string CallTemplate(string[] param, string templateName) // param order replace with template {0}...{n} order
        {

            string body = "";
            try
            {
                //Read template file from the App_Data folder
                using (var sr = new StreamReader(Path.Combine(Server.MapPath("~/MailTemplate/" + templateName)))) //WelcomeMailTemplate.html"
                {
                    body = sr.ReadToEnd();
                };

                string messageBody = string.Format(body, param[0], param[1], param[2]);
                return messageBody;

            }
            catch (Exception)
            {

            }
            return body;
        }
        #endregion



        #region EMAIL_TEMPLATE

        public ActionResult GetTemplateData()
        {

            List<MailTemplate> getTemplateList = new List<MailTemplate>();
            string message = null;
            ServerDataManager dataAccess = new ServerDataManager(new ServerConnectVariable(), DBServerProvider.MS_SQL);
            dataAccess.GetTemplateList(out getTemplateList, out message);
            //create dummy data
            message = Message.Message_OK;
            getTemplateList.Add(new MailTemplate
            {
                Id = 1,
                TemplateName = "test",
                TemplateFilePath = "/mail/ashdkj"
            });

            return Json(new { data = getTemplateList, message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveEmailTemplate(FormCollection form)
        {
            MailTemplate getMailTemplate;
            string message = null;
            try
            {
             
                var path = Path.Combine(Server.MapPath("~/MailTemplate/"));             

                string TemplateName = form["TemplateName"].ToString();
                string Description = form["TemplateDescription"].ToString();
                MailTemplate saveMailData = new MailTemplate()
                {
                    TemplateName = TemplateName,
                    Description = Description,
                    TemplateFilePath = path + TemplateName
                };
            
               
                //save data
                ServerDataManager dataAccess = new ServerDataManager(new ServerConnectVariable(), DBServerProvider.MS_SQL);
                dataAccess.SaveTemplateData(ref saveMailData, out getMailTemplate, out message);

               if (Request.Files.Count > 0 && message ==Message.Message_OK)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        path = Path.Combine(Server.MapPath("~/MailTemplate/"), fileName);
                        file.SaveAs(path);
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json(new { data = getMailTemplate, message = message }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }


}
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

namespace EmailMessenger.Controllers
{
    public class MailController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MailPanel()
        {
            ViewBag.Data = "Server List";
            return View();
        }


        public ActionResult MailTemplate()
        {
            return View();
        }

        #region DatabaseServerConnectivity

        [HttpPost]
        public ActionResult SaveDatabase(string connectionName, string hostNameOrIp, string serverUserName, string userPassword, string databaseName, string tableName) //FormCollection form
        {
            string message = "ok";

            ServerConnectVariable _serverConnectVar = new ServerConnectVariable();


            _serverConnectVar.ConnectionName = connectionName;
            _serverConnectVar.HostNameOrIP = hostNameOrIp;
            _serverConnectVar.ServerUserName = serverUserName;
            _serverConnectVar.UserPassword = userPassword;
            _serverConnectVar.DbName = databaseName;
            _serverConnectVar.TableName = tableName;


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
                                                         _serverConnectVar.DbName,
                                                         _serverConnectVar.TableName); ;

                List<Field> getFiledList = _sqlServerInterface.GetSingleTableFieldList();

                EM_ServerConnect obj = new EM_ServerConnect(
                                                           _serverConnectVar.ConnectionName,
                                                           _serverConnectVar.HostNameOrIP,
                                                           _serverConnectVar.ServerUserName,
                                                           _serverConnectVar.UserPassword,
                                                           _serverConnectVar.DbName,                
                                                           _serverConnectVar.TableName);

              //  IRepository<EM_ServerConnect,int> Repo = new Repository<EM_ServerConnect,ApplicationDbContext>(ApplicationDbContext) ;

                IRepository<EM_ServerConnect,int> newRepo = new Repository<EM_ServerConnect,ApplicationDbContext,int>(new ApplicationDbContext());
                var result = newRepo.Create(obj);
                //db.EM_ServerConnect.Add(obj);
                //db.SaveChanges();
                message = "ok";

                return Json(new {TableFieldList = getFiledList,ServerInfo=_serverConnectVar,message},JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex) {
                message = ex.Message;
            }

            return Json(new { ServerInfo = _serverConnectVar, message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTableField(string[] fieldList) //FormCollection form
        {
            return Json(new {ok=1},JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServerInfo()
        {
            return View();
        }

     
        [HttpGet]
        public ActionResult GetDatabaseList(string hostNameOrIP, string dataBaseUser, string userPassword)
        {

            List<Database> dbNameList = new List<Database>();

            string message = "";

            try
            {

                IServerConnect _sqlServerInterface = new SqlServerConnect(hostNameOrIP,dataBaseUser,userPassword,null,null);
                dbNameList = _sqlServerInterface.GetDatabaseList();

                message = "ok";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { dbNameList, message }, JsonRequestBehavior.AllowGet);

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
                        Attachment attachment = new Attachment(file.InputStream, file.FileName);
                        mail.Attachments.Add(attachment);
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

    }


}
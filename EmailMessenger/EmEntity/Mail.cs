using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmEntity
{
 
   
    //https://stackoverflow.com/questions/840261/passing-arguments-to-c-sharp-generic-new-of-templated-type
    public class Mail<T>
      
        where T:class, 
        new()
       {


        private string _password = null;
        public Mail() { 
        
        }
        public Mail(string encryptedData)
        {
            IMail<T> _mailrepo = new MailRepo<T>();          

            _mailrepo.SetEncryptedData(encryptedData,out _password); //_password contain the decrypted data

            #region process using reflection Without Delegate to call T class Constructor
            //object[] args = new object[] {encryptedData }; //parameter single or multiple like new object[] { 10, encryptedData };
            //var data = (T)Activator.CreateInstance(typeof(T), args); //here var data contain the object public member 
            ////get the property name and its value
            //foreach(var property in typeof(T).GetProperties()){
            //    var name = property.Name;
            //    var value = data.GetType().GetProperty(name).GetValue(this,null).ToString();
            //}
            //string password = data.ToString();
            #endregion

            #region Process with Delegate(ConstructorDelegate) to call Constrauctor
            // you should cache this 'constructor'
            //var myConstructor = CreateConstructor(typeof(T), typeof(int), typeof(string));
            // Call the `myConstructor` fucntion to create a new instance.
            //var myObject = myConstructor(10, encryptedData);
            #endregion

            #region CallingClassPattern And How Tocall
              //public class DecryptData
              //  {         

              //  public int TestInt { get; private set; }
              //  public string TestString { get; private set; }

              //  public DecryptData() { 
    
              //  }
              //  public DecryptData(int testInt, string testString)
              //  {
              //      TestInt = testInt + 12;
              //      TestString = testString + "1234";
              //  }
       
              //  }
            #endregion

        }


        // you should cache this 'constructor'
      
        // this delegate is just, so you don't have to pass an object array. _(params)_
        public delegate object ConstructorDelegate(params object[] args);
        public ConstructorDelegate CreateConstructor(Type type, params Type[] parameters) //static
        {
            // Get the constructor info for these parameters
            var constructorInfo = type.GetConstructor(parameters);

            // define a object[] parameter
            var paramExpr = Expression.Parameter(typeof(Object[]));

            // To feed the constructor with the right parameters, we need to generate an array 
            // of parameters that will be read from the initialize object array argument.
            var constructorParameters = parameters.Select((paramType, index) =>
                // convert the object[index] to the right constructor parameter type.
                Expression.Convert(
                    // read a value from the object[index]
                    Expression.ArrayAccess(
                        paramExpr,
                        Expression.Constant(index)),
                    paramType)).ToArray();

            // just call the constructor.
            var body = Expression.New(constructorInfo, constructorParameters);

            var constructor = Expression.Lambda<ConstructorDelegate>(body, paramExpr);
            return constructor.Compile();
        }

        private void DecryptData(string encryptedData) {
          
           // you should cache this 'constructor'
           var myConstructor = CreateConstructor(typeof(T), typeof(int), typeof(string));

           // Call the `myConstructor` fucntion to create a new instance.
           var myObject = myConstructor(10, encryptedData);
        } 

        #region SendMail     
       public virtual bool SendMail(string Hosting, string FromMail, string FromPassword, string toUser, string subject, string Body, string CC,string BCC)
        {
            try
            {
                if (_password != null && !string.IsNullOrEmpty(_password))
                    FromPassword = _password;

                if (Hosting == "yes")
                {
                    #region SENDMAIL FROM HOST

                    MailMessage message = new MailMessage();
                    MailAddress fromAddress = new MailAddress(FromMail);
                    message.From = fromAddress;
                    message.IsBodyHtml = true;

                    message.To.Add(toUser);

                    if (CC != null && !string.IsNullOrEmpty(CC)){
                        message.CC.Add(CC);
                    }
                    if (BCC != null && !string.IsNullOrEmpty(BCC)){
                        message.Bcc.Add(BCC);
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
                    mail.To.Add(toUser);

                    if (CC != null && !string.IsNullOrEmpty(CC)){
                            mail.CC.Add(CC);                        
                    }
                    if (BCC != null && !string.IsNullOrEmpty(BCC)){
                        mail.Bcc.Add(BCC);
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
                    //foreach (var file in files)
                    //{
                    //    Attachment attachment = new Attachment(file.InputStream, file.FileName);
                    //    mail.Attachments.Add(attachment);
                    //}


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


       public virtual string CallTemplate(string[] param, StreamReader template) // param order replace with template {0}...{n} order string templateName,
       {

           string body = "";
           try
           {
               //Read template file from the App_Data folder
               //using (var sr = new StreamReader(Path.Combine(Server.MapPath("~/MailTemplate/" + templateName)))) //WelcomeMailTemplate.html"
               //{
               //    body = sr.ReadToEnd();
               //};

               body = template.ReadToEnd();
               int i = 0;
               foreach(var item in param){
                   string oldTxt = "{" + i + "}";
                   body = body.Replace(oldTxt,item);
                   i++;
               }               
               //string messageBody = string.Format(body, param[0], param[1], param[2]);
               //return messageBody;

               return body;

           }
           catch (Exception)
           {

           }
           return body;
       }
       #endregion

    }
}

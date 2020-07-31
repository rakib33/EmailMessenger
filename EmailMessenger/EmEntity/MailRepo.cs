using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmEntity
{
   public class MailRepo<T> :IMail<T> where T :class
    {
       string _data = "";
       
      public  void SetEncryptedData(string encrypted,out string decrypted) {

          try
          {
              //do all encryption code here 
              #region process using reflection Without Delegate to call T class Constructor
              object[] args = new object[] { encrypted }; //parameter single or multiple like new object[] { 10, encryptedData };
              var obj = (T)Activator.CreateInstance(typeof(T), args); //here var data contain the object public member 

              //get the property name and its value
              foreach (PropertyInfo property in typeof(T).GetProperties())
              {
                  //Console.WriteLine("Name: " + property.Name + ", Value: " + property.GetValue(typeof(T), null));
                  var name = property.Name;
                  _data = Convert.ToString(property.GetValue(obj, null)); //here this data will be decrypted
                  break;

              }

              decrypted = _data;
          }
          catch (Exception ex) {
              throw ex;
          }      
           //string password = obj.ToString();
           #endregion
       }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailMessenger.App_Code
{
    public class DecryptData
    {

    public string TestString { get; private set; }

    public DecryptData() { 
    
    }
    public DecryptData(string data)
    {
        TestString = data;
        GetDecryptedData();
    }

    private void GetDecryptedData(){
        TestString += "1234";
    }
       
    }
}
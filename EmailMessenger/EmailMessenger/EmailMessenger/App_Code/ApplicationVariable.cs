using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailMessenger.App_Code
{
    #region AboutStructure
    /*
     Features of C# Structures
    -----------------------------------
     1.Structures can have methods, fields, indexers, properties, operator methods, and events.

     2.Structures can have defined constructors, but not destructors. However, you cannot define a default constructor for a structure. The default constructor is automatically defined and cannot be changed.

     3.Unlike classes, structures cannot inherit other structures or classes.

     4.Structures cannot be used as a base for other structures or classes.

     5.A structure can implement one or more interfaces.

     6.Structure members cannot be specified as abstract, virtual, or protected.

     7.When you create a struct object using the New operator, it gets created and the appropriate constructor is called. Unlike classes, structs can be instantiated without using the New operator.

     8.If the New operator is not used, the fields remain unassigned and the object cannot be used until all the fields are initialized.

     Class versus Structure
     * -----------------------------
     1.Classes and Structures have the following basic differences −

     2.classes are reference types and structs are value types
     3.structures do not support inheritance
     4.structures cannot have default constructor
     */
    #endregion
    //use struct because no inheritance 
    public struct ServerConnectVariable
    {
        public string ConnectionName;
        public string HostNameOrIP;
        public string ServerUserName;
        public string UserPassword;
        public string DbName;
        public string TableName;
    };
    
}
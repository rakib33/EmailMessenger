using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmEntity
{
    public class Database
    {
        public string DatabaseName { get; set; }
        public List<BaseTable> TableList { get; set; }
    }

    public class BaseTable
    {
        public string TableName { get; set; }
        public List<BaseField> FieldList { get; set; }
    }

    public class BaseField
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
    }

    public class ServerConnect {
        public int Id { get; set; }
        public virtual string ConnectionName { get; set; }
        public virtual string ServerHostNameIP { get; set; }
        public virtual string ServerUserName { get; set; }
        public virtual string ServerPassword { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual string TableName { get; set; }
    
    }
}

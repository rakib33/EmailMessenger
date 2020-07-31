using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmEntity
{
   public interface IServerConnect
    {
       List<Field> GetSingleTableFieldList();
       List<Database> GetDatabaseList();
      
    }
}

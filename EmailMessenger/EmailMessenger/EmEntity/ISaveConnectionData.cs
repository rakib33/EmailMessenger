using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmEntity
{
   public interface ISaveServerConnect<T,Context>
    {
       T Save();
       T Delete();
       T Edit();
       List<T> GetServerConnectInfo();
    }
}

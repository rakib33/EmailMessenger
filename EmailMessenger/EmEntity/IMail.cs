using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmEntity
{
   public interface IMail<T>
    {
       void SetEncryptedData(string encrypted, out string decrypted);
      
    }
}

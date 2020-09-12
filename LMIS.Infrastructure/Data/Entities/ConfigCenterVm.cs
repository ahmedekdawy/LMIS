using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class ConfigCenterVm
    {
       public string Key { get; set; }
       public string Value { get; set; }
       public int TextType { get; set; }
       public int SexCode { get; set; }
       public int CategoryType { get; set; }
    }
}

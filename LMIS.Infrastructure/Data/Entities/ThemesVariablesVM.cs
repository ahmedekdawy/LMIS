using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
   public  class ThemesVariablesVM
    {
       public int  ThemeVariableID { get; set; }
       public int  ThemeID { get; set; }
       public string VariableID { get; set; }
       public string VariableName { get; set; }
       public string VariableColumnName { get; set; }
       public string GeneralID { get; set; }
       public string GeneralName { get; set; }
       
    }
}

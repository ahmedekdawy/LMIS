using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
   public  class ThemeVm
    {
        public int CodeNo { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string NameFr { get; set; }
        public Nullable<int> ThemeCat { get; set; }
        public Nullable<int> ThemeCat1 { get; set; }
        public Nullable<int> ThemeCat2 { get; set; }
        public string ThemeType { get; set; }
        public string UnitScale { get; set; }
        public string UnitScaleAr { get; set; }
        public string UnitScaleFr { get; set; }
        public string ThemeTypeName { get; set; }
        public int  lang { get; set; }
    }
}

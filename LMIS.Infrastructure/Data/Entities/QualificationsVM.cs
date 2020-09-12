using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class QualificationsVm
    {

        public decimal Id { get; set; }
        public string QualificationEn { get; set; }
        public string QualificationAr { get; set; }
        public string QualificationFr { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
    }
}

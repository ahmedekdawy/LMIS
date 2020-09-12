using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Data.Entities
{
   public class CalendarVM
    {
        public decimal  Id { get; set; }
        public String  Title { get; set; } //100
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int  Type { get; set; }
    }
}

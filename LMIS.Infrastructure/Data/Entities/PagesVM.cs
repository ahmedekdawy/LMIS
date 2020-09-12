using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class PagesVM
    {

        public int id { get; set; }
        public string PageCode { get; set; }
        public string Path { get; set; }
        public string Page { get; set; }
        public string PageDesc { get; set; }

    }
    public class PagesActionsVM
    {
      
        public int id { get; set; }
        public int Actionid { get; set; }
        public int PageId { get; set; }
        public string RoleId { get; set; }

    }
}

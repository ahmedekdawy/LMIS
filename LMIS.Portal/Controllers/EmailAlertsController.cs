using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.Controllers
{
    public class EmailAlertsController : ApiController
    {
        private static readonly IConfigCenterManager MgrConfigCenter = BllFactory.Singleton.ConfigCenterManager;

        [HttpPost]
        [Route("api/Send/SendNewsLetter")]
        public int SendNewsLetter()
        {
            var mr = MgrConfigCenter.GetValue(null, -1, -1, 2);
            DateTime newsLetterLastSentDate=Convert.ToDateTime(mr[0].Value);
            newsLetterLastSentDate=newsLetterLastSentDate.AddDays(int.Parse(mr[1].Value));
            if (DateTime.Compare(DateTime.Now.Date, newsLetterLastSentDate) == 0 && int.Parse(mr[1].Value)>0)
            {
                var result = BllFactory.Singleton.PortalUsersManager.SubscribeNewsLetterUsers();
                Utils.SendEmailAttachments(result.Select(s => s.Email).ToList(), "News Letter", mr[2].Value, null);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("NewsLetterLastSentDate", DateTime.Now.Date.ToString("yyyy/MM/dd"));
                MgrConfigCenter.Post(dictionary);
                return 0;
            }
            return -1;
        }
    }
}

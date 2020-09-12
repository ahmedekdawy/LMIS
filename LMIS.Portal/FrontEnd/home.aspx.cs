using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Utils.SendEmailAttachments(new List<string>() { "ahmed.ekdawy@automationconsultants.com.eg" }, "News Letter", "body", null);
            if (!IsPostBack)
            {
                literalMessage.Text = BllFactory.Singleton.ConfigCenterManager.GetValue("Home.WelcomeMessage", 1,(int)Utils.GetLanguage(), 4)[0].Value;
            }
        }
        [WebMethod]
        public static object SubscribeNewsLetter()
        {
          return BllFactory.Singleton.PortalUsersManager.SubscribeNewsLetter(LMIS.Portal.Helpers.Utils.LoggedUser );
        }
        [WebMethod]
        public static object GetNews()
        {
            var result= BllFactory.Singleton.NewsManager.GetNews("",(int)Utils.GetLanguage());
            return result;

        }
        [WebMethod]
        public static object GetPartners()
        {
           
            var result = BllFactory.Singleton.PartnersManager.GetActivePartners((int)Utils.GetLanguage(), null);
            return result;

        }
         [WebMethod]
        public static object GetSocialMediaLinks()
         {
             return BllFactory.Singleton.ConfigCenterManager.GetValue("", 1, -1, 3);
         }
         [WebMethod]
         public static object GetIndicator()
         {
             var result= new
             {
                 AvailableVacancies = BllFactory.Singleton.Job.JobsPerYear(true, true, (int)Utils.GetLanguage()).Count(w => w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now),
                 UnEployeedJobSeekers = BllFactory.Singleton.IndividualManager.JobSeekers(true ),
                 JobSeekers = BllFactory.Singleton.IndividualManager.JobSeekers(false ),
                 JobsApplied = BllFactory.Singleton.Job.JobApplied(),
                 JobsOffers = BllFactory.Singleton.Job.JobsPerYear(true, false, (int)Utils.GetLanguage()).Count
             };

             return result;
         }
        
    }
}
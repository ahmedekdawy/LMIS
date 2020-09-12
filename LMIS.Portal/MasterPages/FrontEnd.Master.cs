using System;
using System.IO;
using System.Web.Services;
using LMIS.Portal.Helpers;
using System.Web;


namespace LMIS.Portal.MasterPages
{
    public partial class FrontEnd : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (LMIS.Portal.Helpers.Utils.LoggedUser != null)
                    {
                        lblSubScriberEmail.Text = LMIS.Portal.Helpers.Utils.LoggedUser.Email;                        
                    }
                }
                catch
                {

                }
            }
        }
        protected string LinkClass(int id)
        {
            switch (id)
            {
                case 0:
                    if (Utils.LoggedUser != null) return "hide";
                    break;
                case 1:
                    if (Utils.LoggedUser == null) return "hide";
                    break;
            }
            
            return "";
        }
        protected string HeaderBackground()
        {
            var currentPageName = Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath);
            var className = "headerbackground";
            if (currentPageName.StartsWith("JobDetails") || currentPageName.StartsWith("JobApplication"))
            {className += " headerbackground5"; }
            else if (currentPageName.StartsWith("Job") || currentPageName.StartsWith("LabourExchange"))
            { className += " headerbackgroundLE"; }
          else if (currentPageName.Contains("PastAchievements"))
           { className += " PastAchievements"; }
           else if (currentPageName.Contains("NewsFeed"))
           {
               className += " NewsFeed";
           }
           else
           {
               className = "hidden";
           }
           
            
            return className;
                
               
        }
        [WebMethod]
        public static object SubscribeNewsLetter()
        {
            return BllFactory.Singleton.PortalUsersManager.SubscribeNewsLetter(LMIS.Portal.Helpers.Utils.LoggedUser);



        }


        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Session["UserInfo"] = null;
                Session.Abandon();
                Response.Redirect("~/login");
            }
            catch
            { }
        }
    }
}
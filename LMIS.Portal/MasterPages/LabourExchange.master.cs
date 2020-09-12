using LMIS.Portal.Helpers;

using System;
using System.IO;

namespace LMIS.Portal.MasterPages
{
    public partial class LabourExchange : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string TabClass(int fid, string pageNames)
        {
            var user = Utils.LoggedUser;
            var currentPageName = Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath).ToLower();

            var css = pageNames.ToLower().Contains(currentPageName) ? "active " : "";

            if (user == null || user.IsIndividual) css += "hide";
            else
            {
                if (fid == 4 && !user.IsEmployer && !user.IsTrainingProvider) css += "hide";
                if (fid == 15 && !user.IsTrainingProvider) css += "hide";
            }
            
            return css;
        }
    }
}
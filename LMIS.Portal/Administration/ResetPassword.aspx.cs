using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LMIS.Portal.Administration
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Reset_Click(object sender, EventArgs e)
        {



            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();



            var user = manager.FindByName(Email.Text);

            if (user == null)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "F026_NoUser") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
       

            Helpers.Utils.SendEmailAttachments(new List<string>() {Email.Text}
                , (string) GetGlobalResourceObject("CommonControls", "F026_Title")
                ,
                @"'<div align='center'><table align='left' border='0' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-table-lspace:2.25pt;
                   mso-table-rspace:2.25pt;mso-table-anchor-vertical:paragraph;mso-table-anchor-horizontal:   column;mso-table-left:left;mso-padding-alt:0cm 0cm 0cm 0cm'>
                   <tr><td valign='top'><p class='MsoNormal'><b><span>Dear, " + Email.Text + @", <o:p></o:p></span></b></p></td></tr>
                               <tr><td valign='top'></td></tr>
                               <tr><td valign='top'><p class='MsoNormal'> <span>" +(string) GetGlobalResourceObject("CommonControls", "X_ConfirmPasswordReseted") +@" <o:p></o:p></span></p></td></tr>
                               <tr><td valign='top'></td> </tr>
                               <tr><td valign='top'><p class='MsoNormal'><span><a href='" + Request.Url.Scheme + @"://" + Request.Url.Authority + "/ConfirmPassword?t=" + Helpers.Encryption64.Encrypt((DateTime.Now.Year * 365 * 30 + DateTime.Now.Month * 30 + DateTime.Now.Day).ToString()) + "&us=" +
                Helpers.Encryption64.Encrypt(Email.Text)  + "'>" +(string) GetGlobalResourceObject("CommonControls", "X_ClickHere") + @"</a>
                                 <o:p></o:p></span> </p> </td> </tr><tr><td valign='top'></td></tr> </table></div>",null);


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'success',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_ReviewEmail") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);



        }
    }
}
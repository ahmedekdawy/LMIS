using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LMIS.Portal.Administration
{
    public partial class ResetPasswordConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string date = (DateTime.Now.Year * 365 * 30 + DateTime.Now.Month * 30 + DateTime.Now.Day).ToString();
            string userDate = Helpers.Encryption64.Decrypt(Request.QueryString["t"]);
            if (date != userDate)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_PasswordResetedLinkExpired") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);

                return;
            }
            var user = manager.FindByName(Helpers.Encryption64.Decrypt(Request.QueryString["us"]));

            if (user == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "F026_NoUser") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);

                return;
            }

            string code = manager.GeneratePasswordResetToken(user.Id);
            manager.PasswordValidator = new PasswordValidator()
            {

                RequireUppercase = false
            };
            var result = manager.ResetPassword(user.Id, code, Password.Text);
            if (result.Succeeded)
            {

                ClientScript.RegisterStartupScript(GetType(), "alert", @" noty({
                    type: 'success',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);

            }
            else
            {
                if (result.Errors.FirstOrDefault().Contains("is already taken"))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "W001_RegisteredEmail") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                }
                else if (result.Errors.FirstOrDefault().Contains("Passwords must have at least one uppercase"))
                {

                    ClientScript.RegisterStartupScript(GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "W001_InvalidPassword") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                }
                else
                {

                    ClientScript.RegisterStartupScript(GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "+ result.Errors.FirstOrDefault()") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);

                }
            }
             
            
        }

   
    }
}
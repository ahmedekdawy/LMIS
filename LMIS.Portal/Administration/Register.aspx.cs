using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LMIS.Portal.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LMIS.Portal.Administration
{
    public partial class Register : System.Web.UI.Page
    {
        private static readonly IAspNetUsersManager Mgr = BllFactory.Singleton.AspNetUsersManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(1, 1, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                return;
            }
            var result = Mgr.GetUsersAdmin();
            gvregister .DataSource = result;
            gvregister.DataBind();
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
         
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true

            };
            //manager.PasswordValidator = new PasswordValidator()
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true ,
            //    RequireDigit = true,
            //  //  RequireLowercase = false,
            //  //  RequireUppercase = false
            //};
             manager.PasswordValidator = new PasswordValidator()
                {
               
                    RequireUppercase = false
                };
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text,EmailConfirmed=true };
            IdentityResult result;
            if (string.IsNullOrEmpty(hdfid.Value))
            {
                if (Utils.CheckPermission(2, 1, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }
                result = manager.Create(user, Password.Text);
                if (result.Succeeded)
                {
                    var userInfo = BllFactory.Singleton.AspNetUsersManager.GetUserInfo(Email.Text);
                    //internal organization id
                    userInfo.PortalUserId = 1;
                    BllFactory.Singleton.OrganizationContactInfo.Insert(userInfo);
                }

            }
            else
            {
                if (Utils.CheckPermission(3, 1, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }
                user = manager.FindById(hdfid.Value);
                if (user != null)
                {

                    user.UserName = Email.Text;
                    user.Email = Email.Text;
                }

                result = manager.Update(user);
            }
            if (result.Succeeded)
            {
                var result1 = Mgr.GetUsersAdmin();
             
                
                
                
                gvregister.DataSource = result1;
                gvregister.DataBind();
             

                ClientScript.RegisterStartupScript(GetType(), "alert", @" noty({
                    type: 'success',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                Email.Text = string.Empty;

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
                    text: '" + GetGlobalResourceObject("MessagesResource", "+result.Errors.FirstOrDefault()") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);   
            
                }
       

            }
          

                //   signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
              //  IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

           
        }

        protected void gvregister_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(4, 1, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
          
                return;
            }
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(gvregister.DataKeys[e.RowIndex].Values["UserId"].ToString());
            
            if (user != null)
            {

                user.UserName= Email.Text;


            }
            if (user.Roles.Count > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_Error103_InRelation") + @"');", true);
            
                return;
            }
            BllFactory.Singleton.OrganizationContactInfo.Delete(gvregister.DataKeys[e.RowIndex].Values["UserId"].ToString());
            IdentityResult result = manager.Delete(user);
            if (result.Succeeded)
            {
                var result1 = Mgr.GetUsersAdmin();
                gvregister.DataSource = result1;
                gvregister.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);
            
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + result.Errors.FirstOrDefault() + @"');", true);
           

            }
            e.Cancel = true;

        }

        protected void gvregister_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(3, 1, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
              
                return;
            }
            Email.Text = gvregister.DataKeys[e.NewEditIndex].Values["UserName"].ToString();
            hdfid.Value = gvregister.DataKeys[e.NewEditIndex].Values["UserId"].ToString();
            e.Cancel = true;

        }

        protected void gvregister_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvregister.PageIndex = e.NewPageIndex;
            var result = Mgr.GetUsersAdmin();
            gvregister.DataSource = result;
            gvregister.DataBind();
        }

     

        

      

       
    }
}
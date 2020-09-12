using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using LMIS.Portal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace LMIS.Portal.Administration
{   
   
    public partial class UsersInRoles : System.Web.UI.Page
    {
        private static readonly IAspNetUsersManager Mgr = BllFactory.Singleton.AspNetUsersManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (!IsPostBack)
            {
                if (Utils.CheckPermission(1, 3, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }
                var result = Mgr.GetUsersAdmin();
                ddlUsers.DataSource = result;
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataValueField = "UserId";
                ddlUsers.DataBind();



                ApplicationDbContext context = Context.GetOwinContext().Get<ApplicationDbContext>();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var resultRoles = roleManager.Roles.ToList();
                chkRoles.DataSource = resultRoles;
                chkRoles.DataTextField = "Name";
                chkRoles.DataValueField = "id";
                chkRoles.DataBind();
            }
        }

      


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(1, 3, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
            
                return;
            }
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(ddlUsers.SelectedValue);
            var roles = user.Roles.ToList();
            foreach (ListItem _chkRoles in chkRoles.Items)
            {
                _chkRoles.Selected = false;

                foreach (var role in roles)
                {
                    
                    if (_chkRoles.Value == role.RoleId)
                    {
                        _chkRoles.Selected = true;
                    }


                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(2, 3, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
            
                return;
            }
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            bool isinRoll;
            foreach (ListItem _chkRoles in chkRoles.Items)
            {


                isinRoll = manager.IsInRole(ddlUsers.SelectedValue, _chkRoles.Text);
                if (isinRoll)
                {
                    if (!_chkRoles.Selected)
                    {
                        manager.RemoveFromRole(ddlUsers.SelectedValue, _chkRoles.Text);
                    }
                }
                else
                {
                    if (_chkRoles.Selected)
                    {
                        manager.AddToRole(ddlUsers.SelectedValue, _chkRoles.Text);
                    }
                }
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);
            
        }
    }
}
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
    public partial class PagesActions : System.Web.UI.Page
    {
        private static readonly IPagesActionsManager Mgr = BllFactory.Singleton.PagesActionsManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (!IsPostBack)
            {
                if (Utils.CheckPermission(1, 5, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }
                ApplicationDbContext context = Context.GetOwinContext().Get<ApplicationDbContext>();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var resultRoles = roleManager.Roles.ToList();
                ddlRoles.DataSource = resultRoles;
                ddlRoles.DataTextField = "Name";
                ddlRoles.DataValueField = "id";
                ddlRoles.DataBind();

              ddlPages.DataSource=  Utils.GetPages();
              ddlPages.DataTextField = "PageDesc";
              ddlPages.DataValueField = "id";
              ddlPages.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(1, 5, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
         
                return;
            }
            var UsersActions = Mgr.GetPagesActions(int.Parse(ddlPages.SelectedValue), ddlRoles.SelectedValue);
            foreach (ListItem _chkActions in chkActions.Items)
            {
                _chkActions.Selected = false;

                foreach (var Uaction in UsersActions)
                {

                    if (int.Parse( _chkActions.Value) == Uaction.Actionid)
                    {
                        _chkActions.Selected = true;
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
            if (Utils.CheckPermission(2, 5, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
           
                return;
            }
            int pageActionRole = 0;
            foreach (ListItem _chkActions in chkActions.Items)
            {


                pageActionRole = Mgr.checkPageActionRole(int.Parse(_chkActions.Value),int.Parse(ddlPages.SelectedValue),ddlRoles.SelectedValue);
                if (pageActionRole>0)
                {
                    if (!_chkActions.Selected)
                    {
                        Mgr.Delete(int.Parse(ddlPages.SelectedValue), int.Parse(_chkActions.Value), ddlRoles.SelectedValue);
                    }
                }
                else
                {
                    if (_chkActions.Selected)
                    {
                        Mgr.Insert(int.Parse(ddlPages.SelectedValue), int.Parse(_chkActions.Value), ddlRoles.SelectedValue);
                    }
                }
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);
        
        }
    }
}
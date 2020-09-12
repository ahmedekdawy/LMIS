using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Portal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.Administration
{
    public partial class Roles : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(1, 2, Utils.LoggedUser.Roles) >0)
            {
                ApplicationDbContext context = Context.GetOwinContext().Get<ApplicationDbContext>();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var result = roleManager.Roles.ToList();
                gvRoles.DataSource = result;
                gvRoles.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
            }
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
           
            ApplicationDbContext context = Context.GetOwinContext().Get<ApplicationDbContext>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            IdentityResult result;
            if (string.IsNullOrEmpty( hdfid.Value))
            {
                if (Utils.CheckPermission(2, 2, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);


                    return;
                }
                result = roleManager.Create(new IdentityRole(txtRole.Text));
                
            }
            else
            {
                if (Utils.CheckPermission(3, 2, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);


                    return;
                }
                var role = roleManager.FindById(hdfid.Value);
                if (role != null)
                {

                    role.Name = txtRole.Text;

                   
                }

                result = roleManager.Update(role);
            }
             
            if (result.Succeeded)
            {
                var result1 = roleManager.Roles.ToList();
                gvRoles.DataSource = result1;
                gvRoles.DataBind();
                txtRole.Text = string.Empty;
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);

                btnCancel.Visible = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + result.Errors.FirstOrDefault() + @"');", true);
         
   
            }

        }

        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            ApplicationDbContext context = Context.GetOwinContext().Get<ApplicationDbContext>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (e.CommandName == "Edit")
            {
                if (Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
                if (Utils.CheckPermission(3, 2, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
          
                    return;
                }
                txtRole.Text = gvRoles.DataKeys[RemoveAt].Values["Name"].ToString();
                hdfid.Value = gvRoles.DataKeys[RemoveAt].Values["id"].ToString();
                btnCancel.Visible = true ;
            }
            if (e.CommandName == "Delete")
            {
                if (Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
                if (Utils.CheckPermission(4, 2, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
            
                    return;
                }
                var role = roleManager.FindById(gvRoles.DataKeys[RemoveAt].Values["id"].ToString());
                if (role != null)
                {

                    role.Name = txtRole.Text;


                }
                if (role.Users.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_Error103_InRelation") + @"');", true);
              
                    return;
                }
                IdentityResult result = roleManager.Delete(role);
                if (result.Succeeded)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);
              
                    var result1 = roleManager.Roles.ToList();
                    gvRoles.DataSource = result1;
                    gvRoles.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + result.Errors.FirstOrDefault() + @"');", true);
               

                }
            }
           
        }

    

        protected void gvRoles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }

        protected void gvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRole.Text = string.Empty;
            hdfid.Value = string.Empty;
            btnCancel.Visible = false;
        }
            


        
    }
}
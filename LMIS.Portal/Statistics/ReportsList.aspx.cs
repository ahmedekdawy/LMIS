using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace LMIS.Portal.Statistics
{
    public partial class ReportsList : System.Web.UI.Page
    {
        private static readonly IDimThemesManager Mgr = BllFactory.Singleton.DimThemes;
        private static readonly IReportsManager MgrReports = BllFactory.Singleton.Reports;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }

            if (!IsPostBack)
            {
                int lang = (int)Utils.GetLanguage();
                ddlThemeType.Items.Clear();
                ddlThemeType.Items.Add(new ListItem("", "", true));
                ddlThemeType.DataSource = Mgr.GetAllThemeType(lang);
                ddlThemeType.DataTextField = "Name";
                ddlThemeType.DataValueField = "SubID";
                ddlThemeType.DataBind();
            }
        }

        protected void ddlThemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlThemeType.SelectedIndex < 1)
            {
                return;
            }
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
              int lang = (int)Utils.GetLanguage();
            var mr = Mgr.GetAThemesesByType(Utils.LoggedUser, ddlThemeType.SelectedValue, lang);
            string dataTextField;
            switch (lang)
            {
                case 3: dataTextField = "NameAr"; break;
                case 2: dataTextField = "NameFr"; break;
                default : dataTextField = "Name"; break;
            }
            ddlThemes.Items.Clear();
            ddlThemes.Items.Add(new ListItem("", "", true));
            ddlThemes.DataSource = mr.Data;
            ddlThemes.DataTextField = dataTextField;
            ddlThemes.DataValueField = "CodeNo";
            ddlThemes.DataBind();
        }

    

        protected void gvReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateReport?RepId=" + gvReports.SelectedDataKey["ReportID"].ToString());
        }

        protected void gvReports_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(4, 10, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                e.Cancel = true;
                return;
            }
  
         int affectedRows=   MgrReports.Delete(int.Parse(e.Keys["ReportID"].ToString()));

            if (affectedRows > 0)
            {
             btnSearch_Click( sender,  e);
            }
            e.Cancel = true ;

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(1, 10, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }

            var result =
           MgrReports.GetReports(
               string.IsNullOrEmpty(ddlThemes.SelectedValue) ? 0 : int.Parse(ddlThemes.SelectedValue),
               ddlThemeType.SelectedValue, (int)Utils.GetLanguage());
            gvReports.DataSource = result;
            gvReports.DataBind();
        }


    }
}
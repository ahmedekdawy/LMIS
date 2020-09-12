using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces;
using LMIS.Infrastructure.Interfaces.Managers;
using Ninject;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.BackEnd
{
    public partial class DimThemes : System.Web.UI.Page
    {
   
        public const string PageCode = "F010";
        private static readonly IDimThemesManager Mgr = BllFactory.Singleton.DimThemes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
                int lang = (int) Utils.GetLanguage();
                ddlThemeType.DataSource = Mgr.GetAllThemeType(lang);
                ddlThemeType.DataTextField = "Name";
                ddlThemeType.DataValueField = "SubID";
                ddlThemeType.DataBind();
            }

        }

        [WebMethod]
        public static object GetAllThemese()
        {
            int lang = (int) Utils.GetLanguage();
            var mr = Mgr.GetAllThemese(Utils.LoggedUser, lang);

            return Utils.ServiceResponse(PageCode, mr);

        }

        [WebMethod]
        public static object GetAThemesesByType(string themeType)
        {
            int lang = (int) Utils.GetLanguage();
            var mr = Mgr.GetAThemesesByType(Utils.LoggedUser, themeType, lang);
            return Utils.ServiceResponse(PageCode, mr);

        }

        [WebMethod]
        public static object InsertTheme(ThemeVm theme)
        {
            int lang = (int) Utils.GetLanguage();
            var mr = Mgr.GetAllThemese(Utils.LoggedUser, lang);
            return Utils.ServiceResponse(PageCode, mr);


        }

        [WebMethod]
        public static object DeleteTheme(int codeNo, string themeType)
        {

            var mr = Mgr.Delete(Utils.LoggedUser, codeNo, themeType, (int) Utils.GetLanguage());
            return Utils.ServiceResponse(PageCode, mr);


        }

        [WebMethod]
        public static object Save(string themeType, string name, string unitScale, int codeNo)
        {
            var theme = new ThemeVm {ThemeType = themeType, Name = name, UnitScale = unitScale, CodeNo = codeNo};
            var mr = Mgr.Save(Utils.LoggedUser, theme, (int) Utils.GetLanguage());

            return Utils.ServiceResponse(PageCode, mr);

        }

        protected void btnShowInsert_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txthdfEdited.Text = string.Empty;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
          

            var theme = new ThemeVm { ThemeType = ddlThemeType.SelectedValue, Name = txtThemeInsert.Text, NameAr = txtThemeAr.Text, NameFr = txtThemeFr.Text, UnitScale = txtUnitScale.Text, UnitScaleAr = txtUnitScaleAr.Text, UnitScaleFr = txtUnitScaleFr.Text };
            if (!string.IsNullOrEmpty(txthdfEdited.Text))
            {
                if (Utils.CheckPermission(3, 11, Utils.LoggedUser.Roles) < 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'success',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    return;
                }
                theme.CodeNo = int.Parse(txthdfEdited.Text);

            }
            else
            {
                if (Utils.CheckPermission(2, 11, Utils.LoggedUser.Roles) < 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'success',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    return;
                } 
            }
       
            var mr = Mgr.Save(Utils.LoggedUser, theme, (int)Utils.GetLanguage());

            switch (mr.Status)
            {
                case ResponseStatus.Success:
                    txthdfEdited.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'success',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_OperationSuccess") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                                txtThemeInsert.Text = string.Empty;
            txtThemeAr.Text = string.Empty;
            txtThemeFr.Text = string.Empty;
            txtUnitScale .Text = string.Empty;
            txtUnitScaleAr.Text = string.Empty;
            txtUnitScaleFr.Text = string.Empty;
            txthdfEdited.Text  = string.Empty;
            txthdfEditedtype.Text = string.Empty;
            ddlThemeType.SelectedIndex = 0;
            MultiView1.ActiveViewIndex = 0;
                    break;
                case ResponseStatus.InRelation:
                    txthdfEdited.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_Error103_InRelation") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    break;
                case ResponseStatus.Exist:
                          txthdfEdited.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_Exist") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    break;
                case ResponseStatus.Exception:
                    txthdfEdited.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_Error102") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    break;

            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(3, 11, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }


        var result=    Mgr.GetAThemesesByCode(txthdfEditedtype.Text, int.Parse(txthdfEdited.Text));
            if (result != null)
            {
                txtThemeInsert.Text = result.Name;
                txtThemeAr.Text = result.NameAr;
                txtThemeFr.Text = result.NameFr;
                txtUnitScale.Text = result.UnitScale;
                txtUnitScaleAr.Text = result.UnitScaleAr;
                txtUnitScaleFr.Text = result.UnitScaleFr;
                ddlThemeType.SelectedValue = result.ThemeType;
            }

            MultiView1.ActiveViewIndex = 1;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {


            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(4, 11, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
            var mr = Mgr.Delete(Utils.LoggedUser, int.Parse(txthdfEdited.Text), txthdfEditedtype.Text,
                (int) Utils.GetLanguage());

            switch (mr.Status)
            {
                case ResponseStatus.Success:
                    txthdfEdited.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'success',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_OperationSuccess") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    break;
                case ResponseStatus.InRelation:
                    txthdfEdited.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_Error103_InRelation") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    break;
                case ResponseStatus.Exception:
                    txthdfEdited.Text = string.Empty;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_Error102") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    break;

            }

        }

    }
}
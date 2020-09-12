using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using Ninject;

namespace LMIS.Portal
{
    public partial class ThemesVariable : System.Web.UI.Page
    {
       
        [Inject]
        public IDimThemesManager _DimThemesManager { get; set; }
        public static IDimThemesManager _DimThemesManager1 { get; set; }
        public const string PageCode = "F010";
        private static readonly IDimThemesManager Mgr = BllFactory.Singleton.DimThemes;
        private static readonly IThemesVariablesManager MgrVariable = BllFactory.Singleton.ThemesVariables;
        private static readonly IGeneralCodeManager MgrGeneralCode = BllFactory.Singleton.GeneralCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }

            
                if (!IsPostBack)
                {
                    if (Utils.LoggedUser == null)
                    {
                        Response.Redirect("~/login");
                    }
                    int lang = (int)Utils.GetLanguage();
                    ddlThemeType.Items.Clear();
                    ddlThemeType.Items.Add(new ListItem("", "", true));
                    ddlThemeType.DataSource = _DimThemesManager.GetAllThemeType(lang);
                    ddlThemeType.DataTextField = "Name";
                    ddlThemeType.DataValueField = "SubID";
                    ddlThemeType.DataBind();
                }
              
            
            
        }

        
     

        protected void ddlThemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(1, 12, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
            if (ddlThemeType.SelectedIndex <1)
            {
                return;
            }
         
            int lang = (int)Utils.GetLanguage();
            string dataTextField;
            switch (lang)
            {
                case 3: dataTextField = "NameAr"; break;
                case 2: dataTextField = "NameFr"; break;
                default: dataTextField = "Name"; break;
            }
            var mr = Mgr.GetAThemesesByType(Utils.LoggedUser, ddlThemeType.SelectedValue, lang);
            ddlThemes.Items.Clear();
            ddlThemes.Items.Add(new ListItem("","",true ));
            ddlThemes.DataSource = mr.Data;
            ddlThemes.DataTextField = dataTextField;
            ddlThemes.DataValueField = "CodeNo";
            ddlThemes.DataBind();
            
        }

        protected void ddlThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(1, 12, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
            lstVaiables.Items.Clear();
            lstThemeVaiables.Items.Clear();
           
            if (ddlThemes.SelectedIndex <1)
            {
                return;
            }
            int lang = (int)Utils.GetLanguage();
            var mr = MgrVariable.GetThemesesVariable(int.Parse(ddlThemes.SelectedValue), lang);
            lstThemeVaiables.DataSource = mr;
            lstThemeVaiables.DataTextField = "VariableName";
            lstThemeVaiables.DataValueField = "VariableID";
            lstThemeVaiables.DataBind();
            var mrtheme = MgrGeneralCode.GetGeneralCode("1", lang);
        mrtheme = mrtheme.Where(p => !mr.Any(m => m.VariableID == p.GeneralID)).ToList();
        lstVaiables.DataSource = mrtheme;
        lstVaiables.DataTextField = "Name";
            lstVaiables.DataValueField = "GeneralID";
            lstVaiables.DataBind();
        }

        protected void lstVaiables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(2, 12, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
          int affected=  MgrVariable.AddThemesesVariable(int.Parse(ddlThemes.SelectedValue), lstVaiables.SelectedValue);
            ddlThemes_SelectedIndexChanged(sender, e);
        }

        protected void lstThemeVaiables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(4, 12, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
            int affected=MgrVariable.DeleteThemesesVariable(int.Parse(ddlThemes.SelectedValue), lstThemeVaiables.SelectedValue);
            ddlThemes_SelectedIndexChanged(sender, e);
        }
    }
}
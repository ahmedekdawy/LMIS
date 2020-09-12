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
using System.Drawing;
using System.Reflection;
using LMIS.Bll.Managers;
//using DevExpress.Data.Linq;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using LMIS.Utlities.Helpers;
using Ninject;
using Statistics;

namespace LMIS.Portal
{
    public partial class CreateReport : System.Web.UI.Page
    {
        public string ConnectionString12 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static int i;
        

        public const string PageCode = "F010";
        private static readonly IDimThemesManager Mgr = BllFactory.Singleton.DimThemes;
        private static readonly IThemesVariablesManager MgrVariable = BllFactory.Singleton.ThemesVariables;
        private static readonly IGeneralCodeManager MgrGeneralCode = BllFactory.Singleton.GeneralCode;
        private static readonly ISubCodeManager MgrSubCode = BllFactory.Singleton.SubCode;
        private static readonly IReportsManager MgrReports = BllFactory.Singleton.Reports;
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
                ddlThemeType.DataSource = Mgr.GetAllThemeType(lang);
                ddlThemeType.DataTextField = "Name";
                ddlThemeType.DataValueField = "SubID";
                ddlThemeType.DataBind();
                ChangingVariableDrp.Enabled = false;
                RunningVariableDrp.Enabled = false;
                YearFromDrp.Visible = false;
                YearToDrp.Visible = false;
                YearFromLbl.Visible = false;
                YearToLbl.Visible = false;
                SelectLbl.Visible = false;
                if (Request.QueryString["RepId"] != null)
                {
                    BindReport( sender,  e);
                }
            }
            if (ddlThemes.SelectedIndex > 0)
            {
                 CreateRadioBtn( sender,  e);

            }
         
 
        }

        private void BindReport(object sender, EventArgs e)
        {

            var result = MgrReports.GetReportsByID(int.Parse(Request.QueryString["RepId"]), (int)Utils.GetLanguage());
            ddlThemeType.SelectedValue = result.ThemeType;
            ddlThemeType_SelectedIndexChanged(sender, e);
            ddlThemes.SelectedValue = result.ThemeID.ToString();
            ddlThemes_SelectedIndexChanged(sender, e);
            txtNameEn.Text = result.ReportEnName;
            txtNameAr.Text = result.ReportArName;
            txtNameFr.Text = result.ReportFrName;
            txtSource.Text = result.Source;
            txtSourceAr.Text  = result.SourceAr;
            txtSourceFr.Text = result.SourceFr;
            txtPublishYear.Text =Convert.ToString( result.PublishYear);
            hdfReportId.Value = result.ReportID.ToString();
            ChangingVariableDrp.SelectedValue = result.changingVariableID;
            ChangingVariableDrp_SelectedIndexChanged(sender, e);
            RunningVariableDrp.SelectedValue = result.RunningVariableID;
            RunningVariableDrp_SelectedIndexChanged( sender,  e);
            if (result.YearID != null)
            {
                YearFromDrp.SelectedValue = result.YearID;
                YearFromDrp.Visible = true;
                YearToDrp.Visible = true;
            }
            if (result.YearTo != null)
            {
                YearToDrp.SelectedValue = result.YearTo;
                YearFromDrp.Visible = true;
                YearToDrp.Visible = true;
            }
        }

        //protected void CreateReportBtn_Click(object sender, EventArgs e)
        //{
        //    var reportVm = new ReportVM();
        //    reportVm.ReportEnName = txtNameEn.Text;
        //    reportVm.ReportArName = txtNameAr.Text;
        //    reportVm.ReportFrName = txtNameFr.Text;
        //    reportVm.YearTo = YearToDrp.SelectedValue;
        //    reportVm.changingVariableID = ChangingVariableDrp.SelectedValue;
        //    reportVm.RunningVariableID = RunningVariableDrp.SelectedValue;
         
        //    for (int y = 0; y < i; y++)
        //    {
        //        RadioButtonList VariablesRadio = (RadioButtonList) (mydiv.FindControl("VariablesRadio" + y));
        //        RadioButtonList VariableValuehck = (RadioButtonList) (mydiv.FindControl("VariablesValueChck" + y));
        //        if (VariableValuehck.SelectedIndex < 0 || VariablesRadio.SelectedIndex < 0)
        //        {
        //            VariableValuehck.SelectedIndex = 0;
        //            VariablesRadio.SelectedIndex = 0;
        //        }
        //        if (VariablesRadio.SelectedIndex >= 0 && VariableValuehck.SelectedIndex >= 0)
        //        {
        //           reportVm.GetType().GetProperty(VariablesRadio.SelectedValue).SetValue(reportVm,VariablesRadio.SelectedValue);
                  
        //        }

        //    }

           
        //        for (int y = 0; y < i; y++)
        //        {
        //            RadioButtonList VariablesRadio = (RadioButtonList)(mydiv.FindControl("VariablesRadio" + y));
        //            RadioButtonList VariableValuehck = (RadioButtonList)(mydiv.FindControl("VariablesValueChck" + y));
        //            if (VariableValuehck.SelectedIndex < 0 || VariablesRadio.SelectedIndex<0)
        //            {
        //                VariableValuehck.SelectedIndex = 0;
        //                VariablesRadio.SelectedIndex = 0;
        //            }
        //            if (VariablesRadio.SelectedIndex >= 0 && VariableValuehck.SelectedIndex >= 0)
        //            {
        //                insertsql = insertsql + ",'" + VariableValuehck.SelectedValue.ToString() + "'";
                        
        //            }
                    
        //        }
                
        //            insertsql = insertsql + " )";
        //            MgrReports.Save(reportVm);
        //            txtNameEn.Text = "";
        //            ddlThemes.SelectedIndex = 0;
        //            ChangingVariableDrp.SelectedIndex = 0;
        //            ChangingVariableDrp.Enabled = false;
        //            RunningVariableDrp.SelectedIndex = 0;
        //            RunningVariableDrp.Enabled = false;
        //            YearFromDrp.Visible = false;
        //            YearFromLbl.Visible = false;
        //            YearToDrp.Visible = false;
        //            YearToLbl.Visible = false;
        //            NotificationLbl.Text = "The Report has been created.";
        //            //    Response.Redirect("SelectFile.aspx");
                
        //}

        protected void CreateReportBtn_Click(object sender, EventArgs e)
        {

            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(2,10, Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }




            if (string.IsNullOrEmpty(txtNameAr.Text) && string.IsNullOrEmpty(txtNameEn.Text) &&string.IsNullOrEmpty(txtNameFr.Text))
            {
                NotificationLbl.Text = "Enter Report Name.";
                return;
            }

            new ReportsManagers().CreateReport(hdfReportId.Value,txtNameEn.Text ,txtNameAr.Text ,txtNameFr.Text ,txtSource.Text,txtSourceAr.Text ,txtSourceFr.Text,txtPublishYear.Text
                ,ddlThemes.SelectedValue,RunningVariableDrp.SelectedValue,ChangingVariableDrp.SelectedValue,YearFromDrp.SelectedValue,YearToDrp.SelectedValue
                , mydiv);
            txtNameEn.Text = string.Empty;
            txtNameAr.Text = string.Empty;
            txtNameFr.Text = string.Empty;
            txtSource.Text = string.Empty;
            txtSourceAr.Text = string.Empty;
            txtSourceFr.Text = string.Empty;
            txtPublishYear.Text = string.Empty;
            ddlThemes.SelectedIndex = 0;
            ChangingVariableDrp.SelectedIndex = 0;
            ChangingVariableDrp.Enabled = false;
           // RunningVariableDrp.SelectedIndex = 0;
            RunningVariableDrp.Enabled = false;
            YearFromDrp.Visible = false;
            YearFromLbl.Visible = false;
            YearToDrp.Visible = false;
            YearToLbl.Visible = false;
            ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);
        
           // NotificationLbl.Text = "The Report has been created.";
           // ClientScript.RegisterStartupScript(GetType(), "Success", " lmis.ajaxSuccessHandler('Success');",true);
          //  Response.Redirect("~/Statistics/ReportsList.aspx");

        }

        protected void CreateRadioBtn(object sender, EventArgs e)
    {
       
      

        var variables = MgrVariable.GetVariables(RunningVariableDrp.SelectedValue,ChangingVariableDrp.SelectedValue ,string.IsNullOrEmpty(ddlThemes.SelectedValue)?0: int.Parse( ddlThemes.SelectedValue),(int)Utils.GetLanguage());
        i = variables.Count;
        ReportVM result;
        if (Request.QueryString["RepId"] != null)
        {
            result = MgrReports.GetReportsByID(int.Parse(Request.QueryString["RepId"]), (int) Utils.GetLanguage());

        }
        else
        {
            result = null;
        }
        object  sub;
        PropertyInfo propertyInfo;
        if (variables.Count>0)
            {
                var subcode = MgrSubCode.GetAllSubCode(variables[0].VariableID, (int)Utils.GetLanguage());
                for (int x = 0; x < variables.Count; x++)
                {

                    SelectLbl.Visible = true;
                    subcode = MgrSubCode.GetAllSubCode(variables[x].GeneralID, (int)Utils.GetLanguage());
                   

                    RadioButtonList VariablesRadio = new RadioButtonList();
                    ListItem item1;
                    item1 = new ListItem(variables[x].GeneralName, variables[x].VariableColumnName);
                    ListItem item2;
                    item2 = new ListItem("All", variables[x].VariableColumnName);
                    VariablesRadio.Items.Add(item1);
                    VariablesRadio.Items.Add("unselect");
                    VariablesRadio.ID = "VariablesRadio" + x;
                    VariablesRadio.RepeatDirection = RepeatDirection.Horizontal;
                    VariablesRadio.SelectedIndexChanged += this.VariablesRadio_SelectedIndexChanged;
                    VariablesRadio.AutoPostBack = true;
                    VariablesRadio.Font.Bold = true;
                    VariablesRadio.ForeColor = Color.DarkBlue;
                  
                    RadioButtonList VariablesValueRadio = new RadioButtonList();
                    foreach (var item in subcode)
                    {
                        VariablesValueRadio.Items.Add(new ListItem(item.Name, item.SubID ));
                    }
                  //  VariablesValueRadio.DataSource = subcode;
                  //  VariablesValueRadio.DataTextField = "Name";
                   // VariablesValueRadio.DataValueField = "SubID";
                    VariablesValueRadio.ID = "VariablesValueChck" + x;
                    VariablesValueRadio.RepeatDirection = RepeatDirection.Horizontal;
                    VariablesValueRadio.RepeatColumns = 5;
                    VariablesValueRadio.CellPadding = 10;
                  //  VariablesValueRadio.DataBind();
                    VariablesValueRadio.Visible = false;
                    TableCell tcell = new TableCell();
                    if (result != null)
                    {
                        propertyInfo = result.GetType().GetProperty(variables[x].VariableColumnName);
                        sub = propertyInfo.GetValue(result, null);



                        if (sub!=null )
                        {
                            if (sub.ToString().Substring(0, 3) == variables[x].GeneralID)
                            {
                                VariablesRadio.SelectedValue = variables[x].VariableColumnName;
                               
                                VariablesValueRadio.SelectedValue = sub.ToString();

                                VariablesValueRadio.Visible = true;

                            }
                        }

                    }
                    tcell.Controls.Add(VariablesRadio);
                    tcell.Controls.Add(VariablesValueRadio);
                   
                    TableRow tr = new TableRow();
                    tr.Cells.Add(tcell);
                    Table1.Rows.Add(tr);
                    Table1.DataBind();
                   
                }
            }
       
        
    }

    protected void VariablesRadio_SelectedIndexChanged(object sender, EventArgs e)
    {

        for (int x = 0; x < i; x++)
        {
            RadioButtonList RadioLst = (RadioButtonList)(mydiv.FindControl("VariablesRadio" + x));
            RadioButtonList VariablesValueRadio = (RadioButtonList)(mydiv.FindControl("VariablesValueChck" + x));
            if(RadioLst.SelectedIndex  == 0)
            {
                VariablesValueRadio.Visible = true;
            }
            else
            {
                VariablesValueRadio.Visible = false;
            }

        }
    }



    protected void ChangingVariableDrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ChangingVariableDrp.SelectedValue) || string.IsNullOrEmpty(ddlThemes.SelectedValue))
        {
            return;
        }
        SqlConnection con1 = new SqlConnection(ConnectionString12);
            string sqlstrg = "SELECT dbo.GeneralCode.GeneralID, dbo.GeneralCode.Name, dbo.ThemesVariables.ThemeID " +
                             "FROM   dbo.ThemesVariables " +
                             "INNER JOIN   dbo.GeneralCode ON dbo.ThemesVariables.VariableID = dbo.GeneralCode.GeneralID " +
                             "WHERE        (dbo.GeneralCode.GeneralID <> "+ChangingVariableDrp.SelectedValue+") " +
                             " AND (dbo.ThemesVariables.ThemeID = "+ddlThemes.SelectedValue+")" +
                             " And GeneralCode.LanguageID=" + (int)Utils.GetLanguage(); 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstrg;
            con1.Open();
            SqlDataAdapter daAdapter = new SqlDataAdapter(cmd);
            DataSet asd = new DataSet();
            daAdapter.Fill(asd);
            if (asd.Tables[0].Rows.Count != 0)
            {
                RunningVariableDrp.DataSource = asd;
                RunningVariableDrp.DataTextField = "Name";
                RunningVariableDrp.DataValueField = "GeneralID";
                ListItem lstitem = new ListItem("Please Select variable", "");
                RunningVariableDrp.Items.Add(lstitem);
                RunningVariableDrp.DataBind();
                con1.Close();
                RunningVariableDrp.Enabled = true;
            }
            if ( ChangingVariableDrp.SelectedValue == "004")
            {
                 con1 = new SqlConnection(ConnectionString12);
                 sqlstrg = "select SubID, Name from SubCode where GeneralID = '004' and LanguageID=" + (int)Utils.GetLanguage() + " order by Name DESC";
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = sqlstrg;
                con1.Open();
                SqlDataAdapter daAdapter1 = new SqlDataAdapter(cmd1);
                DataSet asd1 = new DataSet();
                daAdapter1.Fill(asd1);
                if (asd1.Tables[0].Rows.Count != 0)
                {
                    YearFromDrp.DataSource = asd1;
                    YearFromDrp.DataTextField = "Name";
                    YearFromDrp.DataValueField = "SubID";
                    YearFromDrp.DataBind();
                    YearToDrp.DataSource = asd1;
                    YearToDrp.DataTextField = "Name";
                    YearToDrp.DataValueField = "SubID";
                    YearToDrp.DataBind();
                    con1.Close();

                }
                YearFromDrp.Visible = true;
                YearToDrp.Visible = true;
                YearFromLbl.Visible = true;
                YearToLbl.Visible = true;
            }
            if (RunningVariableDrp.SelectedValue != "004" && ChangingVariableDrp.SelectedValue != "004")
            {
                YearFromDrp.Visible = false;
                YearToDrp.Visible = false;
                YearFromLbl.Visible = false;
                YearToLbl.Visible = false;
            }
            
    }

    protected void RunningVariableDrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RunningVariableDrp.SelectedValue == "004" )
        {
            SqlConnection con1 = new SqlConnection(ConnectionString12);
            string sqlstrg = "select SubID, Name from SubCode where GeneralID = '004' and LanguageID=" + (int)Utils.GetLanguage() + " order by Name DESC";
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = sqlstrg;
            con1.Open();
            SqlDataAdapter daAdapter1 = new SqlDataAdapter(cmd1);
            DataSet asd1 = new DataSet();
            daAdapter1.Fill(asd1);
            if (asd1.Tables[0].Rows.Count != 0)
            {
                YearFromDrp.DataSource = asd1;
                YearFromDrp.DataTextField = "Name";
                YearFromDrp.DataValueField = "SubID";
                YearFromDrp.DataBind();
                YearToDrp.DataSource = asd1;
                YearToDrp.DataTextField = "Name";
                YearToDrp.DataValueField = "SubID";
                YearToDrp.DataBind();
                con1.Close();
                
            }
            YearFromDrp.Visible = true;
            YearToDrp.Visible = true;
            YearFromLbl.Visible = true;
            YearToLbl.Visible = true;
        }
        if (RunningVariableDrp.SelectedValue != "004" && ChangingVariableDrp.SelectedValue != "004")
        {
            YearFromDrp.Visible = false;
            YearToDrp.Visible = false;
            YearFromLbl.Visible = false;
            YearToLbl.Visible = false;
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
        string dataTextField;
        switch (lang)
        {
            case 3: dataTextField = "NameAr"; break;
            case 2: dataTextField = "NameFr"; break;
            default: dataTextField = "Name"; break;
        }
        var mr = Mgr.GetAThemesesByType(Utils.LoggedUser, ddlThemeType.SelectedValue, lang);
        ddlThemes.Items.Clear();
        ddlThemes.Items.Add(new ListItem("", "", true));
        ddlThemes.DataSource = mr.Data;
        ddlThemes.DataTextField = dataTextField;
        ddlThemes.DataValueField = "CodeNo";
        ddlThemes.DataBind();
    }

    protected void ddlThemes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangingVariableDrp.Items.Clear();
        
            SqlConnection con = new SqlConnection(ConnectionString12);
            string sqlstrg1 = "SELECT dbo.GeneralCode.GeneralID, dbo.GeneralCode.Name " +
                              "FROM dbo.DimThemes " +
                              "INNER JOIN dbo.ThemesVariables ON dbo.DimThemes.CodeNo = dbo.ThemesVariables.ThemeID " +
                              "INNER JOIN dbo.GeneralCode ON dbo.ThemesVariables.VariableID = dbo.GeneralCode.GeneralID " +
                              "WHERE        (dbo.DimThemes.CodeNo = " +(string.IsNullOrEmpty(ddlThemes.SelectedValue)? "-1" :ddlThemes.SelectedValue) +
                              ") and GeneralCode.LanguageID=" + (int) Utils.GetLanguage();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = sqlstrg1;
            con.Open();
            SqlDataAdapter daAdapter1 = new SqlDataAdapter(cmd1);
            DataSet asd1 = new DataSet();
            daAdapter1.Fill(asd1);
        
        if (asd1.Tables[0].Rows.Count != 0)
        {
            ChangingVariableDrp.DataSource = asd1.Tables[0];
            ChangingVariableDrp.DataTextField = "Name";
            ChangingVariableDrp.DataValueField = "GeneralID";
            ChangingVariableDrp.DataBind();
            ListItem lstitem = new ListItem("Please select Variable", "");

            ChangingVariableDrp.Items.Insert(0, lstitem);

        }
        con.Close();
        cmd1.Dispose();
        daAdapter1.Dispose();
        asd1.Dispose();
        ChangingVariableDrp.Enabled = true;
    }

     
    
       
    }
}
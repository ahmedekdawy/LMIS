
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.BackEnd
{
    public partial class SubCode : System.Web.UI.Page
    {
        public const string PageCode = "F028";
        private static readonly IGeneralCodeManager Mgr = BllFactory.Singleton.GeneralCode;
        private static readonly ISubCodeManager MgrSub = BllFactory.Singleton.SubCode;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
                if (Utils.CheckPermission(1, 6, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }
                int lang = (int)Utils.GetLanguage();
                var generalCodesResult = Mgr.GetGeneralCode(lang);
                ddlGeneralCodes.DataSource = generalCodesResult;
                ddlGeneralCodes.DataTextField = "Name";
                ddlGeneralCodes.DataValueField = "GeneralID";
                ddlGeneralCodes.DataBind();

                ddlGeneralCodesParent.DataSource = generalCodesResult;
                ddlGeneralCodesParent.DataTextField = "Name";
                ddlGeneralCodesParent.DataValueField = "GeneralID";
                ddlGeneralCodesParent.DataBind();
            }

        }

        [WebMethod]
        public static List<SubCodeVm> GetAllSubCode(string generalId)
        {

            return MgrSub.GetAllSubCode(generalId, (int)Utils.GetLanguage());

        }
        [WebMethod]
        public static void  Save(string generalId, string NameEn, string NameAr, string NameFr, string subid)
        {



            if (string.IsNullOrEmpty(NameEn) || string.IsNullOrEmpty(NameAr) || string.IsNullOrEmpty(NameFr))
            {
                Utils.GetSubCode(ref NameEn, ref NameAr, ref NameFr);
            }
            if (string.IsNullOrEmpty(subid))
            {
                subid = MgrSub.GetMaxSubCode(generalId, 1);
                subid = subid.Substring(0, 3) + (Convert.ToInt32(subid.Substring(3)) + 1).ToString("00000");
            }

            SubCodeVm[] vm = new SubCodeVm[3];
            vm[0] = new SubCodeVm { GeneralID = generalId, SubID = subid };
            vm[1] = new SubCodeVm { GeneralID = generalId, SubID = subid };
            vm[2] = new SubCodeVm { GeneralID = generalId, SubID = subid };
            vm[0].LanguageID = 1;
            vm[0].Name = NameEn;
            vm[1].LanguageID = 2;
            vm[1].Name = NameFr;
            vm[2].LanguageID = 3;
            vm[2].Name = NameAr;
            MgrSub.Save(vm[0]);


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            
           
            string subid = string.Empty;
            string NameEn = txtNameEn.Text;
            string NameAr = txtNameAr.Text;
            string NameFr = txtNameFr.Text;
            int result;
            if (string.IsNullOrEmpty(txtNameEn.Text) || string.IsNullOrEmpty(txtNameAr.Text) || string.IsNullOrEmpty(txtNameFr.Text))
            {
                Utils.GetSubCode(ref NameEn, ref NameAr, ref NameFr);
            }
            if (string.IsNullOrEmpty(txthdfEdited.Text))
            {
                if (Utils.CheckPermission(2, 6, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }
                subid = MgrSub.GetMaxSubCode(ddlGeneralCodes.SelectedValue, 1);
                if (!string.IsNullOrWhiteSpace(subid))
                {
                    subid = subid.Substring(0, 3) + (Convert.ToInt32(subid.Substring(3)) + 1).ToString("00000");
                }
                else
                {
                    subid = ddlGeneralCodesParent.SelectedValue + "00001";
                }
                List<SubCodeVm> vm = new List<SubCodeVm>();
                vm.Add(new SubCodeVm { GeneralID = ddlGeneralCodes.SelectedValue, SubID = subid });
                vm.Add(new SubCodeVm { GeneralID = ddlGeneralCodes.SelectedValue, SubID = subid });
                vm.Add(new SubCodeVm { GeneralID = ddlGeneralCodes.SelectedValue, SubID = subid });
                vm[0].LanguageID = 1;
                vm[0].Name = NameEn;
                vm[1].LanguageID = 2;
                vm[1].Name = NameFr;
                vm[2].LanguageID = 3;
                vm[2].Name = NameAr;
                if (!string.IsNullOrEmpty(ddlSubCodesParent.SelectedValue))
                {
                    vm[0].ParentSubCodeID = ddlSubCodesParent.SelectedValue;
                    vm[1].ParentSubCodeID = ddlSubCodesParent.SelectedValue;
                    vm[2].ParentSubCodeID = ddlSubCodesParent.SelectedValue;
                }
               // MgrSub.Save(vm[0]);
                //MgrSub.Save(vm[1]);

                result = MgrSub.Save(vm);
            }
            else
            {
                if (Utils.CheckPermission(3, 6, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }
                subid = txthdfEdited.Text;
                result = MgrSub.Update(new SubCodeVm { LanguageID = 1, SubID = subid, Name = txtNameEn.Text });
                result = MgrSub.Update(new SubCodeVm { LanguageID = 2, SubID = subid, Name = txtNameFr.Text });
                result = MgrSub.Update(new SubCodeVm { LanguageID = 3, SubID = subid, Name = txtNameAr.Text });
            }

            if (result > 0)
            {
                txthdfEdited.Text = string.Empty;
                MultiView1.ActiveViewIndex = 0;
                 txtNameEn.Text=string.Empty ;
                 txtNameAr.Text = string.Empty;
                 txtNameFr.Text = string.Empty;
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);

            }
            else if (result == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert",
                    "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_Exist") + @"');", true);
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert",
                    "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_ValidationErrors") + @"');", true);

            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(3, 6, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                return;
            }
            var result = MgrSub.GetSubCode(txthdfEdited.Text);
            foreach (var item in result)
            {
                if (item.LanguageID == 1)
                {
                    txtNameEn.Text = item.Name;
                }
                else if (item.LanguageID == 2)
                {
                    txtNameFr.Text = item.Name;
                }
                else if (item.LanguageID == 3)
                {
                    txtNameAr.Text = item.Name;
                }
            }
            ddlGeneralCodesParent.SelectedValue = result[0].GeneralID;
            if (!string.IsNullOrEmpty(result[0].ParentSubCodeID))
            {
               
                ddlGeneralCodesParent_SelectedIndexChanged(sender, e);
                ddlSubCodesParent.SelectedValue = result[0].ParentSubCodeID;

            }

            MultiView1.ActiveViewIndex = 1;
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(4, 6, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                return;
            }

            int result = MgrSub.Delete(txthdfEdited.Text);
            if (result > 0)
            {
                txthdfEdited.Text = string.Empty;
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);

            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_Error103_InRelation") + @"');", true);

            }
        }

        protected void btnShowInsert_Click(object sender, EventArgs e)
        {
            txtNameEn.Text = string.Empty;
            txtNameAr.Text = string.Empty;
            txtNameFr.Text = string.Empty;
            ddlGeneralCodesParent.SelectedIndex = 0;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txthdfEdited.Text = string.Empty;
            txtNameEn.Text = string.Empty;
            txtNameAr.Text = string.Empty;
            txtNameFr.Text = string.Empty;
            ddlGeneralCodesParent.SelectedIndex = 0;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void ddlGeneralCodesParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lang = (int)Utils.GetLanguage();
            var SubCodesResult = MgrSub.GetAllSubCode(ddlGeneralCodesParent.SelectedValue, lang);
            ddlSubCodesParent.DataSource = SubCodesResult;
            ddlSubCodesParent.DataTextField = "Name";
            ddlSubCodesParent.DataValueField = "SubID";
            ddlSubCodesParent.DataBind();
        }




    }
}
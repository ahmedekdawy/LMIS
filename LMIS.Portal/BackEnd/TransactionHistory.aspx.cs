using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;

namespace LMIS.Portal.BackEnd
{
    public partial class TransactionHistory : System.Web.UI.Page
    {
        private static readonly IRequestLogManager Mgr = BllFactory.Singleton.RequestLog;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
                var mr = Mgr.ListRequestLog();

                if (Utils.CheckPermission(1, 15, Utils.LoggedUser.Roles) < 1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                    return;
                }

                var requstType = mr.DistinctBy(d => d.RequestType.desc).Select(s => new { RequestTypeDesc = s.RequestType.desc});
                var portalUserName = mr.DistinctBy(d => d.PortalUserName).Select(s => new { PortalUserName = s.PortalUserName });
                ddlUsesr.DataSource = portalUserName;
                ddlUsesr.DataBind();
                ddlRequestType.DataSource = requstType;
                ddlRequestType.DataBind();
            }
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (Utils.CheckPermission(5, 15, Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                return;
            }
            var mr = Mgr.ListRequestLog().Where(w => 
                (w.PortalUserName == ddlUsesr.SelectedValue || ddlUsesr.SelectedValue == "All") &&
                (w.RequestType.desc == ddlRequestType.SelectedValue || ddlRequestType.SelectedValue == "All") &&

                (ddlStatus.SelectedValue == "Pending" ? w.Approval == Approval.Pending : w.Approval ==w.Approval ) &&
                (ddlStatus.SelectedValue == "Approved" ? w.Approval == Approval.Approved : w.Approval == w.Approval) &&
                (ddlStatus.SelectedValue == "Rejected" ? w.Approval == Approval.Rejected : w.Approval == w.Approval) &&
                 (ddlTransactionType.SelectedValue == "New" ? w.DeleteDate == null && w.UpdateDate == null : w.PostDate == w.PostDate) &&
                  (ddlTransactionType.SelectedValue == "Updated" ? w.UpdateDate != null && w.DeleteDate == null : w.UpdateDate == w.UpdateDate) &&
                (ddlTransactionType.SelectedValue == "Deleted" ? w.DeleteDate != null : w.DeleteDate == w.DeleteDate) &&
                (
                ((string.IsNullOrEmpty(txtFrom.Text) ? w.PostDate == w.PostDate : w.PostDate.Date  >= DateTime.Parse(txtFrom.Text).Date ) &&
               (string.IsNullOrEmpty(txtTo.Text) ? w.PostDate == w.PostDate : w.PostDate.Date  <= DateTime.Parse(txtTo.Text).Date ) )||
                ((string.IsNullOrEmpty(txtFrom.Text) ? w.UpdateDate == w.UpdateDate : w.UpdateDate.HasValue && w.UpdateDate.Value.Date  >= DateTime.Parse(txtFrom.Text).Date) &&
               (string.IsNullOrEmpty(txtTo.Text) ? w.UpdateDate == w.UpdateDate : w.UpdateDate.HasValue && w.UpdateDate.Value.Date <= DateTime.Parse(txtTo.Text).Date)) ||
                ((string.IsNullOrEmpty(txtFrom.Text) ? w.DeleteDate == w.DeleteDate : w.DeleteDate.HasValue && w.DeleteDate.Value.Date  >= DateTime.Parse(txtFrom.Text).Date) &&
               (string.IsNullOrEmpty(txtTo.Text) ? w.DeleteDate == w.DeleteDate : w.DeleteDate.HasValue && w.DeleteDate.Value.Date  <= DateTime.Parse(txtTo.Text).Date))
                )
                ).Select(s => new { PortalUserName = s.PortalUserName, Date = s.PostDate, RequestTypeDesc = s.RequestType.desc, Status = (s.Approval == Approval.Approved ? "Approved" : (s.Approval == Approval.Pending ? "Pending" : "Rejected")), TransactionType = (s.DeleteDate != null ? "Deleted" : (s.UpdateDate != null ? "Updated" : "New")) });



            ReportViewer.LocalReport.ReportPath = "BackEnd\\rdlc\\TransactionHistory.rdlc";


            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", mr));


            ReportViewer.DataBind();
            ReportViewer.LocalReport.Refresh();
        }
    }
}
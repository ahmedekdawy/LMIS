<%@ Page Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="OpportunityList.aspx.cs" Inherits="LMIS.Portal.LabourExchange.OpportunityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/OpportunityList.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab5default">
        <div id="oppTable" class="col-sm-12">
            <div class="row form-divider col-sm-12 ">
                <div class="col-lg-4"><hr></div>
                <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F005_AllOpp")%></div>
                <div class="col-lg-4"><hr></div>
            </div>
            <div class="col-sm-12 text-center">
                <a id="postNew" href="../LabourExchange/OpportunityPost#anchor" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F005_PostNewOpportunity")%>
                </a>
            </div>
            <table id="grdOpportunities" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F005_OppTitle")%></th>
                        <th></th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                    </tr>
                </tfoot>
                <tbody data-bind="foreach: OpportunityList">
                    <tr data-bind="attr: { id: RowId }">
                        <td data-bind="text: Title"></td>
                        <td>
                            <a href="#" title=" View " data-bind="click: $root.ViewOpportunity" class="editBtn"><i class="fa fa-eye"></i></a>
                            <a href="#" title=" Edit " data-bind="click: $root.EditOpportunity" class="editBtn"><i class="fa fa-edit"></i></a>
                            <a href="#" title=" Delete " data-bind="click: $root.DeleteOpportunity"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>

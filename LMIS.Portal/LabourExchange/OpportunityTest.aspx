<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="OpportunityTest.aspx.cs" Inherits="LMIS.Portal.LabourExchange.OpportunityTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/OpportunityTest.js" async="async"></script>
    
    <table class="table-responsive">
        <thead>
        <tr>
            <th style="width: 65px;">Opp ID</th>
            <th style="width: 90px;">Contact ID</th>
            <th style="width: 120px;">Contact Name</th>
            <th style="width: 100px;">Organization</th>
            <th style="width: 100px;">Title</th>
            <th style="width: 100px;">File Path</th>
            <th style="width: 100px;">Start Date</th>
            <th style="width: 100px;">End Date</th>
            <th style="width: 85px;">Informal ?</th>
            <th style="width: 80px;">Approval</th>
            <th style="width: 85px;">Approved ?</th>
        </tr>
        </thead>
        <tbody data-bind="foreach: OpportunityList">
        <tr>
            <td data-bind="text: OpportunityId"></td>
            <td data-bind="text: ContactId"></td>
            <td data-bind="text: ContactName"></td>
            <td data-bind="text: Organization"></td>
            <td>
                <a href="#" data-bind="click: $root.InspectItem">
                    <span data-bind="text: Title"></span>
                </a>
            </td>
            <td data-bind="text: FilePath"></td>
            <td data-bind="text: moment(StartDate()).format($root.MomentDate)"></td>
            <td data-bind="text: moment(EndDate()).format($root.MomentDate)"></td>
            <td data-bind="text: IsInformal"></td>
            <td data-bind="text: Approval"></td>
            <td data-bind="text: IsApproved"></td>
        </tr>
        </tbody>
        <tfoot>
        <tr>
            <td colspan="11">
                <mark><span id="Result"></span></mark>
            </td>
        </tr>
        <tr>
            <td colspan="11">Total Number of Opportunities: ( <span data-bind="text: OpportunityCount"></span> )</td>
        </tr>
        <tr>
            <td colspan="3">Click to retrieve session ID >></td>
            <td colspan="8" data-bind="text: SessionId, click: GetSessionId(p)" style="font-weight: bold;"></td>
        </tr>
        </tfoot>
    </table>

</asp:Content>
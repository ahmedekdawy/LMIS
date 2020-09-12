<%@ Page Title="<%$ Resources:CommonControls, F006 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="LMIS.Portal.LabourExchange.EventList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/EventList.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
            <div class="row form-divider col-sm-12">
                <div class="col-lg-4"><hr></div>
                <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F006_AllEvents")%></div>
                <div class="col-lg-4"><hr></div>
            </div>

            <div class="col-sm-12 text-center">
                <a id="postNew" href="../LabourExchange/EventPost#anchor" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F006_PostNewEvent")%>
                </a>
            </div>

            <table id="grdEvents" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F006_Titles")%></th>
                        <th></th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                    </tr>
                </tfoot>
                <tbody data-bind="foreach: EventList">
                    <tr data-bind="attr: { id: RowId }">
                        <td data-bind="text: Title"></td>
                        <td>
                            <a href="#" title=" View " data-bind="click: $root.View" class="editBtn"><i class="fa fa-eye"></i></a>
                            <a href="#" title=" Edit " data-bind="click: $root.Edit" class="editBtn"><i class="fa fa-edit"></i></a>
                            <a href="#" title=" Delete " data-bind="click: $root.Delete"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

    </div>

</asp:Content>
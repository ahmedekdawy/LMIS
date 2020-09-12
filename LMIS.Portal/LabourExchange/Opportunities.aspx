<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Opportunities.aspx.cs" Inherits="LMIS.Portal.LabourExchange.Opportunities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/Opportunities.js" async="async"></script>

    <div class="container text-center">
        <div class="blocks" data-bind="foreach: OpportunityList">

            <div class="col-md-4 col-sm-6" data-bind="attr: { id: RowId, title: Title }">
                <div class="block-wrap">
                    <div style="overflow: hidden; width: 310px; height: 180px;">
                        <img data-bind="attr: { src: FilePath } " style="width: auto; height: auto; -moz-min-width: 100%; -ms-min-width: 100%; -o-min-width: 100%; -webkit-min-width: 100%; min-width: 100%; min-height: 100%;" class="img-responsive" onerror="this.onerror=null; this.src='../images/block_01.png';"/>
                    </div>
                    <h2 style="white-space: nowrap; overflow: hidden; -moz-text-overflow: ellipsis; text-overflow: ellipsis;" data-bind="text: Title"></h2>
                    <div class="col-md-12 text-center">
                        <a class="btn  btn-primary btn-new-color" href="#" data-bind="click: $root.ViewOpportunity">
                            <i class="fa fa-link"></i><%=GetGlobalResourceObject("CommonControls", "X_ViewMore")%>
                        </a>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
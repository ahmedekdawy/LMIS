<%@ Page Title="<%$ Resources:CommonControls, F033 %>" Language="C#" MasterPageFile="~/MasterPages/Review.master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="LMIS.Portal.BackEnd.Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <link href="../css/star-rating.css" rel="stylesheet" />
    <script src="../js/star-rating.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../BackEnd/Scripts/Review.js" async="async"></script>
    
    <div class="col-sm-10 col-sm-offset-1" style="display: none;" data-bind="visible: Approval()">
        <label class="ui-state-highlight ui-corner-all" style="cursor: pointer; width: 100%; padding: 5px 15px 5px 15px; text-align: center;" data-bind="visible: Approval() === 1 && !vmReview">
            <%= GetGlobalResourceObject("MessagesResource", "X_Pending") %>
        </label>
        <label class="ui-state-default ui-corner-all" style="cursor: pointer; width: 100%; padding: 5px 15px 5px 15px; text-align: center;" data-bind="visible: Approval() === 2">
            <%=GetGlobalResourceObject("MessagesResource", "X_Approved")%>
        </label>
        <label class="ui-state-error ui-corner-all" style="cursor: pointer; width: 100%; padding: 5px 15px 5px 15px;" data-bind="visible: Approval() === 3">
            <%=GetGlobalResourceObject("MessagesResource", "X_Rejected")%>
            <br/><strong><span data-bind="text: RejectReason()"></span></strong>
        </label>
    </div>

    <div class="col-sm-10 col-sm-offset-1 grdR" style="padding: 15px">
        <table style="border-spacing: 0; width: 100%;" data-bind="visible: DataSet().length > 0">
            <thead>
                <tr>
                    <th colspan="2" data-bind="html: DataSetTitle"></th>
                </tr>
            </thead>
            <tbody data-bind="foreach: DataSet">
                <tr>
                    <td data-bind="html: label" style="width: 150px"></td>
                    <td>
                        <span data-bind="visible: !trilingual && !starRating, html: text"></span>
                        <input data-bind="visible: starRating, css: { 'StarRating': starRating }, attr: { value: text }" readonly="readonly" type="number" data-size="xs" data-symbol="*" >
                        <ul data-bind="visible: trilingual">
                            <li data-bind="visible: !lmis.string.isNullOrWhiteSpace(text.English), html: text.English"></li>
                            <li data-bind="visible: !lmis.string.isNullOrWhiteSpace(text.French), html: text.French"></li>
                            <li data-bind="visible: !lmis.string.isNullOrWhiteSpace(text.Arabic), html: text.Arabic"></li>
                        </ul>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

</asp:Content>
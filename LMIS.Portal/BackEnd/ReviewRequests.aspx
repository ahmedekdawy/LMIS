<%@ Page Title="<%$ Resources:CommonControls, F033 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ReviewRequests.aspx.cs" Inherits="LMIS.Portal.BackEnd.ReviewRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../BackEnd/Scripts/ReviewRequests.js" async="async"></script>

    <div id="dlgReassign" title="<%=GetGlobalResourceObject("MessagesResource", "F033_ReassignAdmin")%>">
        <form>
            <fieldset>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F033_AssignTo")%></label>
                    <select class="form-control" data-bind="options: AdminOptions, value: dlgReassign.userInput, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
                </div>
                <!-- Allow form submission with keyboard without duplicating the dialog button -->
                <input type="submit" tabindex="-1" style="position:absolute; top:-1000px">
            </fieldset>
        </form>
    </div>

    <div class="tab-pane fade in active" id="tab-content">
        <div id="Table" class="col-sm-12">
            <div class="row form-divider col-sm-12 ">
                <div class="col-lg-4"><hr></div>
                <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F033")%></div>
                <div class="col-lg-4"><hr></div>
            </div>
            <div class="row form-divider col-sm-12 text-center" data-bind="visible: IsSuperAdmin">
                <button class="btn btn-default" type="button" data-bind="click: ToggleAllRequests">
                    <i class="fa fa-search"></i>
                    <span data-bind="html: (AsSuperAdmin() ? '<%=GetGlobalResourceObject("CommonControls", "F033_ListMyRequests")%>' : '<%=GetGlobalResourceObject("CommonControls", "F033_ListAllRequests")%>')"></span>
                </button>
            </div>
            <div class="row form-divider col-sm-12 ">
                <div class="col-lg-5">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F033_ReqType")%></label>
                        <select class="form-control" data-bind="options: ReqTypeOptions, value: ReqType, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                </div>
                <div class="col-lg-2"></div>
                <div class="col-lg-5">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F033_TrType")%></label>
                        <select class="form-control" data-bind="options: TrTypeOptions, value: TrType, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                </div>
            </div>
            <div class="row form-divider col-sm-12 ">
                <table id="grd" class="display" style="border-spacing: 0; width: 100%;">
                    <thead>
                        <tr>
                            <th><%=GetGlobalResourceObject("CommonControls", "X_Title")%></th>
                            <th><%=GetGlobalResourceObject("CommonControls", "F033_ReqType")%></th>
                            <th><%=GetGlobalResourceObject("CommonControls", "X_PostDate")%></th>
                            <th><%=GetGlobalResourceObject("CommonControls", "F033_TrType")%></th>
                            <th><%=GetGlobalResourceObject("CommonControls", "X_User")%></th>
                            <th></th>
                            <th data-bind="visible: AsSuperAdmin()"></th>
                        </tr>
                    </thead>
                    <tbody data-bind="foreach: VmList">
                        <tr data-bind="attr: { id: RowId }">
                            <td data-bind="html: Title"></td>
                            <td data-bind="text: RequestTypeDesc"></td>
                            <td data-bind="text: lmis.format.dateToString(Date)"></td>
                            <td data-bind="text: TransactionType"></td>
                            <td data-bind="text: PortalUserName"></td>
                            <td>
                                <a class="actionLabel" data-bind="click: $root.Review"><%=GetGlobalResourceObject("CommonControls", "X_Review")%></a>
                            </td>
                            <td data-bind="visible: $root.AsSuperAdmin()">
                                <a class="actionLabel" data-bind="text: AdminName, click: function() { $root.dlgReassign.open($data, AdminId); }"></a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</asp:Content>
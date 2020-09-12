<%@ Page Title="<%$ Resources:CommonControls, F022 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="JobApplication.aspx.cs" Inherits="LMIS.Portal.LabourExchange.JobApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/JobApplication.js" async="async"></script>
    
    <div id="tab-content" class="col-sm-10 col-sm-offset-1 grd">
        <h2 data-bind="text: Requirements() ? '[' + Requirements().Id + '] ' + Requirements().Title : ''"></h2>
        <p></p>
        <table style="border-spacing: 0; width: 100%">
            <thead>
                <tr>
                    <th><%=GetGlobalResourceObject("CommonControls", "F022_DocName")%></th>
                    <th><%=GetGlobalResourceObject("CommonControls", "X_Template")%></th>
                    <th><%=GetGlobalResourceObject("CommonControls", "X_SelectedFile")%></th>
                    <th data-bind="visible: Mode() !== 'v'"></th>
                </tr>
            </thead>
            <tbody data-bind="foreach: Application" style="min-height: 35px;">
                <tr>
                    <td data-bind="text: DocName"></td>
                    <td>
                        <a class="text-center" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(Template), attr: { href: Template }">
                            <%=GetGlobalResourceObject("CommonControls", "X_Download")%>
                        </a>
                    </td>
                    <td data-bind="text: File() ? File().name : ''"></td>
                    <td data-bind="visible: $root.Mode() !== 'v'">
                        <a class="actionLabel" data-bind="click: $root.BrowseFiles">
                            <%=GetGlobalResourceObject("CommonControls", "X_Browse")%>
                        </a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    
    <div class="col-sm-10 col-sm-offset-1 text-center" data-bind="visible: Application().length > 0 && Mode() !== 'v'">
        <input type="file" id="hdnFileBrowser" data-bind="attr: { accept: AcceptedFiles }, event: { change: ValidateFile }" style="height: 0; visibility: hidden;"/>
        <input value="<%=GetGlobalResourceObject("CommonControls", "X_UploadAndSave")%>" class="btn btn-primary nextBtn btn-lg" type="button" data-bind="click: StartWorkflow" />
    </div>

    <div style="display: none;">
        <div id="F022_AppTemplate"><%=GetGlobalResourceObject("CommonControls", "F022_AppTemplate")%></div>
        <div id="F022_Validations"><%=GetGlobalResourceObject("MessagesResource", "F022_SelectAllFiles")%></div>
        <div id="Step2">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_Saving")%></p>
        </div>
    </div>

</asp:Content>
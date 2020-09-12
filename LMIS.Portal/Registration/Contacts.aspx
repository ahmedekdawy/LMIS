<%@ Page Language="C#" MasterPageFile="~/MasterPages/LabourExchange.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="LMIS.Portal.Registration.Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Registration/Scripts/Contacts.js" async="async"></script>
            
    <div id="tab-content" class="col-sm-12">
            
        <div class="row form-divider col-sm-12">
            <div class="col-lg-4"><hr></div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "R003_AllContacts")%></div>
            <div class="col-lg-4"><hr></div>
        </div>

        <div class="col-sm-12 text-center">
            <a id="postNew" href="Contact.aspx#anchor" class="btn btn-success nextBtn btn-lg">
                <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "R003_AddContact")%>
            </a>
        </div>

        <table id="grdContacts" class="display" style="border-spacing: 0; width: 100%;">
            <thead>
                <tr>
                    <th><%=GetGlobalResourceObject("CommonControls", "X_Email")%></th>
                    <th><%=GetGlobalResourceObject("CommonControls", "W004_FullName")%></th>
                    <th><%=GetGlobalResourceObject("CommonControls", "W004_Department")%></th>
                    <th></th>
                </tr>
            </thead>
            <tbody data-bind="foreach: ContactsList">
                <tr data-bind="attr: { id: RowId }">
                    <td data-bind="text: UserName"></td>
                    <td data-bind="text: FullName"></td>
                    <td data-bind="text: Department"></td>
                    <td>
                        <a href="#" title=" View " data-bind="click: $root.View" class="editBtn"><i class="fa fa-eye"></i></a>
                        <a href="#" title=" Edit " data-bind="click: $root.Edit" class="editBtn"><i class="fa fa-edit"></i></a>
                        <a href="#" title=" Delete " data-bind="click: $root.Delete"><i class="fa fa-trash-o"></i></a>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>

</asp:Content>
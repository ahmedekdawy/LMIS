<%@ Page Title="<%$ Resources:CommonControls,F023_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="LMIS.Portal.Administration.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function confirmDel() {
            return confirm('<%=GetGlobalResourceObject("CommonControls", "X_ConfirmDelete")%>');
        }
    </script>
    <form runat="server">
                   <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
             <p class="text-danger">
             <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
      <div class="form-horizontal">
            <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtRole" CssClass="col-md-2 control-label"><%=GetGlobalResourceObject("CommonControls", "F023_Role")%></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtRole" MaxLength="256" CssClass="form-control validationElement" />
                <asp:HiddenField ID="hdfid" runat="server" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRole"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="*" />
                
            </div>
        </div>
                 <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="btnAddRole"  Text="<%$ Resources:CommonControls,X_Save1 %>" CssClass="btn btn-default" OnClick="btnAddRole_Click" />
                <asp:Button runat="server" ID="btnCancel" Visible="False" ValidationGroup="1" Text="<%$ Resources:CommonControls,X_Cancel %>" CssClass="btn btn-default" OnClick="btnCancel_Click" />
            </div>
        </div>
           <div class="form-group">
               <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" OnRowCommand="gvRoles_RowCommand" DataKeyNames="Name,id" OnRowEditing="gvRoles_RowEditing" OnRowDeleting="gvRoles_RowDeleting" >
                   <Columns >
                     
                       <asp:BoundField DataField="Name" HeaderText="<%$ Resources:CommonControls,F023_Role %>" SortExpression="Name"  />
                       
                     
                       <asp:TemplateField ShowHeader="False">
                           <ItemTemplate>
                               <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"  CssClass="fa fa-edit" ></asp:LinkButton>
                               <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" OnClientClick="return confirmDel();" CommandName="Delete"  CssClass="fa fa-trash-o" ></asp:LinkButton>
                           </ItemTemplate>
                       </asp:TemplateField>
                       
                     
                   </Columns>
               </asp:GridView>
           </div>
     </div>
       </form>
</asp:Content>

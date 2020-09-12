<%@Page Title="<%$ Resources:CommonControls,F024_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="LMIS.Portal.Administration.Register" %>
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
        
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label"><%=GetGlobalResourceObject("CommonControls", "F009_Email")%> *</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control validationElement" TextMode="Email " />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label"><%=GetGlobalResourceObject("CommonControls", "X_Password")%> *</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control validationElement" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="*" />
                  </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label"><%=GetGlobalResourceObject("CommonControls", "F009_RetypePassword")%> *</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control validationElement" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="<%$ Resources:CommonControls,F026_passwordConfirmRequired %>" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="<%$ Resources:CommonControls,F026_passwordConfirmnotMatch %>" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="<%$ Resources:CommonControls,X_Save1%>" CssClass="btn btn-default" />
                <br />
                <asp:HiddenField ID="hdfid" runat="server" />
            </div>
        </div>
            <div class="form-group" style="text-align:center">
               <asp:GridView ID="gvregister"  runat="server" AutoGenerateColumns="False" DataKeyNames="UserName,UserId" OnRowDeleting="gvregister_RowDeleting" OnRowEditing="gvregister_RowEditing" AllowPaging="True" OnPageIndexChanging="gvregister_PageIndexChanging" PageSize="20" >
                   <Columns >
                     
                       <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:CommonControls,F009_Email %>"  SortExpression="UserName"  >
                       
                     
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       
                     
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

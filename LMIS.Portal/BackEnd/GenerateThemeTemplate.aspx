<%@ Page Title="<%$ Resources:CommonControls,F011_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="GenerateThemeTemplate.aspx.cs" Inherits="LMIS.Portal.BackEnd.GenerateThemeTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
      
      

    </script>
        <form id="form1" runat="server">
             <div class="form-group">
                 
                 <label> <%=GetGlobalResourceObject("CommonControls", "F010_Themes")%>* </label>
                <asp:DropDownList ID="ddlThemeType" runat="server" AppendDataBoundItems="True" CssClass="form-control ddlThemeType selectpicker validationElement" AutoPostBack="True" OnSelectedIndexChanged="ddlThemeType_SelectedIndexChanged" ></asp:DropDownList>
           
             </div>
             <div class="form-group">
                 <%=GetGlobalResourceObject("CommonControls", "F010_Title")%> *
                 
                <asp:DropDownList ID="ddlThemes" runat="server" CssClass="form-control validationElement" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlThemes_SelectedIndexChanged" ></asp:DropDownList>
                 
             </div>
          <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "F011_ChooseReport")%>*</label>
                             <asp:DropDownList ID="ddlReports" runat="server" CssClass="form-control validationElement" OnSelectedIndexChanged="ddlReports_SelectedIndexChanged">
                                 <asp:ListItem Selected="True"></asp:ListItem>
                             </asp:DropDownList>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="red" ControlToValidate="ddlReports" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </div>   
   <div class="form-group">
   </div>
              <div class="form-group">
                  
                  <asp:Button ID="btnGenerate" runat="server" Text="<%$ Resources:CommonControls,F011_GenerateTemplater %>" CssClass="btn  btn-default btn-primary" OnClick="btnGenerate_Click" ValidationGroup="1" />
                  
              </div>
 
            </form>
</asp:Content>

<%@ Page Title="<%$ Resources:CommonControls,F025_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="UsersInRoles.aspx.cs" Inherits="LMIS.Portal.Administration.UsersInRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <link href="../css/chosen.css" rel="stylesheet" />
    <form runat="server">
                   <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
        <div id="container">
			<h2><%=GetGlobalResourceObject("CommonControls", "F025_Users")%></h2>
			<div class="side-by-side clearfix">

				<div class="form-group">

					<asp:DropDownList data-placeholder="<%$ Resources:CommonControls,F025_UserSelect %>" runat="server" ID="ddlUsers" class="chzn-select" Style="width: 350px;" AppendDataBoundItems="True" >
					   
						
					</asp:DropDownList>

				</div>
			</div>
          <div class="form-group">
              <br/>
                <asp:Button runat="server" ID="btnSearch"  Text="<%$ Resources:CommonControls,X_Search %>" CssClass="btn btn-group-lg search-icon-block" OnClick="btnSearch_Click"  />
            </div>
            <div class="form-group">
                <br/>
                <asp:CheckBoxList ID="chkRoles" runat="server"   RepeatColumns="5" RepeatDirection="Horizontal" >
                </asp:CheckBoxList>
                
            </div>
            <div class="form-group">
                 <asp:Button runat="server" ID="btnSave"  Text="<%$ Resources:CommonControls,X_Save1 %>" CssClass="btn btn-success  btn-lg btn-large" OnClick="btnSave_Click"  />
            </div>

    <script src="../js/chosen.jquery.js"  type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>		</div>

    </form>

</asp:Content>

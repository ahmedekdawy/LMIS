<%@ Page Title="<%$ Resources:CommonControls, F007 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="LMIS.Portal.Administration.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section id="login" >
	  <div class="container white-bg">

			<div class="col-xs-12">
				<div class="form-wrap">
				<h1><%=GetGlobalResourceObject("CommonControls", "F007_logWith")%></h1>
					<form role="form" action="javascript:;" method="post" id="login-form" autocomplete="off" >
						<div class="form-group">
							<label for="email" class="sr-only"><%=GetGlobalResourceObject("CommonControls", "X_Email")%></label>
							<input type="email" name="email" id="email" class="form-control email" placeholder="<%=GetGlobalResourceObject("CommonControls", "X_Email")%>" >
						</div>
						<div class="form-group">
							<label for="key" class="sr-only"><%=GetGlobalResourceObject("CommonControls", "X_Password")%></label>
							<input type="password" name="key" id="key" class="form-control password" placeholder="<%=GetGlobalResourceObject("CommonControls", "X_Password")%>" >
						</div>
						<div class="checkbox">
							<span class="character-checkbox" onclick="showPassword()"></span>
							<span class="label"><%=GetGlobalResourceObject("CommonControls", "X_Showpassword")%></span>
						</div>
						<input  type="submit" id="btnlogin" class="btn btn-custom btn-lg btn-block" value="<%=GetGlobalResourceObject("CommonControls", "F007")%>" onclick="login()" />
					</form>
					<a href="../Administration/ResetPassword.aspx" class="forget" data-toggle="modal" ><%=GetGlobalResourceObject("CommonControls", "X_Forgotpassword")%></a>
					<hr>
				</div>
			</div>

		</div>
		 </section>

    <script src="Administration/Scripts/LogIn.js"></script>
    <script type="text/javascript ">
      
            function showPassword() {
                if ($(".password").attr("type") == "password") {
                    $(".password").attr("type", "text");

                } else {
                    $(".password").attr("type", "password");
                }
            }
      
    </script>
</asp:Content>

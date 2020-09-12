<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomError.aspx.cs" Inherits="BookMasterUI.CustomError1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>   
 <link href="App_Themes/ThemeAR/Style_AR.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Error</title>

</head>
 
<body>
<center>
<div class="AlertImg">
<img src="/images/Alert.jpg" id="img" runat="server" alt="Alert" width="84" height="84" />&nbsp;</div>
<div class="alert alert-error">
        <a class="close">×</a>
   <h4>
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Overline="False"
                    Font-Size="9pt" Font-Underline="False" ForeColor="Red" 
                    Text="لقد حدث خطأ و قد تم تسجيل الخطأ لإصلاحة. " 
                   ></asp:Label>
   
   </h4> 
                <asp:HyperLink ID="HyperLink_back" runat="server" 
                     Text="عودة للصفحة الرئيسية"></asp:HyperLink>
              <input id="TextBox_Message" type="hidden" runat="server" />
      </div></center>
</body>
</html>



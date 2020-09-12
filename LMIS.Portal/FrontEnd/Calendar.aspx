<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="LMIS.Portal.FrontEnd.Calendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset='utf-8' />
<link href="../css/fullcalendar.css" rel="stylesheet" />
<link href="../css/fullcalendar.print.css" rel="stylesheet" media="print" /><link href="../css/lmis.css" rel="stylesheet" />
<script src="../Scripts/moment.js"></script>
<script src="../js/jquery.js"></script>
<script src="../js/fullcalendar.min.js"></script>
<script src="../FrontEnd/Scripts/Calendar.js"></script>
<script src="../js/jquery.dataTables.js"></script>
<script src="../Scripts/Extensions/lmis.js"></script>
      <%= Scripts.Render("~/bundles/noty") %>
<style>

	body {
		margin: 40px 10px;
		padding: 0;
		font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
		font-size: 14px;
	}

	#calendar {
		max-width: 900px;
		margin: 0 auto;
	}

</style>
</head>

<body>
    <form id="form1" runat="server">
        <center>
      <table>
          <tr>
            <td class="col-sm-3" style="width: 20px; height: 20px; background:#4aab73;color:#fff"><%=GetGlobalResourceObject("CommonControls", "F006_Events")%> </td>
            <td class="col-sm-3" style="width: 20px; height: 20px; background:#dfba24;color:#fff"><%=GetGlobalResourceObject("CommonControls", "Menu_Opportunities")%> </td>
            <td class="col-sm-3" style="width: 20px; height: 20px; background: #15385c;color:#fff"> <%=GetGlobalResourceObject("CommonControls", "F038_Training")%></td>
           </tr>
      </table>
            </center>
  	<div id='calendar'></div>
    </form>
</body>
</html>

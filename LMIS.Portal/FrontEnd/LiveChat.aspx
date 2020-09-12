<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiveChat.aspx.cs" Inherits="LMIS.Portal.FrontEnd.LiveChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title><%=GetGlobalResourceObject("CommonControls", "Menu_StartChat")%></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
   <link href="../css/lmis.css" rel="stylesheet" />
    <script src="../js/jquery.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.1.min.js"></script>
    <script src="../signalR/hubs"></script>
    <script src="../Scripts/knockout-3.3.0.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../Scripts/Extensions/perfSurf.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
       <div id="live-chat-panel" class="live-chat-panel">
        <div class="top-title container" >

            
            <div class="col-lg-2" id="live-chat-hide"><i class="fa fa-minus-square"></i></div>
        </div>
        <div class="live-chat-panel-body " >
          
                  <div class="form-group-chat col-lg-12" data-bind="foreach: messages" style='overflow:scroll;' >
                
                      <div data-bind="html: $data"></div>
                      
                </div>
                <div class="form-group-chat" style="position: relative;">
                    <%--<textarea  class=" col-md-12" id="txtMessage" data-bind="text: message" placeholder="Message" rows="2"></textarea>--%>
                     <textarea class="form-control col-lg-11" id="textarea" name="textarea" data-bind="value: message" placeholder="Message" ></textarea>
                 
                    <input type="button" data-bind="click: $root.sendMessage" class="btn btn-default btn-success col-lg-1" style="margin-top: 2px; margin-left: 2px;position: relative;" value="Send"  />
                    <input type="button" data-bind="click: $root.sendEmail" class="btn btn-default btn-success col-lg-1" style="margin-top: 2px; margin-left: 2px;position: relative;" value="Send Email"  />
                </div>
            
        </div>
    </div>
    </form>

</body>
</html>

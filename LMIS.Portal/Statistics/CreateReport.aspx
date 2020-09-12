<%@ Page Title="<%$ Resources:CommonControls,F017_Title %>" Language="C#" AutoEventWireup="true" CodeBehind="CreateReport.aspx.cs" Inherits="LMIS.Portal.CreateReport" MasterPageFile="~/MasterPages/BackEnd.Master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         
     
      <form id="form1" runat="server">
                 <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
            <script src="http://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
           <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css"/>
           <script type="text/javascript">
               function testvalid(value,id)
               {                 
                   if ($('#' + id).val() != "") {
                       $('#' + id ).removeClass("validationElement");
                   }
                   else {
                       $('#' + id).addClass("validationElement");
                   }              
               
               }

               //$('.validationElement').find('input[type=text],select').change(function() {              
               //        alert("in ba2a");                
                  
               //});
               function showpopup() {
                   if ($(".ddlThemeType").val() === '') {
                       $(".ddlThemeType").focus();
                       return false;
                   }
                   $("#popup").dialog({

                       width: 500
                   });
                   return false;
               }
               var IsDigit = (function () {
                   var KeyIdentifierMap =
                   {
                       End: 35,
                       Home: 36,
                       Left: 37,
                       Right: 39,
                       'U+00007F': 46		// Delete
                   };

                   return function (e) {
                       if (!e)
                           e = event;

                       var iCode = (e.keyCode || e.charCode);

                       if (!iCode && e.keyIdentifier && (e.keyIdentifier in KeyIdentifierMap))
                           iCode = KeyIdentifierMap[e.keyIdentifier];

                       return (
                               (iCode >= 48 && iCode <= 57)		// Numbers
                               || (iCode >= 35 && iCode <= 40)		// Arrows, Home, End
                               || iCode == 8						// Backspace
                               || iCode == 46						// Delete
                               || iCode == 9						// Tab
                       );
                   }
               })();

           </script>
           
    <div id="mydiv" runat="server">
  
        <table >
            <tr>
                <td colspan="2">
                 
                 <asp:Label ID="NotificationLbl" runat="server" ForeColor="Red"></asp:Label>
                </td>
                
                 
                <td rowspan="8" valign="top">
                 
                
                 <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Chart.gif" Height="121px" Width="256px" />
                </td>
                
                 
            </tr>
            <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F017_NameEn")%> **</label></td>
                 <td>
                        
                 
            
                 
                 <asp:TextBox ID="txtNameEn" onchange="testvalid(this.value,this.id)" Columns="100" MaxLength="100" runat="server" CssClass="form-control input-xlarge col-lg-5 validationElement"  placeholder="Please enter report Name" ></asp:TextBox>
             
                    
                 </td>
                 
            </tr>
            <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F017_NameAr")%> **</label></td>
                 <td>
             
               
            
                 
                 <asp:TextBox ID="txtNameAr" onchange="testvalid(this.value,this.id)" runat="server" MaxLength="100" CssClass="form-control input-xlarge validationElement"  placeholder="Please enter report Name" ></asp:TextBox>
                
                 

                 </td>
                 
            </tr>
            <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F017_NameFr")%><span> **</span></label></td>
                 <td>
            
                 <asp:TextBox ID="txtNameFr" onchange="testvalid(this.value,this.id)" runat="server" MaxLength="100" CssClass="form-control input-xlarge validationElement"  placeholder="Please enter report Name" ></asp:TextBox>
                
              
        </td>
                 
            </tr>
             <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F017_Source")%><span> </span></label></td>
                 <td>
            
                 <asp:TextBox ID="txtSource" runat="server" MaxLength="200" CssClass="form-control input-xlarge"  placeholder="Please enter source" ></asp:TextBox>
                
              
        </td>
                 
            </tr>
              <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F017_SourceAr")%><span> </span></label></td>
                 <td>
            
                 <asp:TextBox ID="txtSourceAr" runat="server" MaxLength="200" CssClass="form-control input-xlarge"  placeholder="Please enter source" ></asp:TextBox>
                
              
        </td>
                 
            </tr>

                    <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F017_SourceFr")%><span> </span></label></td>
                 <td>
            
                 <asp:TextBox ID="txtSourceFr" runat="server" MaxLength="200" CssClass="form-control input-xlarge"  placeholder="Please enter source" ></asp:TextBox>
                
              
        </td>
                 
            </tr>
             <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F017_PublishYear")%><span> </span></label></td>
                 <td>
            
                 <asp:TextBox ID="txtPublishYear" runat="server" MaxLength="4" onkeypress="return IsDigit(event);" CssClass="form-control input-xlarge"  placeholder="Please enter publish year" ></asp:TextBox>
                
              
        </td>
                 
            </tr>
            <tr>
                <td><label> <%=GetGlobalResourceObject("CommonControls", "F010_Themes")%> *</label></td>
                 <td>
            
                 
                <asp:DropDownList ID="ddlThemeType" runat="server" AppendDataBoundItems="True" CssClass="form-control ddlThemeType selectpicker validationElement" AutoPostBack="True" OnSelectedIndexChanged="ddlThemeType_SelectedIndexChanged" ></asp:DropDownList>
           
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="ok" ControlToValidate="ddlThemeType" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                
                </td>
                 
            </tr>
            <tr>
               <td> <label> <%=GetGlobalResourceObject("CommonControls", "F010_Theme")%>* </label></td>
                 <td>
            
                 
                <asp:DropDownList ID="ddlThemes" runat="server" CssClass="form-control validationElement" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlThemes_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="ok" ControlToValidate="ddlThemes" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                
                </td>
                 
            </tr>
           
            
            <tr>
                <td>
                    <label> <%=GetGlobalResourceObject("CommonControls", "F017_ChangeVariable")%>* </label>
               
                 
                
                 </td>
                 <td>
                 
                
                 <asp:DropDownList ID="ChangingVariableDrp" runat="server"  ToolTip="Please choose your changing variable (x-axis)" CssClass="form-control validationElement"  AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ChangingVariableDrp_SelectedIndexChanged" ></asp:DropDownList>
                 
                
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ok" ControlToValidate="ChangingVariableDrp" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                 
            </tr>
            <tr>
                <td>
                    <label> <%=GetGlobalResourceObject("CommonControls", "F017_RunVariable")%> </label>
              
                </td>
                 <td>
                <asp:DropDownList ID="RunningVariableDrp" runat="server"   CssClass="form-control" ToolTip="Please choose your running variable which makes another run for your graph"  AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="RunningVariableDrp_SelectedIndexChanged" ></asp:DropDownList>
                 
                
                
                </td>
                 
               
                 <td>
                 <label id="YearFromLbl" runat="server" > <%=GetGlobalResourceObject("CommonControls", "F017_YearFrom")%> </label>

                </td>
                 
               
                <td>
                <asp:DropDownList ID="YearFromDrp" runat="server"  CssClass="form-control" Width="100">
                </asp:DropDownList>

                </td>
                 <td>

                <label id="YearToLbl" runat="server" > <%=GetGlobalResourceObject("CommonControls", "F017_YearTo")%> </label>

                </td>
                 
               
                 <td>
                <asp:DropDownList ID="YearToDrp" runat="server" CssClass="form-control" Width="100">
                </asp:DropDownList>


                </td>
                 
               
            </tr>
            
            <tr>
                <td colspan="5">
                    
                    <asp:Label ID="SelectLbl" runat="server" Text="<%$ Resources:CommonControls,F017_SingleValue %>" Font-Bold="True" Font-Size="Larger" ForeColor="DarkBlue"></asp:Label>
                </td>
                 
               
            </tr>
            
            <tr>
                <td colspan="5">

                    <asp:GridView ID="grdReport" runat="server" AutoGenerateColumns="False" >
                    </asp:GridView>

                </td>
                 
               
            </tr>
                   <tr>
                <td colspan="7">

                <asp:Table ID="Table1" runat="server" CssClass="table-responsive table-striped" ToolTip="Please choose single value from these set of variables">
                </asp:Table>

                </td>
                 
               
            </tr>
            <tr>
                <td colspan="5" style="text-align: center">
                    <asp:HiddenField ID="hdfReportId" runat="server" />
                </td>
                 
               
            </tr>
            
            <tr>
                <td colspan="5" style="text-align: center">
                <asp:Button ID="CreateReportBtn" runat="server" Text="Save" CssClass="btn btn-success nextBtn btn-lg" OnClick="CreateReportBtn_Click" ValidationGroup="ok"/>
                </td>
                 
               
            </tr>
            
            <tr>
                <td colspan="5">
                    &nbsp;</td>
                 
               
            </tr>
            
        </table>
   </div>
        </form>
</asp:Content>

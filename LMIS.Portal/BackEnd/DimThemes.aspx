<%@ Page Title="<%$ Resources:CommonControls,F010_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="DimThemes.aspx.cs" Inherits="LMIS.Portal.BackEnd.DimThemes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      <form id="form1" runat="server">
                     <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
         <script src="http://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css"/>
         
          <script type="text/javascript">
              function showpopup() {
                  if ($(".ddlGeneralCodes").val().toString() == '' && $(".ddlGeneralCodes").val().toString() == '' && $(".ddlGeneralCodes").val().toString() == '') {
                      alert('<%=GetGlobalResourceObject("CommonControls", "F028_SelectGeneralCode")%>');
                         return;
                     }
                     var d = $(".pop");
                     d.dialog({

                         width: 500
                     });
                     // d.parent().appendTo(jQuery("form:first"));
                     return false;
                 }

                 function Save1() {
                     if ($(".txtThemeInsert").val().toString() == '' && $(".txtNameAr").val().toString() == '' && $(".txtNameFr").val().toString() == '') {

                         alert('<%=GetGlobalResourceObject("CommonControls", "F010_Required")%>');
                         $(".txtThemeInsert").focus();
                         return false;
                     }
                    else if ( $(".txtUnitScale").val().toString() == '' ) {
                         alert('<%=GetGlobalResourceObject("CommonControls", "F010_Required")%>');
                        $(".txtUnitScale").focus();
                        return false;
                     }
                    else if ( $(".ddlThemeType").val().toString() == '') {
                         alert('<%=GetGlobalResourceObject("CommonControls", "F010_Required")%>');
                        $(".ddlThemeType").focus();
                        return false;
                     }
                     else {
                         return true;
                     }
                 }

                 function chooseGeneralCode() {
                     if ($(".ddlGeneralCodes").val().toString() == '') {
                         alert('<%=GetGlobalResourceObject("CommonControls", "F028_SelectGeneralCode")%>');
                         return false;
                     } else {
                         return true;
                     }
                 }
             </script>                           
            
           <asp:TextBox ID="txthdfEdited" runat="server" Style="display:none" CssClass="hdfEdited"></asp:TextBox>
           <asp:TextBox ID="txthdfEditedtype" runat="server" Style="display:none" CssClass="txthdfEditedtype"></asp:TextBox>

        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="Vdisplay" runat="server" >
               
      <asp:Button ID="btnShowInsert" runat="server" Text="<%$ Resources:CommonControls,X_Add%>"   class="btn  btn-default btn-primary " OnClick="btnShowInsert_Click" ></asp:Button>
             <br/>
                 <br/>

                <table id="grdTheme" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr >
                         <th><%=GetGlobalResourceObject("CommonControls", "F010_Themes")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F010_Name")%></th>
                         <th><%=GetGlobalResourceObject("CommonControls", "F010_NameAr")%></th>
                         <th><%=GetGlobalResourceObject("CommonControls", "F010_NameFr")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F010_UnitScale")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F010_UnitScaleAr")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F010_UnitScaleFr")%></th>
                         
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                    </tr>
                </tfoot>
                <tbody data-bind="foreach: Themes">
                    <tr >
                        <td >
                              <span data-bind="text: ThemeTypeName" class="show"></span>
                        </td>
                        <td >
                              <span data-bind="text: Name" class="show"></span>
                        </td>
                         <td >
                              <span data-bind="text: NameAr" class="show"></span>
                        </td>
                         <td >
                              <span data-bind="text: NameFr" class="show"></span>
                        </td>
                         <td >
                              <span data-bind="text: UnitScale" class="show"></span>
                        </td>
                         <td >
                              <span data-bind="text: UnitScaleAr" class="show"></span>
                        </td>
                         <td >
                              <span data-bind="text: UnitScaleFr" class="show"></span>
                        </td>
                       
                        <td>
                            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" data-bind="click: $root.UpdateTheme"><i class="fa fa-edit"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" data-bind="click: $root.DeleteTheme"><i class="fa fa-trash-o"></i></asp:LinkButton>
                           </td>
                    </tr>
                </tbody>
            </table>
     
                 </asp:View>
             <asp:View ID="Vedit" runat="server">
                   <div data-bind="root: Theme">
                         <div class="row">
              <div class="col-sm-2">
                            <label> <%=GetGlobalResourceObject("CommonControls", "F010_Themes")%>* </label>
                      </div>
                <div class="col-sm-6">
                             <asp:DropDownList ID="ddlThemeType" runat="server"  CssClass="form-control ddlThemeType validationElement" AppendDataBoundItems="True" >
                                 <asp:ListItem Selected="True"></asp:ListItem>
                             </asp:DropDownList>
                          
                         </div>
             </div>
                      <div class="row">
                          <div class="col-sm-2"><label><%=GetGlobalResourceObject("CommonControls", "F010_Name")%>**</label></div>
                          <div class="col-sm-6"><asp:TextBox ID="txtThemeInsert" runat="server" maxlength="500"  Width="50%"   class="input-group-lg txtThemeInsert validationElement" ></asp:TextBox></div>
                      </div>
                        <div class="row">
                          <div class="col-sm-2"><label><%=GetGlobalResourceObject("CommonControls", "F010_NameAr")%>**</label></div>
                          <div class="col-sm-6"><asp:TextBox ID="txtThemeAr" runat="server" maxlength="500"  Width="50%"   class="input-group-lg txtThemeInsert validationElement" ></asp:TextBox></div>
                      </div>
                        <div class="row">
                          <div class="col-sm-2"><label><%=GetGlobalResourceObject("CommonControls", "F010_NameFr")%>**</label></div>
                          <div class="col-sm-6"><asp:TextBox ID="txtThemeFr" runat="server" maxlength="500"  Width="50%"   class="input-group-lg txtThemeInsert validationElement" ></asp:TextBox></div>
                      </div>
                      <div class="row">
                          <div class="col-sm-2"><label><%=GetGlobalResourceObject("CommonControls", "F010_UnitScale")%>**</label></div>
                          <div class="col-sm-6"><asp:TextBox ID="txtUnitScale" runat="server" maxlength="50"  Width="50%"   class="input-group-lg  txtUnitScale validationElement" ></asp:TextBox></div>
                       </div>
                         <div class="row">
                          <div class="col-sm-2"><label><%=GetGlobalResourceObject("CommonControls", "F010_UnitScaleAr")%>**</label></div>
                          <div class="col-sm-6"><asp:TextBox ID="txtUnitScaleAr" runat="server" maxlength="50"  Width="50%"   class="input-group-lg txtUnitScale validationElement" ></asp:TextBox></div>
                       </div>
                         <div class="row">
                          <div class="col-sm-2"><label><%=GetGlobalResourceObject("CommonControls", "F010_UnitScaleFr")%>**</label></div>
                          <div class="col-sm-6"><asp:TextBox ID="txtUnitScaleFr" runat="server" maxlength="50"  Width="50%"   class="input-group-lg txtUnitScale validationElement" ></asp:TextBox></div>
                       </div>
         
                   <div class="form-group" >
                     <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:CommonControls,X_Save1%>" OnClientClick="return Save1();" class="btn  btn-default btn-primary save" OnClick="btnSave_Click"></asp:Button>
                     <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:CommonControls,X_Cancel1%>"  class="btn  btn-default btn-primary " OnClick="btnCancel_Click"></asp:Button>
              
               </div> 
               </div>
              </asp:View>
        </asp:MultiView>
       </form>
           <script src="../BackEnd/Scripts/DimThemes.js"></script>
            <script src="../Scripts/Extensions/lmis.js" async="async"></script>
</asp:Content>

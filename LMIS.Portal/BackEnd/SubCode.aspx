<%@ Page Title="<%$ Resources:CommonControls,F028_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" enableEventValidation="false" AutoEventWireup="true" CodeBehind="SubCode.aspx.cs" Inherits="LMIS.Portal.BackEnd.SubCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="first">
                   <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
           <script src="http://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css"/>
   
             <script type="text/javascript">
                 function showpopup() {
                     if ($(".ddlGeneralCodes").val().toString() == '') {
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
                     if ($(".ddlGeneralCodesParent").val().toString() == '') {
                         alert('<%=GetGlobalResourceObject("CommonControls", "F028_SelectGeneralCode")%>');
                         return false;
                     }
                     if ($(".txtNameEn").val().toString() == '' && $(".txtNameAr").val().toString() == '' && $(".txtNameFr").val().toString() == '') {
                         alert('<%=GetGlobalResourceObject("CommonControls", "F028_EnterName")%>');
                         return false;
                     } else {
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
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="Vdisplay" runat="server" >
                   
        <div class="form-group">
            <label><%=GetGlobalResourceObject("CommonControls", "F028_GeneralCode")%>* </label>
            <asp:DropDownList ID="ddlGeneralCodes" runat="server" CssClass="form-control ddlGeneralCodes validationElement" AppendDataBoundItems="True" AutoPostBack="True" ValidationGroup="add">
                <asp:ListItem Selected="True"></asp:ListItem>
            </asp:DropDownList>
           
        </div>
        <div class="form-group">
              <asp:Button ID="btnShowInsert" runat="server" Text="<%$ Resources:CommonControls,X_Add%>"   class="btn  btn-default btn-primary " OnClientClick="return chooseGeneralCode();" OnClick="btnShowInsert_Click" ></asp:Button>
                     
            <br/>
            <br/>
            <table id="grdSubCodes" class="display grdSubCodes" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr >
                         <th><%=GetGlobalResourceObject("CommonControls", "F028_subCode")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F010_Name")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F028_ParentSubCode")%></th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                    </tr>
                </tfoot>
                <tbody data-bind="foreach: SubCodes">
                    <tr >
                        <td >
                              <span data-bind="text: SubID" class="show">
                              
                              </span>
                        </td>
                        <td >
                              <span data-bind="text: Name" class="show"></span>
                        </td>
                         <td >
                              <span data-bind="text: ParentSubCodeName" class="show"></span>
                        </td>
                    
                        <td>
                            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" data-bind="click: $root.UpdateSubCode"><i class="fa fa-edit"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" data-bind="click: $root.DeleteSubCode"><i class="fa fa-trash-o"></i></asp:LinkButton>
                    
                        </td>
                    </tr>
                </tbody>
            </table>
        </div> 
            </asp:View>
            <asp:View ID="VInsert" runat="server">
                 
            
               <div data-bind="root: Theme">
            <div class="row">
              <div class="col-sm-2">  
            <label><%=GetGlobalResourceObject("CommonControls", "F028_GeneralCode")%>* </label>
            </div>
                <div class="col-sm-6">
                <asp:DropDownList ID="ddlGeneralCodesParent" runat="server" CssClass="form-control validationElement ddlGeneralCodesParent" AppendDataBoundItems="True"  AutoPostBack="True" OnSelectedIndexChanged="ddlGeneralCodesParent_SelectedIndexChanged">
                <asp:ListItem Selected="True"></asp:ListItem>
            </asp:DropDownList>
           </div>
             </div>
                      <div class="row">
                          <div class="col-sm-2">  
            <label><%=GetGlobalResourceObject("CommonControls", "F028_subCode")%>** </label>
           </div>
                          <div class="col-sm-6">  
                 <asp:DropDownList ID="ddlSubCodesParent" runat="server" CssClass="form-control ddlSubCodesParent validationElement" AppendDataBoundItems="True" >
                <asp:ListItem Selected="True"></asp:ListItem>
            </asp:DropDownList>
            </div>
                    
             </div>
                      
                      
                   <div class="row">
                          <div class="col-sm-2"> 
                     <label><%=GetGlobalResourceObject("CommonControls", "F028_NameEn")%>*</label>
                              </div>
                          <div class="col-sm-6">  
                      <asp:TextBox ID="txtNameEn" runat="server" maxlength="100"  Width="50%"   class="input-group-lg validationElement txtNameEn" ></asp:TextBox>
               </div>
                       </div>
               <div class="row">
                          <div class="col-sm-2"> 
                     <label><%=GetGlobalResourceObject("CommonControls", "F0028_NameAr")%>**</label>
                              </div>
                          <div class="col-sm-6">  
                      <asp:TextBox ID="txtNameAr" runat="server" maxlength="100"  Width="50%"   class="input-group-lg txtNameAr validationElement" ></asp:TextBox>
               </div>
                   </div>
                     <div class="row">
                          <div class="col-sm-2"> 
                     <label><%=GetGlobalResourceObject("CommonControls", "F028_NameFr")%>**</label>
                              </div>
                          <div class="col-sm-6">  
                      <asp:TextBox ID="txtNameFr" runat="server" maxlength="100" Width="50%"  class="input-group-lg txtNameFr validationElement"></asp:TextBox>
               </div>
                          </div>
                   <div class="row">
                        
                       <asp:HiddenField ID="hdfSubCode" runat="server" />
                       <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:CommonControls,X_Save1%>" OnClientClick="return Save1();" class="btn  btn-default btn-primary save" OnClick="btnSave_Click"></asp:Button>
                       <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:CommonControls,X_Cancel1%>"  class="btn  btn-default btn-primary " OnClick="btnCancel_Click"></asp:Button>
              
               </div> 
                 
               </div>
               
       
            </asp:View>
        </asp:MultiView>
     
     
    </form>
    <script src="../BackEnd/Scripts/GeneralCodes.js"></script>
</asp:Content>

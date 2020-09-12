<%@ Page Title="<%$ Resources:CommonControls,F051 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ObsceneWords.aspx.cs" Inherits="LMIS.Portal.BackEnd.ObsceneWords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F051_Post")%>
                </a>
            </div>
            <input type="hidden" id="hdfObsceneWordID" value="0"/>
            <input type="hidden" id="hdfRowID" value="0"/>
            <table id="grdObsceneWords" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F049_Title")%></th>
                        <th></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: ObsceneWordsList">
                    <tr>
                        <td data-bind="text: Description"></td>
                        <td>
             
                    <a id="lnkEdit" data-bind="click: function () { $root.UpdateObsceneWord($data, $index()); }"><i class="fa fa-edit"></i></a>
                    <a id="lnkDelete" data-bind="click: function () { $root.DeleteObsceneWord($data, $index()); }"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

    </div>
    <div class="pop" style="display: none">
                
            <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F049_Title")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="100" class="input-group-lg full-width validationElement Description"  data-bind="text: Description"/>
            </div>
        </div>

         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary" type="button" data-bind="click: Save" />
    </div>

  <script src="../Scripts/knockout.validation.min.js"></script>
  <script src="../BackEnd/Scripts/ObsceneWords.js"></script>

</asp:Content>

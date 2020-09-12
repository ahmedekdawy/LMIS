<%@ Page Title="<%$ Resources:CommonControls,F058 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ConceptsDefinitionsBack.aspx.cs" Inherits="LMIS.Portal.BackEnd.ConceptsDefinitionsBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


    <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F058_Post")%>
                </a>
            </div>
            <input type="hidden" id="hdfConceptDefID" value="0"/>
            <input type="hidden" id="hdfRowID" value="0"/>
            <table id="grdConcept" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F049_Title")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F049_Description")%></th>
                        <th></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: ConceptList">
                    <tr>
                        <td data-bind="text: ConceptDefTitle"></td>
                         <td data-bind="text: ConceptDefDesc"></td>
                        <td class="col-lg-2">
             
                    <a id="lnkEdit" data-bind="click: function () { $root.UpdateConcept($data, $index()); }"><i class="fa fa-edit"></i></a>
                    <a id="lnkDelete" data-bind="click: function () { $root.DeleteConcept($data, $index()); }"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

    </div>
    <div class="pop" style="display: none">
                 <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "X_language")%>*</label></div>
            <div class="col-sm-9">
                      <select id="ddlLanguage" class="ddlLanguage validationElement">
                    <option value="1" selected="selected"><%=GetGlobalResourceObject("CommonControls", "X_English")%></option>
                    <option value="3"><%=GetGlobalResourceObject("CommonControls", "X_Arabic")%></option>
                    <option value="2"><%=GetGlobalResourceObject("CommonControls", "X_French")%></option>
                </select>
            </div>
        </div>
            <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F049_Title")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="200" class="input-group-lg full-width validationElement ConceptDefTitle"  data-bind="text: ConceptDefTitle"/>
            </div>
        </div>

         <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F049_Description")%>*</label></div>
            <div class="col-sm-9">
                <input type="text" maxlength="4000" class="input-group-lg full-width validationElement ConceptDefDesc"  data-bind="text: ConceptDefDesc"/>
              
            </div>
        </div>
         
         
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary" type="button" data-bind="click: Save" />
    </div>
    <script src="../BackEnd/Scripts/ConceptsDefinitionsBack.js"></script>
</asp:Content>

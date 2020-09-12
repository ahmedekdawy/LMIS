<%@ Page Title="<%$ Resources:CommonControls,F056 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="QualificationsBack.aspx.cs" Inherits="LMIS.Portal.BackEnd.QualificationsBack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F056_Post")%>
                </a>
            </div>
            <input type="hidden" id="hdfId" value="0"/>
            <input type="hidden" id="hdfRowID" value="0"/>
            <table id="grdQualification" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F029_Category")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F056_QualificationEN")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F056_QualificationAr")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F056_QualificationFr")%></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: QualificationsList">
                    <tr>
                        <td data-bind="text: GroupName"></td>
                    <td data-bind="text: QualificationEn"></td>
                        <td data-bind="text: QualificationAr"></td>
                        <td data-bind="text: QualificationFr"></td>
                     
                      <td>
             
                    <a id="lnkEdit" data-bind="click: function () { $root.UpdateQualification($data, $index()); }"><i class="fa fa-edit"></i></a>
                    <a id="lnkDelete" data-bind="click: function () { $root.DeleteQualification($data, $index()); }"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

    </div>
    <div class="pop" style="display: none">
                <form>
            <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F029_Category")%>*</label></div>
            <div class="col-sm-9">
                   <select id="ddlCategory" required class="form-control always-white validationElement ddlCategory" data-bind="options: CategoryOptions, value: GroupID, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
 
            </div>
        </div>
        <br/>
          <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F056_QualificationEN")%>**</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="100" required class="input-group-lg full-width QualificationEn validationElement"  data-bind="text: QualificationEn"/>
            </div>
        </div>
        <br/>
          <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F056_QualificationAr")%>**</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="100" required class="input-group-lg full-width QualificationAr validationElement"  data-bind="text: QualificationAr"/>
            </div>
        </div>
        <br/>
             <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F056_QualificationFr")%>**</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="100" required class="input-group-lg full-width QualificationFr validationElement"  data-bind="text: QualificationFr"/>
            </div>
        </div>
        <br/>
        <div class="text-center">
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary " type="button" data-bind="click: function () { $root.Save($data); }" />
            </div>
                    </form>
    </div>
    <script src="../BackEnd/Scripts/QualificationsBack.js"></script>
</asp:Content>

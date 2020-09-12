<%@ Page Title="<%$ Resources:CommonControls,F057 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="FaqBack.aspx.cs" Inherits="LMIS.Portal.BackEnd.FaqBack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F057_Post")%>
                </a>
            </div>
            <input type="hidden" id="hdfId" value="0"/>
            <input type="hidden" id="hdfRowID" value="0"/>
            <table id="grdFaq" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F029_Category")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F057_Question")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F057_Answer")%></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: FaqsList">
                    <tr>
                        <td data-bind="text: GroupName"></td>
                    <td data-bind="text: Question"></td>
                        <td data-bind="text: Answer"></td>
                     
                      <td class="col-lg-2">
             
                    <a id="lnkEdit" data-bind="click: function () { $root.UpdateFaq($data, $index()); }"><i class="fa fa-edit"></i></a>
                    <a id="lnkDelete" data-bind="click: function () { $root.DeleteFaq($data, $index()); }"><i class="fa fa-trash-o"></i></a>
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
                <label><%=GetGlobalResourceObject("CommonControls", "X_language")%>*</label></div>
            <div class="col-sm-9">
                <select id="ddlLanguage" class="form-control always-white validationElement ddlLanguage" data-bind="value:FAQLanguage">
                    <option value="" selected="selected"><%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%></option>
                    <option value="1" ><%=GetGlobalResourceObject("CommonControls", "X_English")%></option>
                    <option value="3"><%=GetGlobalResourceObject("CommonControls", "X_Arabic")%></option>
                    <option value="2"><%=GetGlobalResourceObject("CommonControls", "X_French")%></option>
                </select>
            </div>
        </div>
        <br />
            <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F029_Category")%>*</label></div>
            <div class="col-sm-9">
                   <select id="ddlCategory" required class="form-control always-white validationElement ddlCategory" data-bind="options: CategoryOptions, value: FAQCategoryID, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
 
            </div>
        </div>
        <br/>
          <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F057_Question")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="200" required class="input-group-lg full-width validationElement Question"  data-bind="text: Question"/>
            </div>
        </div>
        <br/>
          <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F057_Answer")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="1000" required class="input-group-lg full-width validationElement Answer"  data-bind="text: Answer"/>
            </div>
        </div>
        <br/>
      
        <div class="text-center">
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary " type="button" data-bind="click: function () { $root.Save($data); }" />
            </div>
                    </form>
    </div>
    <script src="../BackEnd/Scripts/FaqBack.js"></script>
</asp:Content>

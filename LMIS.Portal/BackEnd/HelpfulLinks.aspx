<%@ Page Title="<%$ Resources:CommonControls,F054 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="HelpfulLinks.aspx.cs" Inherits="LMIS.Portal.BackEnd.HelpfulLinks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F054_Post")%>
                </a>
            </div>
            <input type="hidden" id="hdfHelpfulLinkID" value="0"/>
            <input type="hidden" id="hdfRowID" value="0"/>
            <table id="grdHelpfulLink" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F029_Category")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F054")%></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: HelpfulLinkList">
                    <tr>
                        <td data-bind="text: GroupName"></td>
                         
                         <td ><a target="_blank"  data-bind="attr: { href:'http://'+  HelpfulLinkURL().replace('www.','').replace('http://','').replace('https://','')  } "><span data-bind="    text: HelpfulLinkName"></span></a></td>
                      
                     
                      <td>
             
                    <a id="lnkEdit" data-bind="click: function () { $root.UpdateHelpfulLink($data, $index()); }"><i class="fa fa-edit"></i></a>
                    <a id="lnkDelete" data-bind="click: function () { $root.DeleteHelpfulLink($data, $index()); }"><i class="fa fa-trash-o"></i></a>
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
                <label><%=GetGlobalResourceObject("CommonControls", "F054_Name")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="100" required class="input-group-lg full-width validationElement  HelpfulLinkName"  data-bind="text: HelpfulLinkName"/>
            </div>
        </div>
        <br/>
          <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Website")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="100" required class="input-group-lg full-width validationElement HelpfulLinkURL"  data-bind="text: HelpfulLinkURL"/>
            </div>
        </div>
        <br/>
          <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "X_language")%>*</label></div>
            <div class="col-sm-9">

                   <select id="ddlLanguage" class="ddlLanguage validationElement"  required>
                    <option value="1" selected="selected"><%=GetGlobalResourceObject("CommonControls", "X_English")%></option>
                    <option value="3"><%=GetGlobalResourceObject("CommonControls", "X_Arabic")%></option>
                    <option value="2"><%=GetGlobalResourceObject("CommonControls", "X_French")%></option>
                </select>
            </div>
        </div>
        <div class="text-center">
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary " type="button" data-bind="click: function () { $root.Save($data); }" />
            </div>
                    </form>
    </div>
    <script src="../BackEnd/Scripts/HelpfulLinks.js"></script>
</asp:Content>

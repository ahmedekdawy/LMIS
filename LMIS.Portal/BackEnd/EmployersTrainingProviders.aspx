<%@ Page Title="<%$ Resources:CommonControls,F059 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="EmployersTrainingProviders.aspx.cs" Inherits="LMIS.Portal.BackEnd.EmployersTrainingProviders" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


    <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F059_Post")%>
                </a>
            </div>
            <input type="hidden" id="hdfID" value="0"/>
            <input type="hidden" id="hdfRowID" value="0"/>
            <table id="grdConcept" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F049_Title")%></th>
                        <th></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: ConceptList">
                    <tr>
     
   
                        <td data-bind="text: Name"></td>
                        <td>
             
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
                <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationType")%>*</label></div>
            <div class="col-sm-9">
                      <select id="ddlType" class="ddlType validationElement">
                    <option value="0" selected="selected"><%=GetGlobalResourceObject("CommonControls", "F060")%></option>
                    <option value="1"><%=GetGlobalResourceObject("CommonControls", "F061")%></option>
                </select>
            </div>
        </div>
         <br/>
            <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F049_Title")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="200" class="input-group-lg full-width validationElement Name"  data-bind="text: Name"/>
            </div>
        </div>
        <br/>
         <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F049_Description")%>*</label></div>
            <div class="col-sm-9">
                 <input type="text" maxlength="1000" class="input-group-lg full-width validationElement Description"  data-bind="text: Name"/>
               
            </div>
        </div>
         <br/>
         <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Website")%>*</label></div>
            <div class="col-sm-9">
                 <input type="text" maxlength="150" class="input-group-lg full-width validationElement Website"  data-bind="text: Name"/>
               
            </div>
        </div>
          <br/>
         <div class="row" >
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "X_File")%></label></div>
            <div class="col-sm-9">
                <div class="input-group">
                    <input type="text" id="txtLogoPath" class="form-control  txtLogoPath" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnImage")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnLogoPath').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                         </span>
                </div>
                <input type="file" id="hdnLogoPath" data-bind="attr: { accept: AcceptedFilesLogoPath }, event: { change: ValidateLogoPath }" style="height: 0; visibility: hidden;" />
                <p class="form-description">
                    <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                    <a class="text-center LogoPath" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(LogoPath()), attr: { href: lmis.x.downloadURL + 'ConceptNonFormalTraining/' + LogoPath() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
        </div>
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary" type="button" data-bind="click: Save" />
    </div>
    <script src="../BackEnd/Scripts/EmployersTrainingProviders.js"></script>
</asp:Content>

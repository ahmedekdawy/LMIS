<%@ Page Title="<%$ Resources:CommonControls,F052 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ConceptOfNonFormalTrainingBack.aspx.cs" Inherits="LMIS.Portal.BackEnd.ConceptOfNonFormalTrainingBack" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


    <div class="tab-pane fade in active" id="tab6default">
        
        <div id="eventTable" class="col-sm-12">
            
        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F049_PostNewConcept")%>
                </a>
            </div>
            <input type="hidden" id="hdfConceptID" value="0"/>
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
                        <td data-bind="text: ConceptTitle"></td>
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
                <label><%=GetGlobalResourceObject("CommonControls", "F049_Title")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="200" class="input-group-lg full-width validationElement ConceptTitle"  data-bind="text: ConceptTitle"/>
            </div>
        </div>

         <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F049_Description")%>*</label></div>
            <div class="col-sm-9">

                <FCKeditorV2:FCKeditor ID="FCKConceptDescription"  EditorAreaCSS="FCKConceptDescription"   ToolbarStartExpanded="False"  runat="server" BasePath="~/FCKeditor/" ImageBrowserURL="~/FCKeditor/" LinkBrowserURL="~/FCKeditor/"></FCKeditorV2:FCKeditor>

            </div>
        </div>
         
         <div class="row" >
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "X_File")%>*</label></div>
            <div class="col-sm-9">
                <div class="input-group">
                    <input type="text" id="txtImagePath" class="form-control validationElement txtImagePath" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnImage")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnImagePath').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                         </span>
                </div>
                <input type="file" id="hdnImagePath" data-bind="attr: { accept: AcceptedFilesImagePath }, event: { change: ValidateImagePath }" style="height: 0; visibility: hidden;" />
                <p class="form-description">
                    <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                    <a class="text-center ImagePath" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(ImagePath()), attr: { href: lmis.x.downloadURL + 'ConceptNonFormalTraining/' + ImagePath() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
        </div>
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary" type="button" data-bind="click: Save" />
    </div>
    <script src="../BackEnd/Scripts/ConceptOfNonFormalTrainingBack.js"></script>
</asp:Content>

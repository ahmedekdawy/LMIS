<%@ Page Title="<%$ Resources:CommonControls,F062 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="RecruitmentAgencies.aspx.cs" Inherits="LMIS.Portal.BackEnd.RecruitmentAgencies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    



        

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="return  showpopupInsert();" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F062_Post")%>
                </a>
            </div>
            <input type="hidden" id="hdfID" value="0"/>
            <input type="hidden" id="hdfRowID" value="0"/>
            <br/><br/>
            <table id="grdRecruitmentAgency" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                           <th><%=GetGlobalResourceObject("CommonControls", "F062_Titles")%></th>
                           <th><%=GetGlobalResourceObject("CommonControls", "F062_Description")%></th>
                            <th></th>
                  
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: RecruitmentAgenciesList">
                    <tr>
     
   
                           <td data-bind="text: Name"></td>
                           <td data-bind="text: Background"></td>
                        <td>
             
                    <a id="lnkEdit" data-bind="click: function () { $root.Update($data, $index()); }"><i class="fa fa-edit"></i></a>
                    <a id="lnkDelete" data-bind="click: function () { $root.Delete($data, $index()); }"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>

    
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
                <label><%=GetGlobalResourceObject("CommonControls", "F062_Titles")%>*</label></div>
            <div class="col-sm-9">

              <input type="text" maxlength="100" class="input-group-lg full-width validationElement Name"  data-bind="text: Name"/>
            </div>
        </div>
        <br/>
         <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F062_Description")%>*</label></div>
            <div class="col-sm-9">
                 <input type="text" maxlength="500" class="input-group-lg full-width validationElement Background"  data-bind="text: Background"/>
               
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
                <p class="form-Background">
                    <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                    <a class="text-center LogoPath" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(LogoPath()), attr: { href: lmis.x.downloadURL + 'ConceptNonFormalTraining/' + LogoPath() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
        </div>
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary" type="button" data-bind="click: Save" />
    </div>
    <script src="../BackEnd/Scripts/RecruitmentAgencies.js"></script>
</asp:Content>

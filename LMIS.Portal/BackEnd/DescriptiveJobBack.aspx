<%@ Page Title="<%$ Resources:CommonControls,F055 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="DescriptiveJobBack.aspx.cs" Inherits="LMIS.Portal.BackEnd.DescriptiveJobBack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                    <span style="padding-right: 10px;">pdf</span>
                 
                </p>
            </div>
        </div>
    <div class="text-center">
         <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary  extBtn " type="button" data-bind="click: Save" />
 </div>
         <br/><br/>
      <div data-bind="foreach: FilesList">
        <div class="col-lg-1">   <a id="lnkDelete" data-bind="click: function () { $root.DeleteFile(ImagePath()); }"><i class="fa fa-trash-o"></i></a></div>  
        <div class="col-lg-11"><a  href="#" target="_blank" data-bind="attr: { href: lmis.x.downloadURL + 'DescriptiveJob/' + ImagePath() }"><img  width="50px" height="50px" src="../images/pdf.png"/><span data-bind="    text: ImageName"></span></a> </div>
      
     
 
              </div>
  <script src="../BackEnd/Scripts/DescriptiveJobBack.js"></script>
  
</asp:Content>

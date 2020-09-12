<%@ Page Title="<%$ Resources:CommonControls,F036_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="LMIS.Portal.BackEnd.News" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <a id="lnkAdd" onclick="return  showpopupInsert();" class="btn-lg  btn-default btn-primary "><%=GetGlobalResourceObject("CommonControls", "X_Add")%></a>

    <div class="pop" style="display: none">

       
        <div class="row">
            <div class="col-sm-2">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsType")%>*</label></div>
            <div class="col-sm-10">
                <form>
                <input type="radio" name="IsInformal" value="IsInformal"  checked="checked" class="IsInformal" /><%=GetGlobalResourceObject("CommonControls", "F036_IsInformal")%>
                <br />
                <input type="radio" name="IsInformal" value="IsAchievement"   class="IsAchievement"  /><%=GetGlobalResourceObject("CommonControls", "F036_IsAchievement")%>
                    </form>

            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-2">
                <label><%=GetGlobalResourceObject("CommonControls", "X_language")%>*</label></div>
            <div class="col-sm-10">
                <select id="ddlLanguage" class="ddlLanguage validationElement">
                    <option value="1" selected="selected"><%=GetGlobalResourceObject("CommonControls", "X_English")%></option>
                    <option value="3"><%=GetGlobalResourceObject("CommonControls", "X_Arabic")%></option>
                    <option value="2"><%=GetGlobalResourceObject("CommonControls", "X_French")%></option>
                </select>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsTitle")%>*</label></div>
            <div class="col-sm-9">

                <FCKeditorV2:FCKeditor ID="FCKNewsTitle" EditorAreaCSS="NewsTitle" ToolbarStartExpanded="False"  runat="server" BasePath="~/FCKeditor/" ImageBrowserURL="~/FCKeditor/" LinkBrowserURL="~/FCKeditor/"></FCKeditorV2:FCKeditor>

            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsDescription")%>*</label></div>
            <div class="col-sm-9">

                <FCKeditorV2:FCKeditor ID="FCKNewsDescription" ToolbarStartExpanded="False" EditorAreaCSS="NewsDescription"  runat="server" BasePath="~/FCKeditor/" ImageBrowserURL="~/FCKeditor/" LinkBrowserURL="~/FCKeditor/"></FCKeditorV2:FCKeditor>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsDate")%>*</label></div>
            <div class="col-sm-3">
                <input id="txtNewsDate" class="NewsDate datepiker validationElement" type="text" maxlength="12" required /></div>
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsExpiryDate")%>*</label></div>
            <div class="col-sm-3">
                <input id="txtNewsExpiryDate" class="NewsExpiryDate datepiker validationElement" type="text" maxlength="12" required /></div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsBannerPath")%>*</label></div>
            <div class="col-sm-9">
                <div class="input-group">
                    <input type="text" id="txtNewsBanner" class="form-control txtNewsBanner validationElement" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnImage")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnNewsBannerBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                         </span>
                </div>
                <input type="file" id="hdnNewsBannerBrowser" data-bind="attr: { accept: AcceptedFilesBanners }, event: { change: ValidateBanner }" style="height: 0; visibility: hidden;" />
                <p class="form-description">
                    <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                    <a class="text-center Banners" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(NewsBannerPath()), attr: { href: lmis.x.downloadURL+'News/Banners/' + NewsBannerPath() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsIconPath")%>*</label></div>
            <div class="col-sm-9">
                <div class="input-group">
                    <input type="text" id="txtNewsIcon" class="form-control txtNewsIcon validationElement" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnImage")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnNewsIconBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                    </span>
                </div>
                <input type="file" id="hdnNewsIconBrowser" data-bind="attr: { accept: AcceptedFilesIcon }, event: { change: ValidateIcon }" style="height: 0; visibility: hidden;" />
                <p class="form-description">
                    <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                    <a class="text-center Icons" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(NewsIconPath()), attr: { href: lmis.x.downloadURL+'News/Icons/'  + NewsIconPath() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <label><%=GetGlobalResourceObject("CommonControls", "F036_NewsVideoPath")%>*</label></div>
            <div class="col-sm-9">
                <div class="input-group">
                    <input type="text" id="txtNewsVideo" class="form-control validationElement" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnVideo")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnNewsVideoBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                    </span>
                </div>
                <input type="file" id="hdnNewsVideoBrowser" data-bind="attr: { accept: AcceptedFilesVideos }, event: { change: ValidateVideo }" style="height: 0; visibility: hidden;" />
                <p class="form-description">
                    <span style="padding-right: 10px;">Mp4,Flv,Avi,Wmv,3gp,Mpg,Mpeg,Asf</span>
                    <a class="text-center Videos" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(NewsVideoPath()), attr: { href: lmis.x.downloadURL+'News/Videos/'  + NewsVideoPath() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
        </div>
        <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-primary" type="button" data-bind="click: INSERT" />
    </div>
    <br />
    <br />
      <input type="hidden" id="hdfIsInformal" value="<%=GetGlobalResourceObject("CommonControls", "F036_IsInformal")%> " />
       <input type="hidden" id="hdfIsAchievement" value="<%=GetGlobalResourceObject("CommonControls", "F036_IsAchievement")%> " />
     <input type="hidden" id="hdfNewsID"/>
    <input type="hidden" id="hdfRowId"/>
    <table id="grdNews" class="display" style="border-spacing: 0; width: 100%;">
        <thead>
            <tr>
                <th><%=GetGlobalResourceObject("CommonControls", "F036_NewsType")%></th>
                <th><%=GetGlobalResourceObject("CommonControls", "F036_NewsTitle")%></th>
                <th><%=GetGlobalResourceObject("CommonControls", "F036_NewsDate")%></th>
                <th><%=GetGlobalResourceObject("CommonControls", "F036_NewsExpiryDate")%></th>

            </tr>
        </thead>
        <tfoot>
            <tr>
                <th></th>
                <th></th>
            </tr>
        </tfoot>
        <tbody data-bind="foreach: newsList">
            <tr>
                <td>
                    <span data-bind="text: IsInformal() == false ? '<%=GetGlobalResourceObject("CommonControls", "F036_IsAchievement")%>    ' : '<%=GetGlobalResourceObject("CommonControls", "F036_IsInformal")%>    ' " class="show"></span>
                </td>
                <td>
                    <span data-bind="html: NewsTitle" class="show"></span>
                </td>
                <td>
                    <span data-bind="text : NewsDate" class="show"></span>
                    
                </td>
                <td>
                    <span data-bind="text: NewsExpiryDate" class="show"></span>
                </td>


                <td>
                    <a id="lnkEdit" data-bind="click: function(){$root.UpdateNews($data,$index());}"><i class="fa fa-edit"></i></a>
                    <a id="lnkDelete" data-bind="click: function(){$root.DeleteNews($data, $index());}"><i class="fa fa-trash-o"></i></a>
                </td>
            </tr>
        </tbody>
    </table>
    <script src="../BackEnd/Scripts/News.js"></script>
</asp:Content>

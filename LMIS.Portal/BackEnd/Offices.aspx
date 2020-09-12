<%@ Page Title="<%$ Resources:CommonControls, B001 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="Offices.aspx.cs" Inherits="LMIS.Portal.BackEnd.Offices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../BackEnd/Scripts/Offices.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab6default">

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4"><hr></div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "B001")%></div>
            <div class="col-lg-4"><hr></div>
        </div>
        
        <div id="List" class="col-sm-12" data-bind="visible: mode() === 'l'">

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="vm.Post()" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "B001_Post")%>
                </a>
            </div>

            <table id="grdOffices" class="display" style="border-spacing: 0; width: 100%; display: none;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "B001_OfficeTitle")%></th>
                        <th></th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                    </tr>
                </tfoot>
                <tbody data-bind="foreach: OfficeList">
                    <tr data-bind="attr: { id: RowId }">
                        <td data-bind="text: Title.toLocal"></td>
                        <td>
                            <a href="#" title=" View " onclick="vm.View(event)" class="editBtn"><i class="fa fa-eye"></i></a>
                            <a href="#" title=" Edit " onclick="vm.Edit(event)" class="editBtn"><i class="fa fa-edit"></i></a>
                            <a href="#" title=" Delete " onclick="vm.Delete(event)"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

        <div id="Editor" class="col-sm-12" data-bind="visible: mode() !== 'l', with: Office">

            <div class="col-sm-12">

                <div class="form-group">
                    <label id="lblTitle"><%=GetGlobalResourceObject("CommonControls", "B001_Titles")%> *</label>
                    <div class="input-group">
                        <input type="text" id="txtTitle_A" class="form-control validationElement" maxlength="100" data-bind="value: Title.A, attr: { placeholder: Title.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnTitle" class="btn btn-default always-enabled" data-bind="click: function () { Title.LocalizeView(!Title.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtTitle_B" class="form-control" maxlength="100" data-bind="value: Title.B, attr: { placeholder: Title.B.ph }, visible: !Title.Localized()" />
                    <input type="text" id="txtTitle_C" class="form-control" maxlength="100" data-bind="value: Title.C, attr: { placeholder: Title.C.ph }, visible: !Title.Localized()" />
                </div>

                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "B001_Address")%> *</label>
                    <div class="trilingual-textarea">
                        <label lang="en" data-bind="css: lmis.string.isNullOrWhiteSpace(Address.A()) ? 'unedited' : 'edited'">
                            <input type="radio" value="en" class="always-enabled" data-bind="checked: Address.ActiveLang, click: function () { $('#txtaddress_En').focus(); return true; }">
                            <%=GetGlobalResourceObject("CommonControls", "X_English")%>
                        </label>
                        <label lang="fr" data-bind="css: lmis.string.isNullOrWhiteSpace(Address.B()) ? 'unedited' : 'edited'">
                            <input type="radio" value="fr" class="always-enabled" data-bind="checked: Address.ActiveLang, click: function () { $('#txtaddress_Fr').focus(); return true; }">
                            <%=GetGlobalResourceObject("CommonControls", "X_French")%>
                        </label>
                        <label lang="ar" data-bind="css: lmis.string.isNullOrWhiteSpace(Address.C()) ? 'unedited' : 'edited'">
                            <input type="radio" value="ar" class="always-enabled" data-bind="checked: Address.ActiveLang, click: function () { $('#txtaddress_Ar').focus(); return true; }">
                            <%=GetGlobalResourceObject("CommonControls", "X_Arabic")%>
                        </label>
                    </div>
                    <textarea id="txtaddress_En" maxlength="200" data-bind="textInput: Address.A, visible: (Address.ActiveLang() == 'en')" class="form-control always-white validationElement" rows="8"></textarea>
                    <textarea id="txtaddress_Fr" maxlength="200" data-bind="textInput: Address.B, visible: (Address.ActiveLang() == 'fr')" class="form-control always-white validationElement" rows="8"></textarea>
                    <textarea id="txtaddress_Ar" maxlength="200" data-bind="textInput: Address.C, visible: (Address.ActiveLang() == 'ar')" class="form-control always-white validationElement" rows="8"></textarea>
                </div>

            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label id="lblTelephone"><%=GetGlobalResourceObject("CommonControls", "B001_Telephone")%> *</label>
                    <input type="text" maxlength="20" class="form-control phone validationElement" data-bind="textInput: Telephone">
                </div>
                <div class="form-group">
                    <label id="lblMobile"><%=GetGlobalResourceObject("CommonControls", "B001_Mobile")%></label>
                    <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Mobile">
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label id="lblHotline"><%=GetGlobalResourceObject("CommonControls", "B001_Hotline")%> *</label>
                    <input type="text" maxlength="20" class="form-control phone validationElement" data-bind="textInput: Hotline">
                </div>
                <div class="form-group">
                    <label id="lblFax"><%=GetGlobalResourceObject("CommonControls", "B001_Fax")%></label>
                    <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Fax">
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label id="lblDistrict"><%=GetGlobalResourceObject("CommonControls", "B001_District")%>  *</label>
                    <div class="input-group">
                        <input type="text" id="txtDistrict_A" class="form-control validationElement" maxlength="100" data-bind="value: District.A, attr: { placeholder: District.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnDistrict" class="btn btn-default always-enabled" data-bind="click: function () { District.LocalizeView(!District.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtDistrict_B" class="form-control" maxlength="100" data-bind="value: District.B, attr: { placeholder: District.B.ph }, visible: !District.Localized()" />
                    <input type="text" id="txtDistrict_C" class="form-control" maxlength="100" data-bind="value: District.C, attr: { placeholder: District.C.ph }, visible: !District.Localized()" />
                </div>
            </div>

            <div class="col-sm-10 col-sm-offset-1" data-bind="visible: $root.mode() === 'p' || $root.mode() === 'e'">
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-success nextBtn btn-lg pull-right" type="button" data-bind="click: $root.Save" />
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_Cancel")%>" class="btn btn-info nextBtn btn-lg pull-right" type="button" data-bind="click: $root.Cancel" />
            </div>
            <div class="col-sm-10 col-sm-offset-1" data-bind="visible: $root.mode() === 'v'">
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_Ok")%>" class="btn btn-primary nextBtn btn-lg pull-right always-enabled" type="button" data-bind="click: $root.Cancel" />
            </div>

        </div>

    </div>

    <div style="display: none;">
        <div id="RequiredFields">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
            <p><%=GetGlobalResourceObject("MessagesResource", "X_MarkedRequiredFields")%></p>
        </div>
    </div>

</asp:Content>
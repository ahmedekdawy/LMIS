<%@ Page Title="<%$ Resources:CommonControls, B002 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="Unions.aspx.cs" Inherits="LMIS.Portal.BackEnd.Unions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../BackEnd/Scripts/Unions.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab6default">

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4"><hr></div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "B002")%></div>
            <div class="col-lg-4"><hr></div>
        </div>
        
        <div id="List" class="col-sm-12" data-bind="visible: mode() === 'l'">

            <div class="col-sm-12 text-center">
                <a id="postNew" onclick="vm.Post()" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "B002_Post")%>
                </a>
            </div>

            <table id="grdUnions" class="display" style="border-spacing: 0; width: 100%; display: none;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "B002_UnionTitle")%></th>
                        <th></th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                    </tr>
                </tfoot>
                <tbody data-bind="foreach: UnionList">
                    <tr data-bind="attr: { id: RowId }">
                        <td data-bind="text: Name.toLocal"></td>
                        <td>
                            <a href="#" title=" View " onclick="vm.View(event)" class="editBtn"><i class="fa fa-eye"></i></a>
                            <a href="#" title=" Edit " onclick="vm.Edit(event)" class="editBtn"><i class="fa fa-edit"></i></a>
                            <a href="#" title=" Delete " onclick="vm.Delete(event)"><i class="fa fa-trash-o"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

        <div id="Editor" class="col-sm-12" data-bind="visible: mode() !== 'l', with: Union">

            <div class="col-sm-12">

                <div class="form-group">
                    <label id="lblName"><%=GetGlobalResourceObject("CommonControls", "B002_Name")%> *</label>
                    <div class="input-group">
                        <input type="text" id="txtName_A" class="form-control validationElement" maxlength="200" data-bind="value: Name.A, attr: { placeholder: Name.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnName" class="btn btn-default always-enabled" data-bind="click: function () { Name.LocalizeView(!Name.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtName_B" class="form-control" maxlength="200" data-bind="value: Name.B, attr: { placeholder: Name.B.ph }, visible: !Name.Localized()" />
                    <input type="text" id="txtName_C" class="form-control" maxlength="200" data-bind="value: Name.C, attr: { placeholder: Name.C.ph }, visible: !Name.Localized()" />
                </div>

                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "B002_Address")%> *</label>
                    <div class="trilingual-textarea validationElement">
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
                    <textarea id="txtaddress_En" maxlength="1000" data-bind="textInput: Address.A, visible: (Address.ActiveLang() == 'en')" class="form-control always-white validationElement" rows="8"></textarea>
                    <textarea id="txtaddress_Fr" maxlength="1000" data-bind="textInput: Address.B, visible: (Address.ActiveLang() == 'fr')" class="form-control always-white validationElement" rows="8"></textarea>
                    <textarea id="txtaddress_Ar" maxlength="1000" data-bind="textInput: Address.C, visible: (Address.ActiveLang() == 'ar')" class="form-control always-white validationElement" rows="8"></textarea>
                </div>

            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label id="lblTelephone"><%=GetGlobalResourceObject("CommonControls", "B002_Telephone")%></label>
                    <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Telephone">
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label id="lblFax"><%=GetGlobalResourceObject("CommonControls", "B002_Fax")%></label>
                    <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Fax">
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label id="lblEmail"><%=GetGlobalResourceObject("CommonControls", "B002_Email")%></label>
                    <input type="text" maxlength="150" class="form-control" data-bind="textInput: Email">
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "B002_Website")%></label>
                    <input type="text" maxlength="150" class="form-control" data-bind="textInput: Website">
                </div>
            </div>

            <div class="row form-divider col-sm-12">
                <div class="col-lg-4"><hr></div>
                <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "B002_Profs")%></div>
                <div class="col-lg-4"><hr></div>
            </div>

            <div class="col-sm-10 col-sm-offset-1" data-bind="visible: $root.mode() !== 'v'">
                <div class="form-group">
                    <label id="lblProf"><%=GetGlobalResourceObject("CommonControls", "B002_Prof")%> *</label>
                    <div class="input-group">
                        <input type="text" id="txtProf_A" class="form-control validationElement" maxlength="200" data-bind="value: $root.NewProf.A, attr: { placeholder: $root.NewProf.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnProf" class="btn btn-default always-enabled" data-bind="click: function () { $root.NewProf.LocalizeView(!$root.NewProf.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtProf_B" class="form-control" maxlength="200" data-bind="value: $root.NewProf.B, attr: { placeholder: $root.NewProf.B.ph }, visible: !$root.NewProf.Localized()" />
                    <input type="text" id="txtProf_C" class="form-control" maxlength="200" data-bind="value: $root.NewProf.C, attr: { placeholder: $root.NewProf.C.ph }, visible: !$root.NewProf.Localized()" />
                </div>

            </div>
        
            <div class="col-sm-10 col-sm-offset-1" data-bind="visible: $root.mode() !== 'v'">
                <div class="text-center">
                    <input value="<%=GetGlobalResourceObject("CommonControls", "X_Add")%>" class="btn btn-choose-graph btn-sm btn-success" style="width: 100px;" type="button" data-bind="click: $root.AddProf" />
                    <input value="<%=GetGlobalResourceObject("CommonControls", "X_Clear")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: $root.ClearProfs" />
                </div>
            </div>

            <div class="col-sm-10 col-sm-offset-1 grd">
                <p></p>
                <table style="border-spacing: 0; width: 100%;">
                    <thead>
                        <tr>
                            <th><%=GetGlobalResourceObject("CommonControls", "B002_Prof")%></th>
                            <th data-bind="visible: $root.mode() !== 'v'">...</th>
                        </tr>
                    </thead>
                    <tbody data-bind="foreach: Professions, visible: Professions().length > 0">
                        <tr>
                            <td data-bind="text: lmis.globalString.toLocal($data, true)"></td>
                            <td data-bind="visible: $root.mode() !== 'v'">
                                <label data-bind="click: $root.RemoveProf, css: $root.mode() === 'v' ? 'disabledActionLabel' : 'actionLabel'">
                                    <%=GetGlobalResourceObject("CommonControls", "X_Remove")%>
                                </label>
                            </td>
                        </tr>
                    </tbody>
                    <tbody data-bind="visible: Professions().length < 1">
                        <tr>
                            <td colspan="2">
                                <%=GetGlobalResourceObject("MessagesResource", "B002_AddProf")%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="row form-divider col-sm-12">
                <div class="col-lg-4"><hr></div>
                <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "B002_Comms")%></div>
                <div class="col-lg-4"><hr></div>
            </div>
        
            <div class="col-sm-4 col-sm-offset-1" data-bind="visible: $root.mode() !== 'v'">
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "B002_Gov")%> *</label>
                    <select id="ddlGov" class="form-control always-white validationElement" data-bind="options: $root.GovOptions, value: $root.NewGov, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
                </div>
            </div>

            <div class="col-sm-6" data-bind="visible: $root.mode() !== 'v'">
                <div class="form-group">
                    <label id="lblComm"><%=GetGlobalResourceObject("CommonControls", "B002_Comm")%> *</label>
                    <div class="input-group">
                        <input type="text" id="txtComm_A" class="form-control validationElement" maxlength="200" data-bind="value: $root.NewComm.A, attr: { placeholder: $root.NewComm.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnComm" class="btn btn-default always-enabled" data-bind="click: function () { $root.NewComm.LocalizeView(!$root.NewComm.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtComm_B" class="form-control" maxlength="200" data-bind="value: $root.NewComm.B, attr: { placeholder: $root.NewComm.B.ph }, visible: !$root.NewComm.Localized()" />
                    <input type="text" id="txtComm_C" class="form-control" maxlength="200" data-bind="value: $root.NewComm.C, attr: { placeholder: $root.NewComm.C.ph }, visible: !$root.NewComm.Localized()" />
                </div>
            </div>

            <div class="col-sm-10 col-sm-offset-1" data-bind="visible: $root.mode() !== 'v'">
                <div class="text-center">
                    <input value="<%=GetGlobalResourceObject("CommonControls", "X_Add")%>" class="btn btn-choose-graph btn-sm btn-success" style="width: 100px;" type="button" data-bind="click: $root.AddComm" />
                    <input value="<%=GetGlobalResourceObject("CommonControls", "X_Clear")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: $root.ClearComms" />
                </div>
            </div>

            <div class="col-sm-10 col-sm-offset-1 grd">
                <p></p>
                <table style="border-spacing: 0; width: 100%;">
                    <thead>
                        <tr>
                            <th><%=GetGlobalResourceObject("CommonControls", "B002_Gov")%></th>
                            <th><%=GetGlobalResourceObject("CommonControls", "B002_Comm")%></th>
                            <th data-bind="visible: $root.mode() !== 'v'">...</th>
                        </tr>
                    </thead>
                    <tbody data-bind="foreach: Committees, visible: Committees().length > 0">
                        <tr>
                            <td data-bind="text: $data.Gov.desc"></td>
                            <td data-bind="text: lmis.globalString.toLocal($data.Name, true)"></td>
                            <td data-bind="visible: $root.mode() !== 'v'">
                                <label data-bind="click: $root.RemoveComm, css: $root.mode() === 'v' ? 'disabledActionLabel' : 'actionLabel'">
                                    <%=GetGlobalResourceObject("CommonControls", "X_Remove")%>
                                </label>
                            </td>
                        </tr>
                    </tbody>
                    <tbody data-bind="visible: Committees().length < 1">
                        <tr>
                            <td colspan="3">
                                <%=GetGlobalResourceObject("MessagesResource", "B002_AddComm")%>
                            </td>
                        </tr>
                    </tbody>
                </table>
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
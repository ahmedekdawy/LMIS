<div class="col-md-12" style="padding-top: 40px; padding-bottom: 40px;" data-bind="validationOptions: kovOptions">
    <div class="col-md-2">
    </div>
    <div class="col-md-8">
        <div class="col-md-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Country")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: CountryOptions, value: Country, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_City")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: CityOptions, value: City, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label id="lblPostalCode"><%=GetGlobalResourceObject("CommonControls", "X_PostalCode")%>  *</label>
                <input type="text" maxlength="10" class="form-control validationElement" data-bind="textInput: PostalCode">
            </div>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <label id="lblAddress"><%=GetGlobalResourceObject("CommonControls", "X_Address")%></label>
                <div class="trilingual-textarea">
                    <label lang="en" data-bind="css: lmis.string.isNullOrWhiteSpace(OrgAddress.A()) ? 'unedited' : 'edited'">
                        <input type="radio" value="en" class="always-enabled" data-bind="checked: OrgAddress.ActiveLang, click: function () { $('#txtAddress_En').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_English")%>
                    </label>
                    <label lang="fr" data-bind="css: lmis.string.isNullOrWhiteSpace(OrgAddress.B()) ? 'unedited' : 'edited'">
                        <input type="radio" value="fr" class="always-enabled" data-bind="checked: OrgAddress.ActiveLang, click: function () { $('#txtAddress_Fr').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_French")%>
                    </label>
                    <label lang="ar" data-bind="css: lmis.string.isNullOrWhiteSpace(OrgAddress.C()) ? 'unedited' : 'edited'">
                        <input type="radio" value="ar" class="always-enabled" data-bind="checked: OrgAddress.ActiveLang, click: function () { $('#txtAddress_Ar').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_Arabic")%>
                    </label>
                </div>
                <textarea id="txtAddress_En" maxlength="200" data-bind="textInput: OrgAddress.A, visible: (OrgAddress.ActiveLang() == 'en')" class="form-control always-white" rows="4"></textarea>
                <textarea id="txtAddress_Fr" maxlength="200" data-bind="textInput: OrgAddress.B, visible: (OrgAddress.ActiveLang() == 'fr')" class="form-control always-white" rows="4"></textarea>
                <textarea id="txtAddress_Ar" maxlength="200" data-bind="textInput: OrgAddress.C, visible: (OrgAddress.ActiveLang() == 'ar')" class="form-control always-white" rows="4"></textarea>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label id="lblTelephone"><%=GetGlobalResourceObject("CommonControls", "X_Telephone")%></label>
                <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Telephone">
            </div>
        </div>
        <div class="col-md-8">
            <div class="form-group">
                <label id="lblWebsite"><%=GetGlobalResourceObject("CommonControls", "X_Website")%></label>
                <input type="text" maxlength="150" class="form-control" data-bind="textInput: Website">
            </div>
        </div>
    </div>
    <div class="col-md-2">
    </div>
    <div style="display: none">
        <span id="X_InsertNewItem"><%=GetGlobalResourceObject("MessagesResource", "X_InsertNewItem")%></span>
    </div>
</div>
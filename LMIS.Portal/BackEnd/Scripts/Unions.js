function Union(item, idxLookup, lookups) {

    this.UnionId = ko.observable(item.UnionId);
    this.RowId = ko.computed(function () { return "r" + this.UnionId(); }, this);

    this.Name = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    if (item.Name) this.Name.Populate(item.Name.English, item.Name.French, item.Name.Arabic);
    this.Name.toLocal = ko.computed(function () { return lmis.globalString.toLocal(this.Name.dto(), true); }, this);

    this.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    if (item.Address) this.Address.Populate(item.Address.English, item.Address.French, item.Address.Arabic);
    this.Address.toLocal = ko.computed(function () { return lmis.globalString.toLocal(this.Address.dto(), true); }, this);
    this.Address.LocalizeView(false);
    this.Address.ActiveLang = ko.observable(lmis.uiCulture);

    this.Telephone = ko.observable(item.Telephone);
    this.Fax = ko.observable(item.Fax);
    this.Website = ko.observable(item.Website);
    this.Email = ko.observable(item.Email);
    this.Logo = ko.observable(item.Logo);

    this.Professions = ko.observableArray(item.Professions || []);

    if (item.Committees) item.Committees.forEach(function (c) { c.Gov.desc = lmis.globalString.toLocal(lookups[idxLookup.indexOf(c.Gov.id)], true); });
    this.Committees = ko.observableArray(item.Committees || []);

};

function ViewModel() {

    var self = this;
    var actionInProgress = false;

    //Initialize VM
    self.mode = ko.observable("l");
    self.Union = ko.observable();
    self.UnionList = ko.observableArray([]);
    self.UnionList.ItemByEvent = function (e) {
        return self.UnionList().filter(function (i) { return i.RowId() === $(e.target).closest("tr")[0].id; })[0];
    }
    self.NewProf = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.NewComm = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.NewGov = ko.observable();
    self.GovOptions = ko.observableArray();
    self.tmpProfs = [];
    self.tmpComms = [];

    //VM Operations
    self.InitGrid = function (rowId) {
        if (self.grd) {
            self.grd.destroy();
            $("#grd").remove();
        }
        self.grd = $("#grdUnions").clone().insertAfter("#grdUnions").show().prop("id", "grd").DataTable();
        //if (rowId) self.grd.row("#" + rowId).show();
    }
    self.List = function () {

        lmis.ajax("../BackEnd/Unions.aspx/List", null, 0, "show,close",
            function (data) {
                if (data && data.d && data.d.data && data.d.res) {
                    var idxLookup = data.d.res.map(function (x) { return x.Id; });
                    var ds = $.map(data.d.data, function (item) { return new Union(item, idxLookup, data.d.res) });
                    var govOptions = data.d.res.map(function (x) { return { id: x.Id, desc: lmis.globalString.toLocal(x, true) } });
                    self.GovOptions(govOptions);
                    self.UnionList(ds);
                    self.InitGrid();
                }
            });

    }
    self.LoadRecord = function (item, editable) {
        if (!item) return;
        self.Union(item);
        self.DisableUserInput(!editable);
        self.ResetInputMasks();
        item.Name.LocalizeView(false, !editable);
        item.Address.LocalizeView(false);
        self.mode(editable ? "e" : "v");
    }
    self.Post = function () {
        self.Union(new Union({}));
        self.ResetInputMasks();
        self.mode("p");
    }
    self.View = function (e) {
        var item = self.UnionList.ItemByEvent(e);
        self.LoadRecord(item, false);
    }
    self.Edit = function (e) {
        var item = self.UnionList.ItemByEvent(e);
        self.tmpProfs = item.Professions().slice(0);
        self.tmpComms = item.Committees().slice(0);
        self.LoadRecord(item, true);
    }
    self.Delete = function (e) {

        var item = self.UnionList.ItemByEvent(e);

        function onConfirm() {

            var dto = { id: item.UnionId(), reason: "" };

            lmis.ajax("../BackEnd/Unions.aspx/Delete", dto, 0, "show,close",
                function () {
                    self.grd.row("#" + item.RowId()).remove().draw(false);
                    self.UnionList.remove(item);
                }).always(function () {
                    actionInProgress = false;
                });

        }

        function onCancel() {
            actionInProgress = false;
        }

        if (!actionInProgress) {
            actionInProgress = true;
            lmis.notification.confirm(onConfirm, onCancel);
        }

    }
    self.Validate = function (data) {

        var bResult = false;

        //Required Fields
        if (data.Professions().length < 1 || data.Committees().length < 1 || data.Name.isNullOrWhiteSpace()
            || data.Address.isNullOrWhiteSpace()) {

            lmis.notification.error($("#RequiredFields").html());

        } else bResult = true;

        return bResult;

    }
    self.Save = function () {

        var item = self.Union();

        if (!item || !self.Validate(item)) return;

        var dto = {
            data: {
                UnionId: (self.mode() === "e") ? item.UnionId() : 0,
                Name: item.Name.getValue(),
                Address: item.Address.getValue(),
                Telephone: item.Telephone(),
                Email: item.Email(),
                Fax: item.Fax(),
                Website: item.Website(),
                Professions: item.Professions() || [],
                Committees: item.Committees() || []
            },
            validateOnly: false
        };

        return lmis.ajax("../BackEnd/Unions.aspx/Post", dto, 0, "show,close",
            function (data) {
                if (data.d) {
                    if (self.mode() === "p") {
                        item.UnionId(data.d);
                        self.UnionList.push(item);
                    }
                    self.InitGrid(item.RowId());
                    self.mode("l");
                    lmis.notification.success();
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);

            });

    }
    self.Cancel = function () {
        if (self.mode() === "e") {
            self.Union().Professions(self.tmpProfs);
            self.Union().Committees(self.tmpComms);
            self.tmpProfs = [];
            self.tmpComms = [];
        }
        self.mode("l");
    }
    self.AddProf = function () {
        if (!self.NewProf.dto()) return;
        self.Union().Professions.unshift(self.NewProf.dto());
        self.NewProf.ClearText();
    }
    self.RemoveProf = function (item) {
        if (self.mode() === "v") return;
        self.Union().Professions.remove(item);
    }
    self.ClearProfs = function () {
        self.Union().Professions([]);
    }
    self.AddComm = function () {
        if (!self.NewGov() || !self.NewComm.dto()) return;
        self.Union().Committees.unshift({ Gov: { id: self.NewGov(), desc: $("#ddlGov option:selected").text() }, Name: self.NewComm.dto() });
        self.NewComm.ClearText();
    }
    self.RemoveComm = function (item) {
        if (self.mode() === "v") return;
        self.Union().Committees.remove(item);
    }
    self.ClearComms = function () {
        self.Union().Committees([]);
    }

    //UI Operations
    self.DisableUserInput = function (bDisable) {

        if (typeof bDisable === "undefined")
            bDisable = self.EditingBlocked;
        else
            self.EditingBlocked = bDisable;

        $("#Editor :text").css("background-color", "white");
        $("#Editor .always-white").css("background-color", "white");
        $("#Editor :input").attr("disabled", bDisable);
        $("#Editor .always-disabled").attr("disabled", true);
        $("#Editor .always-enabled").attr("disabled", false);

    }
    self.ResetInputMasks = function () {
        lmis.setMask.phone($(".phone"));
    }

    //Initialize UI
    self.List();

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
function Office(item) {

    this.OfficeId = ko.observable(item.OfficeId);
    this.RowId = ko.computed(function () { return "r" + this.OfficeId(); }, this);

    this.Title = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    if (item.Title) this.Title.Populate(item.Title.English, item.Title.French, item.Title.Arabic);
    this.Title.toLocal = ko.computed(function () {
        return lmis.globalString.toLocal(this.Title.dto(), true);
    }, this);

    this.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    if (item.Address) this.Address.Populate(item.Address.English, item.Address.French, item.Address.Arabic);
    this.Address.toLocal = ko.computed(function () { return lmis.globalString.toLocal(this.Address.dto(), true); }, this);
    this.Address.LocalizeView(false);
    this.Address.ActiveLang = ko.observable(lmis.uiCulture);

    this.District = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    if (item.District) this.District.Populate(item.District.English, item.District.French, item.District.Arabic);
    this.District.toLocal = ko.computed(function () { return lmis.globalString.toLocal(this.District.dto(), true); }, this);

    this.Telephone = ko.observable(item.Telephone);
    this.Mobile = ko.observable(item.Mobile);
    this.Fax = ko.observable(item.Fax);
    this.Hotline = ko.observable(item.Hotline);

};

function ViewModel() {

    var self = this;
    var actionInProgress = false;

    //Initialize VM
    self.mode = ko.observable("l");
    self.Office = ko.observable();
    self.OfficeList = ko.observableArray([]);
    self.OfficeList.ItemByEvent = function (e) {
        return self.OfficeList().filter(function (i) { return i.RowId() === $(e.target).closest("tr")[0].id; })[0];
    }

    //VM Operations
    self.InitGrid = function (rowId) {
        if (self.grd) {
            self.grd.destroy();
            $("#grd").remove();
        }
        self.grd = $("#grdOffices").clone().insertAfter("#grdOffices").show().prop("id", "grd").DataTable();
        //if (rowId) self.grd.row("#" + rowId).show();
    }
    self.List = function () {

        lmis.ajax("../BackEnd/Offices.aspx/List", null, 0, "show,close",
            function (data) {
                if (data && data.d) {
                    var ds = $.map(data.d, function (item) { return new Office(item) });
                    self.OfficeList(ds);
                    self.InitGrid();
                }
            });

    }
    self.LoadRecord = function (item, editable) {
        if (!item) return;
        self.Office(item);
        self.DisableUserInput(!editable);
        self.ResetInputMasks();
        item.Title.LocalizeView(false, !editable);
        item.Address.LocalizeView(false);
        item.District.LocalizeView(false, !editable);
        self.mode(editable ? "e" : "v");
    }
    self.Post = function () {
        self.Office(new Office({}));
        self.ResetInputMasks();
        self.mode("p");
    }
    self.View = function (e) {
        var item = self.OfficeList.ItemByEvent(e);
        self.LoadRecord(item, false);
    }
    self.Edit = function (e) {
        var item = self.OfficeList.ItemByEvent(e);
        self.LoadRecord(item, true);
    }
    self.Delete = function (e) {

        var item = self.OfficeList.ItemByEvent(e);

        function onConfirm() {

            var dto = { id: item.OfficeId(), reason: "" };

            lmis.ajax("../BackEnd/Offices.aspx/Delete", dto, 0, "show,close",
                function () {
                    self.grd.row("#" + item.RowId()).remove().draw(false);
                    self.OfficeList.remove(item);
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
        if (data.Title.isNullOrWhiteSpace()
            || data.Address.isNullOrWhiteSpace()
            || data.District.isNullOrWhiteSpace()
            || lmis.string.isNullOrWhiteSpace(data.Telephone())
            || lmis.string.isNullOrWhiteSpace(data.Hotline())) {

            lmis.notification.error($("#RequiredFields").html());

        } else bResult = true;

        return bResult;

    }
    self.Save = function () {

        var item = self.Office();

        if (!item || !self.Validate(item)) return;

        var dto = { data: {
                OfficeId: (self.mode() === "e") ? item.OfficeId() : 0,
                Title: item.Title.getValue(),
                Address: item.Address.getValue(),
                District: item.District.getValue(),
                Telephone: item.Telephone(),
                Mobile: item.Mobile(),
                Fax: item.Fax(),
                Hotline: item.Hotline()
            }
        };

        return lmis.ajax("../BackEnd/Offices.aspx/Post", dto, 0, "show,close",
            function (data) {
                if (data.d) {
                    if (self.mode() === "p") {
                        item.OfficeId(data.d);
                        self.OfficeList.push(item);
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
        self.mode("l");
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
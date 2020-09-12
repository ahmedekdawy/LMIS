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

        lmis.ajax("../FrontEnd/Offices.aspx/List", null, 0, "show,close",
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
        item.Title.LocalizeView(false);
        item.Address.LocalizeView(false);
        item.District.LocalizeView(false);
        self.mode(editable ? "e" : "v");
    }
  
    self.View = function (e) {
        var item = self.OfficeList.ItemByEvent(e);
        self.LoadRecord(item, false);
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
 

 
    //Initialize UI
    self.List();

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
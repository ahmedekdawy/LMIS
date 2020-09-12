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

    item.Committees.forEach(function (c) { c.Gov.desc = lmis.globalString.toLocal(lookups[idxLookup.indexOf(c.Gov.id)], true); });
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

        lmis.ajax("../FrontEnd/Unions.aspx/List", null, 0, "show,close",
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



 
 
    

    //Initialize UI
    self.List();

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
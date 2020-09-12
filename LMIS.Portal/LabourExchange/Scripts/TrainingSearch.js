function ViewModel() {

    var self = this;
    var query = (typeof lmis.queryString["q"] === "undefined") ? "" : decodeURI(lmis.queryString["q"]);
    var actionInProgress = false;
    var grd;

    //Initialize VM
    self.Keywords = ko.observable(query);
    self.VmList = ko.observableArray([]);
    self.Org = ko.observable();
    self.OrgOptions = ko.observableArray([]);
    self.Country = ko.observable();
    self.CountryOptions = ko.observableArray([]);
    self.City = ko.observable();
    self.CityOptions = ko.observableArray([]);

    //VM Operations
    self.Reload = function() {
        if (!lmis.string.isNullOrWhiteSpace(self.Keywords())) {
            var url = "../LabourExchange/TrainingSearch.aspx?q=" + self.Keywords();
            url += "#anchor";
            window.location.assign(encodeURI(url));
        }
    }
    self.Search = function () {

        if (!actionInProgress && !lmis.string.isNullOrWhiteSpace(self.Keywords())) {

            actionInProgress = true;

            lmis.ajax("../LabourExchange/TrainingSearch.aspx/Search", { keywords: self.Keywords(), langCode: lmis.uiCulture }, 0, "show,close",
                function(data) {
                    self.VmList(data.d);
                    grd = $("#grd").DataTable({ ordering: false, dom: "tipr" });
                    grd.columns([1, 2, 3]).visible(false);
                    lmis.datatable.SetOuterBorderStyle($("#grd"), "none");
                    self.UpdateOrgOptions();
                }).always(function () {
                    actionInProgress = false;
                });

        }

    }
    self.UpdateOrgOptions = function () {
        var ds = $.map(self.VmList(), function (item) { return { id: item.PortalUserId, desc: item.Extras["OrgName"] } });
        self.OrgOptions(lmis.arr.unique(ds, "id"));
    }
    self.TrainingDetails = function (key) {
        return "TrainingDetails?k=" + key + "#anchor";
    }

    //Initialize UI
    self.Search();
    lmis.api.SubCodes(self.CountryOptions, "009");
    self.Country.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal);
    });
    $("#txtKeywords").keypress(function (e) {
        if (e.which === 13) self.Reload();
    });
    self.Org.subscribe(function(newVal) {
        if (grd) grd.column(1).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
    self.Country.subscribe(function (newVal) {
        if (grd) grd.column(2).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
    self.City.subscribe(function (newVal) {
        if (grd) grd.column(3).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
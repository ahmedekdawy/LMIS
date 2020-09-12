function ViewModel() {

    var self = this;
    var query = (typeof lmis.queryString["q"] === "undefined") ? "" : decodeURI(lmis.queryString["q"]);
    var filter1 = (typeof lmis.queryString["f1"] === "undefined") ? [] : decodeURI(lmis.queryString["f1"]).split(",");
    var actionInProgress = false;
    var grd;

    //Initialize VM
    self.Keywords = ko.observable(query);
    self.VmList = ko.observableArray([]);
    self.EmploymentTypes = ko.observableArray([]);
    self.EmploymentTypeOptions = ko.observableArray([]);
    self.Country = ko.observable();
    self.CountryOptions = ko.observableArray([]);
    self.City = ko.observable();
    self.CityOptions = ko.observableArray([]);

    //VM Operations
    self.Reload = function() {
        if (!lmis.string.isNullOrWhiteSpace(self.Keywords())) {
            var url = "../LabourExchange/JobSearch.aspx?q=" + self.Keywords();
            if (self.EmploymentTypes().length > 0)
                url += "&f1=" + self.EmploymentTypes().join();
            url += "#anchor";
            window.location.assign(encodeURI(url));
        }
    }
    self.Search = function () {

        if (!actionInProgress && !lmis.string.isNullOrWhiteSpace(self.Keywords())) {

            actionInProgress = true;

            lmis.ajax("../LabourExchange/JobSearch.aspx/Search", { keywords: self.Keywords(), langCode: lmis.uiCulture }, 0, "show,close",
                function(data) {
                    self.VmList(data.d);
                    grd = $("#grd").DataTable({ ordering: false, dom: "tipr" });
                    grd.columns([1, 2, 3]).visible(false);
                    lmis.datatable.SetOuterBorderStyle($("#grd"), "none");
                    self.EmploymentTypes.valueHasMutated();
                }).always(function () {
                    actionInProgress = false;
                });

        }

    }
    self.JobDetails = function (key) {
        return "JobDetails?k=" + key + "#anchor";
    }

    //Initialize UI
    self.Search();
    lmis.api.SubCodes(self.EmploymentTypeOptions, "018", function () {
        self.EmploymentTypes(filter1);
        $("#EmpType").multiselect("rebuild");
    });
    lmis.api.SubCodes(self.CountryOptions, "009");
    self.Country.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal);
    });
    $("#txtKeywords").keypress(function (e) {
        if (e.which === 13) self.Reload();
    });
    self.EmploymentTypes.subscribe(function(newVal) {
        if (grd) grd.column(1).search(newVal.join("|"), true).draw(false);
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
function ViewModel() {

    var self = this;
    var query = (typeof lmis.queryString["q"] === "undefined") ? null : decodeURI(lmis.queryString["q"]);
    var actionInProgress = false;
    var grd;

    //Initialize VM
    self.JobOffer = ko.observable();
    self.JobOfferOptions = ko.observableArray([]);
    self.VmList = ko.observableArray([]);
    self.JobTitle = ko.observable();
    self.JobTitleOptions = ko.observableArray([]);
    self.Skill = ko.observable();
    self.SkillOptions = ko.observableArray([]);
    self.Gender = ko.observable();
    self.GenderOptions = ko.observableArray([]);
    self.Country = ko.observable();
    self.CountryOptions = ko.observableArray([]);
    self.City = ko.observable();
    self.CityOptions = ko.observableArray([]);
    self.ExpFrom = ko.observable(0);
    self.ExpTo = ko.observable(30);

    //VM Operations
    self.Reload = function() {
        var url = "CVSearch.aspx";
        if (self.JobOffer()) url += "?q=" + self.JobOffer();
        url += "#anchor";
        window.location.assign(encodeURI(url));
    }
    self.Search = function () {

        if (!actionInProgress) {

            actionInProgress = true;
            var jobOfferId = self.JobOffer() || 0;

            lmis.ajax("../LabourExchange/CVSearch.aspx/Search", { jobOfferId: jobOfferId, langCode: lmis.uiCulture }, 0, "show,close",
                function (data) {
                    var filters;
                    if (data.d && data.d[0]&& data.d[0].Filters) {
                        filters = data.d.splice(0, 1)[0].Filters;
                        self.ExpFrom(filters.ExpFrom);
                        self.ExpTo(filters.ExpTo);
                        self.ExpFrom.jo = filters.ExpFrom;
                        self.ExpTo.jo = filters.ExpTo;
                    }
                    data.d.forEach(function(i) {
                        i.YOE = Math.round(i.Experience / 360);
                        i.Label = i.FirstName + " " + i.LastName;// + " (" + i.YOE + ")";
                        i.MedFit = (filters && filters.MedConds && i.MedCond) ? filters.MedConds.indexOf(i.MedCond.id) !== -1 : false;
                    });
                    self.VmList(data.d);
                    grd = $("#grd").DataTable({ order: [[8, "desc"], [7, "desc"], [3, "desc"], [1, "desc"]], dom: "tipr" });
                    grd.columns([1, 2, 3, 4, 5, 6, 7, 8]).visible(false);
                    lmis.datatable.SetOuterBorderStyle($("#grd"), "none");
                    grd.on("draw", self.InitStarRatings);
                    self.InitStarRatings();
                }).always(function () {
                    actionInProgress = false;
                });

        }

    }
    self.CandidateDetails = function (key) {
        return "/Individual/Profile?m=v&k=" + key + "#anchor";
    }
    self.InitStarRatings = function() {
        $(".StarRating").rating({
        starCaptions: function (val) {
            return $("#SkillRating").html() + ": " + (20 * val) + " %";
        },
        starCaptionClasses: function (val) {
            if (val < 2.5) return "label label-danger";
            else return "label label-success";
        },
        showClear: false
        });
    }

    //Initialize UI
    lmis.ajax("../LabourExchange/CVSearch.aspx/List", { langCode: lmis.uiCulture }, 0, "show,close", function (data) {
        var ds = $.map(data.d, function (i) {
            return {
                id: i.JobId,
                desc: lmis.string.isNullOrWhiteSpace(i.Title) ? lmis.globalString.toLocal(i.NewTitle, true) : i.Title
            }
        });
        self.JobOfferOptions(ds);
        self.JobOffer(query);
        self.Search();
    });

    $.fn.dataTable.ext.search.push(
        function (settings, data) {
            var min = self.ExpFrom() * 360; // years to days
            var max = self.ExpTo() * 360; // years to days
            var exp = parseInt(data[3]) || 0; // use data for the experience column

            if ((isNaN(min) && isNaN(max)) || (isNaN(min) && exp <= max) ||
                (min <= exp && isNaN(max)) || (min <= exp && exp <= max)) return true;

            return false;
        }
    );

    $("#slider").slider({
        range: true, min: self.ExpFrom(), max: self.ExpTo(), values: [self.ExpFrom(), self.ExpTo()],
        slide: function (event, ui) {
            $("#experience").val(ui.values[0] + " - " + ui.values[1]);
            self.ExpFrom(parseInt(ui.values[0]));
            self.ExpTo(parseInt(ui.values[1]));
            if (grd) grd.draw(false);
        }
    });
    self.ExpFrom.subscribe(function (newVal) {
        $("#slider").slider("values", [newVal, self.ExpTo()]);
        $("#experience").val(newVal + " - " + self.ExpTo());
        if (grd) grd.draw(false);
    });
    self.ExpTo.subscribe(function (newVal) {
        $("#slider").slider("values", [self.ExpFrom(), newVal]);
        $("#experience").val(self.ExpFrom() + " - " + newVal);
        if (grd) grd.draw(false);
    });

    lmis.api.SubCodes(self.JobTitleOptions, "017");
    lmis.api.SubCodes(self.SkillOptions, "021");
    lmis.api.SubCodes(self.GenderOptions, "002");
    lmis.api.SubCodes(self.CountryOptions, "009");
    self.Country.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal);
    });
    self.Skill.subscribe(function (newVal) {
        if (grd) grd.column(1).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
    self.Gender.subscribe(function (newVal) {
        if (grd) grd.column(2).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
    self.Country.subscribe(function (newVal) {
        if (grd) grd.column(4).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
    self.City.subscribe(function (newVal) {
        if (grd) grd.column(5).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
    self.JobTitle.subscribe(function (newVal) {
        if (grd) grd.column(6).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
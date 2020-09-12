function Opportunity(item) {

    this.RowId = ko.computed(function () { return "r" + item.OpportunityId; });
    this.OpportunityId = ko.observable(item.OpportunityId);
    this.Title = ko.observable(lmis.globalString.toLocal(item.Title, true));
    this.FilePath = ko.observable(lmis.x.downloadURL + item.FilePath);

};

function ViewModel() {

    var self = this;
    var mode = lmis.queryString["m"];
    if (mode) mode = mode.toLowerCase();

    //Initialize VM
    self.OpportunityList = ko.observableArray([]);

    //VM Operations
    self.GetOpportunityList = function () {

        var informal = mode ? (mode === "i" ? true : false) : null;

        lmis.ajax("../LabourExchange/Opportunities.aspx/GetOpportunityList", { informal: informal }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Opportunity(item) });
                self.OpportunityList(ds);
            });

    }
    self.ViewOpportunity = function (item) {
        window.open("OpportunityDetails?k=" + item.OpportunityId() + "#anchor", "_blank");
    }

    //Initialize UI
    self.GetOpportunityList();
}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
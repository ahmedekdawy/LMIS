
function Opportunity(item) {


    this.Title = lmis.globalString.toLocal(item.Title, true);
    this.OpportunityDate = lmis.format.dateToString(item.StartDate) + ' - ' + lmis.format.dateToString(item.EndDate);
    this.FilePath = ko.observable(lmis.x.downloadURL + item.FilePath);
};
function ViewModel() {

    var self = this;
    var key = lmis.queryString["k"];
   

    //Initialize VM
    self.Title = ko.observable();
    self.OpportunityDate = ko.observable();
   self.FilePath = ko.observable();
   self.VmList = ko.observableArray();

    //VM Operations
    self.LoadRecord = function () {

        if (lmis.string.isNullOrWhiteSpace(key)) return;
        lmis.ajax("../LabourExchange/OpportunityDetails.aspx/GetOrgContactOpportunityById", { id: key, langCode: lmis.uiCulture }, 0, "show,close",
      function (data) {
          var ds = $.map(data, function (item) { return new Opportunity(item) });
          self.VmList(ds);
      });


    }


    //Initialize UI
    self.LoadRecord();
}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
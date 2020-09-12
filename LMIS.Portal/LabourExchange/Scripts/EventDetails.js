
function Event(item) {

  
    this.Title =lmis.globalString.toLocal(item.Title, true);
    this.Description = ko.observable(lmis.globalString.toLocal(item.Description, true));
    this.EventDate = lmis.format.dateToString(item.StartDate) + ' - ' + lmis.format.dateToString(item.EndDate);
    this.Address = lmis.globalString.toLocal(item.Address, true);
    this.TypeStr = ko.observable(item.TypeStr);
    this.FilePath = ko.observable(lmis.x.downloadURL+item.FilePath);
    this.ContactTelephone = ko.observable(item.ContactTelephone);
    this.ContactWebsite = ko.observable('http://' + item.ContactWebsite);
    this.ContactAddress = ko.observable(lmis.globalString.toLocal(item.ContactAddress, true));
};
function ViewModel() {

    var self = this;
    var key = lmis.queryString["k"];
    var actionInProgress = false;

    //Initialize VM
    self.Title = ko.observable();
    self.EventDate = ko.observable();
    self.Description = ko.observable();
    self.Address = ko.observable();
    self.TypeStr = ko.observable();
    self.FilePath = ko.observable();
    self.ContactTelephone = ko.observable();
    self.ContactWebsite = ko.observable();
    self.ContactAddress = ko.observable();
    self.VmList = ko.observableArray(); 
   
    //VM Operations
    self.LoadRecord = function () {

        if (lmis.string.isNullOrWhiteSpace(key)) return;
        lmis.ajax("../LabourExchange/EventDetails.aspx/Get", { id: key, langCode: lmis.uiCulture }, 0, "show,close",
      function (data) {
          var ds = $.map(data, function (item) { return new Event(item) });
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
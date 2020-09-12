function JobsOffer(data) {
    this.id = ko.observable(data.id);
    this.Count = ko.observable(data.Count);
    this.Title = ko.observable(data.Title);


}


function ViewModel() {

    var self = this;
    self.id = ko.observable();
    self.Count = ko.observable();
    self.Title = ko.observable();
    self.JobsOffersList = ko.observableArray();
    //Initialize VM
    
    self.GetJobsOffers = function () {
        lmis.ajax("../FrontEnd/JobsOffers.aspx/GetJobsOffers", null, 0, "",
         function (data) {
        
             var ds = $.map(data.d, function (item) { return new JobsOffer(item) });
             self.JobsOffersList(ds);
             $('#grdJobsOffers').dataTable();

         });
        
    }
 
    self.GetJobsOffers();

}
$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
});
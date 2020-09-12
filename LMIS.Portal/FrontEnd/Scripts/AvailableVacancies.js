function AvailableVacancie(data) {
    this.id = ko.observable(data.id);
    this.Count = ko.observable(data.Count);
    this.Title = ko.observable(data.Title);


}


function ViewModel() {

    var self = this;
    self.id = ko.observable();
    self.Count = ko.observable();
    self.Title = ko.observable();
    self.AvailableVacanciesList = ko.observableArray();
    //Initialize VM
    
    self.GetAvailableVacancies = function () {
         lmis.ajax("../FrontEnd/AvailableVacancies.aspx/GetAvailableVacancies", null, 0, "",
         function (data) {
        
             var ds = $.map(data.d, function (item) { return new AvailableVacancie(item) });
         self.AvailableVacanciesList(ds);
         $('#grdAvailableVacancies').dataTable();

         });
        
    }
 
    self.GetAvailableVacancies();

}
$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
});
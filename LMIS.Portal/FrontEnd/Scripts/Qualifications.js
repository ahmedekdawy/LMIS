function Qualification(item) {

    this.Id = ko.observable(item.Id);
    
    this.GroupName = ko.observable(item.GroupName);
    switch(lmis.uiCulture) {
        case 'fr':
            this.Qualification = ko.observable(item.QualificationFr);
            break;
        case 'ar':
            this.Qualification = ko.observable(item.QualificationAr);
            break;
        default:
         this.Qualification = ko.observable(item.QualificationEn);
    }
};

function ViewModel() {

    var self = this;
    self.Id = ko.observable();
    self.Qualification = ko.observable();
    self.GroupID = ko.observable();
    self.GroupName = ko.observable();
    self.QualificationsList = ko.observableArray();

    self.GetQualification = function () {

        return lmis.ajax("../FrontEnd/Qualifications.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new Qualification(item) });
                self.QualificationsList(ds);
                $('#grdQualification').dataTable();
            });

    }


    self.GetQualification();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});



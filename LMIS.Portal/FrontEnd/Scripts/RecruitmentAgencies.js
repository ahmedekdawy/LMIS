function RecruitmentAgency(item) {

    this.ID = ko.observable(item.ID);
    this.Name = ko.observable(item.Name);
    this.Background = ko.observable(item.Background);
    this.LogoPath = ko.observable(item.LogoPath);
    this.LogoPathVisible = ko.observable(item.LogoPath!=null);
    this.LanguageID = ko.observable(item.LanguageID);
};

function ViewModel() {

    var self = this;

    self.ID = ko.observable();
    self.Name = ko.observable();
    self.Background = ko.observable();
    self.LogoPath = ko.observable();
    self.LogoPathVisible = ko.observable();
    self.LanguageID = ko.observable();
    self.RecruitmentAgenciesList = ko.observableArray();


    self.GetRecruitmentAgency = function () {

        
        return lmis.ajax("../FrontEnd/RecruitmentAgencies.aspx/Get", { type: 0 }, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new RecruitmentAgency(item) });
                self.RecruitmentAgenciesList(ds);
                $('#grdConcept').dataTable();
            });

    }


    self.GetRecruitmentAgency();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});



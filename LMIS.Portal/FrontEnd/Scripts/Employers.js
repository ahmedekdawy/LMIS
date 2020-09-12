function Concepts(item) {

    this.ID = ko.observable(item.ID);
    this.Name = ko.observable(item.Name);
    this.Description = ko.observable(item.Description);
    this.Website = ko.observable(item.Website);
    this.Type = ko.observable(item.Type);
    this.LogoPath = ko.observable(item.LogoPath);
    this.LanguageID = ko.observable(item.LanguageID);
};

function ViewModel() {

    var self = this;

    self.ID = ko.observable();
    self.Name = ko.observable();
    self.Description = ko.observable();
    self.Website = ko.observable();
    self.Type = ko.observable();
    self.LogoPath = ko.observable();
    self.LanguageID = ko.observable();
    self.ConceptList = ko.observableArray();


    self.GetConcepts = function () {


        return lmis.ajax("../FrontEnd/TrainingProviders.aspx/Get", { type: 1 }, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new Concepts(item) });
                self.ConceptList(ds);
                $('#grdConcept').dataTable();
            });

    }


    self.GetConcepts();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});



function Concepts(item) {

    this.ConceptID = ko.observable(item.ConceptID);
    this.ConceptTitle = ko.observable(item.ConceptTitle);
    this.ConceptDescription = ko.observable(item.ConceptDescription);
    this.ImagePath = ko.observable(lmis.x.downloadURL+'ConceptNonFormalTraining/'+item.ImagePath);

};

function ViewModel() {

    var self = this;

    self.ConceptID = ko.observable();
    self.ConceptTitle = ko.observable();
    self.ConceptDescription = ko.observable();
    self.ImagePath = ko.observable();
    self.ConceptList = ko.observableArray();
   

    self.GetConcepts = function () {

        return lmis.ajax("../FrontEnd/ConceptOfNonFormalTraining.aspx/Get" + (lmis.queryString.ConceptID == null ? '' : "?ConceptID=" + lmis.queryString.ConceptID), null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new Concepts(item) });
                self.ConceptList(ds);

            });

    }

 
    self.GetConcepts();
    

};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});

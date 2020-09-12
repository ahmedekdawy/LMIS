function Concepts(item) {

    this.ConceptDefID = ko.observable(item.ConceptDefID);
    this.ConceptDefTitle = ko.observable(item.ConceptDefTitle);
    this.ConceptDefDesc = ko.observable(item.ConceptDefDesc);

};

function ViewModel() {

    var self = this;

    self.ConceptDefID = ko.observable();
    self.ConceptDefTitle = ko.observable();
    self.ConceptDefDesc = ko.observable();
    self.ConceptList = ko.observableArray();
   

    self.GetConcepts = function () {

        return lmis.ajax("../FrontEnd/ConceptsDefinitions.aspx/Get" + (lmis.queryString.ConceptDefID == null ? '' : "?ConceptDefID=" + lmis.queryString.ConceptDefID), null, 0, "",
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

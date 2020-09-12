function HelpfulLink(item) {

    this.HelpfulLinkID = ko.observable(item.HelpfulLinkID);
    this.HelpfulLinkName = ko.observable(item.HelpfulLinkName);
    this.HelpfulLinkURL = ko.observable(item.HelpfulLinkURL);
    this.GroupID = ko.observable(item.GroupID);
    this.GroupName = ko.observable(item.GroupName);
    this.HelpfulLinkLanguage = ko.observable(item.HelpfulLinkLanguage);
};

function ViewModel() {

    var self = this;
    self.HelpfulLinkID = ko.observable();
    self.HelpfulLinkName = ko.observable();
    self.HelpfulLinkURL = ko.observable();
    self.GroupID = ko.observable();
    self.GroupName = ko.observable();
    self.HelpfulLinkLanguage = ko.observable();
    self.HelpfulLinkList = ko.observableArray();
    self.CategoryOptions = ko.observableArray();
    self.onSuccess = function () {
        lmis.api.SubCodes(self.CategoryOptions, "103", function () {
            //self.GroupID(self.AsyncItems.GroupID);
        });

    }
   
    self.GetHelpfulLink = function () {

        return lmis.ajax("../FrontEnd/HelpfulLinksFront.aspx/Get", self.onSuccess(), 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new HelpfulLink(item) });
                self.HelpfulLinkList(ds);
              
           
            });

    }
    self.GetHelpfulLink();
  

};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});

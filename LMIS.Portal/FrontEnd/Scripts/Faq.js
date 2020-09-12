var Faq = function (item) {

    this.FAQID = ko.observable(item.FAQID);
    this.Question = ko.observable(item.Question);
    this.Answer = ko.observable(item.Answer);
    this.FAQCategoryID = ko.observable(item.FAQCategoryID);
    this.GroupName = ko.observable(item.GroupName);
    this.FAQLanguage = ko.observable(item.FAQLanguage);

};

function ViewModel() {

    var self = this;
    self.FAQID = ko.observable();
    self.Question = ko.observable();
    self.Answer = ko.observable();
    self.FAQCategoryID = ko.observable();
    self.FAQLanguage = ko.observable();
    self.GroupName = ko.observable();
    self.CategoryOptions = ko.observableArray();
    self.FaqsList = ko.observableArray();

    lmis.api.SubCodes(self.CategoryOptions, "105", function () {
        //self.GroupID(self.AsyncItems.GroupID);
    });

 
    self.GetFaq = function () {
        var dto = { id: (lmis.queryString.id == null ? 0 : lmis.queryString.id) };
        return lmis.ajax("../FrontEnd/Faq.aspx/Get" , dto, 0, "",
            function (data) {
                self.FaqsList(data.d.Data);
              
            });

    }


    self.GetFaq();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});



function Concepts(item) {

    this.ImagePath = ko.observable(item.split(/[\\]+/).pop());
    this.ImageName = ko.observable(item.split(/[_]+/).pop());
};

function ViewModel() {

    var self = this;
    var validExtensions = ".pdf";
    var maxFileSize = 10 * 1024 * 1024; // 1 MBytes
    self.AcceptedFilesImagePath = validExtensions;
    self.ImagePath = ko.observable();
    self.ImageName = ko.observable();
    self.FilesList = ko.observableArray();
  
    self.GetFiles = function () {


        return lmis.ajax("../FrontEnd/DescriptiveJob.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.res, function (item) { return new Concepts(item) });
                self.FilesList(ds);

            });

    }


    self.GetFiles();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});



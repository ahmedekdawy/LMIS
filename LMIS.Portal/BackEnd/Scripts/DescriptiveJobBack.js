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
     self.ValidateImagePath = function (item, e) {
         self.ValidateImagePath = function (item, e) {

        var selectedFile = e.target.files[0];

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    self.ImagePath(selectedFile);
                    $("#txtImagePath").val(selectedFile.name);
                } else self.ClearImagePathFile();
            } else self.ClearImagePathFile();
        } else self.ClearImagePathFile();

    }
    self.ClearImagePathFile = function () {

        self.ImagePath(null);
        $("#txtImagePath").val("");
        lmis.fileInput.clear($("#hdnImagePath"));

    }
        var selectedFile = e.target.files[0];

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    self.ImagePath(selectedFile);
                    $("#txtImagePath").val(selectedFile.name);
                } else self.ClearImagePathFile();
            } else self.ClearImagePathFile();
        } else self.ClearImagePathFile();

    }
    self.ClearImagePathFile = function () {

        self.ImagePath(null);
        $("#txtImagePath").val("");
        lmis.fileInput.clear($("#hdnImagePath"));

    }
    self.Save = function () {
      
    
            self.UploadImagePath();
        

    }
    self.UploadImagePath = function () {

        if (!self.ImagePath()) {
            self.ClearImagePath();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/UploadDocWithPath?path=" + '/DescriptiveJob', self.ImagePath(), 0, "show",
            function (data) {
                self.ImagePath(data);
                lmis.notification.success();
                self.GetConcepts();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearImagePathFile();   //Validation Error

            });

    }


    self.DeleteFile = function ( name) {

        if (confirm($("#X_ConfirmContinue").html())) {

            return lmis.ajax("../api/upload/UploadDelete?path=" + '/DescriptiveJob/' + name, null, 0, "",
         function (data) {
             if (data == 0) {
                 lmis.notification.success();
                 self.GetConcepts();
             }
             else if (data == -4) {
                 lmis.notification.error($("#X_NotAuthorized").text());
             }
         });


        } else {
            return false;
        }


    }
    self.GetConcepts = function () {

  
        return lmis.ajax("../BackEnd/DescriptiveJobBack.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.res, function (item) { return new Concepts(item) });
                self.FilesList(ds);
          
            });

    }


    self.GetConcepts();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});

function clearControls() {

    self.ImagePath('');

    $(".ImagePath").prop("href", '');

}

function ViewModel() {

    var self = this;
    var validFiles = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var inputMaskDate = $.trim($("#X_InputMaskDate").html());
    var momentDate = $.trim($("#X_MomentDate").html());

    //UI Setup
    $("#txtStartDate").inputmask(inputMaskDate, { "clearIncomplete": true });
    $("#txtEndDate").inputmask(inputMaskDate, { "clearIncomplete": true });

    //ViewModel Data
    self.ValidFiles = validFiles;
    self.Title = ko.observable();
    self.File = ko.observable();
    self.StartDate = ko.observable();
    self.EndDate = ko.observable();

    //ViewModel Operations
    self.Validate = function () {

        var bResult = false;

        //Required Fields
        if (!self.Title() || !self.Title().trim() || !self.File()
            || !self.StartDate() || !self.EndDate()
            || !moment(self.StartDate(), momentDate).isValid()
            || !moment(self.EndDate(), momentDate).isValid()) {

            noty({
                type: "error",
                text: $("#RequiredFields").html(),
                modal: true, killer: true
        });

        } else {

            //Valid Dates
            var dtNow = new Date();
            var dtStartDate = moment(self.StartDate(), momentDate).toDate();
            var dtEndDate = moment(self.EndDate(), momentDate).toDate();

            if (dtStartDate < dtNow || dtEndDate < dtStartDate ) {

                noty({
                    type: "error",
                    text: $("#Validations").html(),
                    modal: true, killer: true
                });

            }
            else bResult = true;

        }

        return bResult;

    };
    self.ValidateFile = function (item, e) {

        var selectedFile = e.target.files[0];

        function invalidate() {
            alert(lmis.x.InvalidFile(validFiles, maxFileSize));
            self.ClearFile();
        }

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validFiles)) {
                    self.File(selectedFile);
                    $("#txtFileName").val(selectedFile.name);
                } else invalidate();
            } else invalidate();
        } else self.ClearFile();

    }
    self.ClearFile = function () {

        self.File(null);
        $("#txtFileName").val("");
        $("#hdnFileBrowser").replaceWith($("#hdnFileBrowser").val("").clone(true));

    }

}

$(document).ready(function() {
    ko.applyBindings(new ViewModel());
});
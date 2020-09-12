function RecruitmentAgency(item) {

    this.RecruitmentAgencieID = ko.observable(item.RecruitmentAgencieID);
    this.Name = ko.observable(item.Name);
    this.Background = ko.observable(item.Background);
    this.LogoPath = ko.observable( item.LogoPath);
    this.LanguageID = ko.observable(item.LanguageID);
};

function ViewModel() {

    var self = this;
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    self.AcceptedFilesLogoPath = validExtensions;
    self.RecruitmentAgencieID = ko.observable();
    self.Name = ko.observable();
    self.Background = ko.observable();
    self.LogoPath = ko.observable();
    self.LanguageID = ko.observable();
    self.RecruitmentAgenciesList = ko.observableArray();
    self.ValidateLogoPath = function (item, e) {

        var selectedFile = e.target.files[0];

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    self.LogoPath(selectedFile);
                    $("#txtLogoPath").val(selectedFile.name);
                } else self.ClearLogoPathFile();
            } else self.ClearLogoPathFile();
        } else self.ClearLogoPathFile();

    }
    self.ClearLogoPathFile = function () {

        self.LogoPath(null);
        $("#txtLogoPath").val("");
        lmis.fileInput.clear($("#hdnLogoPath"));

    }
    self.Save = function () {

        if ($(".Name").val().toString() == '') {
            $(".Name").focus(); return;
        }
        if ($(".Background").val().toString() == '') {
            $(".Background").focus(); return;
        }
       
        
        if ($("#txtLogoPath").val() == '') {
            self.Post();
        } else {
            self.UploadLogoPath();
            
        }
     
            
            
        
        
    }
    self.UploadLogoPath = function () {

        if (!self.LogoPath()) {
            self.ClearLogoPath();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" , self.LogoPath(), 0, "show",
            function (data) {
                self.LogoPath(data);

             
                    self.Post();
              

            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearLogoPathFile();   //Validation Error

            });

    }
    self.Post = function () {
     
      

            var RecruitmentAgencyVm = {
                RecruitmentAgencieID: $("#hdfID").val(),
                Name: $(".Name").val(),
                Background: $(".Background").val(),
                Website: $(".Website").val(),
                Type: $(".ddlType").val(),
                LogoPath: self.LogoPath(),
                LanguageID: $(".ddlLanguage").val().toString()
            };
            var dto = { vm: RecruitmentAgencyVm };
            return lmis.ajax("../BackEnd/RecruitmentAgencies.aspx/Post", dto, 0, "show,close", function (data) {
                if (data.d.Data == null) {
                    lmis.notification.error($("#X_Exist").text());
                } else {
                    lmis.notification.success();

                    if ($("#hdfID").val() != '0') {
                        var index = parseInt($("#hdfRowID").val());

                        self.RecruitmentAgenciesList()[index].Name(data.d.Data.Name);
                        self.RecruitmentAgenciesList()[index].Background(data.d.Data.Background);
                    self.RecruitmentAgenciesList()[index].LogoPath(data.d.Data.LogoPath);
                        self.RecruitmentAgenciesList()[index].LanguageID(data.d.Data.LanguageID);
                    } else {
                        self.RecruitmentAgenciesList.push(new RecruitmentAgency(data.d.Data));
                    }
                   
                    clearControls();
                    $("#hdfID").val(0);
                    $("#hdfRowID").val(0);
                }
            });
        
    }
    self.Update = function (RecruitmentAgency, indx) {
  
       
        $(".Name").val(RecruitmentAgency.Name());
        $(".Background").val(RecruitmentAgency.Background());
        $(".ddlLanguage").val(RecruitmentAgency.LanguageID());
        
        self.LogoPath(RecruitmentAgency.LogoPath());

        $(".LogoPath").prop("href", lmis.x.downloadURL + RecruitmentAgency.LogoPath());
        
        $("#hdfID").val(RecruitmentAgency.RecruitmentAgencieID());
        $("#hdfRowID").val(indx);
        
      
    showpopup('Edit');
   
       
    }
    self.Delete = function (RecruitmentAgency, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {
           
            var dto = { ID: RecruitmentAgency.RecruitmentAgencieID() };
            return lmis.ajax("../BackEnd/RecruitmentAgencies.aspx/Delete", dto, 0, "",
                function (data) {
                    console.log(data.d);
                    if (data.d.Status== 0) {
                        self.RecruitmentAgenciesList.remove(RecruitmentAgency);
                        $('#grdRecruitmentAgency').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }


    }
    self.GetRecruitmentAgency = function () {

        return lmis.ajax("../BackEnd/RecruitmentAgencies.aspx/Get" , null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new RecruitmentAgency(item) });
                self.RecruitmentAgenciesList(ds);
                $('#grdRecruitmentAgency').dataTable();
            });

    }


    self.GetRecruitmentAgency();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});
function showpopupInsert() {
    $("#hdfID").val('0');
    clearControls();
    showpopup('Add');
}
function clearControls() {
    
    
    $(".Name").val('');
    $(".Background").val('');
    $(".Website").val('');
    $(".ddlLanguage").val(1);
    $(".ddlType").val(1);
    $("#hdfID").val('0');

    $(".LogoPath").hide();
    $("#txtLogoPath").val('');
}
function showpopup(_title) {

    var d = $(".pop");
    d.dialog({
        title: _title,
        position: 'fixed',
        top: 20,

        width: 700
    });
    return false;
}
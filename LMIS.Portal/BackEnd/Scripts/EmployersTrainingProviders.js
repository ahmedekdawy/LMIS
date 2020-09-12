function Concepts(item) {

    this.ID = ko.observable(item.ID);
    this.Name = ko.observable(item.Name);
    this.Description = ko.observable(item.Description);
    this.Website = ko.observable(item.Website);
    this.Type = ko.observable(item.Type);
    this.LogoPath = ko.observable( item.LogoPath);
    this.LanguageID = ko.observable(item.LanguageID);
};

function ViewModel() {

    var self = this;
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    self.AcceptedFilesLogoPath = validExtensions;
    self.ID = ko.observable();
    self.Name = ko.observable();
    self.Description = ko.observable();
    self.Website = ko.observable();
    self.Type = ko.observable();
    self.LogoPath = ko.observable();
    self.LanguageID = ko.observable();
    self.ConceptList = ko.observableArray();
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
        if ($(".Description").val().toString() == '') {
            $(".Description").focus(); return;
        }
        if ($(".Website").val().toString() == '') {
            $(".Website").focus(); return;
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
     
      

            var ConceptVm = {
                ID: $("#hdfID").val() ,
                Name: $(".Name").val(),
                Description: $(".Description").val(),
                Website: $(".Website").val(),
                Type: $(".ddlType").val(),
                LogoPath: self.LogoPath(),
                LanguageID: $(".ddlLanguage").val().toString()
            };
            var dto = { vm: ConceptVm };
            return lmis.ajax("../BackEnd/EmployersTrainingProviders.aspx/Post", dto, 0, "show,close", function (data) {
                if (data.d.Data == null) {
                    lmis.notification.error($("#X_Exist").text());
                } else {
                    lmis.notification.success();

                    if ($("#hdfID").val() != '0') {
                        var index = parseInt($("#hdfRowID").val());

                        self.ConceptList()[index].Name(data.d.Data.Name);
                        self.ConceptList()[index].Description(data.d.Data.Description);
                        self.ConceptList()[index].Website(data.d.Data.Website);
                        self.ConceptList()[index].Type(data.d.Data.Type);
                        self.ConceptList()[index].LogoPath(data.d.Data.LogoPath);
                        self.ConceptList()[index].LanguageID(data.d.Data.LanguageID);
                    } else {
                        self.ConceptList.push(new Concepts(data.d.Data));
                    }
                   
                    clearControls();
                    $("#hdfID").val(0);
                    $("#hdfRowID").val(0);
                }
            });
        
    }
    self.UpdateConcept = function (Concept, indx) {
  
       
        $(".Name").val(Concept.Name());
        $(".Description").val(Concept.Description());
        $(".Website").val(Concept.Website());
        $(".ddlLanguage").val(Concept.LanguageID());
        $(".ddlType").val(Concept.Type());
        
        self.LogoPath(Concept.LogoPath());

        $(".LogoPath").prop("href", lmis.x.downloadURL + Concept.LogoPath());
        
        $("#hdfID").val(Concept.ID());
        $("#hdfRowID").val(indx);
        
      
    showpopup('Edit');
   
       
    }
    self.DeleteConcept = function (concept, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {
           
            var dto = { ID: concept.ID() };
            return lmis.ajax("../BackEnd/EmployersTrainingProviders.aspx/Delete", dto, 0, "",
                function (data) {
                    console.log(data.d);
                    if (data.d.Status== 0) {
                        self.ConceptList.remove(concept);
                        $('#grdConcept').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }


    }
    self.GetConcepts = function () {

        return lmis.ajax("../BackEnd/EmployersTrainingProviders.aspx/Get" , null, 0, "",
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
function showpopupInsert() {
    $("#hdfID").val('0');
    clearControls();
    showpopup('Add');
}
function clearControls() {
    
    
    $(".Name").val('');
    $(".Description").val('');
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
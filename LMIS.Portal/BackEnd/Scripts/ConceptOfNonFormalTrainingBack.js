function Concepts(item) {

    this.ConceptID = ko.observable(item.ConceptID);
    this.ConceptTitle = ko.observable(item.ConceptTitle);
    this.ConceptDescription = ko.observable(item.ConceptDescription);
    this.ImagePath = ko.observable( item.ImagePath);
    this.LanguageID = ko.observable(item.LanguageID);
};

function ViewModel() {

    var self = this;
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    self.AcceptedFilesImagePath = validExtensions;
    self.ConceptID = ko.observable();
    self.ConceptTitle = ko.observable();
    self.ConceptDescription = ko.observable();
    self.ImagePath = ko.observable();
    self.LanguageID = ko.observable();
    self.ConceptList = ko.observableArray();
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
    self.Save = function () {

        if ($(".ConceptTitle").val().toString() == '') {
            $(".ConceptTitle").focus(); return;
        }
        if ($("#txtImagePath").val() == '') {
            $("#txtImagePath").focus();
        }
        var FCKConceptDescription = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKConceptDescription');
        if (FCKConceptDescription.GetXHTML(true) == '') {
            lmis.notification.error($("#X_RequiredFieldErrors").html());
              return;
        }
        if (FCKConceptDescription.GetXHTML(true).length > 4000) {
            lmis.notification.error($("#X_MaxLength").html());
              return;
        }
            self.UploadImagePath();
            
        
        
    }
    self.UploadImagePath = function () {

        if (!self.ImagePath()) {
            self.ClearImagePath();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" + '/ConceptNonFormalTraining', self.ImagePath(), 0, "show",
            function (data) {
                self.ImagePath(data);

             
                    self.Post();
              

            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearImagePathFile();   //Validation Error

            });

    }
    self.Post = function () {
     
        var FCKConceptDescription = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKConceptDescription').GetXHTML(true);

            var ConceptVm = {
                ConceptID: $("#hdfConceptID").val() ,
                ConceptTitle: $(".ConceptTitle").val(),
                ConceptDescription: FCKConceptDescription,
                ImagePath: self.ImagePath(),
                LanguageID: $(".ddlLanguage").val().toString()
            };
            var dto = { vm: ConceptVm };
            return lmis.ajax("../BackEnd/ConceptOfNonFormalTrainingBack.aspx/Post", dto, 0, "show,close", function (data) {
                if (data.d.Data == null) {
                    lmis.notification.error($("#X_Exist"));
                } else {
                    lmis.notification.success();

                    if ($("#hdfConceptID").val() != '0') {
                        self.ConceptList.splice($("#hdfRowID").val(), 1);
                        $('#grdConcept').dataTable().fnDeleteRow($("#hdfRowID").val(), null, true);
                    }
                    self.ConceptList.push(new Concepts(data.d.Data));
                    clearControls();
                    $("#hdfConceptID").val(0);
                    $("#hdfRowID").val(0);
                }
            });
        
    }
    self.UpdateConcept = function (Concept, indx) {
  
       
        $(".ConceptTitle").val(Concept.ConceptTitle());
        $(".ddlLanguage").val(Concept.LanguageID());

        self.ImagePath(Concept.ImagePath());

        $(".ImagePath").prop("href", lmis.x.downloadURL +'ConceptNonFormalTraining/'+ Concept.ImagePath());
        
        $("#hdfConceptID").val(Concept.ConceptID());
        $("#hdfRowID").val(indx);
        
        $(".FCKConceptDescription").prop("style", false);
      
  var editor=  FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKConceptDescription');
        editor.SetHTML(Concept.ConceptDescription());
    showpopup('Edit');
   
       
    }
    self.DeleteConcept = function (concept, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {
           
            var dto = { ConceptID: concept.ConceptID() };
            return lmis.ajax("../BackEnd/ConceptOfNonFormalTrainingBack.aspx/Delete", dto, 0, "",
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

        return lmis.ajax("../BackEnd/ConceptOfNonFormalTrainingBack.aspx/Get" , null, 0, "",
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
    $("#hdfConceptID").val('0');
    clearControls();
    showpopup('Add');
}
function clearControls() {
    
    FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKConceptDescription').SetHTML('');
    
    $(".ConceptTitle").val('');
    $(".ddlLanguage").val(1);

    $("#hdfConceptID").val('0');

    $(".ImagePath").hide();
    $("#txtImagePath").val('');
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
function Concepts(item) {

    this.ConceptDefID = ko.observable(item.ConceptDefID);
    this.ConceptDefTitle = ko.observable(item.ConceptDefTitle);
    this.ConceptDefDesc = ko.observable(item.ConceptDefDesc);
    this.LanguageID = ko.observable(item.LanguageID);
};

function ViewModel() {

    var self = this;
    self.ConceptDefID = ko.observable();
    self.ConceptDefTitle = ko.observable();
    self.ConceptDefDesc = ko.observable();
    self.LanguageID = ko.observable();
    self.ConceptList = ko.observableArray();

    self.Save = function () {

        if ($(".ConceptDefTitle").val().toString() == '') {
            $(".ConceptDefTitle").focus(); return;
        }
        if ($(".ConceptDefDesc").val() == '') {
            $(".ConceptDefDesc").focus(); return;
        }
      
        var ConceptVm = {
            ConceptDefID: $("#hdfConceptDefID").val(),
            ConceptDefTitle: $(".ConceptDefTitle").val(),
            ConceptDefDesc: $(".ConceptDefDesc").val(),
            LanguageID: $(".ddlLanguage").val().toString()
        };
        var dto = { vm: ConceptVm };
        return lmis.ajax("../BackEnd/ConceptsDefinitionsBack.aspx/Post", dto, 0, "show,close", function (data) {
            if (data.d.Data == null) {
                lmis.notification.error($("#X_Exist").text());
            } else {
                lmis.notification.success();

                if ($("#hdfConceptDefID").val() != '0') {
                    var index = parseInt($("#hdfRowID").val());
                    self.ConceptList()[index].ConceptDefID(data.d.Data.ConceptDefID);
                    self.ConceptList()[index].ConceptDefTitle(data.d.Data.ConceptDefTitle);
                    self.ConceptList()[index].ConceptDefDesc(data.d.Data.ConceptDefDesc);
                    self.ConceptList()[index].LanguageID(data.d.Data.LanguageID);

                } else {
                    self.ConceptList.push(new Concepts(data.d.Data));
                }
                clearControls();
                $("#hdfConceptDefID").val(0);
                $("#hdfRowID").val(0);
            }
        });
        
        
    }
   
 
    self.UpdateConcept = function (Concept, indx) {
  
       
        $(".ConceptDefTitle").val(Concept.ConceptDefTitle());
        $(".ddlLanguage").val(Concept.LanguageID());
        
        
        $("#hdfConceptDefID").val(Concept.ConceptDefID());
        $("#hdfRowID").val(indx);
        
        $(".ConceptDefDesc").val(Concept.ConceptDefDesc());
      
  
    showpopup('Edit');
   
       
    }
    self.DeleteConcept = function (concept, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {
           
            var dto = { ConceptDefID: concept.ConceptDefID() };
            return lmis.ajax("../BackEnd/ConceptsDefinitionsBack.aspx/Delete", dto, 0, "",
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

        return lmis.ajax("../BackEnd/ConceptsDefinitionsBack.aspx/Get" , null, 0, "",
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
    $("#hdfConceptDefID").val('0');
    clearControls();
    showpopup('Add');
}
function clearControls() {
    

    
    $(".ConceptDefTitle").val('');
    $(".ConceptDefDesc").val('');
    $(".ddlLanguage").val(1);

    $("#hdfConceptDefID").val('0');

}
function showpopup(_Title) {

    var d = $(".pop");
    d.dialog({
        title: _Title,
        position: 'fixed',
        top: 20,

        width: 700
    });
    return false;
}
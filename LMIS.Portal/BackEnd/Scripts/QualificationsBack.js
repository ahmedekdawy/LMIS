var Qualification=function (item) {

    this.Id = ko.observable(item.Id);
    this.QualificationEn = ko.observable(item.QualificationEn);
    this.QualificationAr = ko.observable(item.QualificationAr);
    this.GroupID = ko.observable(item.GroupID);
    this.GroupName = ko.observable(item.GroupName);
    this.QualificationFr = ko.observable(item.QualificationFr);
   
};

function ViewModel() {

    var self = this;
    self.Id = ko.observable();
    self.QualificationEn = ko.observable();
    self.QualificationAr = ko.observable();
    self.GroupID = ko.observable();
    self.GroupName = ko.observable();
    self.QualificationFr = ko.observable();
    self.CategoryOptions = ko.observableArray();
    self.QualificationsList = ko.observableArray();

    lmis.api.SubCodes(self.CategoryOptions, "104", function () {
        //self.GroupID(self.AsyncItems.GroupID);
    });

    self.Save = function () {
        if ($(".ddlCategory").val().toString() == '') {
            $(".ddlCategory").focus(); return;
        }
        if ($(".QualificationEn").val().toString() == '' && $(".QualificationAr").val().toString() == '' && $(".QualificationFr").val().toString() == '') {
            $(".QualificationEn").focus(); return;
        }
    
      

        var QualificationVm = {
            Id: $("#hdfId").val(),
            GroupID: $(".ddlCategory").val(),
            GroupName: $(".ddlCategory option:selected").text(),
            QualificationEn: $(".QualificationEn").val(),
            QualificationAr: $(".QualificationAr").val(),
            QualificationFr: $(".QualificationFr").val()
        };
   
        var dto = { vm: QualificationVm };
        return lmis.ajax("../BackEnd/QualificationsBack.aspx/Post", dto, 0, "show,close", function (data) {
            if (data.d.Data == null) {
                lmis.notification.error($("#X_Exist").text());

            } else {
                lmis.notification.success();
                //self.QualificationsList.push(new Qualification(data));
                
                if ($("#hdfId").val() != '0') {
                    var index = parseInt($("#hdfRowID").val());
                    self.QualificationsList()[index].Id(data.d.Data.Id);
                    self.QualificationsList()[index].GroupID(data.d.Data.GroupID);
                    self.QualificationsList()[index].GroupName(data.d.Data.GroupName);
                    self.QualificationsList()[index].QualificationEn(data.d.Data.QualificationEn);
                    self.QualificationsList()[index].QualificationAr(data.d.Data.QualificationAr);
                    self.QualificationsList()[index].QualificationFr(data.d.Data.QualificationFr);

                } else {
                    self.QualificationsList.push(new Qualification(data.d.Data));
                }
              
                clearControls();
                $("#hdfId").val(0);
                $("#hdfRowID").val(0);
             
            }
        });

    }
    self.UpdateQualification = function (Qualification, indx) {

        $(".QualificationEn").val(Qualification.QualificationEn());
        $("#hdfId").val(Qualification.Id());
        $("#hdfRowID").val(indx);
        $(".QualificationFr").val(Qualification.QualificationFr());
        $(".QualificationAr").val(Qualification.QualificationAr());
        $(".ddlCategory").val(Qualification.GroupID());
     

        showpopup('Edit');
    }
    self.DeleteQualification = function (qualification, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {

            var dto = { Id: qualification.Id() };
            return lmis.ajax("../BackEnd/QualificationsBack.aspx/Delete", dto, 0, "",
                function (data) {
                    console.log(data.d);
                    if (data.d.Status == 0) {
                        self.QualificationsList.splice(indx,1);
                        $('#grdQualification').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }

        
    }
    self.GetQualification = function () {

        return lmis.ajax("../BackEnd/QualificationsBack.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new Qualification(item) });
                self.QualificationsList(ds);
                $('#grdQualification').dataTable();
            });

    }


    self.GetQualification();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});
function showpopupInsert() {
    $("#hdfId").val('0');
    clearControls();
    showpopup('Add');
}
function clearControls() {
    $(".ddlCategory").val('');
    $(".QualificationEn").val('');
    $(".QualificationAr").val('');
    $(".QualificationFr").val('');
}
function showpopup(_title) {

    var d = $(".pop");
    d.dialog({
        title: _title,
        width: 600
    });
    return false;
}
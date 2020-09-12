

function GeneralCode() {
    var self = this;
    self.GeneralID = ko.observable("");
    self.Name = ko.observable("");

}

function SubCode(data) {
    this.GeneralID = ko.observable(data.GeneralID);
    this.SubID = ko.observable(data.SubID);
    this.Name = ko.observable(data.Name);
    this.SubCodes = ko.observable(data.SubCodes);
    this.ParentSubCodeName = ko.observable(data.ParentSubCodeName);
    
}
function GeneralCodeVm() {
    var self = this;
    self.SubCodes = ko.observableArray([]);
    self.GetAllSubCode = function () {
        if ($(".ddlGeneralCodes").val().toString() == '') {
            return;
        }
        var dto = { generalId: $(".ddlGeneralCodes").val().toString() };

        lmis.ajax("../BackEnd/SubCode.aspx/GetAllSubCode", dto, 0, "show,close",
        function (data) {
            var ds = $.map(data.d, function (item) { return new SubCode(item) });
            self.SubCodes(ds);
            var table = $('#grdSubCodes').dataTable();
       
        });

       

    };
    self.DeleteSubCode = function (data) {


        if (confirm("Are You Sure")) {
            $(".hdfEdited").val(data.SubID());
            return true;
        }

        return false;
    };
    self.UpdateSubCode = function (data) {


            $(".hdfEdited").val(data.SubID());
            return true;
      
    };
    self.Save = function () {
        if ($(".ddlGeneralCodes").val().toString() == '') {
            return;
        }
        var dto = { generalId: $(".ddlGeneralCodes").val().toString(), NameEn: $(".txtNameEn").val().toString(), NameAr: $(".txtNameAr").val().toString(), NameFr: $(".txtNameFr").val().toString(),subid:null };

        lmis.ajax("../BackEnd/SubCode.aspx/Save", dto, 0, "show,close");

    };
   
    self.GetAllSubCode();

   
}
function Save() {
    if ($(".ddlGeneralCodes").val().toString() == '') {
        return;
    }
    var dto = { generalId: $(".ddlGeneralCodes").val().toString(), NameEn: $(".txtNameEn").val().toString(), NameAr: $(".txtNameAr").val().toString(), NameFr: $(".txtNameFr").val().toString(),subid:null };

    lmis.ajax("../BackEnd/SubCode.aspx/Save", dto, 0, "show,close",
    function (data) {
        var ds = $.map(data.d, function (item) { return new SubCode(item) });
        self.SubCodes(ds);
 
        var table = $(".grdSubCodes").dataTable();
        table.fnClearTable();

        table.fnDraw();

        table.fnAddData();

    });



};
$(document).ready(function() {
    
 ko.applyBindings(new GeneralCodeVm());
});

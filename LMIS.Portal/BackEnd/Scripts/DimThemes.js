// <reference path="jquery-2.0.3.min.js" />
// <reference path="knockout-3.0.0.js" />



function Theme(data) {
    this.CodeNo = ko.observable(data.CodeNo);
    this.Name = ko.observable(data.Name);
    this.NameAr = ko.observable(data.NameAr);
    this.NameFr = ko.observable(data.NameFr);
    this.ThemeCat = ko.observable(data.ThemeCat);
    this.ThemeCat1 = ko.observable(data.ThemeCat1);
    this.ThemeCat2 = ko.observable(data.ThemeCat2);
    this.ThemeType = ko.observable(data.ThemeType);
    this.UnitScale = ko.observable(data.UnitScale);
    this.UnitScaleAr = ko.observable(data.UnitScaleAr);
    this.UnitScaleFr = ko.observable(data.UnitScaleFr);
    this.ThemeTypeName = ko.observable(data.ThemeTypeName);


}


function GeneralCodeVm() {
    var self = this;
    self.CodeNo = ko.observableArray();
    self.Name = ko.observable();
    self.NameAr = ko.observable();
    self.NameFr = ko.observable();
    self.ThemeCat = ko.observable();
    self.ThemeCat1 = ko.observable();
    self.ThemeCat2 = ko.observable();
    self.ThemeType = ko.observable();
    self.UnitScale = ko.observable();
    self.UnitScaleAr = ko.observable();
    self.UnitScaleFr = ko.observable();
    self.ThemeTypeName = ko.observable();
    self.Themes = ko.observableArray([]);
    self.GetAllThemes = function () {
 
        lmis.ajax("../BackEnd/DimThemes.aspx/GetAllThemese", null, 0, "show,close",
        function (data) {
            var ds = $.map(data.d, function (item) { return new Theme(item) });
            self.Themes(ds);
            var table = $('#grdTheme').dataTable();

        });



    };
    self.DeleteTheme = function (data) {


        if (confirm($("#X_ConfirmContinue").html())) {
            $(".hdfEdited").val(data.CodeNo());
            $(".hdfEditedtype").val(data.ThemeType());
            return true;
        }

        return false;
    };
    self.UpdateTheme = function (data) {


        $(".hdfEdited").val(data.CodeNo());
        $(".txthdfEditedtype").val(data.ThemeType());
        
        return true;

    };
    self.Save = function () {
        if ($(".ddlGeneralCodes").val().toString() == '') {
            $(".ddlGeneralCodes").focus();
            return;
        }
        var dto = { generalId: $(".ddlGeneralCodes").val().toString(), NameEn: $(".txtNameEn").val().toString(), NameAr: $(".txtNameAr").val().toString(), NameFr: $(".txtNameFr").val().toString(), subid: null };

        lmis.ajax("../BackEnd/SubCode.aspx/Save", dto, 0, "show,close");

    };

    self.GetAllThemes();


}
function Save() {
    if ($(".ddlGeneralCodes").val().toString() == '') {
        return;
    }
    var dto = { generalId: $(".ddlGeneralCodes").val().toString(), Name: $(".txtThemeInsert").val().toString(), NameAr: $(".txtNameAr").val().toString(), NameFr: $(".txtNameFr").val().toString(), subid: null };

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
$(document).ready(function () {

    ko.applyBindings(new GeneralCodeVm());
});



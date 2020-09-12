// <reference path="jquery-2.0.3.min.js" />
// <reference path="knockout-3.0.0.js" />



function Theme(data) {
    this.CodeNo = ko.observable(data.CodeNo);
    this.Name = ko.observable(data.Name);
    this.ThemeType = ko.observable(data.ThemeType);
    this.UnitScale = ko.observable(data.UnitScale);
    this.ThemeTypeName = ko.observable(data.ThemeTypeName);


}
function Themevariable(data) {
    this.ThemeVariableID = ko.observable(data.ThemeVariableID);
    this.ThemeID = ko.observable(data.ThemeID);
    this.VariableID = ko.observable(data.VariableID);
    this.VariableName = ko.observable(data.VariableName);

  
}

function ThemeViewModel() {
    var self = this;

    self.CodeNo = ko.observableArray();
    self.Name = ko.observable();
    self.ThemeType = ko.observable();
    self.UnitScale = ko.observable();
    self.ThemeTypeName = ko.observable();
    self.Themes = ko.observableArray([]);

    self.ThemeVariableID = ko.observable();
    self.ThemeID = ko.observable();
    self.ThemeTypeName = ko.observable();
    self.Themevariables = ko.observableArray([]);

    self.AddTheme = function () {
        self.Themes.push(new Theme({
            CodeNo: self.CodeNo(),
            Name: self.Name(),
             ThemeType: self.ThemeType(),
            UnitScale: self.UnitScale(),
            ThemeTypeName: self.ThemeTypeName()
        }));
        self.CodeNo(""),
        self.Name(""),
        self.ThemeType(""),
        self.UnitScale(""),
        self.ThemeTypeName("");
    };

    self.DeleteTheme = function (dataContext) {
        var itemToDelete = dataContext;

        alert(itemToDelete.CodeNo);
        alert(itemToDelete);
        alert(itemToDelete.Name);
        $.ajax({
            type: "POST",
            url: '../BackEnd/GenerateThemeTemplate.aspx/DeleteTheme',
            data: { codeNo: codeNo },
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                alert(result.d);
                self.Themes.remove(Theme);
            },
            error: function (err) {
                alert(err.status + " - " + err.statusText);
            }
        });

    };

    self.SaveTheme = function () {
        $.ajax({
            type: "POST",
            url: '../BackEnd/GenerateThemeTemplate.aspx/SaveTheme',
            data: ko.toJSON({ data: self.Themes }),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                alert(result.d);
            },
            error: function (err) {
                alert(err.status + " - " + err.statusText);
            }
        });
    };
    $('.ddlThemeType').on('change', function () {
        $.ajax({
            type: "Post",
            url: "../BackEnd/GenerateThemeTemplate.aspx/GetAThemesesByType",
            data: "{themeType:'" + $(".ddlThemeType").val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (results) {
                var ds = $.map(results.d, function (item) {
                    return new Theme(item);
                });
                self.Themes(ds);
            },
            error: function (err) {
                alert(err.status + " - " + err.statusText);
            }
        });

    });
    $('.ddlThemes').on('change', function () {
        $.ajax({
            type: "Post",
            url: "../BackEnd/GenerateThemeTemplate.aspx/GetThemesesVariable",
            data: "{themeID:'" + $(".ddlThemes").val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (results) {
                var ds = $.map(results.d, function (item) {
                    return new Themevariable(item);
                });

                self.Themevariables(ds);
                alert(self.Themevariables.count);
                alert("ds:"+ds.count);
            },
            error: function (err) {
                alert(err.status + " - " + err.statusText);
            }
        });

    });
 
}

$(document).ready(function () {
    ko.applyBindings(new ThemeViewModel());
});


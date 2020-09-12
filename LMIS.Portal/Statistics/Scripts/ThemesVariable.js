$('.ddlThemeType').on('change', function () {
    var dto = { themeType: $(".ddlThemeType").val().toString() };

    lmis.ajax("DimThemes.aspx/GetAThemesesByType", dto, 0, "show,close",
    function (data) {
        var ds = $.map(data.d, function (item) { return new Theme(item) });
        self.Themes(ds);

        grdTheme = $("#grdTheme").dropdown();
    });


});
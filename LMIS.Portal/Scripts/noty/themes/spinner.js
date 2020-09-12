$.noty.themes.spinnerTheme = {
    name: "spinnerTheme",
    modal: $.noty.themes.defaultTheme.modal,
    style: function () {
        this.$message.css({ "text-align": "center" });
        var spinnerTemplate = "<table id='lmis-spinner' align='center'>" +
            "<tr><td align='center' style='padding-bottom: 1em;'>" +
            "<img src='/css/images/robik.gif' style='cursor: pointer;'></td></tr>" +
            "<tr id='lmis-spinner-msg'>" +
            "<td class='ui-state-default ui-corner-all' style='cursor: pointer; text-align: center; min-width: 300px;'></td>" +
            "</tr></table>";
        this.$message.append(spinnerTemplate);
        this.$message.find(".noty_text").appendTo(this.$message.find("#lmis-spinner-msg > td"));
        if (!this.options.text) this.$message.find("#lmis-spinner-msg").remove();
    },
    callback: {
        onShow: function () {
            $.noty.themes.defaultTheme.helpers.borderFix.apply(this);
        },
        onClose: function () {
            $.noty.themes.defaultTheme.helpers.borderFix.apply(this);
        }
    }
};
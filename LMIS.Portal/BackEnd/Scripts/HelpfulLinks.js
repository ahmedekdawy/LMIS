var HelpfulLink=function (item) {

    this.HelpfulLinkID = ko.observable(item.HelpfulLinkID);
    this.HelpfulLinkName = ko.observable(item.HelpfulLinkName);
    this.HelpfulLinkURL = ko.observable(item.HelpfulLinkURL);
    this.GroupID = ko.observable(item.GroupID);
    this.GroupName = ko.observable(item.GroupName);
    this.HelpfulLinkLanguage = ko.observable(item.HelpfulLinkLanguage);
};

function ViewModel() {

    var self = this;
    self.HelpfulLinkID = ko.observable();
    self.HelpfulLinkName = ko.observable();
    self.HelpfulLinkURL = ko.observable();
    self.GroupID = ko.observable();
    self.GroupName = ko.observable();
    self.HelpfulLinkLanguage = ko.observable();
    self.CategoryOptions = ko.observableArray();
    self.HelpfulLinkList = ko.observableArray();

    lmis.api.SubCodes(self.CategoryOptions, "103", function () {
        //self.GroupID(self.AsyncItems.GroupID);
    });

    self.Save = function () {
        if ($(".ddlCategory").val().toString() == '') {
            $(".ddlCategory").focus(); return;
        }
        if ($(".HelpfulLinkName").val().toString() == '') {
            $(".HelpfulLinkName").focus(); return;
        }
        if ($(".HelpfulLinkURL").val().toString() == '') {
            $(".HelpfulLinkURL").focus(); return;
        }

      

        var HelpfulLinkVm = {
            HelpfulLinkID: $("#hdfHelpfulLinkID").val(),
            GroupID: $(".ddlCategory").val(),
            GroupName: $(".ddlCategory option:selected").text(),
            HelpfulLinkName: $(".HelpfulLinkName").val(),
            HelpfulLinkURL: $(".HelpfulLinkURL").val(),
            HelpfulLinkLanguage: $(".ddlLanguage").val()
        };
        var dto = { vm: HelpfulLinkVm };
        return lmis.ajax("../BackEnd/HelpfulLinks.aspx/Post", dto, 0, "show,close", function (data) {
            if (data.d.Data == null) {
                lmis.notification.error($("#X_Exist"));

            } else {
                lmis.notification.success();
                 
                if ($("#hdfHelpfulLinkID").val() != '0') {
                   
                    self.HelpfulLinkList.splice($("#hdfRowID").val(), 1);
                    $('#grdHelpfulLink').dataTable().fnDeleteRow($("#hdfRowID").val(), null, true);
                } 
                self.HelpfulLinkList.push(new HelpfulLink(data.d.Data));
                clearControls();
                $("#hdfHelpfulLinkID").val(0);
                $("#hdfRowID").val(0);
             
            }
        });

    }
    self.UpdateHelpfulLink = function (HelpfulLink, indx) {

        $(".HelpfulLinkName").val(HelpfulLink.HelpfulLinkName());
        $("#hdfHelpfulLinkID").val(HelpfulLink.HelpfulLinkID());
        $("#hdfRowID").val(indx);
        $(".ddlLanguage").val(HelpfulLink.HelpfulLinkLanguage());
        $(".HelpfulLinkURL").val(HelpfulLink.HelpfulLinkURL());
        $(".ddlCategory").val(HelpfulLink.GroupID());
     

        showpopup('Edit');
    }
    self.DeleteHelpfulLink = function (HelpfulLink, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {

            var dto = { HelpfulLinkID: HelpfulLink.HelpfulLinkID() };
            return lmis.ajax("../BackEnd/HelpfulLinks.aspx/Delete", dto, 0, "",
                function (data) {
                    console.log(data.d);
                    if (data.d.Status == 0) {
                        self.HelpfulLinkList.remove(HelpfulLink);
                        $('#grdHelpfulLink').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }


    }
    self.GetHelpfulLink = function () {

        return lmis.ajax("../BackEnd/HelpfulLinks.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new HelpfulLink(item) });
                self.HelpfulLinkList(ds);
                $('#grdHelpfulLink').dataTable();
            });

    }


    self.GetHelpfulLink();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});
function showpopupInsert() {
    $("#hdfHelpfulLinkID").val('0');
    clearControls();
    showpopup('Add');
}
function clearControls() {
    
    $(".HelpfulLinkName").val('');
    $(".HelpfulLinkURL").val('');
}
function showpopup(_title) {

    var d = $(".pop");
    d.dialog({
        title: _title,
        width: 600
    });
    return false;
}
function Email(item) {

    this.EmailID = ko.observable(item.EmailID);
    this.Title = ko.observable(item.Title);
    this.EmailAddress = ko.observable(item.EmailAddress);
};

function ViewModel() {

    var self = this;
    self.EmailID = ko.observable().extend({ required: true });
    self.Title = ko.observable().extend({ required: true });
    self.EmailAddress = ko.observable().extend({ required: true });
    self.EmailsList = ko.observableArray();
 
  
   
    self.Save = function () {

        if ($(".Title").val() == '') {
            $(".Title").focus(); return;
        }
        if ($(".EmailAddress").val() == '') {
            $(".EmailAddress").focus(); return;
        }
        var validateemail = validateEmail($(".EmailAddress").val());
        if (!validateemail) {
            lmis.notification.error($("#X_InvalidEmail").html());
            $(".EmailAddress").focus(); return;
        }
        var EmailsVm = {
            EmailID: $("#hdfEmailID").val(),
            Title: $(".Title").val(),
            EmailAddress: $(".EmailAddress").val()
        };
        var dto = { vm: EmailsVm };
        return lmis.ajax("../BackEnd/ListOfEmails.aspx/Post", dto, 0, "show,close", function (data) {
            if (data.d.Data == null) {
                lmis.notification.error($("#X_Exist").html());
            } else {
                lmis.notification.success();
            

            if ($("#hdfEmailID").val() != '0') {
                self.EmailsList.splice($("#hdfRowID").val(), 1);
                $('#grdEmails').dataTable().fnDeleteRow($("#hdfRowID").val(), null, true);
            }
            self.EmailsList.push(new Email(data.d.Data));
                clearControls();
            $("#hdfEmailID").val(0);
            $("#hdfRowID").val(0);
           
        }
        });
      
    }
    self.UpdateEmail = function (Email, indx) {

        $(".Title").val(Email.Title());
        $(".EmailAddress").val(Email.EmailAddress());
        $("#hdfEmailID").val(Email.EmailID());
        $("#hdfRowID").val(indx);

     


        showpopup('Edit');
    }
    self.DeleteEmail = function (Email, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {

            var dto = { EmailID: Email.EmailID() };
            return lmis.ajax("../BackEnd/ListOfEmails.aspx/Delete", dto, 0, "",
                function (data) {
                    console.log(data.d);
                    if (data.d.Status == 0) {
                        self.EmailsList.remove(Email);
                        $('#grdEmails').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }


    }
    self.GetEmails = function () {

        return lmis.ajax("../BackEnd/ListOfEmails.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new Email(item) });
                self.EmailsList(ds);
                $('#grdEmails').dataTable();
            });

    }


    self.GetEmails();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});
function showpopupInsert() {
    $("#hdfEmailID").val('0');
    $("#hdfRowID").val('0');

    clearControls();
   
    showpopup('Add');
}
function clearControls() {
    $(".Title").val('');
    $(".EmailAddress").val('');
}
function showpopup(_title) {

    var d = $(".pop");
    d.dialog({
        title: _title,
        width: 400
    });
    return false;
}
function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}
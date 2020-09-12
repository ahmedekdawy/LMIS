function ObsceneWord(item) {

    this.ObsceneWordID = ko.observable(item.ObsceneWordID);
    this.Description = ko.observable(item.Description);
};

function ViewModel() {

    var self = this;
    self.ObsceneWordID = ko.observable();
    self.Description = ko.observable().extend({ required: true });

    self.ObsceneWordsList = ko.observableArray();
 

   
    self.Save = function () {

        if ($(".Description").val().toString() == '') {
            $(".Description").focus(); return;
        }

        var ObsceneWordsVm = {
            ObsceneWordID: $("#hdfObsceneWordID").val(),
            Description: $(".Description").val()
        };
        var dto = { vm: ObsceneWordsVm };
        return lmis.ajax("../BackEnd/ObsceneWords.aspx/Post", dto, 0, "show,close", function(data) {
            if (data.d.Data == null) {
                lmis.notification.error($("#X_Exist"));
            } else {
                lmis.notification.success();
            

            if ($("#hdfObsceneWordID").val() != '0') {
                self.ObsceneWordsList.splice($("#hdfRowID").val(), 1);
                $('#grdObsceneWords').dataTable().fnDeleteRow($("#hdfRowID").val(), null, true);
            }
            self.ObsceneWordsList.push(new ObsceneWord(data.d.Data));
                clearControls();
            $("#hdfObsceneWordID").val(0);
            $("#hdfRowID").val(0);
           
        }
        });
      
    }
    self.UpdateObsceneWord = function (ObsceneWord, indx) {

        $(".Description").val(ObsceneWord.Description());
        $("#hdfObsceneWordID").val(ObsceneWord.ObsceneWordID());
        $("#hdfRowID").val(indx);

     


        showpopup('Edit');
    }
    self.DeleteObsceneWord = function (ObsceneWord, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {

            var dto = { ObsceneWordID: ObsceneWord.ObsceneWordID() };
            return lmis.ajax("../BackEnd/ObsceneWords.aspx/Delete", dto, 0, "",
                function (data) {
                    console.log(data.d);
                    if (data.d.Status == 0) {
                        self.ObsceneWordsList.remove(ObsceneWord);
                        $('#grdObsceneWords').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }


    }
    self.GetObsceneWords = function () {

        return lmis.ajax("../BackEnd/ObsceneWords.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new ObsceneWord(item) });
                self.ObsceneWordsList(ds);
                $('#grdObsceneWords').dataTable();
            });

    }


    self.GetObsceneWords();


};

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});
function showpopupInsert() {
    $("#hdfObsceneWordID").val('0');
    clearControls();
    showpopup('Add');
}
function clearControls() {
    $(".Description").val('');
}
function showpopup(_title) {

    var d = $(".pop");
    d.dialog({
        title: _title,
        width: 400
    });
    return false;
}
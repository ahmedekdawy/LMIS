var Faq=function (item) {

    this.FAQID = ko.observable(item.FAQID);
    this.Question = ko.observable(item.Question);
    this.Answer = ko.observable(item.Answer);
    this.FAQCategoryID = ko.observable(item.FAQCategoryID);
    this.GroupName = ko.observable(item.GroupName);
    this.FAQLanguage = ko.observable(item.FAQLanguage);
   
};

function ViewModel() {

    var self = this;
    self.FAQID = ko.observable();
    self.Question = ko.observable();
    self.Answer = ko.observable();
    self.FAQCategoryID = ko.observable();
    self.FAQLanguage = ko.observable();
    self.GroupName = ko.observable();
     self.CategoryOptions = ko.observableArray();
    self.FaqsList = ko.observableArray();

    lmis.api.SubCodes(self.CategoryOptions, "105", function () {
        //self.GroupID(self.AsyncItems.GroupID);
    });

    self.Save = function () {
        if ($(".ddlLanguage").val().toString() == '') {
            $(".ddlLanguage").focus(); return;
        }
        if ($(".ddlCategory").val().toString() == '') {
            $(".ddlCategory").focus(); return;
        }
        if ( $(".Question").val().toString() == '') {
            $(".Question").focus(); return;
        }
        if ( $(".Answer").val().toString() == '' ) {
            $(".Answer").focus(); return;
        }
        
        var FaqVm = {
            FAQID: $("#hdfId").val(),
            FAQCategoryID: $(".ddlCategory").val(),
            GroupName: $(".ddlCategory option:selected").text(),
            FAQLanguage:$(".ddlLanguage").val(),
            Question: $(".Question").val(),
            Answer: $(".Answer").val()
        };
   
        var dto = { vm: FaqVm };
        return lmis.ajax("../BackEnd/FaqBack.aspx/Post", dto, 0, "show,close", function (data) {
            if (data.d.Data == null) {
                lmis.notification.error($("#X_Exist").text());

            } else {
                lmis.notification.success();
                
                
                if ($("#hdfId").val() != '0') {
                   
                    var index = parseInt($("#hdfRowID").val());
                
                    self.FaqsList()[index].Question(data.d.Data.Question);
                    self.FaqsList()[index].Answer(data.d.Data.Answer);
                    self.FaqsList()[index].GroupName(data.d.Data.GroupName);
                    self.FaqsList()[index].FAQLanguage(data.d.Data.FAQLanguage);
                    self.FaqsList()[index].FAQCategoryID(data.d.Data.FAQCategoryID);

                } else {
                    self.FaqsList.push(new Faq(data.d.Data));
                }
              
                clearControls();
                $("#hdfId").val(0);
                $("#hdfRowID").val(0);
             
            }
        });

    }
    self.UpdateFaq = function (Faq, indx) {

        $(".Question").val(Faq.Question());
        $(".Answer").val(Faq.Answer());
        $("#hdfId").val(Faq.FAQID());
        $("#hdfRowID").val(indx);
        $(".ddlCategory").val(Faq.FAQCategoryID());
        $(".ddlLanguage").val(Faq.FAQLanguage());
        

        showpopup('Edit');
    }
    self.DeleteFaq = function (Faq, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {

            

            var FaqVm = {
                Id: Faq.FAQID()
            };
            return lmis.ajax("../BackEnd/FaqBack.aspx/Delete", FaqVm, 0, "",
                function (data) {
                    console.log(data.d);
                    if (data.d.Status == 0) {
                        self.FaqsList.splice(indx,1);
                        $('#grdFaq').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }

        
    }
    self.GetFaq = function () {

        return lmis.ajax("../BackEnd/FaqBack.aspx/Get", null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new Faq(item) });
                self.FaqsList(ds);
                $('#grdFaq').dataTable();
            });

    }


    self.GetFaq();


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
    $(".ddlLanguage").val('');
    $(".Question").val('');
    $(".FaqAr").val('');
    $(".Answer").val('');
}
function showpopup(_title) {

    var d = $(".pop");
    d.dialog({
        title: _title,
        width: 600
    });
    return false;
}
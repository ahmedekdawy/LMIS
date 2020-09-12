
function Theme(data) {
    this.Comment = ko.observable(data.Comments);
    this.Name = ko.observable(data.Name);


}
function Email(data) {
    this.Title = ko.observable(data.Title);
    this.EmailAddress = ko.observable(data.EmailAddress);


}
function Office(data) {
    this.Value = ko.observable(data.Value);
 
}

function ViewModel() {

   
    var self = this;

    $(".siteRating").rating({
        min: 0, max: 5, step: 0.1, size:"xs", hoverOnClear: false,
        starCaptionClasses: function (val) {
            if (val < 3) return "label label-danger";
            else return "label label-success";
        }
    });

    $("input[name='feddback'][value='feeddBack']").prop("checked", true).trigger("change");

    //Initialize VM
    self.Comments = ko.observable();
  
    self.Testimonials = ko.observable();
    self.FeedBackType = ko.observable();
    self.Title = ko.observable();
    self.EmailAddress = ko.observable();
    self.Value = ko.observable();
    self.FeedBackOptions = ko.observableArray();
    self.TestimonialList = ko.observableArray();
    self.Offices = ko.observableArray();
    self.Emails = ko.observableArray();
    //Initialize UI
    
    

    //UI Operations
    self.DisableUserInput = function (bDisable) {

        if (typeof bDisable === "undefined")
            bDisable = self.EditingBlocked;
        else
            self.EditingBlocked = bDisable;

        $("#tab-content :text").css("background-color", "white");
        $("#tab-content .always-white").css("background-color", "white");
        $("#tab-content :input").attr("disabled", bDisable);
        $("#tab-content .bsmulti").each(function () {
            lmis.multiselect.DelayedAction($(this), bDisable ? "disable" : "enable");
        });
        $("#tab-content .always-disabled").attr("disabled", true);
        $("#tab-content .always-enabled").attr("disabled", false);

    }
    self.GetTestimonials = function () {

        return lmis.ajax("../FrontEnd/ContactUs.aspx/GetTestimonials", null, 0, "",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Theme(item) });
                self.TestimonialList(ds);
                 
              
            });

    }
    self.GetOffices = function () {

        return lmis.ajax("../FrontEnd/ContactUs.aspx/GetOffices", null, 0, "",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Office(item) });
                self.Offices(ds);


            });

    }
    self.GetEmails = function () {

        return lmis.ajax("../FrontEnd/ContactUs.aspx/GetEmails", null, 0, "",
            function (data) {
                var ds = $.map(data.d.Data, function (item) { return new Email(item) });
                self.Emails(ds);


            });

    }
    self.GetMap = function () {

        return lmis.ajax("../FrontEnd/ContactUs.aspx/GetMap", null, 0, "",
            function (data) {

                //var url = 'https://maps.google.com/maps?z=12&t=m&hl=' + lmis.uiCulture + '&q=loc:' + data.d[0].Value + '&output=embed';
                var url = data.d[0].Value + '&hl=' + (lmis.uiCulture.indexOf('ar') !== -1 ? 'ar-eg' : 'en-us');
                $('#gmapIframe').attr('src', url);

            });

    }
    self.GetTestimonials();
    self.GetOffices();
    self.GetEmails();
    self.GetMap();
    lmis.api.SubCodes(self.FeedBackOptions, "101", function () {
        //self.FeedBack(self.AsyncItems.FeedBack);
    });
    self.Save = function () {

        if ($(".Description").val() == '') {
            $(".Description").focus();
            return;
        }


        if ($('input[name="feddback"]:checked', '.isTestinomial').val() == 'feeddBack') {
            if ($(".ddlFeedBack").val() == '') {
                $(".ddlFeedBack").focus();
                return;
            }
            if ($(".Title").val() == '') {
                $(".Title").focus();
                return;
            }
            var FeedbackVM = {
                FeedbackTypeId: $(".ddlFeedBack").val().toString(),
                Title: $(".txtsubject").val().toString(),
                Description: $(".Description").val().toString(),
                subid: null
            };
            var dto = { item: FeedbackVM };
            lmis.ajax("../FrontEnd/ContactUs.aspx/InsertFeedback", dto, 0, "show,close");
           
        }
        else {

            var TestimonialsVM = {
                SiteRating: Math.round($(".txtsiteRating").val().toString()),
                Comments: $(".Description").val().toString(),
                subid: null
            };
            var dto = { item: TestimonialsVM };
            lmis.ajax("../FrontEnd/ContactUs.aspx/InsertTestimonials", dto, 0, "show,close");
            
        }
      
       

    };

}

$(document).ready(function() {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
});
function isFeedBack(e) {
    
    if (e.value == 'feeddBack') {
        $(".lblFeedBack").show();
        $(".ddlFeedBack").show();
        $("#divRating").hide();
        $(".subject").show();

      
    } else {
    

        $(".lblFeedBack").hide();
        $(".ddlFeedBack").hide();
        $("#divRating").show();
        $(".subject").hide();

    }

}
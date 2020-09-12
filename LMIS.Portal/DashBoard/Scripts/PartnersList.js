function Partners(item) {

    this.PartnerID = ko.observable(item.PartnerID);
    this.PortalUserID = ko.observable(item.PortalUserID);
    this.CEOEmail = ko.observable(item.CEOEmail);
    this.Is_Approved = ko.observable(item.Is_Approved);
    this.YearFounded = ko.observable(item.YearFounded);
    this.RejectReason = ko.observable(item.RejectReason);
    this.OrganizationContactID = ko.observable(item.OrganizationContactID);
    this.LanguageID = ko.observable(item.LanguageID);
    this.CEOFirstName = ko.observable(item.CEOFirstName);
    this.CEOLastName = ko.observable(item.CEOLastName);
    this.GeneralDescriptionCoreBusiness = ko.observable(item.GeneralDescriptionCoreBusiness);
    this.PossibleAreaOfCooperation = ko.observable(item.PossibleAreaOfCooperation);
    this.ZipPostalCode = ko.observable(item.ZipPostalCode);
    this.Telephone = ko.observable(item.Telephone);
    this.OrganizationWebsite = ko.observable(item.OrganizationWebsite);
    this.OrganizationName = ko.observable(item.OrganizationName);
    this.Address = ko.observable(item.Address);
    this.OrganizationLogoPath = ko.observable(config.employer.logoPath + item.OrganizationLogoPath);
    this.GeneralDescriptionCoreBusiness = ko.observable(item.GeneralDescriptionCoreBusiness);
    this.PossibleAreaOfCooperation = ko.observable(item.PossibleAreaOfCooperation);
    this.PartnerDetails = ko.observable('../Partners/Details?PartnerID=' + item.PartnerID);
};

function ViewModel() {

    var self = this;

    self.PartnerID = ko.observable();
    self.PortalUserID = ko.observable();
    self.CEOEmail = ko.observable();
    self.Is_Approved = ko.observable();
    self.YearFounded = ko.observable();
    self.RejectReason = ko.observable();
    self.OrganizationContactID = ko.observable();
    self.LanguageID = ko.observable();
    self.CEOFirstName = ko.observable();
    self.CEOLastName = ko.observable();
    self.GeneralDescriptionCoreBusiness = ko.observable();
    self.PossibleAreaOfCooperation = ko.observable();
    self.ZipPostalCode = ko.observable();
    self.Telephone = ko.observable();
    self.OrganizationWebsite = ko.observable();
    self.OrganizationName = ko.observable();
    self.Address = ko.observable();
    self.OrganizationLogoPath = ko.observable();
    self.GeneralDescriptionCoreBusiness = ko.observable();
    self.PossibleAreaOfCooperation = ko.observable();
    self.PartnerDetails = ko.observable();
    self.PartnersList = ko.observableArray();
    self.Insert = function () {
        if ($(".CEOEmail").val() == ""
            || $(".YearFounded").val() == ""
            || $(".CEOFirstName").val() == ""
            || $(".CEOLastName").val() == ""
            || $(".GeneralDescriptionCoreBusiness").val() == ""
           ) {
           // lmis.notification.error($("#RequiredFields").val());
            lmis.notification.error($("#X_RequiredFieldErrors").html());
       
        } else {
         
            var partnerVm = {
                CEOEmail: $(".CEOEmail").val().toString(),
                YearFounded: $(".YearFounded").val().toString(),
                CEOFirstName: $(".CEOFirstName").val().toString(),
                CEOLastName: $(".CEOLastName").val().toString(),
                GeneralDescriptionCoreBusiness: $(".GeneralDescriptionCoreBusiness").val().toString(),
                PossibleAreaOfCooperation: $(".PossibleAreaOfCooperation").val().toString()
            };
            var dto = { partner: partnerVm };
            return lmis.ajax("BecomePartner.aspx/Insert", dto, 0, "");
        }
    }

    self.GetNews = function () {

        return lmis.ajax("../DashBoard/Partners.aspx/GetPartners" + (lmis.queryString.partnerid == null ? '' : "?PartnerID=" + lmis.queryString.partnerid), null, 0, "",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Partners(item) });
                self.PartnersList(ds);
               
            });

    }

    self.GetNewsDetails = function () {

        return lmis.ajax("../DashBoard/PartnerDetails.aspx/GetPartners?PartnerID=" + lmis.queryString.partnerid, { PartnerID: lmis.queryString.partnerid }, 0, "",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Partners(item) });
                self.PartnersList(ds);

            });

    }
    if (lmis.queryString.partnerid > 0) {
        self.GetNewsDetails();
    } else {
        self.GetNews();
    }

};

$(document).ready(function() {
    ko.applyBindings(new ViewModel());
});
function GetPartnerMessage() {

    return lmis.ajax("../DashBoard/Partners.aspx/GetPartnerMessage", null, 0, "",
        function (data) {
    

            $(".becomePartner").html(data.d.filter(function (x) { return x.Key == "BecomePartner" })[0].Value);

        });

}

GetPartnerMessage();
$(document).ready(function () {
    $(".YearFounded").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});
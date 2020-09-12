function RegWizard() {

    var self = this;

    self.key = parseInt(lmis.queryString["k"]);
    self.key = isNaN(self.key) ? null : self.key;
    self.profileType = document.cookie.replace(/(?:(?:^|.*;\s*)ProfileType\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    self.viewOnly = document.cookie.replace(/(?:(?:^|.*;\s*)ViewOnly\s*\=\s*([^;]*).*$)|^.*$/, "$1") !== "";

    //Admin Review Support
    self.vmReview = window.vmReview;
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();
    self.initReview = function() {
        if (window.vmReview && self.key) window.vmReview.init("profile", self.key);
    }

    var profileUrl;

    if (self.profileType === "ind") {
        profileUrl = "/registration/widgets/ind/indprofile.js";
        self.approvalWebmethod = "IndApprove";
    }
    else if (self.profileType === "org") {
        profileUrl = "/registration/widgets/org/orgprofile.js";
        self.approvalWebmethod = "OrgApprove";
    }

    if (profileUrl)
        ko.components.register("profile", { require: profileUrl });
    else {
        ko.components.register("profile", { template: "Error" });
        window.location = "/frontend/home";
    }

}

$(document).ready(function () {
    window.vm = new RegWizard();
    ko.applyBindings(vm);
})
define(function (require) {

    var markup = require("text!./orgprofile.aspx");
    var cardList = require("/scripts/extensions/cardList.js");
    var orgDetails = require("/registration/widgets/org/orgdetails.js");
    var orgContactInfo = require("/registration/widgets/org/orgcontactinfo.js");
    var orgServices = require("/registration/widgets/org/orgservices.js");

    function orgProfile(args) {

        var self = this;
        cardList.extend(self, args.root);

        var res = lmis.resFromHtml(markup, ["tabOrgDets", "tabContactInfo", "tabServices"]);

        self.viewOnly = args.viewOnly;
        self.isDirty.subscribe(lmis.notification.browser.confirmUnsavedChanges);

        var vmOrgDets = self.addCard(res["tabOrgDets"], orgDetails, { mode: "v", editable: !self.viewOnly }).viewModel;
        self.addCard(res["tabContactInfo"], orgContactInfo, { mode: "v", editable: !self.viewOnly, dtoAsProperty: true });
        self.addCard(res["tabServices"], orgServices, { mode: "v", editable: !self.viewOnly });

        self.root.reviewEditors = {
            industry: { value: vmOrgDets.Industry, options: vmOrgDets.IndustryOptions }
        };

        self.LoadProfile = function() {
            lmis.ajax("Profile.aspx/OrgLoad", { portalUserId: args.key }, 0, "show,close", function (data) {
                self.dataSet(data.d);
                self.root.Approval(data.d.Approval);
                self.root.RejectReason(data.d.RejectReason);
                self.root.initReview();
                $('#txtName_A').removeClass("validationElement");
            });
        };
        self.UpdateProfile = function() {
            
            var dto = self.dto();

            if (!dto) {
                return false;
            }

            function onSuccess() {
                lmis.ajaxSuccessHandler();
                self.dataSet(dto);
                self.root.Approval(1);
            }

            function onError(xhr) {
                lmis.ajaxErrorHandler(xhr);
            }

            function update() {
                dto = self.dto(); //to update dto with server file names
                lmis.ajax("Profile.aspx/OrgUpdate", { data: dto }, 0, "show,close", onSuccess, onError);
            }

            function uploadProfile() {
                vmOrgDets.UploadProfile(update, onError, "show");
            }

            vmOrgDets.UploadLogo(uploadProfile, onError, "show");

            return true;

        };

        self.LoadProfile();
        setTimeout(self.postDOM, 1);

    };

    return { template: markup, viewModel: orgProfile };

});
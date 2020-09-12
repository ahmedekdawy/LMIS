function OrgContact(cardList, credentials, orgContact) {

    var self = this;
    cardList.extend(self);

    self.viewOnly = false; //this could be set by server if viewOnly access is allowed for non-orgAdmins
    self.key = parseInt(lmis.queryString["k"]);
    self.key = isNaN(self.key) ? null : self.key;
    self.mode = ko.observable(self.key === null ? "p" : lmis.queryString["m"]);
    if (self.mode()) self.mode(self.mode().toLowerCase());
    if (["p", "v", "e"].indexOf(self.mode()) === -1) self.mode("v");

    var res = lmis.resFromHtml($("#tab").html(), ["tabCredentials", "tabOrgContact"]);
    if (self.mode() === "p" && !self.viewOnly) self.addCard(res["tabCredentials"], credentials, { requireAuthLetter: false });
    self.addCard(res["tabOrgContact"], orgContact, { mode: self.mode(), editable: self.mode() !== "v" && !self.viewOnly });

    self.Get = function() {

        if (self.key === null) return;

        lmis.ajax("../Registration/Contact.aspx/Get", { contactId: self.key }, 0, "show,close", function (data) {
            self.dataSet(data.d);
            self.cards().forEach(function (c) { c.mode(self.mode()) });
        });

    };

    self.Update = function () {

        var dto = self.dto();

        if (!dto) {
            return false;
        }

        if (self.mode() === "e") dto = $.extend({}, self.dataSet(), dto);

        function onSuccess() {
            self.cards().forEach(function (c) { c.mode("v") });
            self.mode("v");
            lmis.ajaxSuccessHandler();
            self.dataSet(dto);
        }

        function onError(xhr) {
            lmis.ajaxErrorHandler(xhr);
        }

        lmis.ajax("../Registration/Contact.aspx/Update", { data: dto, password: dto.Password || null }, 0, "show,close", onSuccess, onError);

        return true;

    };

    setTimeout(self.postDOM, 1);

    if (self.mode() !== "p") self.Get();

}

require(["/scripts/extensions/cardlist.js", "/registration/widgets/credentials.js", "/registration/widgets/org/orgcontact.js"], function (cardList, credentials, orgContact) {
    $(document).ready(function() {
        window.vm = new OrgContact(cardList, credentials, orgContact);
        ko.applyBindings(vm);
    });
});

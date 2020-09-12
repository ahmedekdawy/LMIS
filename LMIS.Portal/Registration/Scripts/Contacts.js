function Contact(item) {

    this.RowId = ko.computed(function () { return "r" + item.ContactId; });
    this.ContactId = ko.observable(item.ContactId);
    this.UserName = ko.observable(item.UserName);
    this.FullName = ko.observable(lmis.globalString.toLocal(item.FullName, true));
    this.Department = ko.observable(lmis.globalString.toLocal(item.Department, true));

};

function OrgContactList() {

    var self = this;
    var grdContacts;
    var actionInProgress = false;

    //Initialize VM
    self.ContactsList = ko.observableArray([]);

    //VM Operations
    self.List = function () {

        lmis.ajax("../Registration/Contacts.aspx/List", null, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Contact(item) });
                self.ContactsList(ds);
                grdContacts = $("#grdContacts").DataTable();
            });

    }
    self.View = function (item) {
        window.location.assign("Contact?m=v&k=" + item.ContactId() + "#anchor");
    }
    self.Edit = function (item) {
        window.location.assign("Contact?m=e&k=" + item.ContactId() + "#anchor");
    }
    self.Delete = function (item) {

        function onConfirm() {

            var dto = { contactId: item.ContactId(), reason: "" };

            lmis.ajax("../Registration/Contacts.aspx/Delete", dto, 0, "show,close",
                function () {
                    grdContacts.row("#" + item.RowId()).remove().draw(false);
                    self.ContactsList.remove(item);
                }).always(function () {
                    actionInProgress = false;
                });

        }

        function onCancel() {
            actionInProgress = false;
        }

        if (!actionInProgress) {
            actionInProgress = true;
            lmis.notification.confirm(onConfirm, onCancel);
        }

    }

    //Initialize UI
    self.List();
}

$(document).ready(function() {
    window.vm = new OrgContactList();
    ko.applyBindings(vm);
});
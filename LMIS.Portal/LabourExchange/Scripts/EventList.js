function Event(item) {

    this.RowId = ko.computed(function () { return "r" + item.EventId; });
    this.EventId = ko.observable(item.EventId);
    this.Title = ko.observable(lmis.globalString.toLocal(item.Title, true));

};

function ViewModel() {

    var self = this;
    var grdEvents;
    var actionInProgress = false;

    //Initialize VM
    self.EventList = ko.observableArray([]);

    //VM Operations
    self.List = function () {

        lmis.ajax("../LabourExchange/EventList.aspx/List", null, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Event(item) });
                self.EventList(ds);
                grdEvents = $("#grdEvents").DataTable();
            });

    }
    self.View = function (item) {
        window.location.assign("EventPost?m=v&k=" + item.EventId() + "#anchor");
    }
    self.Edit = function (item) {
        window.location.assign("EventPost?m=e&k=" + item.EventId() + "#anchor");
    }
    self.Delete = function (item) {

        function onConfirm() {

            var dto = { id: item.EventId(), reason: "" };

            lmis.ajax("../LabourExchange/EventList.aspx/Delete", dto, 0, "show,close",
                function () {
                    grdEvents.row("#" + item.RowId()).remove().draw(false);
                    self.EventList.remove(item);
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

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
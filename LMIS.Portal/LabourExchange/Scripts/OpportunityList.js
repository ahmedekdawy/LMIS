function Opportunity(item) {

    this.RowId = ko.computed(function() { return "r" + item.OpportunityId; });
    this.OpportunityId = ko.observable(item.OpportunityId);
    this.Title = ko.observable(lmis.globalString.toLocal(item.Title, true));

};

function ViewModel() {

    var self = this;
    var grdOpportunities;
    var actionInProgress = false;

    //Initialize VM
    self.OpportunityList = ko.observableArray([]);

    //VM Operations
    self.GetOpportunityList = function() {

        lmis.ajax("../LabourExchange/OpportunityList.aspx/GetOpportunityList", null, 0, "show,close",
            function(data) {
                var ds = $.map(data.d, function(item) { return new Opportunity(item) });
                self.OpportunityList(ds);
                grdOpportunities = $("#grdOpportunities").DataTable();
            });

    }
    self.ViewOpportunity = function(item) {
        window.location.assign("OpportunityPost?m=v&k=" + item.OpportunityId() + "#anchor");
    }
    self.EditOpportunity = function(item) {
        window.location.assign("OpportunityPost?m=e&k=" + item.OpportunityId() + "#anchor");
    }
    self.DeleteOpportunity = function(item) {

        function onConfirm() {

            var dto = { id: item.OpportunityId(), reason: "" };

            lmis.ajax("../LabourExchange/OpportunityList.aspx/DeleteOpportunityById", dto, 0, "show,close",
                function() {
                    grdOpportunities.row("#" + item.RowId()).remove().draw(false);
                    self.OpportunityList.remove(item);
                }).always(function() {
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
    self.GetOpportunityList();

}

$(document).ready(function() {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
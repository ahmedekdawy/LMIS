function Opportunity(item) {

    this.OpportunityId = ko.observable(item.OpportunityId);
    this.ContactId = ko.observable(item.ContactId);
    this.ContactName = ko.observable(item.ContactName.English);
    this.Organization = ko.observable(item.OrganizationName.English);
    this.Title = ko.observable(item.Title.English);
    this.FilePath = ko.observable(item.FilePath);
    this.StartDate = ko.observable(item.StartDate);
    this.EndDate = ko.observable(item.EndDate);
    this.IsInformal = ko.observable(item.IsInformal);
    this.Approval = ko.observable(item.Approval);
    this.IsApproved = ko.computed(function() {
        return (item.Approval === 2 ? "Yes" : "No");
    });

};

function ViewModel() {

    var self = this;
    var pMsg = "Session ID: ";

    //ViewModel Data
    self.p = pMsg;
    self.MomentDate = $.trim($("#X_MomentDate").html());
    self.SessionId = ko.observable();
    self.OpportunityList = ko.observableArray([]);
    self.OpportunityCount = ko.computed(function() {
        return self.OpportunityList().length;
    });

    //ViewModel Operations
    self.GetSessionId = function(d) {
        $.ajax({
            type: "POST", timeout: 5000,
            url: "../LabourExchange/OpportunityTest.aspx/GetSessionId",
            data: ko.toJSON({ clientMessage: d }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                self.SessionId(data.d);
            },
            error: lmis.ajaxErrorHandler
        });
    };
    self.GetOpportunityList = function() {
        $.ajax({
            type: "POST", timeout: 15000,
            url: "../LabourExchange/OpportunityTest.aspx/GetOpportunityList",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                $("#Result").text("AJAX Call for 'GetOpportunityList()' Succeeded.");
                var ds = $.map(data.d, function(item) { return new Opportunity(item) });
                self.OpportunityList(ds);
            },
            error: function(xhr) {
                $("#Result").text("AJAX Call for 'GetOpportunityList()' Failed.");
                lmis.ajaxErrorHandler(xhr);
            }
        });
    };
    self.InspectItem = function(item) {
        self.SessionId(item.Title());
    };

    //Initialization
    self.GetOpportunityList();

};

$(document).ready(function() {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
});
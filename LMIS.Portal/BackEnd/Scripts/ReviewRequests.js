function VmItem(item, res) {

    this.RowId = "r" + item.Id;
    this.Id = item.Id;
    this.AdminId = item.Admin.id;
    this.AdminName = item.Admin.desc;
    this.PortalUserId = item.PortalUserId;
    this.PortalUserName = item.PortalUserName;
    this.RequestType = item.RequestType.id;
    this.RequestTypeDesc = item.RequestType.desc;
    this.RequestId = item.RequestId;
    this.Title = lmis.string.highlight(item.Title, res.ObsceneWords);
    this.PostDate = item.PostDate;
    this.UpdateDate = item.UpdateDate;
    this.DeleteDate = item.DeleteDate;
    this.Date = item.DeleteDate || item.UpdateDate || item.PostDate;
    this.Approval = item.Approval;

    if (item.DeleteDate)
        this.TransactionType = res.Deleted;
    else if (item.UpdateDate)
        this.TransactionType = res.Updated;
    else
        this.TransactionType = res.New;
};

function ViewModel() {

    var self = this;
    var query = (typeof lmis.queryString["q"] === "undefined") ? "" : decodeURI(lmis.queryString["q"]);
    var grd;

    //Initialize VM
    self.VmList = ko.observableArray([]);
    self.IsSuperAdmin = ko.observable(false);
    self.AsSuperAdmin = ko.observable(false);
    self.AdminOptions = ko.observableArray([]);
    self.ReqType = ko.observable();
    self.ReqTypeOptions = ko.observableArray([]);
    self.TrType = ko.observable();
    self.TrTypeOptions = ko.observableArray([]);
    self.ObsceneWords = ko.observableArray([]);

    //VM Operations
    self.ListRequests = function (forAllAdmins) {

        lmis.ajax("../BackEnd/ReviewRequests.aspx/ListRequests", { langCode: lmis.uiCulture, forAllAdmins: forAllAdmins }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d.data, function (item) { return new VmItem(item, data.d.res) });
                self.VmList(ds);
                self.IsSuperAdmin(data.d.res.IsSuperAdmin);
                grd = $("#grd").DataTable();
                self.UpdateFilterOptions();
            });

    }
    self.UpdateFilterOptions = function () {

        var ds1 = $.map(self.VmList(), function (item) { return { id: item.RequestTypeDesc, desc: item.RequestTypeDesc } });
        self.ReqTypeOptions(lmis.arr.unique(ds1, "id"));

        var ds2 = $.map(self.VmList(), function (item) { return { id: item.TransactionType, desc: item.TransactionType } });
        self.TrTypeOptions(lmis.arr.unique(ds2, "id"));

    }
    self.ListAdmins = function() {

        lmis.ajax("../BackEnd/ReviewRequests.aspx/ListAdmins", { availableOnly: true, excludeSuper: false }, 0, "",
            function (data) {
                self.AdminOptions(data.d);
            });

    }
    self.ToggleAllRequests = function () {
        if (self.AsSuperAdmin())
            window.location.replace(encodeURI("ReviewRequests#anchor"));
        else
            window.location.replace(encodeURI("ReviewRequests?q=all#anchor"));
    }
    self.Review = function (item) {
        if (item.RequestType === "02900001")
            window.location.assign(encodeURI("../Registration/Profile.aspx?m=r&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900002")
            window.location.assign(encodeURI("../Individual/Profile.aspx?m=r&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900003")
            window.location.assign(encodeURI("../BackEnd/Review.aspx?q=partner&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900004")
            window.location.assign(encodeURI("../LabourExchange/OpportunityPost.aspx?m=r&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900005")
            window.location.assign(encodeURI("../LabourExchange/EventPost.aspx?m=r&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900006")
            window.location.assign(encodeURI("../LabourExchange/JobPost.aspx?m=r&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900007")
            window.location.assign(encodeURI("../LabourExchange/TrainingPost.aspx?m=r&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900008")
            window.location.assign(encodeURI("../BackEnd/Review.aspx?q=feedback&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
        if (item.RequestType === "02900009")
            window.location.assign(encodeURI("../BackEnd/Review.aspx?q=testimonial&k=" + item.RequestId + "&log=" + item.Id + "#anchor"));
    }
    self.Reassign = function() {

        var item = self.dlgReassign.dataItem;
        var newAdminId = self.dlgReassign.userInput();

        if (newAdminId === item.AdminId) return;

        lmis.ajax("../BackEnd/ReviewRequests.aspx/Reassign", { id: item.Id, newAdminId: newAdminId }, 0, "show,close",
            function () {
                var admins = self.AdminOptions();
                var idxAdmins = admins.map(function(a) { return a.id; });
                var newAdminName = admins[idxAdmins.indexOf(newAdminId)].desc;

                $(grd.cell("#" + item.RowId, 6).node()).find("a").text(newAdminName);
                item.AdminId = newAdminId;
                item.AdminName = newAdminName;
            });

    }
    self.InitDialogs = function () {
        self.dlgReassign = lmis.dialog($("#dlgReassign"), 230, 350, null, self.Reassign);
    }

    //Initialize UI
    if (query === "all") {
        self.AsSuperAdmin(true);
        self.ListRequests(true);
        self.ListAdmins();
    } else {
        self.ListRequests(false);
    }
    self.InitDialogs();
    self.ReqType.subscribe(function (newVal) {
        if (grd) grd.column(1).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });
    self.TrType.subscribe(function (newVal) {
        if (grd) grd.column(3).search(lmis.string.isNullOrWhiteSpace(newVal) ? "" : newVal).draw(false);
    });

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
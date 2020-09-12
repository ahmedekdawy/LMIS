function VmItem(item) {

    this.RowId = "r" + item.JobId;
    this.Id = item.JobId;
    this.Title = lmis.string.isNullOrWhiteSpace(item.Title)
        ? lmis.globalString.toLocal(item.NewTitle, true) : item.Title;
    this.PostDate = item.PostDate;
    this.Approval = item.Approval;

};

function ViewModel() {

    var self = this;
    var actionInProgress = false;
    var grd;

    //Initialize VM
    self.VmList = ko.observableArray([]);
    self.DetailsFor = ko.observable();
    self.Applicants = ko.observableArray([]);

    //VM Operations
    self.List = function () {

        lmis.ajax("../LabourExchange/JobList.aspx/List", { langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new VmItem(item) });
                self.VmList(ds);
                grd = $("#grd").DataTable();
            });

    }
    self.View = function (item) {
        window.location.assign("JobPost?m=v&k=" + item.Id + "#anchor");
    }
    self.Edit = function (item) {
        window.location.assign("JobPost?m=e&k=" + item.Id + "#anchor");
    }
    self.Delete = function (item) {

        function onConfirm() {

            var dto = { id: item.Id, reason: "" };

            lmis.ajax("../LabourExchange/JobList.aspx/Delete", dto, 0, "show,close",
                function () {
                    grd.row("#" + item.RowId).remove().draw(false);
                    self.VmList.remove(item);
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
    self.DetailApplicants = function(item) {
        
        lmis.ajax("../LabourExchange/JobList.aspx/DetailApplicants", { id: item.Id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data && data.d)
                    data.d.forEach(function (a) {
                        a.status = ko.observable(a.status);
                        a.canAccept = ko.computed(function () { return a.status() !== 3; });
                        a.canReject = ko.computed(function () { return a.status() !== 4; });
                        a.canHire = ko.computed(function () { return a.status() === 3; });
                        a.statusDesc = ko.computed(function() {
                            switch (a.status()) {
                                case 2:
                                    return $("#F004_Viewed").text();
                                case 3:
                                    return $("#X_Accepted").text();
                                case 4:
                                    return $("#X_Rejected").text();
                                case 5:
                                    return $("#F004_Hired").text();
                                default:
                                    return $("#F004_New").text();
                            }
                        });
                    });
                self.Applicants(data.d);
                self.DetailsFor(item.Title);
                location.hash = "#applicants";
            });

    }
    self.RecommendCVs = function(item) {
        window.open("CVSearch?q=" + item.Id + "#anchor", "_blank");
    }
    self.ApplicantDetails = function (app) {
        window.open("/Individual/Profile?m=v&k=" + app.userId + "#anchor", "_blank");
        if (app.status() < 2 || app.status() > 5) self.ChangeApplicantStatus(app, 2)();
    }
    self.ChangeApplicantStatus = function (app, status) {
        return function () {
            function onConfirm() {
                lmis.ajax("../LabourExchange/JobList.aspx/ChangeApplicantStatus", { id: app.id, status: status }, 0, "show,close",
                    function () {
                        app.status(status);
                    });
            }
            if ((app.status() === 3 || app.status() === 4 || app.status() === 5) && status !== 5)
                lmis.notification.confirm(onConfirm);
            else onConfirm();
        }
    }

    //Initialize UI
    self.List();

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
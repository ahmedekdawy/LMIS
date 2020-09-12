function ViewModel() {

    var self = this;
    var key = lmis.queryString["k"];
    var actionInProgress = false;

    //Initialize VM
    self.CanApply = ko.observable(false);
    self.VmList = ko.observable({ Extras: {} });

    //VM Operations
    self.LoadRecord = function () {

        if (lmis.string.isNullOrWhiteSpace(key)) return;

        lmis.ajax("../LabourExchange/TrainingDetails.aspx/View", { id: key, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    self.VmList(data.d);
                    self.CanApply(data.d.Extras.CanApply);
                }
            });

    }
    self.Apply = function () {

        if (!actionInProgress && !lmis.string.isNullOrWhiteSpace(key)) {

            actionInProgress = true;

            lmis.ajax("../LabourExchange/TrainingDetails.aspx/Apply", { id: key }, 0, "show,close",
                function () {
                    self.VmList().Extras.Applied = true;
                    self.VmList().Extras.CanApply = false;
                    self.CanApply(false);
                    lmis.notification.success();
                }).always(function () {
                    actionInProgress = false;
                });

        }

    }

    //Initialize UI
    self.LoadRecord();
}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
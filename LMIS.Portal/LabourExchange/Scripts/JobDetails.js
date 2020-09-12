function ViewModel() {

    var self = this;
    var key = lmis.queryString["k"];
    var actionInProgress = false;

    //Initialize VM
    self.CanApply = ko.observable(false);
    self.VmList = ko.observable({ Extras: {} });

    //VM Operations
    self.LoadRecord = function (id) {

        if (lmis.string.isNullOrWhiteSpace(id)) return;

        lmis.ajax("../LabourExchange/JobDetails.aspx/View", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    self.VmList(data.d);
                    self.CanApply(data.d.Extras.CanApply);
                }
            });

    }
    self.ListUniqueSkills = function () {

        if (!self.VmList().Skills || self.VmList().Skills.length < 1) return [];

        return lmis.arr.unique(self.VmList().Skills.map(function(item) {
            return item.Skill;
        }), "id").map(function(item) {
            return item.desc;
        });

    }
    self.Apply = function () {

        if (!self.VmList().Extras.Express) {
            window.location.assign("JobApplication?k=" + key + "#anchor");
            return;
        }

        if (!actionInProgress && !lmis.string.isNullOrWhiteSpace(key)) {

            actionInProgress = true;

            lmis.ajax("../LabourExchange/JobDetails.aspx/Apply", { id: key }, 0, "show,close",
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
    self.Skills = ko.computed(self.ListUniqueSkills);
    self.LoadRecord(key);
}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})
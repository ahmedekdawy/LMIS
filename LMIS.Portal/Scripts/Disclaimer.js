var ViewModel = function() {
    self.disclaimer = ko.observable();
    lmis.api(self.disclaimer, "cnfg", "orgreg.disclaimer", null, null, "show,close");
}
$(document).ready(function () {
    ko.applyBindings(ViewModel());
})
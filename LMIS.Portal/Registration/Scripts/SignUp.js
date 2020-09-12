function RegWizard() {
    
    var regAs = lmis.queryString["as"], wizardUrl;

    if (regAs && regAs.toLowerCase() === "org") regAs = "org";
    else regAs = "ind";

    if (regAs === "ind")
        wizardUrl = "/registration/widgets/ind/indregwiz.js";
    else
        wizardUrl = "/registration/widgets/org/orgregwiz.js";

    ko.components.register("reg-wizard", { require: wizardUrl });

}

$(document).ready(function () {
    window.vm = new RegWizard();
    ko.applyBindings(vm);
})
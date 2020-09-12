var EducationViewModel = function () {
    var self = this;

    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.url = ko.observable();
    self.EducationLevel = ko.observable();
    self.optionsEducationLevel = ko.observableArray([]);
    lmis.api.SubCodes(self.optionsEducationLevel, "006", function () {
        self.EducationLevel(self.AsyncItems.EducationLevel);
    });

    self.EducationList = ko.observableArray([]);
    self.EducationLevel.subscribe(function () {

        var EducationLevelobj = self.EducationLevel();
        if (EducationLevelobj == "00600002" || EducationLevelobj == "00600003" || EducationLevelobj == "00600004" || EducationLevelobj == "00600005") {
            $("#postNew").attr("href", "UniversityInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");
        }
        else if (EducationLevelobj == "00600007") {
            $("#postNew").attr("href", "SchoolInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");

        }
        else if (EducationLevelobj == "00600006") {
            $("#postNew").attr("href", "InstitutionInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");
        }

    });

    self.EducationLevel();

    return self;
}
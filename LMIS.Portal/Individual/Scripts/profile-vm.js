var ProfileViewModel = function () {

    var self = this;
    var validImageExtensions = ".gif,.jpg,.jpeg,.png";
    var mode = lmis.queryString["m"], key = parseInt(lmis.queryString["k"]);

    if (!mode || isNaN(key)) mode = "p";
    else mode = mode.toLowerCase();

    if (!key || isNaN(key)) key = 0;
    if (key > 0) {
        $('.orginization').hide();
    }

    self.vmReview = window.vmReview;
    self.Mode = ko.observable(mode);
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.FirstName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.LastName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: "", frPh: "", arPh: "" } });
    self.Address.ActiveLang = ko.observable();
    self.FullName = ko.observable();
    self.AddressLocalized = ko.observable();
    self.Email = ko.observable();
    self.MobileNo = ko.observable();
    self.TelephoneNo = ko.observable();
    self.AllowtoViewMyInfo = ko.observable();
    self.IDNumber = ko.observable();
    self.AcceptedImageFiles = validImageExtensions;
    self.Image = ko.observable();
    self.ServerImageName = ko.observable(config.individual.defaultPhotoPath);
    self.PhotoPath = ko.observable(config.individual.defaultPhotoPath);
    self.DateOfBirth = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });

    self.Country = ko.observable();
    self.City = ko.observable();
    self.Militarystatus = ko.observable();
    self.Maritalstatus = ko.observable();
    self.Gender = ko.observable();
    self.GenderId = ko.observable();
    self.Nationailty = ko.observable();
    self.IDType = ko.observable();
    self.Medicalconditions = ko.observable();
    self.Reviews = ko.observable();

    self.Educations = ko.observableArray();
    self.Experiences = ko.observableArray();
    self.Skills = ko.observableArray();
    self.Trainings = ko.observableArray();
    self.Certificates = ko.observableArray();
    self.Jobs = ko.observableArray();
    self.AppliedTrainings = ko.observableArray();

    self.GS = ko.observable();
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();
    self.Address.ActiveLang(lmis.uiCulture);

    //----------------------------- Operations and functions ---------------------------//

    self.Load = function () {
        lmis.ajax("../Individual/Profile.aspx/GetProfile", { id: key, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {

                if (data.d) {
                    self.FirstName.Populate(data.d.FirstName.English, data.d.FirstName.French, data.d.FirstName.Arabic);
                    self.LastName.Populate(data.d.LastName.English, data.d.LastName.French, data.d.LastName.Arabic);
                    self.Address.Populate(data.d.Address.English, data.d.Address.French, data.d.Address.Arabic);
                    self.AddressLocalized(data.d.AddressLocalized),
                   self.FullName(data.d.FullName),
                   self.GS(data.d.GS);
                    self.Approval(data.d.Approval);
                    self.RejectReason(data.d.RejectReason);
                    self.DateOfBirth(lmis.format.dateToString(data.d.DateOfBirth));
                    self.Email(data.d.Email),
                        self.MobileNo(data.d.MobileNo),
                        self.TelephoneNo(data.d.TelephoneNo),
                        self.Gender(data.d.Gender),
                       self.GenderId(data.d.GenderId),
                        self.Maritalstatus(data.d.MaritalStatus),
                        self.Militarystatus(data.d.MilitaryStatus),
                        self.Nationailty(data.d.Nationality),
                        self.ServerImageName(data.d.PhotoPath),
                        (data.d.PhotoPath != null) ? self.PhotoPath((data.d.PhotoPath.length > 0) ? config.individual.photoPath + data.d.PhotoPath : config.individual.defaultPhotoPath) : self.PhotoPath(config.individual.defaultPhotoPath),
                        self.Medicalconditions(data.d.IndividualMedical),
                        self.Country(data.d.Country),
                        self.City(data.d.City);

                    //Localize Trilingual Text Views
                    self.FirstName(data.d.FirstName);
                    self.LastName(data.d.LastName);
                    self.Address(data.d.Address);

                    //load educations
                    self.Educations(data.d.Educations);

                    //load experience history
                    self.Experiences(data.d.Experiences);

                    //load skills
                    self.Skills(data.d.Skills);

                    //load trainings
                    self.Trainings(data.d.Trainings);

                    //load trainings
                    self.Certificates(data.d.Certificates);

                    //load jobs
                    self.Jobs(data.d.Jobs);

                    //load jobs
                    self.AppliedTrainings(data.d.AppliedTrainings);

                    //Admin Review?
                    self.Approval(data.d.Approval);
                    self.RejectReason(data.d.RejectReason);
                    self.Reviews(data.d.Reviews);
                    if (window.vmReview) window.vmReview.init("indProfile", key);
                }
            });
    }

    self.RefreshPersonalInfo = function () {

        lmis.ajax("../Individual/Profile.aspx/GetProfile", { id: key, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {

                if (data.d) {
                    self.FirstName.Populate(data.d.FirstName.English, data.d.FirstName.French, data.d.FirstName.Arabic);
                    self.LastName.Populate(data.d.LastName.English, data.d.LastName.French, data.d.LastName.Arabic);
                    self.Address.Populate(data.d.Address.English, data.d.Address.French, data.d.Address.Arabic);
                    self.DateOfBirth(lmis.format.dateToString(data.d.DateOfBirth));
                    self.Email(data.d.Email),
                   self.MobileNo(data.d.MobileNo),
                   self.TelephoneNo(data.d.TelephoneNo),
                   self.Gender(data.d.Gender),
                   self.Maritalstatus(data.d.MaritalStatus),
                   self.Militarystatus(data.d.MilitaryStatus),
                   self.Nationailty(data.d.Nationality),
                   self.ServerImageName(data.d.PhotoPath),

                   (data.d.PhotoPath != null) ? self.PhotoPath((data.d.PhotoPath.length > 0) ? config.individual.photoPath + data.d.PhotoPath : config.individual.defaultPhotoPath) : self.PhotoPath(config.individual.defaultPhotoPath),

                   self.Medicalconditions(data.d.IndividualMedical),
                   self.Country(data.d.Country),
                   self.City(data.d.City)

                    //Localize Trilingual Text Views
                    self.FirstName(data.d.FirstName);
                    self.LastName(data.d.LastName);
                    self.Address(data.d.Address);

                    //load educations
                    self.Educations(data.d.Educations);

                    //load experience history
                    self.Experiences(data.d.Experiences);

                    //load skills
                    self.Skills(data.d.Skills);

                    //load trainings
                    self.Trainings(data.d.Trainings);

                    //load trainings
                    self.Certificates(data.d.Certificates);

                    //load jobs
                    self.Jobs(data.d.Jobs);

                    //load jobs
                    self.AppliedTrainings(data.d.AppliedTrainings);
                }
            });

    };

    self.EditPersonalInfo = function () {

    }
    
    self.InitEditDialogs = function () {
        self.dlgEditPersonalInfo = lmis.dialog($("#dlgEditPersonalInfo"), 650, 830, null, null, null);
        self.dlgEditEducationalInfo = lmis.dialog($("#dlgEditEducationalInfo"), 460, 830, null, null, null);
        self.dlgEditExperienceInfo = lmis.dialog($("#dlgEditExperienceInfo"), 460, 830, null, null, null);
        self.dlgEditTrainingInfo = lmis.dialog($("#dlgEditTrainingInfo"), 460, 830, null, null, null);
        self.dlgEditCertificateInfo = lmis.dialog($("#dlgEditCertificateInfo"), 400, 830, null, null, null);
        self.dlgEditSkillInfo = lmis.dialog($("#dlgEditSkillInfo"), 650, 800, null, null, null);

    }
    self.OnError = function () {
        lmis.notification.error('an error has occurred!');
    }
    self.EducationDelete = function (id,index) {

       

            function onConfirm() {
                
                var deleteReason = prompt("Delete Reason?");
                if (deleteReason != null) {
                    var dto = {
                        id: id,
                        reason: deleteReason
                    };

                    return lmis.ajax("EditEducationalInfo.aspx/EducationDelete", dto, 0, "show,close",
                        function(data) {

                            $(".Educations > li").eq(index).remove();
                            if ($(".Educations > li").length < 1) {
                                $(".addEducations").show();
                            }
                            Educations.remove(index);
                            
                        });
                }

            }
            lmis.notification.confirm(onConfirm, null);

    }
    self.TrainingeDelete = function (id, index) {



        function onConfirm() {

            var deleteReason = prompt("Delete Reason?");
            if (deleteReason != null) {
                var dto = {
                    id: id,
                    reason: deleteReason
                };

                return lmis.ajax("EditTrainingInfo.aspx/TrainingeDelete", dto, 0, "show,close",
                    function(data) {

                        $(".Tranings > li").eq(index).remove();
                        if ($(".Tranings > li").length < 1) {
                            $(".addTranings").show();
                        }

                    });
            }

        }
        lmis.notification.confirm(onConfirm, null);

    }
    self.CertificateDelete = function (id, index) {



        function onConfirm() {

            var deleteReason = prompt("Delete Reason?");
            if (deleteReason != null) {
                var dto = {
                    id: id,
                    reason: deleteReason
                };

                return lmis.ajax("EditCertificateInfo.aspx/CertificateDelete", dto, 0, "show,close",
                    function(data) {

                        $(".Certificate > li").eq(index).remove();
                        if ($(".Certificate > li").length < 1) {
                            $(".CertificateAdd").show();
                        }

                    });
            }


        }
        lmis.notification.confirm(onConfirm, null);

    }
    self.ExperienceDelete = function (id, index) {



        function onConfirm() {

            var deleteReason = prompt("Delete Reason?");
            if (deleteReason != null) {
                var dto = {
                    id: id,
                    reason: deleteReason
                };

                return lmis.ajax("EditExperienceInfo.aspx/ExperienceDelete", dto, 0, "show,close",
                    function(data) {

                        $(".Experience > li").eq(index).remove();

                        if ($(".Experience > li").length < 1) {
                            $(".addExperience").show();
                        }
                    });
            }

        }
        lmis.notification.confirm(onConfirm, null);

    }
    self.SkillDelete = function (id, index) {



        function onConfirm() {

            var deleteReason = prompt("Delete Reason?");
            if (deleteReason != null) {
                var dto = {
                    id: id,
                    reason: deleteReason
                };

                return lmis.ajax("EditSkillInfo.aspx/SkillDelete", dto, 0, "show,close",
                    function(data) {

                        $(".Skill > li").eq(index).remove();
                        if ($(".Skill > li").length < 1) {
                            $(".addSkill").show();
                        }

                    });
            }

        }
        lmis.notification.confirm(onConfirm, null);

    }

    //Initialize UI
    if (window.location.href.indexOf("Profile") > -1) {
        self.Load(); // view mode
        self.InitEditDialogs();
    }
}



$(document).ready(function () {
    window.vm = new ProfileViewModel();
    ko.applyBindings(vm);
})
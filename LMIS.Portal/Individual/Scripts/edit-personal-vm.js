

//ko.validation.rules.pattern.message = 'Invalid.';

//ko.validation.init({
//    registerExtenders: true,
//    messagesOnModified: true,
//    insertMessages: true,
//    parseInputAttributes: true,
//    messageTemplate: null,
//    decorateElement: true,
//}, true);


function EditPersonalViewModel() {
    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    //TODO: back to business to set category values
    var categoryCode = '013';
    var subCategoryCode = '01300001';
   
        self.AsyncItems = {};
        self.AsyncItems.Occurrence = [];
        self.PortalUsersID = ko.observable();
        self.FirstName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh }, required: true});
        self.LastName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh }, required: true });
        self.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
        self.Address.ActiveLang = ko.observable();
        self.Email = ko.observable().extend({ required: true });
        self.MobileNo = ko.observable().extend({ required: true });
        self.TelephoneNo = ko.observable();
        self.AllowtoViewMyInfo = ko.observable();
        self.IDNumber = ko.observable().extend({ required: true });
        self.DateOfBirth = ko.observable().extend({ required: true, date: { dateFormat: lmis.x.momentDateFormat } });
        self.Image = ko.observable(null);
        self.ServerImageName = ko.observable();
        self.AcceptedImageFiles = validExtensions;
        self.Address.ActiveLang(lmis.uiCulture);
        self.optionsMilitarystatus = ko.observableArray([]);
        self.Militarystatus = ko.observable();//.extend({ required: true });
        self.CountryOptions = ko.observableArray([]);
        self.Country = ko.observable().extend({ required: true });
        self.CityOptions = ko.observableArray([]);
        self.City = ko.observable().extend({ required: true });
        self.optionsMaritalstatus = ko.observableArray([]);
        self.Maritalstatus = ko.observable();
        self.optionsGender = ko.observableArray([]);
        self.Gender = ko.observable().extend({ required: true });
        self.optionsNationailty = ko.observableArray([]);
        self.Nationailty = ko.observable().extend({ required: true });
        self.optionsIDType = ko.observableArray([]);
        self.IDType = ko.observable().extend({ required: true });
        self.optionsMedicalconditions = ko.observableArray([]);
        self.Medicalconditions = ko.observable().extend({ required: true });
       
       
        lmis.api.SubCodes(self.CountryOptions, "009", function () {
            self.Country(self.AsyncItems.Country);
            self.Country.subscribe(function (newVal) {
                lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal);
            });
            lmis.api.SubCodesExeculude(self.optionsGender, "002", "00200001", function () {
                self.Gender(self.AsyncItems.Gender);
                lmis.api.SubCodes(self.optionsMilitarystatus, "012", function () {
                    self.Militarystatus(self.AsyncItems.Militarystatus);
                });
            });

            lmis.api.SubCodes(self.optionsNationailty, "011", function () {
                self.Nationailty(self.AsyncItems.Nationailty);
                lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004", function () {
                    self.IDType(self.AsyncItems.IDType);
                });
            });
            lmis.api.SubCodes(self.optionsMedicalconditions, "014", function () {
                self.Medicalconditions(self.AsyncItems.Medicalconditions);
            });
            lmis.api.SubCodes(self.optionsMaritalstatus, "005", function () {
                self.Maritalstatus(self.AsyncItems.Maritalstatus);
                
                self.Load('e');
            });
            
        });
    //----------------------------- Validation group ---------------------------//
    self.personalValidation = [
    //self.TelephoneNo,
    //self.MobileNo,
    //self.Address,
    //self.Password,
    //self.RetypePassword,
    //self.Email,
    //self.DateOfBirth,
    //self.Country,
    //self.City,
    //self.Militarystatus,
    //self.Gender,
    //self.Nationailty,
    //self.IDType,
    //self.IDNumber,
    //self.Medicalconditions
    ];

    //----------------------------- Operations ---------------------------//
    self.ValidateIDType = function () {
       
        if (self.Nationailty() != 'undefined') {
            if (self.Nationailty() == '01100001') {
                lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004", function() {
                    self.IDType(self.AsyncItems.IDType);
                });
            } else {
                lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004,01300002", function() {
                    self.IDType(self.AsyncItems.IDType);
                });
            }
        }
    }
    self.ValidateMilitarystatus = function () {
        if (self.Gender() == '00200002') {
            $('.Militarystatus').show();
            self.Militarystatus.extend({ required: true });
        } else {
            $('#ddlmilitary').val('');
            self.Militarystatus.extend({ required: false });
            $('.Militarystatus').hide();
        }
    }
    self.Validate = function () {
       
        var errors = ko.validation.group(self);

        if (errors().length > 0) {
            errors.showAllMessages();//!self.ValidateMultiLangControls() ||
            return false;
        }
        else
            return true;
    }
    self.Save = function () {
    //    n = lmis.notification.progress($("#Step1").html());

        //var isValid = self.Validate();
        //if (!isValid) {
        //    lmis.notification.error();
        //    return;
        //}

         if (!self.Image()) {
             self.UpdateRecord();
         } else {
             self.UploadImage();
         }
        
        //else
        //    UploadImage(self.UpdateRecord, OnError)
    }
    self.UpdateRecord = function () {
      //  n = lmis.notification.progress($("#Step2").html());
        console.log(self.Address.getValue().French);
        console.log(self.Address.getValue().Arabic);
        var dto = {
            individualObject: {
                PortalUsersID: self.PortalUsersID(),
                Email: self.Email(),
                MobileNo: self.MobileNo(),
                TelephoneNo: self.TelephoneNo(),
                DateOfBirth: self.DateOfBirth.Value,
                MaritalStatusId: self.Maritalstatus(),
                MilitaryStatus_Id: self.Militarystatus(),
                NationalityId: self.Nationailty(),
                CountryID: self.Country(),
                CityID: self.City(),
                AllowtoViewMyInfo: (self.AllowtoViewMyInfo()) ? 1 : 0,
                IndividualMedicalID: self.Medicalconditions(),
                PhotoPath: self.ServerImageName(),
                IDType:self.IDType(),
                IDNumber:self.IDNumber(),
                GenderId:self.Gender(),
                // Details translations
                Translation:
                    [{
                        LanguageID: 1,
                        FirstName: self.FirstName.getValue().English,
                        LastName: self.LastName.getValue().English,
                        Address: self.Address.getValue().English
                    },
                    {
                        LanguageID: 2,
                        FirstName: self.FirstName.getValue().French,
                        LastName: self.LastName.getValue().French,
                        Address: self.Address.getValue().French
                    },
                    {
                        LanguageID: 3,
                        FirstName: self.FirstName.getValue().Arabic,
                        LastName: self.LastName.getValue().Arabic,
                        Address: self.Address.getValue().Arabic
                    }],

                //AspNetUser object
                User: {
                    PhoneNumber: self.MobileNo(),
                    Email: self.Email()
                },

                //Portal user object
                PortalUser: {
                    IDType: self.IDType(),
                    IDNumber: self.IDNumber()
                }
            }
        };

        return lmis.ajax("../Individual/EditPersonalInfo.aspx/PostUpdates", dto, 0, "",
            function (data) {
                if (data.d) {
                   // lmis.notification.success();
                    window.parent.jQuery('#dlgEditPersonalInfo').dialog('close');
                    window.parent.vm.RefreshPersonalInfo();
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
            });
    }
    self.Load = function (mode) {
        
        lmis.ajax("../Individual/EditPersonalInfo.aspx/GetDetails", { mode: mode }, 0, "show,close",
            function (data) {
                if (data.d != "isPostBack") {
                    //Populate UI
                    self.PortalUsersID(data.d.PortalUsersID),
                    self.FirstName.Populate(data.d.FirstName.English, data.d.FirstName.French, data.d.FirstName.Arabic);
                    self.LastName.Populate(data.d.LastName.English, data.d.LastName.French, data.d.LastName.Arabic);
                    self.Address.Populate(data.d.Address.English, data.d.Address.French, data.d.Address.Arabic);
                    self.DateOfBirth(lmis.format.dateToString(data.d.DateOfBirth));
                    self.Email(data.d.Email);
                    self.MobileNo(data.d.MobileNo);
                    self.TelephoneNo(data.d.TelephoneNo);
                    self.Maritalstatus(data.d.MaritalStatusId);
                    self.Militarystatus(data.d.MilitaryStatus_Id);
                        self.Nationailty(data.d.NationalityId);
                           
                               if (self.Nationailty() == '01100001') {
                                   lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004", function() {
                                       self.IDType(self.AsyncItems.IDType);
                                       self.IDType(data.d.IDType);
                                   });
                               } else {
                                   lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004,01300002", function() {
                                       self.IDType(self.AsyncItems.IDType);
                                       self.IDType(data.d.IDType);
                                   });
                               }

                    self.Country(data.d.CountryID);
                    self.City(data.d.CityID);
                    
                    self.IDNumber(data.d.IDNumber);
                    self.Gender(data.d.GenderId);
                    self.Medicalconditions(data.d.IndividualMedicalID);
                    self.AllowtoViewMyInfo(data.d.AllowtoViewMyInfo);
                    self.ServerImageName(data.d.PhotoPath);
                
                    //Localize Trilingual Text Views
                    self.FirstName.LocalizeView(true);
                    self.LastName.LocalizeView(true);
                    self.Address.LocalizeView(true);

                    //if (data.d.NationalityId != 'undefined') {
                    //    if (data.d.NationalityId == '01100001') {
                    //        lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004", function () {
                    //            self.IDType(self.AsyncItems.IDType);
                    //        });
                    //    } else {
                    //        lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004,01300002", function () {
                    //            self.IDType(self.AsyncItems.IDType);
                    //        });
                    //    }
                    //}
                   
                    if (self.Gender() == '00200002') {
                        $('.Militarystatus').show();
                        self.Militarystatus.extend({ required: true });
                    } else {
                        $('#ddlmilitary').val('');
                        self.Militarystatus.extend({ required: false });
                        $('.Militarystatus').hide();
                    }
                }
            });
    }
    self.OnError = function () {
        lmis.notification.error('an error has occurred!');
    }

    //----------------------------- Image upload functions ---------------------------//
    self.ValidateImage = function (item, e) {
     
        var selectedImage = e.target.files[0];

        if (selectedImage != null) {
            if (selectedImage.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedImage.name, validExtensions)) {
                    self.Image(selectedImage);
                    $("#txtImageName").val(selectedImage.name);
                } else self.ClearImage();
            } else self.ClearImage();
        } else self.ClearImage();

    }
    self.ClearImage = function () {
      
        self.Image(null);
        self.ServerImageName(null);
        $("#txtImageName").val("");
        lmis.fileInput.clear($("#hdnImage11Browser"));

    }
    self.UploadImage = function () {

        if (!self.Image()) {
            self.ClearImage();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" + config.individual.photoFolder, self.Image(), 0, "show/close",
            function (data) {
                self.ServerImageName(data);
                self.UpdateRecord();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearImage();   //Validation Error
                
            });
    }
    //Initialize UI
  
        
  

        
    



    //console.log('1'+mode);
    //if (lmis.string.isNullOrWhiteSpace(self.Militarystatus()) || lmis.string.isNullOrWhiteSpace(self.Gender())
    //        || lmis.string.isNullOrWhiteSpace(self.Nationailty()) || lmis.string.isNullOrWhiteSpace(self.Medicalconditions())
    //        || lmis.string.isNullOrWhiteSpace(self.Country()) || lmis.string.isNullOrWhiteSpace(self.TypeID()) || lmis.string.isNullOrWhiteSpace(self.City())) {
    //    setTimeout(function () {
    //        self.Load(mode); // view mode

    //    }, 2000);
    //}
    //else


}

$(document).ready(function () {
    
    ko.applyBindings(new EditPersonalViewModel());
    
})
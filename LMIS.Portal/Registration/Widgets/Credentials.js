define(function (require) {

    var markup = require("text!./credentials.aspx");

    function model(args) {

        var self = this; args = args || {};
        var res = lmis.resFromHtml(markup, ["msgRegisteredEmail", "msgInvalidPassword", "msgPasswordsMismatch"]);

        self.docExtensions = ".pdf,.doc,.docx";
        self.maxFileSize = 1 * 1024 * 1024; // 1 MBytes

        self.ReqAuthLetter = ko.observable(args.requireAuthLetter);
        self.AuthLetter = ko.observable();
        self.AuthLetterFileName = ko.computed(function() {
            return self.AuthLetter() ? self.AuthLetter().name || self.AuthLetter() : undefined;
        }).extend({ required: self.ReqAuthLetter() });
        self.AuthLetterUploaded = ko.computed(function () {
            return self.AuthLetter() && !self.AuthLetter().name;
        });

        self.ValidateAuthLetter = function(item, e) {

            var authLetter = e.target.files[0];

            if(authLetter != null) {
                if (authLetter.size <= self.maxFileSize) {
                    if (lmis.fileInput.matchExtension(authLetter.name, self.docExtensions)) {
                        self.AuthLetter(authLetter);
                    } else self.ClearAuthLetter();
                } else self.ClearAuthLetter();
            } else self.ClearAuthLetter();

        }
        self.ClearAuthLetter = function () {

            self.AuthLetter(null);
            lmis.fileInput.clear($("#hdnAuthLetterBrowser"));

        }

        ko.validation.rules["unregisteredEmail"] = {
            async: true,
            validator: function(newVal, vParams, onSuccess) {
                lmis.api(false, "ow01", newVal, onSuccess);
            },
            message: res["msgRegisteredEmail"]
        };
        ko.validation.registerExtenders();

        self.UserName = ko.observable().extend({ required: true, email: true, unregisteredEmail: true });
        self.Password = ko.observable().extend({
            required: true,
            pattern: { params: lmis.regex.password, message: res["msgInvalidPassword"] }
        });
        self.PasswordConfirmation = ko.observable().extend({
            required: true,
            equal: { params: self.Password, message: res["msgPasswordsMismatch"] }
        });

        self.UploadAuthLetter = function (onSuccess, onError, progress) {

            onSuccess = onSuccess || lmis.ajaxSuccessHandler;
            onError = onError || lmis.ajaxErrorHandler;

            if (!self.AuthLetter() || self.AuthLetterUploaded()) {
                if (self.AuthLetterUploaded()) onSuccess(); else onError();
                return false;
            }

            return lmis.ajaxUpload("/api/upload/doc/", self.AuthLetter(), 0, progress,
                function (data) {
                    self.AuthLetter(data);
                    onSuccess(data);
                },
                function (xhr) {
                    if (xhr.status === 400) self.ClearAuthLetter();   //Validation Error
                    onError(xhr);
                });

        }

        self.errors = ko.validation.group(self);
        self.dto = ko.computed(function() {

            if (self.errors().length > 0) return false;
            if (self.UserName.isValidating()) return false;

            return lmis.ko.toJS(self, ["UserName", "Password", "AuthLetterFileName"]);

        });

    };

    return { template: markup, viewModel: model };

});
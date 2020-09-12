//Dependencies: jquery, jquery.noty, jquery.inputmask + extensions, moment, knockout

lmis = window.lmis ||  function() {

    var logActive = true;
    var notyProgress = null;
    var lastGuid = "";

    $(document).ready(function () {
        lmis.uiCulture = document.cookie.replace(/(?:(?:^|.*;\s*)CultureInfo\s*\=\s*([^;]*).*$)|^.*$/, "$1");
        lmis.x.downloadURL = window.location.href.toString().split(window.location.host)[0] + window.location.host + "/Uploads/";
        lmis.x.dateMask = $.trim($("#X_InputMaskDate").html());
        lmis.x.momentDateFormat = $.trim($("#X_MomentDate").html());
        lmis.x.datePickerFormat = $.trim($("#X_DatePickerDate").html());
        $.fn.dataTable.moment(lmis.x.momentDateFormat);
        lmis.x.enPh = $("#X_TypeInEnglish").html();
        lmis.x.frPh = $("#X_TypeInFrench").html();
        lmis.x.arPh = $("#X_TypeInArabic").html();
        lmis.x.InvalidFileForUpload = $("#X_InvalidFileForUpload").html();
        lmis.x.InvalidFile = function (ext, size) { return lmis.x.InvalidFileForUpload.replace("{0}", ext).replace("{1}", (size / (1024 * 1024)).toFixed(2)); };
        lmis.x.OK = $("#X_OK").html();
        lmis.x.Cancel = $("#X_Cancel").html();
        lmis.x.Required = $("#X_Required").html();
        lmis.x.UnsavedChanges = $("#X_UnsavedChanges").html();
        lmis.x.InvalidEmail = $("#X_InvalidEmail").html();
        if (ko.validation) {
            ko.validation.init({
                grouping: { live: true, observable: true },
                decorateInputElement: true,
                decorateElementOnModified: false
            }, true);
            ko.validation.rules.required.message = lmis.x.Required;
            ko.validation.rules.email.message = lmis.x.InvalidEmail;
        }
    });

    var defaults =
    {
        multiselectOptions:
        {
            buttonWidth: "100%",
            maxHeight: 200,
            enableClickableOptGroups: true,
            enableCaseInsensitiveFiltering: true
        },
        multiselectNoFilterOptions:
        {
            buttonWidth: "100%",
            maxHeight: 200,
            enableClickableOptGroups: true
        }
    };

    var queryString = {};
    window.location.search.replace(/#.*$/, "").replace(
        new RegExp("([^?=&]+)(=([^&]*))?", "g"),
        function($0, $1, $2, $3) { queryString[$.trim($1).toLowerCase()] = (typeof $3 === "undefined" ? "" : $3); }
    );

    var regex = {
        password: /(?=^[^\s]{6,128}$)((?=.*?\d)(?=.*?[A-Z])(?=.*?[a-z])|(?=.*?\d)(?=.*?[^\w\d\s])(?=.*?[a-z])|(?=.*?[^\w\d\s])(?=.*?[A-Z])(?=.*?[a-z])|(?=.*?\d)(?=.*?[A-Z])(?=.*?[^\w\d\s]))^.*/
    };

    var scrollIntoView = function (id) {
        if (!document.getElementById(id)) id = "anchor";
        document.getElementById(id).scrollIntoView();
    }

    var focusFirstInput = function(element) {
        if (!element || typeof element.focus !== "function") element = $(document);
        setTimeout(function () {
            if (element.find(".validationElement").length > 0)
                element.find(".validationElement").first().focus();
            else if (element.find(".focus-group :input:visible:enabled:first").length > 0)
                element.find(".focus-group :input:visible:enabled:first").focus();
        }, 500);
    }

    var resFromHtml = function (html, ids) {

        var res = {};
        var $html = $("<div />", { html: html });

        if (typeof ids === "string") ids = ids.split(",");

        ids.forEach(function(id) {
            res[id] = $html.find("#" + id.trim()).html();
        });

        return res;

    }

    var setDateMask = function(element)
    {
        element.inputmask(lmis.x.dateMask, { "clearIncomplete": true });
        element.datepicker({ dateFormat: lmis.x.datePickerFormat, changeMonth: true, changeYear: true });
    }

    var setTimeMask = function(element) {
        element.inputmask({
            mask: "h:s t\\m",
            placeholder: "hh:mm xm",
            alias: "datetime", hourFormat: "12",
            clearIncomplete: true
        });
    }

    var setDecimalMask = function(element, beforeRadix, afterRadix) {
        element.inputmask("decimal", { integerDigits: beforeRadix, digits: afterRadix, autoGroup: true, groupSeparator: ",", allowMinus: false });
        element.css("text-align", "left");
    }
    var setIntegerMask = function (element, beforeRadix) {
        element.inputmask("decimal", { integerDigits: beforeRadix,allowMinus: false });
        element.css("text-align", "left");
    }

    var setPhoneMask = function(element) {
        element.inputmask("Regex", { regex: "(?:\\+?\\d{1,3}[ -]?\\d{2,8}[ -]?\\d{3,9})" });
    }

    var setUrlMask = function(element) {
        // To Be Implemented
        element.inputmask("Regex", { regex: "(.*?)" });
        //element.inputmask("Regex", { regex: "((http:\\\/\\\/)|(https:\\\/\\\/))?(www\\.)?[-a-zA-Z0-9:%._\\+~#=]{2,256}\\.[a-z]{2,4}\\b([-a-zA-Z0-9:%_\\+.~#?&\/\/=]*)" });
    }

    var setEmailMask = function(element) {
        element.inputmask("Regex", { regex : "[a-zA-Z0-9._%-]+@[a-zA-Z0-9-]+\\.[a-zA-Z]{2,4}" });
    }

    var isNullOrWhiteSpace = function(str, options) {
        if (typeof str === "number") return false;
        if (str === null || typeof str !== "string") return true;
        if (typeof options === "string") return (str === options);
        if (options && options.constructor === Array) return ($.inArray(str, options) > -1);
        return str.match(/^\s*$/) !== null;
    }

    var formatDateToString = function(date, localize) {
        if (localize === true)
            return moment.utc(date).local().format(lmis.x.momentDateFormat);
        else
            return moment.utc(date).format(lmis.x.momentDateFormat);
    }

    var formatTimeToString = function(time, localize) {
        if (localize === true)
            return moment.utc(time).local().format("hh:mm a");
        else
            return moment.utc(time).format("hh:mm a");
    }

    var formatApprovalStatus = function(statusCode) {
        switch (statusCode) {
        case 1:
            return "Pending";
        case 2:
            return "Approved";
        case 3:
            return "Rejected";
        default:
            return "N/A";
        }
    }

    var formatNullableString = function(obj, propName) {

        var ret = obj;

        if (typeof obj === "object" && obj && typeof propName === "string")
            ret = obj[propName];

        if(isNullOrWhiteSpace(ret)) return "N/A";
        else return ret;

    }

    var formatStringToNumber = function (str, nullValue) {
        if (typeof nullValue === "undefined") nullValue = 0;
        if (str === null) return nullValue;
        if (typeof str === "number") return str;
        if (typeof str !== "string") return NaN;
        return Number(str.replace(",", ""));
    }

    var stringCotains = function(str1, str2, ci) {

        if (typeof str1 != "string" || typeof str2 != "string") return false;

        if (ci) return (str1.toLowerCase().indexOf(str2.toLowerCase()) > -1);
        else return (str1.indexOf(str2) > -1);

    }

    var highlight = function (text, search) {

        var rx = "";

        function isTrilingualTextWithValue(tt) {
            return !!tt && tt.hasOwnProperty("English") && tt.hasOwnProperty("French") && tt.hasOwnProperty("Arabic")
                && (!isNullOrWhiteSpace(tt.English) || !isNullOrWhiteSpace(tt.French) || !isNullOrWhiteSpace(tt.Arabic));
        }

        function escape(str) {
            return str.replace(new RegExp("[.\\\\+*?\\[\\^\\]$(){}=!<>|:\\-]", "g"), "\\$&"); //escape special characters
        }
        
        if ((typeof text === "string" && !isNullOrWhiteSpace(text)) || isTrilingualTextWithValue(text)) {
            if (typeof search === "string" && !isNullOrWhiteSpace(search))
                rx = escape(search);
            else if (search && search.constructor === Array && search.length > 0) {
                for (var i = 0; i < search.length; i++)
                    rx += escape(search[i]) + "|";
                rx = rx.replace(/\|$/, "");
            }
        }

        if (rx === "") return text;

        if (typeof text === "string")
            return text.replace(new RegExp("(" + rx + ")", "gi"), "<mark>$1</mark>");
        else
            return {
                English: isNullOrWhiteSpace(text.English) ? null : text.English.replace(new RegExp("(" + rx + ")", "gi"), "<mark>$1</mark>"),
                French: isNullOrWhiteSpace(text.French) ? null : text.French.replace(new RegExp("(" + rx + ")", "gi"), "<mark>$1</mark>"),
                Arabic: isNullOrWhiteSpace(text.Arabic) ? null : text.Arabic.replace(new RegExp("(" + rx + ")", "gi"), "<mark>$1</mark>")
            }

    }

    var localString = function(globalString, fallbackIfEmpty) {

        if (typeof globalString !== "object" || globalString === null) return "";

        var ret;

        switch(lmis.uiCulture) {
            case "ar":
                ret = globalString.Arabic;
                if (fallbackIfEmpty && isNullOrWhiteSpace(ret)) ret = globalString.English;
                if (fallbackIfEmpty && isNullOrWhiteSpace(ret)) ret = globalString.French;
                break;
            case "fr":
                ret = globalString.French;
                if (fallbackIfEmpty && isNullOrWhiteSpace(ret)) ret = globalString.English;
                if (fallbackIfEmpty && isNullOrWhiteSpace(ret)) ret = globalString.Arabic;
                break;
            default:
                ret = globalString.English;
                if (fallbackIfEmpty && isNullOrWhiteSpace(ret)) ret = globalString.French;
                if (fallbackIfEmpty && isNullOrWhiteSpace(ret)) ret = globalString.Arabic;
                break;
        }

        if (typeof ret != "string") ret = "";

        return ret;

    }

    var matchFileExtension = function(fileName, validExtensions) {
        return (new RegExp("(" + validExtensions.split(",").join("|").replace(/\./g, "\\.") + ")$")).test(fileName);
    }

    var clearFileInput = function(element) {
        element.replaceWith(element.val("").clone(true));
    }

    var confirmNotification = function(onConfirm, onCancel, html) {

        return noty({
            type: "error",
            text: html ? html : $("#X_ConfirmContinue").html(),
            layout: "center", closeWith: [],
            modal: true, killer: true,
            callback: {
                onShow: function () { $(".noty_modal").off("click"); },
                onClose: function () { $(".noty_modal").on("click", function () { $.noty.closeAll(); }); }
            },
            buttons: [
                {
                    addClass: "btn btn-primary", text: $("#X_OK").html(),
                    onClick: function($noty) { $noty.close(); if (typeof onConfirm == "function") onConfirm(); }
                },
                {
                    addClass: "btn btn-primary", text: $("#X_Cancel").html(),
                    onClick: function($noty) { $noty.close(); if (typeof onCancel == "function") onCancel(); }
                }
            ]
        });

    }

    var progressNotification = function(html) {

        if (notyProgress && notyProgress.closed) notyProgress = null;

        notyProgress = notyProgress || noty({
            type: "information",
            text: html,
            theme: "spinnerTheme",
            layout: "center",
            animation: { open: { height: "show" }, close: { height: "hide" }, speed: 1 },
            closeWith: [],
            modal: true,
            killer: true,
            callback: {
                onShow: function () { $(".noty_modal").off("click"); },
                onClose: function() { $(".noty_modal").on("click", function() { $.noty.closeAll(); }); },
                afterClose: function() { notyProgress = null; }
            }
        });

        notyProgress.setText(html);
        return notyProgress;

    }

    var successNotification = function(html) {

        return noty({
            type: "success",
            text: (html) ? html : $("#Success").html(),
            layout: "center", closeWith: ["click", "backdrop"],
            modal: true, killer: true
        });

    }

    var errorNotification = function(html) {
        
        return noty({
            type: "error",
            text: (html) ? html : "Error",
            layout: "center", closeWith: ["click", "backdrop"],
            modal: true, killer: true
        });

    }

    var killNotification = function() {
        if (notyProgress) notyProgress.close();
    }

    var browserNotifications = {};
    browserNotifications.confirmUnsavedChanges = function(activate) {
        window.onbeforeunload = activate ? function() { return lmis.x.UnsavedChanges; } : null;
    }

    var guid = function(dt) {

        var seed = (dt || (new Date()).getTime()).toString();
        var id = seed.substr(seed.length - 8, 4) + "." + seed.substr(seed.length - 4);

        if (id === lastGuid.substr(0, id.length)) {
            var idx = lastGuid.lastIndexOf("-");
            if (idx === -1) id += "-1";
            else id += "-" + (parseInt(lastGuid.substr(idx + 1)) + 1);
        }

        lastGuid = id;

        return id;
    }

    var ajaxSuccessHandler = function () {
        successNotification();
    }

    function responseErrorMsg(xhr, defString) {

        var err;

        if (xhr.responseJSON)
            if (typeof xhr.responseJSON === "string")
                err = xhr.responseJSON;
            else if (typeof xhr.responseJSON.d === "string")
                err = xhr.responseJSON.d;
            else if (xhr.responseJSON.d)
                err = xhr.responseJSON.d.Message;
            else
                err = xhr.responseJSON.Message;
        else
            if (xhr.Message) err = xhr.Message;

        if (typeof err !== "string") err = defString || "";

        return err;

    }

    function responseStackTrace(xhr, defString) {

        var err;

        if (xhr.responseJSON) {
            if (xhr.responseJSON.StackTrace)
                err = xhr.responseJSON.StackTrace;
            else if (xhr.responseJSON.StackTraceString)
                err = xhr.responseJSON.StackTraceString;
        }
        else
            err = xhr.responseText;

        if (typeof err !== "string") err = defString || "";

        return err;

    }

    var ajaxErrorHandler = function (xhr) {

        //Timeout
        if (xhr.status === 0)
            alert("Timeout:\n\nResource is not available.");

        //Validation Error
        if (xhr.status === 400) {

            $("#ValidationMessage").text(responseErrorMsg(xhr, "BLL Error"));
            errorNotification($("#ServerValidations").html());

        }

        //Exception
        if (xhr.status === 500)
            alert(xhr.status + " - " + xhr.statusText);

    }

    function logAjaxRequest(xhr, url, timeout) {

        if (!logActive) return;

        xhr.requestTime = (new Date()).getTime();
        xhr.id = guid(xhr.requestTime);

        console.log("[" + xhr.id + ": " + timeout + "] " + url);
        
    }

    function logAjaxResponse(xhr) {

        if (!logActive) return;

        var delay = (new Date()).getTime() - xhr.requestTime;
        var err = "", tag = " [" + xhr.id + ": " + delay + "] ";

        if (xhr.status === 0)
            err = "----- TIMEOUT -----" + tag;

        if (xhr.status === 200)
            err = "***** Success *****" + tag;

        if (xhr.status === 204)
            err = "----- NO DATA -----" + tag;

        if (xhr.status === 400)
            err = "---- BLL ERROR ----" + tag + (xhr.logMsg ? "---- " + responseErrorMsg(xhr) : "");

        if (xhr.status === 404)
            err = "---- NOT FOUND ----" + tag;

        if (xhr.status === 500) {

            err = "---- EXCEPTION ----" + tag + "\n";
            err += responseErrorMsg(xhr) + "\n" + responseStackTrace(xhr) + "\n-------------------";

        }

        if (err === "")
            err = "---- ERROR " + xhr.status + " ----";

        console.log(err);

    }

    var ajax = function(url, dto, timeout, progress, onSuccess, onError) {

        if (!dto) dto = {};
        if (typeof timeout != "number") timeout = 0;
        if (typeof onSuccess != "function") onSuccess = ajaxSuccessHandler;
        if (typeof onError != "function") onError = ajaxErrorHandler;

        if (stringCotains(progress, "show", true)) notyProgress = progressNotification();

        var ret = $.ajax({
            type: "POST", url: url, data: JSON.stringify(dto), timeout: timeout,
            contentType: "application/json; charset=utf-8", dataType: "json",
            success: function(data, status, xhr) {
                if (data && (data.ErrorId || (data.d && data.d.ErrorId))) {
                    xhr.status = 400;
                    xhr.Message = (data.Message || data.d.Message);
                }
                logAjaxResponse(xhr);
                if (xhr.status !== 200)
                    onError(xhr, "Error", "Error");
                else
                    onSuccess(data, status, xhr);
            },
            error: function(xhr, status, error) { logAjaxResponse(xhr); onError(xhr, status, error); }
        }).always(function() {
            if (stringCotains(progress, "close", true) && notyProgress) notyProgress.close();
        });

        logAjaxRequest(ret, url, timeout);

        return ret;

    }

    var ajaxProgressHandler = function(e) {

        var html;

        if (typeof e.target.fileName !== "string") e.target.fileName = "";

        if (e.lengthComputable) {
            html = $("#X_FileUploadProgress").html().replace("{0}", e.target.fileName);
            var percentComplete = Math.round(10000 * e.loaded / e.total) / 100;
            $("#Progress").html(html.replace("{1}", percentComplete.toFixed(2)));
        } else {
            html = $("#X_FileUpload").html().replace("{0}", e.target.fileName);
            $("#Progress").html(html);
        };

        progressNotification($("#Upload").html());

    }

    var ajaxUpload = function(url, file, timeout, progress, onSuccess, onError, onProgress) {

        if (!file || (!file.name && (file.constructor !== Array || file.length === 0))) return null;
        if (typeof timeout != "number") timeout = 0;
        if (typeof onSuccess != "function") onSuccess = ajaxSuccessHandler;
        if (typeof onError != "function") onError = ajaxErrorHandler;
        if (typeof onProgress != "function") onProgress = ajaxProgressHandler;

        var fileNames = [];
        var fd = new FormData();

        if (file.constructor !== Array)
            fd.append("", file);
        else
            for (var i = 0; i < file.length; i++)
            {
                fd.append(file[i].id, file[i].file);
                fileNames.push(file[i].file.name);
            }

        var ret = $.ajax({
            type: "POST", url: url, data: fd, timeout: timeout,
            contentType: false, processData: false, cache: false,
            success: function(data, status, xhr) {
                if (data.ErrorId || (data.d && data.d.ErrorId)) {
                    xhr.status = 400;
                    xhr.Message = (data.Message || data.d.Message);
                }
                logAjaxResponse(xhr);
                if (xhr.status !== 200)
                    onError(xhr, "Error", "Error");
                else
                    onSuccess(data, status, xhr);
            },
            error: function(xhr, status, error) { logAjaxResponse(xhr); onError(xhr, status, error); },
            xhr: function() {

                var xhr = $.ajaxSettings.xhr();

                if (stringCotains(progress, "show", true) && xhr.upload) {
                    xhr.upload.fileName = file.name || "";
                    xhr.upload.onprogress = onProgress;
                } else progress = false;

                return xhr;
                
            }
        }).always(function() {
            if (stringCotains(progress, "close", true) && notyProgress) notyProgress.close();
        });

        ret.logMsg = true;
        logAjaxRequest(ret, url, timeout);

        return ret;

    }
    var ajaxUploadDelete = function (url, file, timeout, progress, onSuccess, onError, onProgress) {

  
        if (typeof timeout != "number") timeout = 0;
        if (typeof onSuccess != "function") onSuccess = ajaxSuccessHandler;
        if (typeof onError != "function") onError = ajaxErrorHandler;
        if (typeof onProgress != "function") onProgress = ajaxProgressHandler;

        var fileNames = [];
        var fd = new FormData();

        if (file.constructor !== Array)
            fd.append("", file);
        else
            for (var i = 0; i < file.length; i++) {
                fd.append(file[i].id, file[i].file);
                fileNames.push(file[i].file.name);
            }

        var ret = $.ajax({
            type: "POST", url: url, data: fd, timeout: timeout,
            contentType: false, processData: false, cache: false,
            success: function (data, status, xhr) {
                if (data.ErrorId || (data.d && data.d.ErrorId)) {
                    xhr.status = 400;
                    xhr.Message = (data.Message || data.d.Message);
                }
                logAjaxResponse(xhr);
                if (xhr.status !== 200)
                    onError(xhr, "Error", "Error");
                else
                    onSuccess(data, status, xhr);
            },
            error: function (xhr, status, error) { logAjaxResponse(xhr); onError(xhr, status, error); },
            xhr: function () {

                var xhr = $.ajaxSettings.xhr();

                if (stringCotains(progress, "show", true) && xhr.upload) {
                    xhr.upload.fileName = file.name || "";
                    xhr.upload.onprogress = onProgress;
                } else progress = false;

                return xhr;

            }
        }).always(function () {
            if (stringCotains(progress, "close", true) && notyProgress) notyProgress.close();
        });

        ret.logMsg = true;
        logAjaxRequest(ret, url, timeout);

        return ret;

    }
    var api = function (observableOptions, fid, args, onSuccess, onError, progress) {

        if (observableOptions) observableOptions(observableOptions.destroyAll ? [] : {});

        function successHandler(data) {
            if (observableOptions) observableOptions(data);
            if (typeof onSuccess === "function") onSuccess(data);
        }

        function errorHandler(xhr) {
            if (typeof onError === "function") onError(xhr);
        }

        if (typeof args === "undefined" || args === null) args = {};
        else if (args.constructor !== Array) args = [args];

        var ret = ajax("/api/dataservice/" + fid + "/" + lmis.uiCulture, args, 0, progress, successHandler, errorHandler);
        ret.logMsg = true;

        return ret;

    }
    var apiExeculude = function (observableOptions, fid, execludedIds, args, onSuccess, onError, progress) {

        if (observableOptions) observableOptions(observableOptions.destroyAll ? [] : {});

        function successHandler(data) {
            if (observableOptions) observableOptions(data);
            if (typeof onSuccess === "function") onSuccess(data);
        }

        function errorHandler(xhr) {
            if (typeof onError === "function") onError(xhr);
        }

        if (typeof args === "undefined" || args === null) args = {};
        else if (args.constructor !== Array) args = [args];

        var ret = ajax("/api/dataservice/" + fid + "/" + execludedIds + "/" + lmis.uiCulture, args, 0, progress, successHandler, errorHandler);
        ret.logMsg = true;

        return ret;

    }

    api.SubCodes = function (observableOptions, generalId, onSuccess, onError) {
        return api(observableOptions, "subcodes/" + generalId, null, onSuccess, onError);
    }
    api.SubCodesExeculude = function (observableOptions, generalId, execludedIds, onSuccess, onError) {
        return apiExeculude(observableOptions, "subcodes/" + generalId, execludedIds, null, onSuccess, onError);
    }
    api.GroupedSubCodes = function (observableOptions, generalId, onSuccess, onError) {
        return api(observableOptions, "subcodes/group/" + generalId, null, onSuccess, onError);
    }

    api.SubCodesByParent = function (observableOptions, observableValue, generalId, parentSubCodeId, onSuccess, onError) {

        observableValue(null);
        observableOptions([]);

        if (isNullOrWhiteSpace(parentSubCodeId)) return null;

        return api(observableOptions, "subcodes/byparent/" + generalId + "/" + parentSubCodeId, null, onSuccess, onError);

    }

    var observeEveryArray = function (obj) {

        for (var property in obj)
            if (obj.hasOwnProperty(property) && typeof obj[property] === "object")
                obj[property] = observeEveryArray(obj[property]);

        return (obj.constructor === Array) ? ko.observableArray(obj) : obj;

    }

    var addOptionToDummyGroup = function (observableEntry, observableOptions, observableSelections) {

        var dummyGroup;
        var options = observableOptions();

        if (options[0] && options[0].id === "+")
            dummyGroup = observableOptions.shift();
        else
            dummyGroup = { id: "+", desc: "Other", subset: ko.observableArray([]) };

        var newId = guid();
        var newVal = observableEntry();

        dummyGroup.subset.unshift({ id: newId, desc: newVal });
        observableEntry(null);

        observableOptions.unshift(dummyGroup);
        observableSelections.push(newId);

    }

    var separateSelections = function(observableSelections) {

        var arr = observableSelections();
        var selections = [], additions = [];

        for (var i = 0; i < arr.length; i++)
            if (arr[i].indexOf("+") >= 0)
                additions.push(arr[i].substr(arr[i].indexOf("+") + 1));
            else
                selections.push(arr[i]);

        return { selections: selections, additions: additions }
    }

    var koToJs = function(obj, props) {

        if (!obj) return obj;

        var ret = {};
        var processAll = !props || $.type(props) !== "array" || props.length < 1;

        if ($.type(props) === "array") props.push("English", "French", "Arabic");

        $.each(obj, function (key, val) {
            if (processAll || props.indexOf(key) !== -1) {

                var v = ko.isObservable(val) ? val() : val;
                
                switch ($.type(v)) {
                    case "function":
                    case "undefined":
                    case "null":
                        break;
                    case "object":
                        ret[key] = $.extend({}, v);
                        break;
                    case "array":
                        ret[key] = $.extend([], v);
                        break;
                    default:
                        ret[key] = v;
                }
            }
        });

        return ret;

    }

    var koFromJs = function(obj, dto) {

        if (!obj || !dto) return obj;
        
        $.each(dto, function (key, val) {
            if (obj.hasOwnProperty(key)) {
                if (ko.isObservable(obj[key])) {
                    var field = obj[key].target || obj[key];
                    switch (field.extendedAs) {
                        case "date":
                            field(formatDateToString(val));
                            break;
                        case "time":
                            field(formatTimeToString(val));
                            break;
                        case "trilingualText":
                            val = val || {};
                            field.Populate(val.English, val.French, val.Arabic);
                            field.LocalizeView(false);
                            break;
                        case "fileInput":
                        default:
                            field(val);
                    }
                } else obj[key] = val;
            }
        });

        return obj;

    }

    var multiselect = {};
    multiselect.Toggle = function (element) {
        setTimeout(function () {
            element.next(".btn-group").children(":button").trigger("click");
        }, 150);
    }
    multiselect.DelayedAction = function(element, action) {
        setTimeout(function () {
            element.multiselect(action);
            if (action === "disable") multiselect.SetTooltip(element);
        }, 150);
    }
    multiselect.SetTooltip = function(element) {
        var div = element.next(".btn-group");
        div.attr("data-toggle", "tooltip");
        div.attr("data-original-title", div.children(":button").attr("title"));
        $("[data-toggle='tooltip']").tooltip();
    }

    var datatable = {};
    datatable.SetOuterBorderStyle = function (element, style) {
        element.css("border-style", style);
        element.find("thead tr th").css("border-style", style);
    }

    var arr = {};
    arr.unique = function (source, key) {

        if (source.constructor !== Array || typeof key !== "string") return [];

        return source.filter(function (item, idx, result) {
            var keys = $.map(result, function (obj) { return obj[key]; });
            return keys.indexOf(item[key]) === idx;
        });

    }

    var dialog = function(element, h, w, onOpen, onOk, onCancel, onClose) {

        var addOk = (onOk !== null);
        var addCancel = (onCancel !== null);
        var userInput = ko.observable("");
        var auto = element.hasOwnProperty("div");
        var e = auto ? element.div : element;

        function isValidButton(o) {
            return typeof o === "object" && o !== null && o.hasOwnProperty("text") && o.hasOwnProperty("click");
        }

        if (typeof onOpen !== "function") onOpen = lmis.doNothing;
        if (typeof onOk !== "function" && !isValidButton(onOk)) onOk = lmis.doNothing;
        if (typeof onCancel !== "function" && !isValidButton(onCancel)) onCancel = lmis.doNothing;
        if (typeof onClose !== "function") onClose = lmis.doNothing;

        if (typeof onOk === "function" && typeof onCancel === "function" && onOk.toString() === onCancel.toString()) addCancel = false;

        var dlg = e.dialog({
            autoOpen: false,
            height: h,
            width: w,
            modal: true,
            buttons: (function() {
                var ret = [];
                if (addOk)
                    ret.push({
                        text: lmis.x.OK,
                        click: function () {
                            dlg.dialog("close");
                            onOk(userInput());
                        }
                    });
                if (addCancel)
                    ret.push({
                        text: lmis.x.Cancel,
                        click: function () {
                            dlg.dialog("close");
                            onCancel();
                        }
                    });
                return ret;
            })(),
            close: function () {
                dlg.form[0].reset();
                onClose();
            },
            position: { my: "center", at: "top+300", of: window }
        });

        dlg.form = dlg.find("form").on("submit", function (event) {
            event.preventDefault();
            onOk(userInput());
        });

        if (auto) {
            dlg.title = element.title;
            dlg.label = element.label;
            dlg.maxLength = (typeof element.maxLength !== "number" || element.maxLength < 1) ? 0 : element.maxLength;
        }

        dlg.userInput = userInput;
        dlg.open = function(dataItem, defaultUserInput) {
            dlg.dataItem = dataItem;
            userInput(typeof defaultUserInput === "undefined" ? "" : defaultUserInput);
            userInput.valueHasMutated();
            dlg.dialog("open");
            onOpen();
        }
        dlg.close = function() {
            dlg.dialog("close");
        }

        return dlg;

    }

    return {
        "doNothing": function() {},
        "uiCulture": "en", // Set on DOM ready
        "x": {}, // Set on DOM ready, Holds DOM injected Global Variables
        "defaults": defaults, "queryString": queryString, "regex": regex, "scrollIntoView": scrollIntoView, "focusFirstInput": focusFirstInput, "resFromHtml": resFromHtml,
        "setMask": { date: setDateMask, time: setTimeMask, decimal: setDecimalMask, phone: setPhoneMask, url: setUrlMask, email: setEmailMask, integer: setIntegerMask },
        "format": { dateToString: formatDateToString, timeToString: formatTimeToString, approvalStatus: formatApprovalStatus, nullableString: formatNullableString },
        "string": { isNullOrWhiteSpace: isNullOrWhiteSpace, contains: stringCotains, toNumber: formatStringToNumber, highlight: highlight },
        "globalString": { toLocal: localString },
        "fileInput": { matchExtension: matchFileExtension, clear: clearFileInput },
        "notification": { confirm: confirmNotification, progress: progressNotification, success: successNotification, error: errorNotification, kill: killNotification, browser: browserNotifications },
        "guid": guid,
        "ajaxSuccessHandler": ajaxSuccessHandler, "ajaxErrorHandler": ajaxErrorHandler, "ajax": ajax,
        "ajaxProgressHandler": ajaxProgressHandler, "ajaxUpload": ajaxUpload,
        "api": api,
        "ko": { observeEveryArray: observeEveryArray, addOptionToDummyGroup: addOptionToDummyGroup, separateSelections: separateSelections, toJS: koToJs, fromJS: koFromJs },
        "multiselect": multiselect, "datatable": datatable, "arr": arr, "dialog": dialog
    }

}()
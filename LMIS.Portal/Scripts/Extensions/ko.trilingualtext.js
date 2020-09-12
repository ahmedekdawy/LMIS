ko.extenders.trilingualText = function(target, options) {

    var isPopulated = false;
    var disableEvaluation = false;
    var isPhCleared = false;

    target.extendedAs = "trilingualText";
    target.dto = ko.observable();
    target.dto.target = target;
	target.Localized = ko.observable();
	target.A = ko.observable(), target.A.ph = ko.observable();
	target.B = ko.observable(), target.B.ph = ko.observable();
	target.C = ko.observable(), target.C.ph = ko.observable();
    target.English = "", target.French = "", target.Arabic = "";

    if (ko.validation)
        target.dto.extend({ required: options.required || false });

	function hasValue(str) {
		if (str === null) return false;
		return str !== undefined && str.match(/^\s*$/) === null;
	}

	function evaluate() {

	    if (disableEvaluation) return;

		var en = "", fr = "", ar = "";

		if (target.Localized()) {
			switch (options.uiCulture) {
				case "ar":
					ar = target.A();
					break;
				case "fr":
					fr = target.A();
					break;
				default:
					en = target.A();
					break;
			}
		} else {
			en = target.A();
			fr = target.B();
			ar = target.C();
		}

	    isPopulated = false;
	    target.English = en; target.French = fr; target.Arabic = ar;
	    target.dto(target.getValue(true));

	}

	target.LocalizeView = function(localize, clearPh) {

	    isPhCleared = clearPh === true || (clearPh === undefined && isPhCleared === true);
	    if (!options.uiCulture) return;
	    if (target.Localized() === localize) return;

	    try {

	        disableEvaluation = true;

	        if (localize) {

	            var localizedText, localizedPlaceholder;

	            switch (options.uiCulture) {
	                case "ar":
	                    if (!isPopulated) {
	                        target.English = "";
	                        target.French = "";
	                    }
	                    localizedText = target.Arabic;
	                    localizedPlaceholder = options.arPh;
	                    break;
	                case "fr":
	                    if (!isPopulated) {
	                        target.English = "";
	                        target.Arabic = "";
	                    }
	                    localizedText = target.French;
	                    localizedPlaceholder = options.frPh;
	                    break;
	                default:
	                    if (!isPopulated) {
	                        target.French = "";
	                        target.Arabic = "";
	                    }
	                    localizedText = target.English;
	                    localizedPlaceholder = options.enPh;
	                    break;
	            }

	            target.A(localizedText);
	            target.B("");
	            target.C("");
	            //target.A.ph(isPopulated ? "" : localizedPlaceholder);
	            target.A.ph(localizedPlaceholder);
	            target.B.ph("");
	            target.C.ph("");

	        } else {
	            target.A(target.English);
	            target.B(target.French);
	            target.C(target.Arabic);
	            //target.A.ph(isPopulated ? "" : options.enPh);
	            //target.B.ph(isPopulated ? "" : options.frPh);
	            //target.C.ph(isPopulated ? "" : options.arPh);
	            target.A.ph(options.enPh);
	            target.B.ph(options.frPh);
	            target.C.ph(options.arPh);
	        }

	        target.Localized(localize);

	    } finally {
	        if (isPhCleared) target.ClearPlaceholders();
	        disableEvaluation = false;
	    }

	}

	target.Populate = function(en, fr, ar) {

	    if (!options.uiCulture) return;

	    try {

	        disableEvaluation = true;

	        target.English = (typeof en == "string" ? en : "");
	        target.French = (typeof fr == "string" ? fr : "");
	        target.Arabic = (typeof ar == "string" ? ar : "");

	        if (target.Localized()) {
	            switch (options.uiCulture) {
	            case "ar":
	                target.A(target.Arabic);
	                break;
	            case "fr":
	                target.A(target.French);
	                break;
	            default:
	                target.A(target.English);
	                break;
                }
	            target.B("");
	            target.C("");
	        } else {
	            target.A(target.English);
	            target.B(target.French);
	            target.C(target.Arabic);
	        }

	    } finally {
	        isPopulated = true;
	        disableEvaluation = false;
	        target.dto(target.getValue(true));
	    }

	}

    target.Repopulate = function() {
        isPopulated = true;
    }

	target.ClearText = function() {
	    target.Populate("", "", "");
	    isPopulated = false;
	}

    target.ClearPlaceholders = function() {
        target.A.ph("");
        target.B.ph("");
        target.C.ph("");
    }

	target.isNullOrWhiteSpace = function() {
	    return !hasValue(target.English) && !hasValue(target.French) && !hasValue(target.Arabic);
	}

	target.getValue = function(nullable) {

	    var en = hasValue(target.English) ? target.English : null;
	    var fr = hasValue(target.French) ? target.French : null;
	    var ar = hasValue(target.Arabic) ? target.Arabic : null;

	    if (nullable === true && en === null && fr === null && ar === null) return null;

	    return { English: en, French: fr, Arabic: ar };

    }

	target.LocalizeView(true);
	target.A.subscribe(evaluate);
	target.B.subscribe(evaluate);
	target.C.subscribe(evaluate);

	return target;

}
//Dependencies: moment.js

ko.extenders.date = function (target, options) {

    target.extendedAs = "date";
    target.Value = null;
    target.dto = ko.observable();
    target.dto.target = target;

    if (ko.validation)
        target.dto.extend({ required: (options && options.required) || false });

    function evaluate() {
        var m = moment.utc(target(), options.dateFormat);
        if (m.isValid()) target.Value = m.toDate();
        else target.Value = null;
        target.dto(target.Value);
    }

    target.subscribe(evaluate);

    return target;

}

ko.extenders.time = function (target, options) {

    target.extendedAs = "time";
    target.Value = null;
    target.dto = ko.observable();
    target.dto.target = target;

    if (ko.validation)
        target.dto.extend({ required: (options && options.required) || false });

    function evaluate() {
        var m = moment.utc(target(), ["h:m a", "H:m"]);
        if (m.isValid())
            target.Value = moment.utc("01-01-1755 " + target(), ["DD-MM-YYYY h:m a", "DD-MM-YYYY H:m"]).toDate();
        else target.Value = null;
        target.dto(target.Value);
    }

    target.subscribe(evaluate);

    return target;

}
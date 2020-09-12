ko.extenders.fileInput = function (target, options) {

    target.extendedAs = "fileInput";
    target.dto = ko.observable();
    target.dto.target = target;

    if (ko.validation)
        target.dto.extend({ required: options.required || false });

    function evaluate() {
        target.dto(target() ? target().name || target() : undefined);
    }

    target.uploaded = ko.computed(function() {
        return target() && !target().name;
    });

    if (options.preview) {
        target.preview = ko.observable();
        target.subscribe(function (img) {

            if (img) {
                if (img.name) {
                    var reader = new FileReader();
                    reader.onload = function(e) {
                        target.preview("url(" + e.target.result + ")");
                    }
                    reader.readAsDataURL(img);
                } else if (typeof img === "string") {
                    target.preview("url(" + lmis.x.downloadURL + img + ")");
                } else target.preview("");
            } else target.preview("");

        });
    }

    target.subscribe(evaluate);

    return target;

}
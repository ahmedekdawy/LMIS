define(function () {

    var cardList = {};
    var controller = {};
    var cardId = 0;

    controller.cards = ko.observableArray();

    controller.addCard = function (name, widget, model) {

        var id = cardId++;
        var templateId, viewModel, args;

        if (widget && typeof widget === "string" && model && typeof model === "object") {
            templateId = widget;
            viewModel = model;
        } else if (widget && typeof widget === "object" && widget.template && widget.viewModel) {
            templateId = "card-" + lmis.guid().replace(".", "-");
            viewModel = widget.viewModel;
            args = model;

            var template = $("<script id='" + templateId + "' type='text/html'>");
            template.html(widget.template);
            $(document.body).append(template);
        }

        if ($("#" + (templateId || "")).length === 0) {
            templateId = "card-" + lmis.guid().replace(".", "-");
            viewModel = {};

            var defTemplate = $("<script id='" + templateId + "' type='text/html'>");
            defTemplate.html("<div class='under-constr' />");
            $(document.body).append(defTemplate);
        }

        if (args === undefined || args === null) args = {};

        if (typeof viewModel === "function") {
            viewModel.prototype.mode = ko.observable(args.mode || "p");
            viewModel.prototype.dataUpdateHandler = function (propName, emptyVal) {
                return function () {
                    viewModel.resettingData = true;
                    if (propName && viewModel.dataSet) {
                        var newVal = viewModel.dataSet[propName];
                        if (viewModel.mode() === "v" && emptyVal && (newVal === undefined || newVal === null || lmis.string.isNullOrWhiteSpace(newVal))) newVal = emptyVal;
                        viewModel[propName](newVal);
                    }
                    viewModel.resettingData = false;
                }
            };
        }
        viewModel = typeof viewModel === "function" ? new viewModel(args) : viewModel;
        viewModel.cardList = cardList;

        var isDirty = ko.observable();
        if (ko.isObservable(viewModel.dto))
            viewModel.dto.subscribe(function(newVal) {
                isDirty(!viewModel.resettingData && (isDirty() || newVal !== false));
            });

        viewModel.reset = function(data) {
            viewModel.resettingData = true;
            var propName = viewModel.dtoPropertyName;
            if (data) viewModel.dataSet = propName ? data[propName] : data;
            if (viewModel.mode() !== "p") lmis.ko.fromJS(viewModel, viewModel.dataSet);
            if (viewModel.mode() === "e") viewModel.mode("v");
            viewModel.resettingData = false;
            isDirty(false);
        }

        var isValidated = ko.computed(function() {
            var dto = viewModel ? viewModel.dto : undefined;
            return typeof dto === "undefined" || (typeof dto === "function" && dto());
        });

        viewModel.mode.subscribe(function (newVal) {
            var eid = "#" + templateId + id + " ";
            var disabled = newVal === "v";
            $(eid + ":text").css("background-color", "white");
            $(eid + ".always-white").css("background-color", "white");
            $(eid + ":input").attr("disabled", disabled);
            $(eid + ".always-disabled").attr("disabled", true);
            $(eid + ".always-enabled").attr("disabled", false);
        });

        var css = ko.computed(function() {
            var m = viewModel.mode() || "";
            switch (m.toLowerCase()) {
            case "e":
                return isValidated() ?  (isDirty() ? "panel-warning" : "panel-success") : "panel-danger";
            case "v":
                return isDirty() ? "panel-warning" : "panel-default";
            default:
                return "panel-default";
            }
        });

        var card = {
            id: id,
            name: name,
            template: templateId,
            viewModel: viewModel,
            cardList: cardList,
            isEditable: ko.observable(args.editable === true),
            isDirty: isDirty,
            isValidated: isValidated,
            mode: viewModel.mode,
            css: css,
            onEdit: function() { viewModel.mode("e"); },
            onReset: function () { viewModel.reset(); },
            onAccept: function () { viewModel.mode("v"); }
        };
        
        controller.cards.push(card);

        return card;

    };
    controller.getCard = function (name) {

        if (typeof name === "object") return name;

        var idxLookup;

        if (typeof name === "number")
            idxLookup = controller.cards().map(function (c) { return c.id; });
        else if (typeof name === "string")
            idxLookup = controller.cards().map(function (c) { return c.name; });
        else
            idxLookup = [];

        return controller.cards()[idxLookup.indexOf(name)];

    };

    controller.dataSet = ko.observable();
    controller.dataSet.subscribe(function(data) {
        controller.cards().forEach(function(c) {
            c.viewModel.reset(data);
        });
    });
    controller.isDirty = ko.computed(function () {
        return controller.cards().some(function (c) {
            return c.isDirty();
        });
    });
    controller.isValidated = ko.computed(function () {
        return controller.cards().every(function (c) {
            return c.isValidated();
            });
        });
    controller.canSubmit = controller.isValidated;
    controller.extend = function (vm, root) {
        cardList = vm;
        $.extend(cardList, controller);
        if (root) {
            root.cardList = cardList;
            cardList.root = root;
        }
    };
    controller.dto = function () {
        var ret = {};
        var allCardsReady = controller.cards().every(function (s) {
            var dto = typeof s.viewModel.dto === "function" ? s.viewModel.dto() : true;
            if (!dto) return false;
            if (dto !== true) $.extend(ret, dto);
            return true;
        });
        return allCardsReady ? ret : false;
    };

    controller.postDOM = function() {

        controller.cards().forEach(function (c) {
            if (c.viewModel.postDOM) c.viewModel.postDOM();
            c.mode.valueHasMutated(); 
        });

    };

    return controller;

});
define(function () {

    var wizard = {};
    var controller = {};
    var stepId = 0;

    controller.steps = ko.observableArray();
    controller.currentStep = ko.observable();
    controller.currentStep.subscribe(function (newStep) {
        wizard.vmx = (newStep || {}).viewModel;
        window.vmx = wizard.vmx;
    });

    controller.addStep = function (name, widget, model) {

        var id = stepId++;
        var templateId, viewModel, args;

        if (widget && typeof widget === "string" && model && typeof model === "object") {
            templateId = widget;
            viewModel = model;
        } else if (widget && typeof widget === "object" && widget.template && widget.viewModel) {
            templateId = "wiz-" + lmis.guid().replace(".", "-");
            viewModel = widget.viewModel;
            args = model;

            var template = $("<script id='" + templateId + "' type='text/html'>");
            template.html(widget.template);
            $(document.body).append(template);
        }

        if ($("#" + (templateId || "")).length === 0)
         {
            templateId = "wiz-" + lmis.guid().replace(".", "-");
            viewModel = {};

            var defTemplate = $("<script id='" + templateId + "' type='text/html'>");
            defTemplate.html("<div class='under-constr' />");
            $(document.body).append(defTemplate);
        }

        if (args === undefined || args === null) args = {};

        if (typeof viewModel === "function") {
            viewModel.prototype.mode = ko.observable(args.mode || "p");
            viewModel.prototype.dataUpdateHandler = function () { return lmis.doNothing; };
        }
        viewModel = typeof viewModel === "function" ? new viewModel(args) : viewModel;
        viewModel.wizard = wizard;

        var step = {
            id: id,
            name: name,
            template: templateId,
            viewModel: viewModel,
            wizard: wizard,
            isActive: ko.computed(function () {
                return step === controller.currentStep();
            }),
            isAvailable: ko.computed(function () {
                for (var i = 0; i < id; i++)
                    if (controller.steps()[i] && !(controller.steps()[i]).isValidated()) {
                        return false;
                    }
                return true;
            }),
            isValidated: ko.computed(function () {
                var dto = viewModel ? viewModel.dto : undefined;
                return typeof dto === "undefined" || (typeof dto === "function" && dto());
            })
        };

        controller.steps.push(step);

        return step;

    };
    controller.getStep = function (name) {

        if (typeof name === "object") return name;

        var idxLookup;

        if (typeof name === "number")
            idxLookup = controller.steps().map(function (s) { return s.id; });
        else if(typeof name === "string")
            idxLookup = controller.steps().map(function (s) { return s.name; });
        else
            idxLookup = [];

        return controller.steps()[idxLookup.indexOf(name)];

    };

    controller.isValidated = ko.computed(function () {
        return controller.steps().every(function(s) {
            return s.isValidated();
        });
    });
    controller.currentIndex = ko.computed(function () {
        return controller.steps.indexOf(controller.currentStep());
    });
    controller.atFirstStep = ko.computed(function () {
        return controller.currentIndex() < 1;
    });
    controller.atLastStep = ko.computed(function () {
        return controller.currentIndex() >= controller.steps().length - 1;
    });
    controller.canSubmit = ko.computed(function () {
        return controller.atLastStep() && controller.isValidated();
    });
    controller.canGoNext = ko.computed(function () {
        return !controller.atLastStep() && (!controller.currentStep() || controller.currentStep().isValidated());
    });
    controller.goNext = function (autoFocus) {
        controller.go(controller.steps()[controller.currentIndex() + 1], autoFocus);
    };
    controller.goLast = function (autoFocus) {
        controller.go(controller.steps()[controller.steps().length - 1], autoFocus);
    };
    controller.goPrevious = function (autoFocus) {
        controller.go(controller.steps()[controller.currentIndex() - 1], autoFocus);
    };
    controller.goFirst = function (autoFocus) {
        controller.go(controller.steps()[0], autoFocus);
    };
    controller.go = function (name, autoFocus) {

        var requestStep = controller.getStep(name);

        if (typeof requestStep === "undefined") return;

        var currentIdx = controller.currentIndex();
        var requestIdx = controller.steps.indexOf(requestStep);
        var newIdx = requestIdx;

        for (var i = 0; i <= requestIdx; i++)
            if (controller.steps()[i] && !(controller.steps()[i]).isValidated()) {
                newIdx = i; break;
            }

        if (newIdx === currentIdx) return;
        controller.currentStep(controller.steps()[newIdx]);

        lmis.scrollIntoView();
        if (autoFocus !== false && !controller.currentStep().isValidated()) lmis.focusFirstInput();
        if (typeof controller.currentStep().viewModel.postDOM === "function")
            setTimeout(controller.currentStep().viewModel.postDOM, 1);

    };
    controller.go.f = function(name, autoFocus) {
        return function() {
            controller.go(name, autoFocus);
        }
    }
    controller.extend = function (vm, root) {
        wizard = vm;
        $.extend(wizard, controller);
        if (root) {
            root.wizard = wizard;
            wizard.root = root;
        }
    };
    controller.dto = function () {
        var ret = {};
        var allStepsReady = controller.steps().every(function (s) {
            var dto = typeof s.viewModel.dto === "function" ? s.viewModel.dto() : true;
            if (!dto) return false;
            if (dto !== true) $.extend(ret, dto);
            return true;
        });
        return allStepsReady ? ret : false;
    };

    return controller;

});
(function ($) {
    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
              .css({ position: "relative", display: "inline-block", width: "100%", "padding-right": "2.2em" })
              .insertAfter(this.element);

            var src = this.element;
            this._createAutocomplete();
            this._createShowAllButton();

            src.hide();
            $(src).on("cbChange", function (e, newVal, options) {
                var selected = options().filter(function (i) { return i.id === newVal; });
                var txt = newVal ? (selected[0] && selected[0].desc) || "" : "";
                if ($(this).data("customCombobox").input[0].value !== txt)
                    $($(this).data("customCombobox").input[0]).val(txt);
            });
        },

        _createAutocomplete: function () {
            var selected = this.element.children(":selected"),
              value = selected.val() ? selected.text() : "";

            this.input = $("<input>")
                .appendTo(this.wrapper)
                .val(value)
                .attr("title", "")
                .css({ margin: "0", padding: "5px 10px", width: "100%" })
                .addClass("ui-widget ui-widget-content ui-state-default ui-corner-left notClearable")
                .autocomplete({
                    delay: 0,
                    minLength: 0,
                    source: $.proxy(this, "_source")
                })
                .tooltip({
                    tooltipClass: "ui-state-highlight"
                });

            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    this._trigger("select", event, {
                        item: ui.item.option
                    });
                    $(this.element[0]).trigger("change");
                },

                autocompletechange: "_removeIfInvalid",

                blur: "_removeIfInvalid"
            });

            return this.input;
        },

        _createShowAllButton: function () {
            var input = this.input,
              wasOpen = false;

            return $("<a>")
              .attr("tabIndex", -1)
              .attr("title", "")
              .css({ position: "absolute", top: "0", bottom: "0", padding: "0", "margin-left": "-1px", width: "2.2em" })
              .tooltip()
              .appendTo(this.wrapper)
              .button({
                  icons: {
                      primary: "ui-icon-triangle-1-s"
                  },
                  text: false
              })
              .removeClass("ui-corner-all")
              .addClass("ui-corner-right")
              .mousedown(function () {
                  wasOpen = input.autocomplete("widget").is(":visible");
              })
              .click(function () {
                  input.focus();

                  // Close if already visible
                  if (wasOpen) {
                      return;
                  }

                  // Pass empty string as value to search for, displaying all results
                  input.autocomplete("search", "");
              });
        },

        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
                return undefined;
            }));
        },

        _removeIfInvalid: function (event, ui) {

            // Selected an item, nothing to do
            if (ui && ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = this.input.val(),
              valueLowerCase = value.toLowerCase(),
              valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
                return undefined;
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            // Remove invalid value
            this.input.val("");
              //.attr("title", value + " not found")
              //.tooltip("open");
            this.element.val("");
            $(this.element[0]).trigger("change");
            //this._delay(function () {
            //    this.input.tooltip("close").attr("title", "");
            //}, 2500);
            this.input.autocomplete("instance").term = "";
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });
})(jQuery);

ko.bindingHandlers.searchableCombo = {
    init: function(element, valueAccessor, allBindingsAccessor) {
        // Apply jQuery plugin
        $(element).combobox();

        // Subscribe to the value binding, and trigger the change event.
        allBindingsAccessor().value.subscribe(function(newVal) {
            $(element).trigger("cbChange", [newVal, allBindingsAccessor().options]);
        });

        // Clean up jQuery plugin on dispose
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            // This will be called when the element is removed by Knockout or
            // if some other part of your code calls ko.removeNode(element)
            $(element).combobox("destroy");
        });
    }
};
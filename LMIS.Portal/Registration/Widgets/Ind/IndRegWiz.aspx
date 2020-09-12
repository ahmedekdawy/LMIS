<!-- ko with: currentStep -->
<h2 data-bind="text: id + ': ' + name"></h2>
<div data-bind="template: { name: template, data: viewModel }"></div>
<!-- /ko -->
<hr />
<button data-bind="click: goPrevious, enable: !atFirstStep()">Previous</button>
<button data-bind="click: goNext, visible: !atLastStep()">Next</button>

<script id="basicTmpl" type="text/html">
    <div data-bind="text: message"></div>
</script>

<script id="choiceTmpl" type="text/html">
    <input type="checkbox" data-bind="checked: choiceOne" />Choice One
    <br />
    <input type="checkbox" data-bind="checked: choiceTwo" />Choice Two
</script>

<script id="confirmTmpl" type="text/html">
    <button data-bind="click: confirm">Confirm</button>
</script>
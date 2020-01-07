ko.bindingHandlers.caseAmount = {
    update: function (element, valueAccessor) {
        // retrieve observable value
        var value = ko.utils.unwrapObservable(valueAccessor()) || 0;
        //convert to number of string
        value = + value;
        //format currency
        var formattedText = "$" + value.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
        //apply formatted text to the underlying DOM element
        $(element).text(formattedText.split(".")[0]);
    }
};
ko.bindingHandlers.boardAmount = {
    update: function (element, valueAccessor) {
        // retrieve observable value
        var value = ko.utils.unwrapObservable(valueAccessor()) || 0;
        //convert to number of string
        value = + value;
        //format currency
        var formattedText = value.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
        //apply formatted text to the underlying DOM element
        $(element).text(formattedText.split(".")[0]);
    }
};
ko.bindingHandlers.bindHTML = {
    init: function () {
        // we will handle the bindings of any descendants
        return { controlsDescendantBindings: true };
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // must read the value so it will update on changes to the value
        var value = ko.utils.unwrapObservable(valueAccessor());
        // create the child html using the value
        ko.applyBindingsToNode(element, { html: value });
        // apply bindings on the created html
        ko.applyBindingsToDescendants(bindingContext, element);
    }
};
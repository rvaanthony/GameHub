$(document).ready(function () {
    ko.utils.extend(this, new AjaxHelper());
    ko.components.register('loading-component', {
        viewModel: LoadingViewModel,
        template: {
            element: 'Loading_Component'
        }
    });
    ko.components.register('error-component', {
        viewModel: ErrorViewModel,
        template: {
            element: 'Error_Component'
        }
    });
});

function getUrlVars() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi,
        function (m, key, value) {
            vars[key] = value;
        });
    return vars;
}

function getNumberLength(number) {
    return number.toString().length;
}
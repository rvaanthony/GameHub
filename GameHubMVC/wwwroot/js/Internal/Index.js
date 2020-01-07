var IndexViewModel = function (params) {
    var self = this;
    self.loading = ko.observable(true);
    self.error = ko.observable(false);
    self.errorMsg = ko.observable();
    var notyf = new Notyf();

    self.UINotification = function (type, msg) {
        if (type === 'alert') {
            notyf.alert(msg === null ? "Something went wrong, please reload page." : msg);
            return;
        }
        notyf.confirm(msg);
        return;
    };

    self.PageLoad = function () {
    };

    $(document).ready(function () {
        self.PageLoad();
    });
};

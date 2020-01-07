var DashboardViewModel = function (params) {
    var notify = new Notyf();
    var self = this;
    self.loading = ko.observable(true);
    self.error = ko.observable(false);
    self.errorMsg = ko.observable();

    self.UINotification = function (type, msg) {
        if (type === "alert") {
            notify.alert(msg === null ? "Something went wrong, please reload page." : msg);
            return;
        }
        notify.confirm(msg);
        return;
    };

    self.PageLoad = function () {
    };

    $(document).ready(function () {
        self.PageLoad();
    });
};
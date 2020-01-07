var JobsViewModel = function (params) {
    var self = this;
    self.loading = ko.observable(true);
    self.invalidPage = ko.observable(false);
    self.error = ko.observable(false);
    self.errorMsg = ko.observable();
    self.clientId = ko.observable();
    self.jobListings = ko.observableArray();
    var notyf = new Notyf(); 

    self.GetJobs = function () {
        AjaxCall('/api/job/listings', 'POST', { clientGId: self.clientId(), active: true }).done(function (response) {
            if (response.errorFlag) {
                self.UINotification('alert', null);
                return;
            }
            ko.utils.arrayForEach(response.list, function (item) {
                self.jobListings.push(new JobListingViewModel(self, item));
            });
            self.loading(false);
        });
    };

    self.UINotification = function (type, msg) {
        if (type === 'alert') {
            notyf.alert(msg === null ? "Something went wrong, please reload page." : msg);
            return;
        }
        notyf.confirm(msg);
        return;
    };

    self.InvalidPage = function () {
        self.invalidPage(true);
        self.error(true);
        self.loading(false);
    };

    self.SetIncomingParams = function () {
        if (params !== undefined)
            self.clientId(params.clientId);
        else
            self.InvalidPage();
    };

    self.PageLoad = function () {
        self.SetIncomingParams();
        if (self.clientId() === undefined) {
            self.InvalidPage();
            return;
        }

        self.GetJobs();
    };

    $(document).ready(function () {
        self.PageLoad();
    });
};

var JobListingViewModel = function (parent, data) {
    var self = this;
    var inner = {};
    self.id = ko.observable();
    self.clientGId = ko.observable();
    self.created = ko.observable();
    self.name = ko.observable();
    self.description = ko.observable();
    self.info = ko.observable();
    self.status = ko.observable();
    self.showDetails = ko.observable(false);
    self.error = ko.observable(false);

    self.ToggleDetails = function () {
        self.showDetails(!self.showDetails());
    };

    self.ApplyToJob = function () {
        window.location = "/iframe/apply";
    };

    self.SetIncomingParams = function () {
        if (data === undefined || data === null) {
            self.error(true);
            return;
        }

        self.id(data.id);
        self.clientGId(data.clientGId);
        self.created(data.created);
        self.name(data.name);
        self.description(data.description);
        self.info(data.info);
        self.status(data.status);
    };

    self.UINotification = function (type, msg) {
        parent.UINotification(type, msg);
    };

    self.PageLoad = function () {
        self.SetIncomingParams();
    };

    $(document).ready(function () {
        self.PageLoad();
    });
};
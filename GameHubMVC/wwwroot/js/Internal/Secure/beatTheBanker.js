var BeatTheBankerViewModel = function (params) {
    var notify = new Notyf();
    var self = this;
    var inner = { openCount: 0 };
    self.loading = ko.observable(false);
    self.error = ko.observable(false);
    self.errorMsg = ko.observable();
    self.onMainMenu = ko.observable(true);
    self.gameInfo = ko.observable();
    self.game = ko.observable({ html: "" });
    self.boardInfo = ko.observableArray();
    self.leftBoard = ko.observableArray();
    self.rightBoard = ko.observableArray();
    self.selectedCase = ko.observable("");
    self.selectedCaseId = ko.observable();
    self.bankOffer = ko.observable({ amount: 0 });
    self.gameOver = ko.observable(false);
    self.swapAccepted = ko.observable({ swapped: ko.observable(false), guid: "" });
    self.ruleNumber = ko.observable(1);
    self.ruleInfo = ko.observable("Choose a case for your own");
    self.acceptedOffer = ko.observable(false);
    self.leaderBoard = ko.observableArray();
    self.showLeaderBoard = ko.observable(false);

    $("#bankerOfferModal").on("hidden.bs.modal", function () {
        if (!self.acceptedOffer())
            self.DeclineOffer();
    });

    self.NextRule = function() {
        if (self.ruleNumber() === 3)
            return;
        self.ruleNumber(self.ruleNumber() + 1);
    };

    self.NewGame = function() {
        AjaxCall('../api/btb/new/game', 'POST', null).done(function (response) {
            if (response.errorFlag) {
                self.UINotification('alert', null);
                return;
            }
            self.gameInfo(response);
            self.LoadGameComponents(self.gameInfo().gameGuid);
        });
    };

    self.ShowLeaderboard = function() {
        self.GetLeaderBoard();
        self.showLeaderBoard(true);
        self.onMainMenu(false);
    };

    self.GetLeaderBoard = function() {
        AjaxCall('../api/playfab/34997/leaderboard/MoneyWon', 'GET', null).done(function (response) {
            if (response.errorFlag) {
                self.UINotification('alert', null);
                return;
            }
            self.leaderBoard(response.data.leaderboard);
            console.log(self.leaderBoard());
        });
    };

    self.LoadGameComponents = function (gameId) {
        var gameParams = "parent: $data, gameId: '" + gameId + "'";
        self.game({ html: "<game-component params=\"" + gameParams + "\"></game-component>" });
        self.BindComponents();
        self.onMainMenu(false);
        //$('#ruleModal').modal();
    };

    self.GetBoardInfo = function() {
        AjaxCall('../api/btb/game/board', 'GET', null).done(function (response) {
            if (response.errorFlag) {
                self.UINotification('alert', null);
                return;
            }
            self.boardInfo(response.list);
            self.SetBoard();
        });
    };

    self.AcceptOffer = function () {
        self.acceptedOffer(true);
        AjaxCall('../api/btb/accept/offer', 'POST', self.bankOffer()).done(function (response) {
            if (response.errorFlag) {
                self.UINotification('alert', null);
                return;
            }
            self.gameOver(true);
            $('#bankerOfferModal').modal('hide');
        });
    };

    self.DeclineOffer = function () {
        AjaxCall('../api/btb/decline/offer', 'POST', self.bankOffer()).done(function (response) {
            if (response.errorFlag) {
                self.UINotification('alert', null);
                return;
            }
            $('#bankerOfferModal').modal('hide');
            if (inner.openCount === 19)
                $('#swapCaseModal').modal();
        });
    };

    self.AcceptSwap = function () {
        AjaxCall('../api/btb/swap/final/case', 'POST', self.gameInfo()).done(function (response) {
            if (response.errorFlag) {
                self.UINotification('alert', null);
                return;
            }
            self.BuildChosenCase({ caseNumber: ko.observable(response.caseNumber), caseGuid: ko.observable(response.guid) });
            self.swapAccepted({ swapped: ko.observable(true), guid: ko.observable(response.guid) });
            $('#swapCaseModal').modal('hide');
            self.gameOver(true);
        });
    };

    self.SetBoard = function () {
        var counter = 0;
        ko.utils.arrayForEach(self.boardInfo(), function (item) {
            if (counter > 9)
                self.rightBoard.push(new BoardItemModel(self, item));
            else
                self.leftBoard.push(new BoardItemModel(self, item));
            counter++;
        });
    };

    self.CaseOpened = function (data) {
        inner.openCount++;
        if (self.selectedCase() === "") {
            self.BuildChosenCase(data);
            return;
        }
        var itemFound = false;
        ko.utils.arrayForEach(self.rightBoard(), function (item) {
            if (item.amount() === data.caseAmount) {
                item.UpdateCss();
                itemFound = true;
            }
        });
        if (!itemFound)
            ko.utils.arrayForEach(self.leftBoard(), function (item) {
                if (item.amount() === data.caseAmount) {
                    item.UpdateCss();
                    itemFound = true;
                }
            });
        if (inner.openCount === 19)
            self.gameOver(true);
    };

    self.BuildChosenCase = function (data) {
        var caseParams = "parent: $root, caseNumber: " + data.caseNumber() + ", guid: '" + data.caseGuid() + "'";
        var html = " <case-component params=\"" + caseParams + "\"></case-component>";
        self.selectedCase(html);
        self.BindComponents();
        self.selectedCaseId(data.caseGuid());
    };

    self.BindComponents = function () {
        self.UnbindComponents();
        ko.components.register('game-component', {
            viewModel: BeatTheBankerModel,
            template: {
                element: 'Beat_The_Banker_Game_Component'
            }
        });
        ko.components.register('case-component', {
            viewModel: CaseModel,
            template: {
                element: 'Case_Component'
            }
        });
    };

    self.UnbindComponents = function () {
        ko.components.unregister('game-component');
        ko.components.unregister('case-component');
    };

    self.UINotification = function (type, msg) {
        if (type === "alert") {
            notify.alert("alert", msg === null ? "Something went wrong, please reload page." : msg);
            return;
        }
        notify.confirm("confirm", msg);
        return;
    };

    self.PageLoad = function () {
        self.GetBoardInfo();
    };

    $(document).ready(function () {
        self.PageLoad();
    });
};

var BoardItemModel = function (parent, item) {
    var self = this;
    self.amount = ko.observable(item.amount);
    self.cssClass = ko.observable("row activeCase");

    self.UpdateCss = function () {
        if (parent.gameOver())
            return;
        self.cssClass("row openedCase");
        setTimeout(function () {
            self.cssClass("row inactiveCase");
        }, 1000); 
    };
};
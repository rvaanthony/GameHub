namespace GameHub.Classes
{
    public class Enums
    {
        public enum UserStatus
        {
            New = 1,
            Active = 2,
            Inactive = 3,
            Suspended = 4
        }

        public enum LoginMethod
        {
            B2C_1_SUSI = 1
        }

        public enum HttpMethodType
        {
            GET = 1,
            POST = 2,
            PATCH = 3,
            PUT = 4,
            DELETE = 5
        }

        public enum TokenStatus
        {
            Active = 5,
            Revoked = 6,
            Inactive = 7,
            Expired = 8
        }

        public enum TrackingActionType
        {
            UserCreated = 1,
            FirstNameChange = 2,
            LastNameChange = 3,
            UserEmailChange = 4,
            UsernameChange = 5,
            PlayFabLogin = 6,
            TokenAcquired = 7,
            TokenRevoked = 8,
            BTBCaseChosen = 9,
            BTBCaseOpened = 10,
            BTBBankOffer = 11,
            BTBDeclinedBankOffer = 12,
            BTBAcceptedBankOffer = 13,
            CurrencyAdded = 14,
            PlayFabUpdateUserStats = 15,
            PlayFabGetLeaderboard = 16,
            PlayFabUserCreated = 17,
            Login = 18
        }

        public enum TableEntity
        {
            tblUser = 1,
            tblToken = 2,
            tblBTBGame = 3,
            tblBTBGameCase = 4,
            tblBTBGameBankerOffer = 5,
            tblBTBGameChoosenCase = 6,
            tblPlayFabUser = 7,
            tblLogin = 8
        }

        public enum SeverityCode
        {
            Low = 1,
            Medium = 2,
            High = 3,
            Critical = 4
        }

        public enum BTBStatusType
        {
            Started = 9,
            Completed = 10,
            Active = 11,
            Swapped = 12,
            Accepted = 13,
            Declined = 14,
            Pending = 15,
            New = 16,
            Opened = 17,
            Selected = 18
        }

        public enum BTBGameResult
        {
            BankOfferAccepted = 1,
            CaseKept = 2
        }
    }

}

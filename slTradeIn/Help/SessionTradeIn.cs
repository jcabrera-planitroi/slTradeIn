namespace slTradeIn.Help
{
    public interface ISessionValueProvider
    {
        string iUserID { get; set; }
        string iCartID { get; set; }
        string vUserEmail { get; set; }
        string vUserName { get; set; }
        string iLogisticID { get; set; }
        string bSerializedReport { get; set; }
        string iShippingType { get; set; }

        string iCategoryID { get; set; }
        string iManufacturerID { get; set; }
        string iModelNumberID { get; set; }
        string bIsAdmin { get; set; }
        string bIsTechPartner { get; set; }
        string vVerificationCode { get; set; }

        string iManualQuoteID { get; set; }
    }

    public static class SessionTradeIn
    {
        private static ISessionValueProvider _sessionValueProvider;
        private static readonly object lockObject = new object();

        public static void Initialize(ISessionValueProvider provider)
        {
            if (_sessionValueProvider == null)
            {
                lock (lockObject)
                {
                    if (_sessionValueProvider == null)
                    {
                        _sessionValueProvider = provider;
                    }
                }
            }
        }

        public static void SetSessionValueProvider(ISessionValueProvider provider)
        {
            _sessionValueProvider = provider;
        }

        public static string iUserID
        {
            get { return _sessionValueProvider.iUserID; }
            set { _sessionValueProvider.iUserID = value; }
        }
        public static string vUserName
        {
            get { return _sessionValueProvider.vUserName; }
            set { _sessionValueProvider.vUserName = value; }
        }

        public static string vUserEmail
        {
            get { return _sessionValueProvider.vUserEmail; }
            set { _sessionValueProvider.vUserEmail = value; }
        }

        public static string iCartID
        {
            get { return _sessionValueProvider.iCartID; }
            set { _sessionValueProvider.iCartID = value; }
        }

        public static string iLogisticID
        {
            get { return _sessionValueProvider.iLogisticID; }
            set { _sessionValueProvider.iLogisticID = value; }
        }
        public static string iShippingType
        {
            get { return _sessionValueProvider.iShippingType; }
            set { _sessionValueProvider.iShippingType = value; }
        }
        public static string bSerializedReport
        {
            get { return _sessionValueProvider.bSerializedReport; }
            set { _sessionValueProvider.bSerializedReport = value; }
        }

        public static string iCategoryID
        {
            get { return _sessionValueProvider.iCategoryID; }
            set { _sessionValueProvider.iCategoryID = value; }
        }

        public static string iManufacturerID
        {
            get { return _sessionValueProvider.iManufacturerID; }
            set { _sessionValueProvider.iManufacturerID = value; }
        }
        public static string iModelNumberID
        {
            get { return _sessionValueProvider.iModelNumberID; }
            set { _sessionValueProvider.iModelNumberID = value; }
        }

        public static string bIsAdmin
        {
            get { return _sessionValueProvider.bIsAdmin; }
            set { _sessionValueProvider.bIsAdmin = value; }
        }

        public static string bIsTechPartner
        {
            get { return _sessionValueProvider.bIsTechPartner; }
            set { _sessionValueProvider.bIsTechPartner = value; }
        }

        public static string vVerificationCode
        {
            get { return _sessionValueProvider.vVerificationCode; }
            set { _sessionValueProvider.vVerificationCode = value; }
        }
        public static string iManualQuoteID
        {
            get { return _sessionValueProvider.iManualQuoteID; }
            set { _sessionValueProvider.iManualQuoteID = value; }
        }

        public class WebSessionValueProvider : ISessionValueProvider
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            private const string _iUserID = "slTradeInSite_iUserID";
            private const string _vUserEmail = "slTradeInSite_vUserEmail";
            private const string _vUserName = "slTradeInSite_vUserName";
            private const string _iCartID = "slTradeInSite_iCartID";
            private const string _iLogisticID = "slTradeInSite_iLogisticID";
            private const string _iShippingType = "slTradeInSite_iShippingType";
            private const string _bSerializedReport = "slTradeInSite_bSerializedReport";
            private const string _iCategoryID = "slTradeInSite_iCategoryID";
            private const string _iManufacturerID = "slTradeInSite_iManufacturerID";
            private const string _iModelNumberID = "slTradeInSite_iModelNumberID";
            private const string _bIsAdmin = "slTradeInSite_bIsAdmin";
            private const string _bIsTechPartner = "slTradeInSite_bIsTechPartner";
            private const string _vVerificationCode = "slTradeInSite_vVerificationCode";
            private const string _iManualQuoteID = "slTradeInSite_iManualQuoteID";

            public WebSessionValueProvider(IHttpContextAccessor httpContextAccessor) =>
                _httpContextAccessor = httpContextAccessor;

            public string iUserID
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iUserID) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iUserID, value); }
            }
            public string vUserEmail
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_vUserEmail) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_vUserEmail, value); }
            }
            public string vUserName
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_vUserName) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_vUserName, value); }
            }

            public string iCartID
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iCartID) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iCartID, value); }
            }

            public string iLogisticID
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iLogisticID) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iLogisticID, value); }
            }
            public string iShippingType
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iShippingType) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iShippingType, value); }
            }
            public string bSerializedReport
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_bSerializedReport) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_bSerializedReport, value); }
            }
            public string iCategoryID
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iCategoryID) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iCategoryID, value); }
            }
            public string iManufacturerID
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iManufacturerID) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iManufacturerID, value); }
            }
            public string iModelNumberID
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iModelNumberID) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iModelNumberID, value); }
            }
            public string bIsAdmin
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_bIsAdmin) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_bIsAdmin, value); }
            }
            public string bIsTechPartner
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_bIsTechPartner) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_bIsTechPartner, value); }
            }
            public string vVerificationCode
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_vVerificationCode) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_vVerificationCode, value); }
            }

            public string iManualQuoteID
            {
                get { return _httpContextAccessor.HttpContext?.Session.GetString(_iManualQuoteID) ?? ""; }
                set { _httpContextAccessor.HttpContext?.Session.SetString(_iManualQuoteID, value); }
            }
        }
    }
}
namespace callRecords.Models
{
    /// <summary>
    /// MSAL Config POCO
    /// </summary>
    public  class GENConfig
    {
        public string NotificationType { get; set; }
        public int ThresholdLimit { get; set; }
        public bool SendOnlyWhenThresholdExceeded {get; set; }
        public string TeamsWebHook {get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string TenantID {get; set; }

    }
}
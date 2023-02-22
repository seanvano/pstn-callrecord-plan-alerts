namespace callRecords.Models
{
    /// <summary>
    /// MSAL Config POCO
    /// </summary>
    public  class GENConfig
    {
        public string NotificationType { get; set; }
        public int ThresholdLimit { get; set; }
        public string TeamsWebHook {get; set; }
    }
}
namespace callRecords.Models
{
    /// <summary>
    /// MSAL Config POCO
    /// </summary>
    public  class MSALConfig
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string TenantID {get; set; }
    }
}
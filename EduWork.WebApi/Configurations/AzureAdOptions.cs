namespace EduWork.WebApi.Configuration
{
    public record AzureAdOptions
    {
        public const string Section = "AzureAd";
        public string Instance { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string CallbackPath { get; set; } = string.Empty;
        public string Scopes { get; set; } = string.Empty;
    }
}

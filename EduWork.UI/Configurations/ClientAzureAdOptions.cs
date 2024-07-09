namespace EduWork.UI.Configurations
{
    public class ClientAzureAdOptions
    {
        public const string Section = "AzureAd";

        public string BaseUrl { get; set; } = string.Empty;

        public string Scope {  get; set; } = string.Empty;
    }
}

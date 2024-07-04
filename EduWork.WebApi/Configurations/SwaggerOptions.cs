namespace EduWork.WebApi.Configuration
{
    public record SwaggerOptions
    {
        public const string Section = "SwaggerAzureAd";
        public string AuthorizationUrl { get; set; } = string.Empty;
        public string TokenUrl { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
    }
}

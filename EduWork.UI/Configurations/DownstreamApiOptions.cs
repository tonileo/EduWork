namespace EduWork.UI.Configurations
{
    public class DownstreamApiOptions
    {
        public const string Section = "DownstreamApi";

        public string BaseUrl { get; set; } = string.Empty;

        public string Scope {  get; set; } = string.Empty;
    }
}

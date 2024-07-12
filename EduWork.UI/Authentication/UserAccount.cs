using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace EduWork.UI.Authentication
{
    public class UserAccount : RemoteUserAccount
    {
        [JsonPropertyName("amr")]
        public string[]? AuthenticationMethod { get; set; }
    }
}

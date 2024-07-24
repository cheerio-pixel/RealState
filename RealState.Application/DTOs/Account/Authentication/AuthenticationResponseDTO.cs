using System.Text.Json.Serialization;

using RealState.Application.DTOs.User;

namespace RealState.Application.DTOs.Account.Authentication
{
    public class AuthenticationResponseDTO
    {
        public ApplicationUserDTO CurrentUser { get; set; } = null!;

        public bool Success { get; set; }

        public string? Error { get; set; }

        public string JWToken { get; set; } = null!;

        [JsonIgnore]
        public string RefreshToken { get; set; } = null!;
    }
}

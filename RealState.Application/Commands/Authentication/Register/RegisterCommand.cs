using System.Text.Json.Serialization;

using MediatR;

using RealState.Application.DTOs.Role;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.Authentication.Register
{
    public class RegisterCommand
    : IRequest<ApplicationUserDTO>
    {
        [SwaggerParameter(Description = "User's first name")]
        public string FirstName { get; set; } = null!;

        [SwaggerParameter(Description = "User's last name")]
        public string LastName { get; set; } = null!;

        [SwaggerParameter(Description = "Username for the account")]
        public string UserName { get; set; } = null!;

        [SwaggerParameter(Description = "User's email address")]
        public string Email { get; set; } = null!;

        [SwaggerParameter(Description = "User's identification card number")]
        public string IdentifierCard { get; set; } = null!;

        [SwaggerParameter(Description = "User's phone number (optional)")]
        public string? PhoneNumber { get; set; }

        [SwaggerParameter(Description = "Password for the account")]
        public string Password { get; set; } = null!;

        [JsonIgnore]
        public RoleTypes Role { get; set; }
    }
}
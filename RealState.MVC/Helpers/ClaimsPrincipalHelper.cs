using System.Security.Claims;

using RealState.Application.Enums;

namespace RealState.MVC.Helpers
{
    internal static class ClaimsPrincipalHelper
    {
        /// <summary>
        /// Return the id of the current logged user. Throws if non-existant
        /// </summary>
        public static Guid GetId(this ClaimsPrincipal self)
        {
            var guid = self.FindFirstValue(ClaimTypes.NameIdentifier);
            ArgumentException.ThrowIfNullOrEmpty(guid);
            return Guid.Parse(guid);
        }

        /// <summary>
        /// Return the id of the current logged user. Throws if non-existant
        /// </summary>
        public static string GetUnparsedId(this ClaimsPrincipal self)
        {
            var guid = self.FindFirstValue(ClaimTypes.NameIdentifier);
            ArgumentException.ThrowIfNullOrEmpty(guid);
            return guid;
        }

        /// <summary>
        /// Get the parsed Role of the current logged user. Throws if non-existant
        /// </summary>
        public static RoleTypes GetMainRole(this ClaimsPrincipal self)
        {
            string? role = self.FindFirstValue(ClaimTypes.Role);
            ArgumentException.ThrowIfNullOrEmpty(role);
            if (Enum.TryParse(role, out RoleTypes result))
            {
                return result;
            }
            throw new InvalidDataException(role);
        }

        /// <summary>
        /// Get the Role of the current logged user. Throws if non-existant
        /// </summary>
        public static string GetUnparsedMainRole(this ClaimsPrincipal self)
        {
            string? role = self.FindFirstValue(ClaimTypes.Role);
            ArgumentException.ThrowIfNullOrEmpty(role);
            return role;
        }

        public static bool IsLoggedIn(this ClaimsPrincipal self)
        {
            return self.Identities.First().Name != null;
        }
    }
}
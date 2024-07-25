namespace RealState.Application.DTOs.Account.Authentication
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow > Expires;

        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }

        public string ReplaceByToken { get; set; } = null!;

        public bool IsActive => Revoked == null && !IsExpired;

    }
}

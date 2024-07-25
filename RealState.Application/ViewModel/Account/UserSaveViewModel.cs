namespace RealState.Application.ViewModel.Account
{
    public class UserSaveViewModel
    {
        public string? Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public string IdentifierCard { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        public List<string> RolesId { get; set; } = [];
    }
}

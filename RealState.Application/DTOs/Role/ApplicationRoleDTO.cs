namespace RealState.Application.DTOs.Role
{
    public class ApplicationRoleDTO
    {
        public ApplicationRoleDTO(string name)
        {
            Name = name;
        }

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}

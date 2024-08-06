namespace RealState.Application.DTOs.User
{
    public class UserStatisticsDto
    {
        public int AgentInactive { get; set; } = 0;
        public int AgentActive { get; set; } = 0;
        public int AdminInactive { get; set; } = 0;
        public int AdminActive { get; set; } = 0;
        public int DeveloperInactive { get; set; } = 0;
        public int DeveloperActive { get; set; } = 0;
        public int ClientInactive { get; set; } = 0;
        public int ClientActive { get; set; } = 0;
    }
}

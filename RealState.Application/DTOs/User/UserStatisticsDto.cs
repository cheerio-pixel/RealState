namespace RealState.Application.DTOs.User
{
    public class UserStatisticsDto
    {
        public int AgentInactive { get; set; }
        public int AgentActive { get; set; }
        public int AdminInactive { get; set; }
        public int AdminActive { get; set; }
        public int DeveloperInactive { get; set; }
        public int DeveloperActive { get; set; }
        public int ClientInactive { get; set; }
        public int ClientActive { get; set; }
    }
}

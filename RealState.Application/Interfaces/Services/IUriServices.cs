namespace RealState.Application.Interfaces.Services
{
    public interface IUriServices
    {
        public string GetUrl(string token, string userId, string path);
    }
}

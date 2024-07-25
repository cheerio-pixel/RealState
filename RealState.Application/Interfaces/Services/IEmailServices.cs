using RealState.Application.DTOs.EmailServices;

namespace RealState.Application.Interfaces.Services
{
    public interface IEmailServices
    {
        public Task<bool> SendAsync(EmailRequestDTO request);
    }
}

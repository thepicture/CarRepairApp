using System.Threading.Tasks;

namespace CarRepairApp.Services
{
    public interface IMessageService
    {
        Task InformAsync(object information);
        Task WarnAsync(object warning);
        Task<bool> AskAsync(object question);
        Task InformErrorAsync(object error);
    }
}

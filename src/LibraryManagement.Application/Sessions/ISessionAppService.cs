using System.Threading.Tasks;
using Abp.Application.Services;
using LibraryManagement.Sessions.Dto;

namespace LibraryManagement.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

using System.Threading.Tasks;
using Abp.Application.Services;
using LibraryManagement.Authorization.Accounts.Dto;

namespace LibraryManagement.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

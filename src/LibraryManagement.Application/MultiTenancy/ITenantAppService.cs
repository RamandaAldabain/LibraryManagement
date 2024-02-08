using Abp.Application.Services;
using LibraryManagement.MultiTenancy.Dto;

namespace LibraryManagement.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


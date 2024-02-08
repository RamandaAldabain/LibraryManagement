using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LibraryManagement.EntityFrameworkCore;
using LibraryManagement.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace LibraryManagement.Web.Tests
{
    [DependsOn(
        typeof(LibraryManagementWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class LibraryManagementWebTestModule : AbpModule
    {
        public LibraryManagementWebTestModule(LibraryManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LibraryManagementWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(LibraryManagementWebMvcModule).Assembly);
        }
    }
}
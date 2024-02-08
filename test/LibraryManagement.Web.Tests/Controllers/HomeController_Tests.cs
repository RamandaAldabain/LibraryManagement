using System.Threading.Tasks;
using LibraryManagement.Models.TokenAuth;
using LibraryManagement.Web.Controllers;
using Shouldly;
using Xunit;

namespace LibraryManagement.Web.Tests.Controllers
{
    public class HomeController_Tests: LibraryManagementWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
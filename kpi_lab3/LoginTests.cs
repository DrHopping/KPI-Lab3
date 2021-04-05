using System;
using System.Linq;
using kpi_lab3.Models;
using kpi_lab3.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace kpi_lab3
{
    public class LoginTests : IDisposable
    {
        private IWebDriver driver;
        private User user;
        public LoginTests()
        {
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            driver = new ChromeDriver(options);
            var guid = Guid.NewGuid().ToString().Substring(0, 4);
            user = new User
            {
                Username = $"admin",
                Password = "admin123456"
            };
        }

        //Failing Test
        [Fact]
        public void LoginFailed_WrongPassword()
        {
            var page = new LoginPageObject(driver);
            page.EnterUsername(user.Username);
            page.EnterPassword(user.Password + "!");
            page.Submit();

            var actual = page.IsErrorVisible();
            var expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LoginSuccessful()
        {
            var page = new LoginPageObject(driver);
            page.EnterUsername(user.Username);
            page.EnterPassword(user.Password);
            page.Submit();

            var actual = page.IsLoggedIn();
            var expected = true;

            Assert.Equal(expected, actual);
        }


        public void Dispose() => driver?.Dispose();
    }
}

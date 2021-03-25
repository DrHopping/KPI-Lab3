using System;
using kpi_lab3.Models;
using kpi_lab3.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace kpi_lab3
{
    public class SignUpTests : IDisposable
    {
        private IWebDriver driver;
        private User user;
        public SignUpTests()
        {
            driver = new ChromeDriver();
            var guid = Guid.NewGuid().ToString().Substring(0, 4);
            user = new User
            {
                Username = $"User{guid}",
                Password = "PaSSword123!",
                Email = $"email{guid}@gmail.com"
            };
        }

        [Fact]
        public void SignUpFailed_WrongEmail()
        {
            var page = new SignUpPageObject(driver);
            page.EnterUsername(user.Username);
            page.EnterPassword(user.Password);
            page.EnterConfirmPassword(user.Password);
            page.EnterEmail(user.Email + "!");
            page.Submit();

            var actual = page.IsWrongEmailErrorVisible();
            var expected = true;

            Assert.Equal(expected,actual);
        }

        [Fact]
        public void SignUpFailed_PasswordDoesNotMatch()
        {
            var page = new SignUpPageObject(driver);
            page.EnterUsername(user.Username);
            page.EnterPassword(user.Password);
            page.EnterConfirmPassword(user.Password + "!");
            page.EnterEmail(user.Email);
            page.Submit();

            var actual = page.IsErrorVisible();
            var expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SignUpSuccessful()
        {
            var page = new SignUpPageObject(driver);
            page.EnterUsername(user.Username);
            page.EnterPassword(user.Password);
            page.EnterConfirmPassword(user.Password);
            page.EnterEmail(user.Email);
            page.Submit();

            var actual = page.IsSignedIn();
            var expected = true;

            Assert.Equal(expected, actual);
        }

        public void Dispose() => driver?.Dispose();
    }
}
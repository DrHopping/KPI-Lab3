using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace kpi_lab3.PageObjects
{
    public class LoginPageObject
    {
        private IWebDriver _driver;
        private WebDriverWait wait;
        private string url = "https://hoppingblogproject.web.app";

        public LoginPageObject(IWebDriver driver)
        {
            _driver = driver;
            driver.Navigate().GoToUrl($"{url}/login");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "input[formcontrolname='username']")]
        private IWebElement usernameField;
        [FindsBy(How = How.CssSelector, Using = "input[formcontrolname='password']")]
        private IWebElement passwordField;
        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement submitButton;

        public void EnterUsername(string username) => usernameField.SendKeys(username);
        public void EnterPassword(string password) => passwordField.SendKeys(password);
        public void Submit() => submitButton.Click();
        public bool IsErrorVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert")));
            return true;
        }
        public bool IsLoggedIn()
        {
            wait.Until(ExpectedConditions.UrlToBe($"{url}/home"));
            return true;
        }
    }
}
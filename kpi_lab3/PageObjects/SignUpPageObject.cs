using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace kpi_lab3.PageObjects
{
    public class SignUpPageObject
    {
        private IWebDriver _driver;
        private WebDriverWait wait;
        private string url = "https://hoppingblogproject.web.app";

        public SignUpPageObject(IWebDriver driver)
        {
            _driver = driver;
            driver.Navigate().GoToUrl($"{url}/signup");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "body > div > app-signup > div > div > div > form")]
        private IWebElement form;
        [FindsBy(How = How.XPath, Using = "/html/body/app-root/body/div/app-signup/div/div/div/form/div[1]/input")]
        private IWebElement usernameField;
        [FindsBy(How = How.XPath, Using = "/html/body/app-root/body/div/app-signup/div/div/div/form/div[2]/input")]
        private IWebElement passwordField;
        [FindsBy(How = How.XPath, Using = "/html/body/app-root/body/div/app-signup/div/div/div/form/div[3]/input")]
        private IWebElement confirmPasswordField;
        [FindsBy(How = How.XPath, Using = "/html/body/app-root/body/div/app-signup/div/div/div/form/div[4]/input")]
        private IWebElement emailField;
        [FindsBy(How = How.XPath, Using = "/html/body/app-root/body/div/app-signup/div/div/div/form/div[4]")]
        private IWebElement emailForm;
        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement submitButton;

        public void EnterUsername(string username) => usernameField.SendKeys(username);
        public void EnterPassword(string password) => passwordField.SendKeys(password);
        public void EnterConfirmPassword(string password) => confirmPasswordField.SendKeys(password);
        public void EnterEmail(string email) => emailField.SendKeys(email);
        public void Submit() => submitButton.Click();
        public bool IsSignedIn()
        {
            wait.Until(ExpectedConditions.UrlToBe($"{url}/home"));
            return true;
        }
        public bool IsWrongEmailErrorVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".invalid-feedback")));
            return true;
        }
        public bool IsErrorVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert")));
            return true;
        }

    }
}
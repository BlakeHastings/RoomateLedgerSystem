using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Models;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Exporters.Selenium
{
    public class AccountHistoryExporter : IAccountHistoryExporter
    {
        private ILogger<AccountHistoryExporter>? _logger { get; set; }

        public AccountHistoryExporter(ILogger<AccountHistoryExporter>? logger = null)
        {
            _logger = logger;
        }
        
        public IEnumerable<AccountHistory> Export(string username, string password)
        {
            using var driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            SignIn(username, password, driver, wait);
            return GetAccountHistory(driver, wait);
        }

        private static IEnumerable<AccountHistory> GetAccountHistory(ChromeDriver driver, WebDriverWait wait)
        {
            //TODO: Add page state validation before running this

            // there absolutely is a better way to do this
            // this bypasses the dumb ass date picker needed to change the data date range
            IJavaScriptExecutor js = driver;
            dynamic title = js.ExecuteScript("""
                var accountHistoryWrapper = document.createElement("div");
                accountHistoryWrapper.id = "accountHistoryResponse";
                document.body.appendChild(accountHistoryWrapper);

                bindAccountHistory = (res) => document.getElementById('accountHistoryResponse').innerHTML = JSON.stringify(res.ActLedger.LedgerItem.LedgerItems); 
                getAccountHistory('01/1950','01/2200');
                """);

            wait.Until((driver) => driver.FindElement(By.Id("accountHistoryResponse"))?.Text != String.Empty);

            IWebElement accountHistoryResponseElement = (IWebElement)js.ExecuteScript("return document.getElementById('accountHistoryResponse');");
            var data = accountHistoryResponseElement.Text;
            return JsonSerializer.Deserialize<IEnumerable<AccountHistory>>(data);
        }

        private void SignIn(string username, string password, IWebDriver driver, WebDriverWait? wait = null)
        {
            //TODO: Handle invalid username / password
            wait ??= new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://bpp.northcentralepa.com/onlineportal/My-Account/Account-History");

            // email input id = #Username
            var emailInput = driver.FindElement(By.Id("txtUsername"));
            emailInput.SendKeys(username);

            // password input = #Password
            var passwordInput = driver.FindElement(By.Id("txtPassword"));
            passwordInput.SendKeys(password);

            // submit button id = #SignIn
            var submitButton = driver.FindElement(By.Id("dnn_ctr384_CustomerLogin_btnLogin"));
            submitButton.Click();

            // wait for signin redirect to finish before returning
            var currUrl = driver.Url;
            wait.Until((driver) => driver.Url != currUrl);
        }
    }
}

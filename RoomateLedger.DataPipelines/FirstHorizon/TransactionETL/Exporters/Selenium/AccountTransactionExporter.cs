using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Models;

namespace RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Exporters.Selenium
{
    public class AccountTransactionExporter : IAccountTransactionExporter
    {
        private ILogger<AccountTransactionExporter>? _logger { get; set; }

        public AccountTransactionExporter(ILogger<AccountTransactionExporter> logger = null)
        {
            _logger = logger;
        }   

        public IEnumerable<AccountTransaction> Export(string accountNumber, string username, string password)
        {
            using var driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            SignIn(username, password, driver, wait);
            //AcceptAllCookies(driver, wait);
            //ClickRecentActivityTab(driver, wait);
            
            var accountTransactions = GetAccountTransactions(driver, wait);
            return accountTransactions;
        }

        private IEnumerable<AccountTransaction> GetAccountTransactions(ChromeDriver driver, WebDriverWait wait)
        {
            throw new NotImplementedException();
        }

        private void SignIn(string username, string password, ChromeDriver driver, WebDriverWait wait)
        {
            string URL = "https://www.firsthorizon.com/Personal/Login";
            wait ??= new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // need to handle two factor authentication

            // email input id
            var userIdInput = driver.FindElement(By.CssSelector("input[name='userid']"));
            userIdInput.SendKeys(username);

            // password input
            var passwordInput = driver.FindElement(By.CssSelector("input[name='password']"));
            passwordInput.SendKeys(password);

            // submit button
            var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();



            throw new NotImplementedException();
        }
    }
}

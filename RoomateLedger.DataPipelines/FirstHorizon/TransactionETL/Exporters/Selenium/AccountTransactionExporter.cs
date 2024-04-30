using Microsoft.Extensions.Logging;
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
            // need to handle two factor authentication
            throw new NotImplementedException();
        }
    }
}

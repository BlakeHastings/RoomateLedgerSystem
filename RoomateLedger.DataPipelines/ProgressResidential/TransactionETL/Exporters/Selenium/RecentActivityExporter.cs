
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RoomateLedger.Core.Entities;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Models;

namespace RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Exporters.Selenium;

public class RecentActivityExporter : IRecentActivityExporter
{
    private ILogger<RecentActivityExporter> _logger { get; set; }

    public RecentActivityExporter(ILogger<RecentActivityExporter> logger = null)
    {
        _logger = logger;
    }

    public IEnumerable<RecentActivity> Export(string subdomain, string username, string password)
    {
        var transactions = new List<Transaction>();
        using var driver = new ChromeDriver();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        SignIn(subdomain, username, password, driver, wait);
        AcceptAllCookies(driver, wait);
        ClickRecentActivityTab(driver, wait);
        var recentActivity = GetRecentActivity(driver, wait);

        return recentActivity;
    }

    private void AcceptAllCookies(IWebDriver driver, WebDriverWait wait = null)
    {
        wait ??= new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var acceptAllCookiesButton = wait.Until((driver) => driver.FindElement(By.Id("onetrust-accept-btn-handler")));
        acceptAllCookiesButton.SendKeys(Keys.Enter);
    }

    private IEnumerable<RecentActivity> GetRecentActivity(IWebDriver driver, WebDriverWait? wait = null)
    {
        //TODO: Validate table is accessible
        wait ??= new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var recentActivity = new List<RecentActivity>();

        var table = wait.Until((driver) => driver.FindElement(By.Id("PendingActivityDetails")));
        var tableBody = table.FindElement(By.TagName("tbody"));

        // next button at the bottom of table for stepping through pagination
        var tableNextPageButtonWrapper = driver.FindElement(By.CssSelector("li.next"));
        var tableNextPageButton = tableNextPageButtonWrapper.FindElement(By.TagName("a"));

        while (!IsNextButtonDisabled(tableNextPageButtonWrapper))
        {
            var tableRows = tableBody.FindElements(By.TagName("tr"));

            foreach (var row in tableRows)
            {
                var recentActivityRow = new RecentActivity();
                recentActivityRow.Date = DateOnly.Parse(row.FindElement(By.CssSelector("[data-label='Date']")).Text);
                recentActivityRow.PaymentsAndCharges = row.FindElement(By.CssSelector("[data-label='Payments and Charges']")).Text;
                recentActivityRow.Charge = decimal.Parse(row.FindElement(By.CssSelector("[data-label='Charge']")).Text.Replace("$", ""));
                recentActivityRow.Payment = decimal.Parse(row.FindElement(By.CssSelector("[data-label='Payments']")).Text.Replace("$", ""));
                recentActivityRow.Balance = decimal.Parse(row.FindElement(By.CssSelector("[data-label='Balance']")).Text.Replace("$", ""));

                recentActivity.Add(recentActivityRow);
            }

            tableNextPageButton.Click();
        }

        return recentActivity;
    }

    private bool IsNextButtonDisabled(IWebElement tableNextPageButtonWrapper)
    {
        return tableNextPageButtonWrapper.GetDomAttribute("class").Contains("disabled");
    }

    private void ClickRecentActivityTab(IWebDriver driver, WebDriverWait? wait = null)
    {
        wait ??= new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var recentActivityTab = wait.Until((driver) => driver.FindElement(By.Id("LinkRecentActivity")));
        recentActivityTab.Click();
    }

    private void SignIn(string subdomain, string username, string password, IWebDriver driver, WebDriverWait? wait = null)
    {
        //TODO: Handle invalid username / password
        wait ??= new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Navigate().GoToUrl($"https://{subdomain}.securecafe.com/residentservices/apartmentsforrent/payments.aspx#tab_RecentActivity");

        // email input id = #Username
        var emailInput = driver.FindElement(By.Id("Username"));
        emailInput.SendKeys(username);

        // password input = #Password
        var passwordInput = driver.FindElement(By.Id("Password"));
        passwordInput.SendKeys(password);

        // submit button id = #SignIn
        var submitButton = driver.FindElement(By.Id("SignIn"));
        submitButton.Click();

        // wait for signin redirect to finish before returning
        var currUrl = driver.Url;
        wait.Until((driver) => driver.Url != currUrl);
    }
}

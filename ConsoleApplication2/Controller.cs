using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Controller
    {
        private readonly string temporaryRepositoryName = "TemporaryRepository";
        private readonly string email = "benfranklin890@gmail.com";
        private readonly string password = "benfranklin890";
        private readonly string url = "http://www.github.com/";

        public void Start()
        {    
            //chromedriver
            using (IWebDriver driver = new ChromeDriver(getRelativePath()))
            {
                //Browse to GitHub home page: https://github.com/
                driver.Navigate().GoToUrl(url);

                // Click the signIn
                driver.FindElement(By.XPath("//a[@href='/login']")).Click();

                //Find login&Password Input text 
                IWebElement loginElement = driver.FindElement(By.Name("login"));
                IWebElement passwordElement = driver.FindElement(By.Name("password"));

                // Enter gmail credentials that was created before
                loginElement.SendKeys(email);
                passwordElement.SendKeys(password);
                //Submit the form
                passwordElement.Submit();

                // Open the dropdown so the options are visible
                driver.FindElement(By.XPath("//a[@href='/new']")).Click();
                //choose the first options
                IReadOnlyCollection<IWebElement> menu = driver.FindElements(By.ClassName("dropdown-menu-content"));
                IReadOnlyCollection<IWebElement> options = menu.ElementAt(0).FindElements(By.TagName("a"));
                options.ElementAt(0).Click();
                //Write the repository name into the "Repository name" field
                IWebElement repositoryNameTxtField = driver.FindElement(By.Name("repository[name]"));
                repositoryNameTxtField.SendKeys(temporaryRepositoryName);

                //Write repository description.
                IWebElement repositoryDescriptionTxtField = driver.FindElement(By.Name("repository[description]"));
                repositoryDescriptionTxtField.SendKeys("This is Temporary Repository. Created By Benjamin Franklin.");
                repositoryDescriptionTxtField.Submit();

                string issueBtn = String.Format("//a[@href='/BenjaminFranklin1888/{0}/issues']", temporaryRepositoryName);
                driver.FindElement(By.XPath(issueBtn)).Click();
                //New issue click
                string newIssueBtn = String.Format("//a[@href='/BenjaminFranklin1888/{0}/issues/new']", temporaryRepositoryName);
                IWebElement elem = driver.FindElement(By.XPath(newIssueBtn));
                IJavaScriptExecutor jss = (IJavaScriptExecutor)driver;
                jss.ExecuteScript("arguments[0].click();", elem);

                //Write issue title into the "Title" field
                IWebElement title = driver.FindElement(By.Name("issue[title]"));
                title.SendKeys("Benjamin Franklin, Title");
                //Write issue content into the "Leave a comment" field
                IWebElement msgBody = driver.FindElement(By.Id("issue_body"));
                msgBody.SendKeys("As an inventor, he is known for the lightning rod, bifocals, and the Franklin stove, among other inventions");
                msgBody.Submit();
                Console.ReadKey();
            }        
        }

        public string getRelativePath()
        {
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory);
            StringBuilder fullDirectory = new StringBuilder();
            return fullDirectory.Append(directory.Parent.Parent.FullName).ToString();     
        }
    }
}

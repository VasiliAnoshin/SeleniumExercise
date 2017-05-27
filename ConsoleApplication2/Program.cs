using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string temporaryRepositoryName="TemporaryRepository";
            using (IWebDriver driver = new ChromeDriver("C:\\Users\\User\\Downloads\\chromedriver_win32"))
            {
                //1) Browse to GitHub home page: https://github.com/
                driver.Navigate().GoToUrl("http://www.github.com/");

                //2) Click the signIn
                driver.FindElement(By.XPath("//a[@href='/login']")).Click();
               
                //3) Find login&Password Input text 
                IWebElement loginElement = driver.FindElement(By.Name("login"));
                IWebElement passwordElement = driver.FindElement(By.Name("password"));

                // Enter gmail credentials that was created before
                loginElement.SendKeys("benfranklin890@gmail.com");
                passwordElement.SendKeys("benfranklin890");
                //Submit the form
                passwordElement.Submit();

                // Open the dropdown so the options are visible
                driver.FindElement(By.XPath("//a[@href='/new']")).Click();
                //choose the first options
                IReadOnlyCollection<IWebElement> menu = driver.FindElements(By.ClassName("dropdown-menu-content"));                
                IReadOnlyCollection<IWebElement> options=menu.ElementAt(0).FindElements(By.TagName("a"));
                options.ElementAt(0).Click();
                //Write the repository name into the "Repository name" field
                IWebElement repositoryNameTxtField = driver.FindElement(By.Name("repository[name]"));
                repositoryNameTxtField.SendKeys(temporaryRepositoryName);

                //Write the repository description.
                IWebElement repositoryDescriptionTxtField = driver.FindElement(By.Name("repository[description]"));
                repositoryDescriptionTxtField.SendKeys("This is Temporary Repository. Created By Benjamin Franklin.");
                repositoryDescriptionTxtField.Submit();

                
                string issueBtn = String.Format("//a[@href='/BenjaminFranklin1888/{0}/issues']", temporaryRepositoryName);              
                driver.FindElement(By.XPath(issueBtn)).Click();
                ///BenjaminFranklin1888/tempRepo1/issues/new
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
    }          
}

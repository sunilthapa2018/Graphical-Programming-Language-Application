using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Paint
{
    class LiveRepo
    {
        public LiveRepo()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://github.com/login";
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Id("login_field")).SendKeys("luck_sunilthapa@yahoo.com");
            driver.FindElement(By.Id("password")).SendKeys("Sunil1995");
            driver.FindElement(By.Name("commit")).Click();
            driver.FindElement(By.XPath("//span[@title='Graphical-Programming-Language-Application']")).Click();
        }



    }
}

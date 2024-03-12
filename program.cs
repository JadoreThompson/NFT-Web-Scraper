using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
//using System.Windows.Forms;
using Xamarin.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
//using System.Windows.Forms;

class Program
{
    static void Main(string[] args)
    {
        EdgeOptions options = new EdgeOptions();
        EdgeDriver driver = new EdgeDriver(options);
        options.AddArgument("--start-maximized");
        List<string> titleList = new List<string>();
        HashSet<string> uniqueTitles = new HashSet<string>();
        try
        {
            driver.Navigate().GoToUrl("https://coinmarketcap.com/nft/upcoming/");
            PrintTitles(driver, "title", uniqueTitles);

            for (int i = 0; i < 10; i++) // Adjust the number of times to scroll down
            {
                // Scroll down by executing JavaScript
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, 500); document.body.scrollHeight;");

                // Wait for 2 seconds to allow content to load
                Thread.Sleep(2000);
                PrintTitles(driver, "title", uniqueTitles);
            }

            titleList.AddRange(uniqueTitles);

            foreach (string title in titleList)
            {
                Console.WriteLine(title);
            }

            int ii = 0;
            bool amount = false;
           
            for (int i = 0; i < titleList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {titleList[i]}");
            }
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("https://uk.search.yahoo.com/search;_ylt=AwrLAvTVN.plHzIA8NVLBQx.;_ylc=X1MDMjExNDcxNzAwMwRfcgMyBGZyA3lmcC10BGZyMgNzYi10b3AEZ3ByaWQDTlBvZnJaaFhSUy5Bdk5LVkRTeTdkQQRuX3JzbHQDMARuX3N1Z2cDMARvcmlnaW4DdWsuc2VhcmNoLnlhaG9vLmNvbQRwb3MDMARwcXN0cgMEcHFzdHJsAzAEcXN0cmwDMTcEcXVlcnkDeGJvcmclMjBuZnQlMjB0d2l0dGVyBHRfc3RtcAMxNzA5ODQ4NTM5?p=xborg+nft+twitter&fr2=sb-top&fr=yfp-t&fp=1");
            var gotoEnd = driver.FindElement(By.XPath("/html/body/div/div/div/div/div/button"));
            gotoEnd.Click();
            var accept = driver.FindElement(By.XPath("/html/body/div/div/div/div/form/div[2]/div[2]/button[1]"));
            accept.Click();

            while (amount != true)
            {
                var searchBox = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div/div[3]/form/div[1]/input"));
                searchBox.Clear();
                searchBox.SendKeys($"{titleList[ii]} nft twitter");
                ii++;
                searchBox.SendKeys(Keys.Enter);
                Thread.Sleep(3000);
                var twitterLink = driver.FindElement(By.XPath("//*[@id=\"web\"]/ol/li[1]/div/div[1]/h3/a"));

                twitterLink.Click();
                string url = twitterLink.GetAttribute("href");
                Thread.Sleep(2000);
                Console.WriteLine(url);
                Thread.Sleep(1500);

                if (ii == titleList.Count)
                {
                    amount = true;
                }
                else
                {
                    amount = false;
                }

                List<string> tabs = new List<string>(driver.WindowHandles);
                driver.SwitchTo().Window(tabs[0]);
                Thread.Sleep(2000);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        finally
        {
            // Close browser
            //driver.Quit();
        }
    }
    public static void PrintTitles(IWebDriver driver, string className, HashSet<string> uniqueTitles)
    {
        var elements = driver.FindElements(By.ClassName(className));
        foreach (var element in elements)
        {
            string title = element.Text;
            if (!uniqueTitles.Contains(title))
            {
                uniqueTitles.Add(title);
            }
        }
    }

}

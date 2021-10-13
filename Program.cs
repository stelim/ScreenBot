using OpenQA.Selenium;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Edge;

namespace ScreenBot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string filename = "vgn_" + DateTime.Now.ToString("yyyy-dd-M__HH-mm-ss");
            EdgeOptions options = new EdgeOptions();
            //options.AddArgument("headless");
            var driver = new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
            try
            {
                driver.Navigate().GoToUrl("https://www.vgn.de");
                var acceptButton = driver.FindElement(By.Id("Cookie_All"));
                acceptButton.Click();

                driver.Navigate().GoToUrl("https://www.vgn.de/abfahrten/");
                var inputHaltestelle = driver.FindElement(By.Id("name_dm"));
                inputHaltestelle.SendKeys("Peterskirche");
                inputHaltestelle.Submit();
                System.Threading.Thread.Sleep(2000);
                var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                screenshot.SaveAsFile(filename + ".png");

                driver.Navigate().GoToUrl("https://start.vag.de/desktop/");
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.Id("gdpr-cookie-accept")).Click();


                System.Threading.Thread.Sleep(2000);

                inputHaltestelle = driver.FindElement(By.Id("haltestelle"));
                inputHaltestelle.SendKeys("Peterskirche");
                System.Threading.Thread.Sleep(800);
                inputHaltestelle.SendKeys(Keys.Down);
                inputHaltestelle.SendKeys(Keys.Enter);

                System.Threading.Thread.Sleep(200);

                screenshot = (driver as ITakesScreenshot).GetScreenshot();
                screenshot.SaveAsFile("vag_" + DateTime.Now.ToString("yyyy-dd-M__HH-mm-ss") + ".png");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            driver.Close();
            driver.Quit();
        }
    }
}

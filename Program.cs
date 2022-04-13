using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    class Program
    {

        static void Main(string[] args)
        {

            try
            {
                var titulo_extraer = "";
                var precio = "";
                //Inicio del bot
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://www.amazon.com/-/es/");
                driver.Manage().Window.Maximize();
                //Pocicionarse en el buscador
                IWebElement input = driver.FindElement(By.XPath("//*[@id=\"twotabsearchtextbox\"]"));
                input.Click();
                Wait(driver, 500);
                //Buscar 
                input.SendKeys("Ofertas");
                Wait(driver, 500);
                //Enter
                input.SendKeys(Keys.Return);
                Wait(driver, 2000);
                //Buscar primer resultado
                IWebElement objeto = driver.FindElement(By.CssSelector("#search > div.s-desktop-width-max.s-desktop-content.s-opposite-dir.sg-row > div.s-matching-dir.sg-col-16-of-20.sg-col.sg-col-8-of-12.sg-col-12-of-16 > div > span:nth-child(4) > div.s-main-slot.s-result-list.s-search-results.sg-row > div:nth-child(4) > div > div > div > div > div.a-section.a-spacing-small.s-padding-left-small.s-padding-right-small > div.a-section.a-spacing-none.a-spacing-top-small.s-title-instructions-style > h2 > a > span"));
                objeto.Click();
                Wait(driver, 3000);
                //Guardar el titulo en variable
                titulo_extraer = driver.FindElement(By.Id("productTitle")).Text;
                //Guardar precio
                precio = driver.FindElement(By.XPath("//*[@id=\"corePrice_desktop\"]/ div/table/tbody/tr/td[2]/span[1]")).Text;
                //Imprimir
                Console.WriteLine("Producto en oferta: " + titulo_extraer);
                Console.WriteLine("Precio: " + precio);
                //Cerrar navegador
                driver.Close();
                //Detener ejecucion
                driver.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void Wait(IWebDriver driver, int miliseconds, int maxTimeOutSeconds = 60)
        {
            //IWebDriver driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 1, maxTimeOutSeconds));
            var delay = new TimeSpan(0, 0, 0, 0, miliseconds);
            var timestamp = DateTime.Now;
            wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        }
        //Cuando hay elementos no visibles temporalmente en el DOM
        public static void WaitForElementToBecomeVisibleWithinTimeout(IWebDriver driver, IWebElement element, int timeout)
        {
            new WebDriverWait(driver,
                TimeSpan.FromSeconds(timeout)).Until(ElementIsVisible(element));
        }

        private static Func<IWebDriver, bool> ElementIsVisible(IWebElement element)
        {
            return driver =>
            {
                try
                {
                    return element.Displayed;
                }
                catch (Exception)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
            };
        }

    }
}

﻿using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace SeleniumNUnitParam
{
    //Enum for browserType
    public enum BrowerType
    {
        Chrome,
        Firefox,
        IE
    }

    public class Hooks : Base
    {
        private BrowerType _browserType;

        [SetUp]
        public void InitializeTest()
        {
            //Get the value from NUnit-console --params 
            //e.g. nunit3-console.exe --params:Browser=Firefox \SeleniumNUnitParam.dll
            //If nothing specified, test will run in Chrome browser
            var browserType = TestContext.Parameters.Get("Browser", "Chrome");
            //Parse the browser Type, since its Enum
            _browserType = (BrowerType)Enum.Parse(typeof(BrowerType), browserType);
            //Pass it to browser
            ChooseDriverInstance(_browserType);
        }

        private void ChooseDriverInstance(BrowerType browserType)
        {
            if (browserType == BrowerType.Chrome)
                Driver = new ChromeDriver(@"D:\git_MyTest\JenkinsTest\SeleniumNUnitParam-master\SeleniumNUnitParam\bin\Debug");
            else if (browserType == BrowerType.Firefox)
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                service.FirefoxBinaryPath = @"D:\Driver\geckodriver";
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;
                Driver = new FirefoxDriver(service);
            }
            else if(browserType == BrowerType.IE)
            {
                Driver = new InternetExplorerDriver();
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}

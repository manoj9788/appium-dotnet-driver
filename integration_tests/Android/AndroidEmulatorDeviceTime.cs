﻿using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.Android
{
    class AndroidEmulatorDeviceTime
    {
        private AppiumDriver<AndroidElement> driver;

        [SetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce()
                ? Caps.getAndroid501Caps(Apps.get("androidApiDemos"))
                : Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "android - complex");
                capabilities.SetCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
        }

        [TearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void DeviceTimeTest()
        {
            string time = driver.DeviceTime;
            Console.WriteLine(time);
            Assert.AreEqual(true, time.Length == 28);
        }
    }
}
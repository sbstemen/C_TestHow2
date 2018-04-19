/*   EXAMPLE TEST_xx_Task  */

// *************************************************************
// Coder Camps
// 8444 N. 90th Street St. 110
// Scottsdale, AZ
// -- SBS ~ 20180314
// Copyright (c) 2016-18
// Project:      CC.Student.Basic
// *************************************************************

namespace CC.LMS.Student.ExampleTaskNameXX
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Utility;

    [TestClass]
    public class Test_XX_TaskName /* Must Match File name */
    {
        private static string chromePath = Directory.GetCurrentDirectory() + "\\assets\\";
        private static string testName = Properties.Settings.Default.TestName;
        private static string logPath =
            Properties.Settings.Default.LogPath + testName + DateTime.Now.ToString("-MM-dd-HHmm");

        private UserData usrData = new UserData();
        private Test_XX_NewTaskProcess A~callSign = new Test_XX_NewTaskProcess();
        private Utilities utils = new Utilities(logPath);


        [TestInitialize]
        public void TestUserSetup()
        {
            this.utils.UDataFiller(this.usrData);
            this.usrData.ClientUrl = Properties.Settings.Default.WZURL;
            this.usrData.Password = Properties.Settings.Default.Password;
            this.usrData.LogInAlias = Properties.Settings.Default.WZStudent;
        }


        [TestMethod]
        public void DaNameDisplayedInTestExplorer()
        { // replace user log in data for now
            int passCount = 0;
            int failCount = 0;
            this.usrData.LogInAlias = "WoZaUtO";
            this.usrData.Password = "314159";
            string[] targetUser = new string[3];

            using (IWebDriver webDriver = new ChromeDriver(chromePath))
            {
                A~callSign.BrowserReady(webDriver, this.utils, this.usrData.ClientUrl);

                A~callSign.SignIn(webDriver, this.utils, this.usrData, ref passCount, ref failCount); // Admin Sign in

                A~callSign.CreateUser(webDriver, this.utils, ref targetUser);

                A~callSign.ReadyFirstLogOn(webDriver, this.utils, ref targetUser);

                A~callSign.SignOff(webDriver, this.utils, this.usrData, ref passCount, ref failCount);

                webDriver.Close();

            }

            utils.RandomPause(); // Closed Browser to allow student Log In to process, not shadowed by Admin

            using (IWebDriver webDriver = new ChromeDriver(chromePath))
            { 
                A~callSign.BrowserReady(webDriver, this.utils, this.usrData.ClientUrl);

                A~callSign.PerformRegistration(webDriver, this.utils, ref targetUser, ref passCount, ref failCount);

                A~callSign.NewStdntLogIn(webDriver, this.utils, ref targetUser, ref passCount, ref failCount);

                A~callSign.SignOff(webDriver, this.utils, this.usrData, ref passCount, ref failCount);

                webDriver.Close();
            }
        }
    }
}

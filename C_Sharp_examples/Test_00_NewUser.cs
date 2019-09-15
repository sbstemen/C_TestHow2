// *************************************************************
// Coder Camps
// 8444 N. 90th Street St. 110
// Scottsdale, AZ
// -- SBS ~ 20180314
// Copyright (c) 2016-18
// Project:      CC.Student.Basic
// *************************************************************

namespace CC.LMS.Student.Setup00
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Utility;

    [TestClass]
    public class Test_00_NewUser
    {
        private static string runDate = DateTime.Now.ToString("-dd-HHmm");
        private static string assetPath = Directory.GetCurrentDirectory() + "\\assets\\";
        private static string testName = Properties.Settings.Default.TestName + runDate;
        private static string logPath = Properties.Settings.Default.LogPath + testName;
        private UserData usrData = new UserData();
        private Test_00_NewUserProcess aNUser = new Test_00_NewUserProcess();
        private Utilities utils = new Utilities(logPath);
        private string[] targetUser = new string[3];
        private int passCount = 0;
        private int failCount = 0;


        [TestInitialize]
        public void TestUserSetup()
        {
            this.utils.UDataFiller(this.usrData);
            this.usrData.ClientUrl = Properties.Settings.Default.WZURL;
            this.usrData.Password = "314159";
            this.usrData.LogInAlias = "WoZaUtO";
            aNUser.FileCleanUp(assetPath, "TargetUserData.txt");
        }


        [TestMethod]
        public void Admin_A_NUser()
        {
            using (IWebDriver webDriver = new ChromeDriver(assetPath))
            {
                aNUser.BrowserReady(webDriver, this.utils, this.usrData.ClientUrl);

                aNUser.SignIn(webDriver, this.utils, this.usrData, ref passCount, ref failCount);

                aNUser.CreateUser(webDriver, this.utils, ref targetUser);

                aNUser.GetUsrUrl(webDriver, this.utils, ref targetUser);

                aNUser.SignOff(webDriver, this.utils, this.usrData, ref passCount, ref failCount);

                webDriver.Close();

            }

            utils.RandomPause();

            using (IWebDriver webDriver = new ChromeDriver(assetPath))
            { 
                aNUser.BrowserReady(webDriver, this.utils, this.usrData.ClientUrl);

                aNUser.RegNewUsrViaUrl(webDriver, this.utils, ref targetUser, ref passCount, ref failCount);

                aNUser.FirstUsrLogIn(webDriver, this.utils, ref targetUser, ref passCount, ref failCount);

                aNUser.SignOff(webDriver, this.utils, this.usrData, ref passCount, ref failCount);

                webDriver.Close();
            }
        }
    }
}

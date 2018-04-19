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
    public class Test_00_Assignments
    {
        private static string usrFile = "TargetUserData.txt";
        private static string runDate = DateTime.Now.ToString("-dd-HHmm");
        private static string assetPath = Directory.GetCurrentDirectory() + "\\assets\\";
        private static string testName = Properties.Settings.Default.TestName + runDate;
        private static string logPath = Properties.Settings.Default.LogPath + testName;
        private UserData usrData = new UserData();
        private Test_00_AssignementsProcessing assignments = new Test_00_AssignementsProcessing();
        private Utilities utils = new Utilities(logPath);
        private string[] targetUserData = new string[3];
        private string userTarget;
        private int passCount = 0;
        private int failCount = 0;

        [TestInitialize]
        public void TestUserSetup()
        {
            try
            {
                Assert.IsTrue(assignments.UserFileExists(usrFile));
            }
            catch (Exception exText)
            {
                utils.MakeLogEntry("FAILED to even have a user file ready!");
                utils.MakeLogEntry(Environment.NewLine + "No Reason To Continue!!" + Environment.NewLine);
                utils.MakeLogEntry(Environment.NewLine + exText + Environment.NewLine);
                Environment.Exit(1);                
            }
            this.utils.UDataFiller(this.usrData);
            this.usrData.LogInAlias = "WOZAUTO";
            this.usrData.Password = "314159";
            this.assignments.ReadUsrFile(ref targetUserData, usrFile);
            userTarget = targetUserData[0];
        }

        [TestMethod]
        public void Admin_A_NUser_Assign()
        {
            using (IWebDriver webDriver = new ChromeDriver(assetPath))
            {
                assignments.BrowserReady(webDriver, this.utils, this.usrData.ClientUrl);

                assignments.UserTargetSignIn(webDriver, this.utils, targetUserData, ref passCount, ref failCount);

                assignments.SignOff(webDriver, this.utils, ref passCount, ref failCount);

                assignments.AdminSignIn(webDriver, this.utils, this.usrData, ref passCount, ref failCount);

                //userTarget = "x";  // Fail
                //userTarget = "Ln99451"; // READY
                userTarget = "Ln94847";  // NOT READY

                assignments.CourseAssignments(webDriver, this.utils, userTarget, ref passCount, ref failCount);

                assignments.SignOff(webDriver, this.utils, ref passCount, ref failCount);

                webDriver.Close();
            }

        }
    }
}

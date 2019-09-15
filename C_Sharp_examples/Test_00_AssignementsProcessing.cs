// *************************************************************
// Coder Camps
// 8444 N. 90th Street St. 110
// Scottsdale, AZ
// -- SBS ~ 20180314
// Copyright (c) 2016-18
// Project:      CC.Student.NewUser   (password magic number line 160)
// *************************************************************


namespace CC.LMS.Student.Setup00
{
    using System;
    using System.Drawing;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Utility;

    /// <summary>
    /// Reads in the newly created user and assigns courses
    /// </summary>
    internal class Test_00_AssignementsProcessing
    {

        /// <summary>
        /// Opens browser to log in page Google used for browser timing
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="utils">The utils.</param>
        /// <param name="client">The client.</param>
        /// <returns>Web Driver to Log In</returns>
        public IWebDriver BrowserReady(IWebDriver webDriver, Utilities utils, string client)
        {
            Size browserSize = new Size(1650, 1000);
            string startPage = @"https://www.google.com/";
            webDriver.Navigate().GoToUrl(startPage);
            webDriver.Manage().Window.Size = browserSize;
            utils.RandomPause(2);
            webDriver.Navigate().GoToUrl(client);
            string pageText = webDriver.PageSource;
            utils.RandomPause(2);
            utils.PageIsReady(webDriver, utils, pageText, "Sign in with Microsoft");
            return webDriver;
        }

        /// <summary>
        /// Test new user account just log in and out.  
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="utils">The utils.</param>
        /// <param name="usrData">The usr data.</param>
        /// <param name="passCount">The pass count.</param>
        /// <param name="failCount">The fail count.</param>
        /// <returns>IWebDriver</returns>
        public IWebDriver UserTargetSignIn(IWebDriver webDriver, Utilities utils, string[] tUserData, ref int passCount, ref int failCount)
        {
            string pageText = string.Empty;
            string searchText = "You have not been enrolled in any courses";
            webDriver.FindElement(By.Id("Username")).SendKeys(tUserData[0]);
            webDriver.FindElement(By.Id("Password")).SendKeys(tUserData[1]);
            utils.RandomPause(2);
            webDriver.FindElement(By.ClassName("cc-btn-sign-in")).Click();
            utils.RandomPause();
            pageText = webDriver.PageSource.ToString();

            try
            {
                Assert.IsTrue(pageText.Contains(searchText));
                {
                    utils.MakeLogEntry("User Not Enrolled Yet!");
                    utils.RandomPause(2);
                    passCount = ++passCount;
                    searchText = string.Empty;
                }
            }
            catch (Exception exText)
            {
                searchText = "Graded Assignments";
                if (pageText.Contains(searchText))
                {
                    utils.MakeLogEntry("Student Login and Assignments completed.");
                }
                else
                {
                    utils.MakeLogEntry("FAILED FAILED Tried to authenticate as a student something went really wrong");
                    utils.MakeLogEntry("Log On Failed for client");
                    utils.MakeLogEntry("Exception Code" + exText);
                    failCount = failCount++;
                    webDriver.Close();
                    Environment.Exit(1);
                }
            }

            utils.RandomPause(1);
            return webDriver;
        }

        /// <summary>
        /// Existing User Sign On, usually Admin.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="utils">The utils.</param>
        /// <param name="usrData">The usr data.</param>
        /// <param name="passCount">The pass count.</param>
        /// <param name="failCount">The fail count.</param>
        /// <returns>IWebDriver</returns>
        public IWebDriver AdminSignIn(IWebDriver webDriver, Utilities utils, UserData usrData, ref int passCount, ref int failCount)
        {
            string pageText = string.Empty;
            string searchText = "Reporting";
            webDriver.FindElement(By.Id("Username")).SendKeys(usrData.LogInAlias);
            webDriver.FindElement(By.Id("Password")).SendKeys(usrData.Password);
            utils.RandomPause(2);
            webDriver.FindElement(By.ClassName("cc-btn-sign-in")).Click();
            utils.RandomPause();
            pageText = webDriver.PageSource.ToString();

            try
            {
                Assert.IsTrue(pageText.Contains(searchText));
                {
                    utils.MakeLogEntry("Admin Account shows Reporting");
                    utils.RandomPause(2);
                    passCount = ++passCount;
                    searchText = string.Empty;
                }
            }
            catch (Exception expText)
            {
                searchText = "Graded Assignments";
                if (pageText.Contains(searchText))
                {
                    utils.MakeLogEntry("Student Login was completed.");
                }
                else
                {
                    utils.MakeLogEntry("FAILED FAILED Tried to authenticate as a student something went really wrong");
                    utils.MakeLogEntry("Log On Failed for client " + usrData.LogInAlias);
                    utils.MakeLogEntry("Exception Code" + expText);
                    failCount = failCount++;
                    Assert.Fail();
                }
            }

            utils.RandomPause(1);
            return webDriver;
        }


        public IWebDriver CourseAssignments(IWebDriver webDriver, Utilities utils, string usrToAssign, ref int passCount, ref int failCount)
        {
            bool usrAccountReady = false;
            string usrNotEnrolled = "The user is not enrolled in any sections";
            string usrLoggedInOnce = "Last active on";
            string DoNotUseThisUserAccountTest = "-Never Logged In-";

            string pageText;

            string usrElementCss = "div.row div.col-md-11.col-xs-10 div.media div.media-body h6.pull-left";

            string manageUsrBtnCss = "div.cc-registered-user div.media.cc-registered-user-card div.media-body > div.pull-right button";

            string usrSrchCss = "div.cc-admin div.col-md-5 div.cc-admin-users div.panel.panel-default div.panel-heading div.media.cc-panel-header div.media-body div.pull-right input.form-control";

            IWebElement usrSrchBox = webDriver.FindElement(By.CssSelector(usrSrchCss));

            usrSrchBox.SendKeys(usrToAssign);

            var selUsrDisplayed = webDriver.FindElements(By.CssSelector(usrElementCss));

            selUsrDisplayed[0].Click();

            var usrMgmtBtn = webDriver.FindElement(By.CssSelector(manageUsrBtnCss));

            usrMgmtBtn.Click();

            pageText = webDriver.PageSource;

            // Test that we are on a user account that can be managed. 
            // Check that page text has words "The user is not enrolled in any sections"  
            // Then test that the user page text does NOT have these words.  " -Never Logged In-  

            if (pageText.Contains(usrNotEnrolled))
            {
                if (pageText.Contains(usrLoggedInOnce))
                {
                    if (pageText.Contains(DoNotUseThisUserAccountTest))
                    {
                        usrAccountReady = false;
                    }
                    else
                    {
                        usrAccountReady = true;
                    }
                }
            }

            try
            {
                Assert.IsTrue(usrAccountReady);
            }
            catch (Exception exText)
            {
                utils.MakeLogEntry("FAILED FAILED UserAccount is not ready");
                utils.MakeLogEntry("Log On Failed for client");
                utils.MakeLogEntry("Exception Code" + exText);
                failCount = failCount++;
                webDriver.Close();
                Environment.Exit(1);
            }


            // Scroll to viee the element (Save) from CRM Id fields 
            /* 
             html.lc-cb-container-vi body#body div#root div#app div.container div#app-content.row div.col-md-12.cc-admin-user-detail div#cc-user-detail.cc-user-detail.row div.col-md-6.col-xs-12 div div div.row div.cc-user-detail-status div.cc-user-detail-status__pull-right button.sc-bdVaJa.gfjjHQ 
             * */


            // In elment sections courses 

            /*
             html.lc-cb-container-vi body#body div#root div#app div.container div#app-content.row div.col-md-12.cc-admin-user-detail div#cc-user-detail.cc-user-detail.row div.col-md-6.col-xs-12 div.cc-admin-batch-enrollment div.panel.panel-default div.cc-admin-batch-enrollment-wrapper div.cc-batch-enrollment div.form-group.has-feedback.has-error input#assignmentIds.form-control

                Shows ID value of 'assignmentIds'

                Enter string "ZQATest02, ZQATest05, ZQATest08'

             */


            // Assign courses ZQATest02, ZQATest05, ZQATest08. 

            // IN the date field START 
            /*

            html.lc-cb-container-vi body#body div#root div#app div.container div#app-content.row div.col-md-12.cc-admin-user-detail div#cc-user-detail.cc-user-detail.row div.col-md-6.col-xs-12 div.cc-admin-batch-enrollment div.panel.panel-default div.cc-admin-batch-enrollment-wrapper div.cc-batch-enrollment div.form-group.has-feedback.has-error div.rdt input.form-control

                string == "04/01/2018"


             */


            // in the date field COMPLETE  04/20/2018

            /*

             html.lc-cb-container-vi body#body div#root div#app div.container div#app-content.row div.col-md-12.cc-admin-user-detail div#cc-user-detail.cc-user-detail.row div.col-md-6.col-xs-12 div.cc-admin-batch-enrollment div.panel.panel-default div.cc-admin-batch-enrollment-wrapper div.cc-batch-enrollment div.form-group.has-feedback.has-success div.rdt.rdtOpen input.form-control

             */

            // Then submit step 1. 
            // this is a button 

            /*

             html.lc-cb-container-vi body#body div#root div#app div.container div#app-content.row div.col-md-12.cc-admin-user-detail div#cc-user-detail.cc-user-detail.row div.col-md-6.col-xs-12 div.cc-admin-batch-enrollment div.panel.panel-default div.cc-admin-batch-enrollment-wrapper button.sc-bdVaJa.gfjjHQ

             */


            // Now there will be a modal.  
            // Probably has 3 buttons the X, the Cancel and finally the Enroll click. 

            /*

            html.lc-cb-container-vi body#body.modal-open div div.fade.in.modal div.modal-dialog div.modal-content div.sc-ifAKCX.ijrwBU.modal-footer button.sc-bdVaJa.gfjjHQ

             */



            // When completed the "Sections will display items.  
            return webDriver;
        }

        /// <summary>
        /// Reads the new user for login and testing
        /// </summary>
        /// <param name="createdUser"></param>
        /// <returns>Returns the user details</returns>
        public string[] ReadUsrFile(ref string[] createdUser, string fileName)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\assets\\" + fileName;
            int counter = 0;
            string line;
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                createdUser[counter] = line;
                counter++;
            }

            file.Close();
            return createdUser;
        }

        /// <summary>
        /// Signs off.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="utils">The utils.</param>
        /// <param name="usrData">The usr data.</param>
        /// <param name="passCount">The pass count.</param>
        /// <param name="failCount">The fail count.</param>
        /// <returns>IWebDriver</returns>
        public IWebDriver SignOff(IWebDriver webDriver, Utilities utils, ref int passCount, ref int failCount)
        {
            try
            {
                {
                    var closeButton = webDriver.FindElement(By.CssSelector("div.modal-content button.close"));
                    closeButton.Click();
                }
            }
            catch (Exception exText)
            {
                utils.MakeLogEntry(exText.ToString());
            }

            var dashboardButton = webDriver.FindElements(By.CssSelector("div.navbar-collapse.collapse ul.nav.navbar-nav.navbar-right > li"));

            dashboardButton[0].Click();

            utils.RandomPause(1);

            var logOffButton = webDriver.FindElement(By.ClassName("qa-logout-button"));

            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", logOffButton);

            utils.RandomPause(1);

            logOffButton.Click();

            return webDriver;
        }

        public bool UserFileExists(string file)
        {
            string filePathTest = Directory.GetCurrentDirectory() + "\\assets\\" + file;
            bool isFilethere = File.Exists(filePathTest);
            return isFilethere;
        }

    }
}

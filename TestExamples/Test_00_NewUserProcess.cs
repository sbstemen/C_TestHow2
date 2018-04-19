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
    /// Creates a new user in QA environment
    /// </summary>
    internal class Test_00_NewUserProcess
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
        /// Existing User Sign On, usually Admin.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="utils">The utils.</param>
        /// <param name="usrData">The usr data.</param>
        /// <param name="passCount">The pass count.</param>
        /// <param name="failCount">The fail count.</param>
        /// <returns>IWebDriver</returns>
        public IWebDriver SignIn(IWebDriver webDriver, Utilities utils, UserData usrData, ref int passCount, ref int failCount)
        {
            string pageText = string.Empty;
            string searchText = "Courses";
            webDriver.FindElement(By.Id("Username")).SendKeys(usrData.LogInAlias);
            webDriver.FindElement(By.Id("Password")).SendKeys(usrData.Password);
            utils.RandomPause(2);
            webDriver.FindElement(By.ClassName("cc-btn-sign-in")).Click();
            utils.RandomPause();

            pageText = webDriver.PageSource.ToString();

            searchText = "Organizations"; 

            try
            {
                Assert.IsTrue(pageText.Contains(searchText));
                {
                    utils.MakeLogEntry("Admin Account");
                    utils.RandomPause(2);
                    passCount = ++passCount;
                    searchText = string.Empty;
                }
            }
            catch (Exception expText)
            {
                searchText = "Graded Assignments";
                pageText = webDriver.PageSource.ToString();
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
        
        /// <summary>
        /// Creates a new random user & saves data to a temporary file.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="utils"></param>
        /// <param name="studentNew"></param>
        /// <returns>WebDriver</returns>
        public IWebDriver CreateUser(IWebDriver webDriver, Utilities utils, ref string[] studentNew)
        {
            string utcTimeCode = DateTime.UtcNow.Ticks.ToString();
            string fName = "Fn" + utils.TruncationTool(utcTimeCode, 5);
            utcTimeCode = utils.Reverse(utcTimeCode);
            string lName = "Ln" + utils.TruncationTool(utcTimeCode, 5);
            string usrLogInId = lName + fName;
            string eMail = usrLogInId + "@SkyK9.com";

            string usrSrchCss = "div.cc-admin div.col-md-5 div.cc-admin-users div.panel.panel-default div.panel-heading div.media.cc-panel-header div.media-body div.pull-right input.form-control";

            string usrRegLink = "div.col-md-5 div.cc-admin-users div.panel.panel-default li.list-group-item.cc-panel-item.cc-panel-item-clickable.cc-panel-item-placeholder div.row div.col-md-12";

            string enterFist = "div.modal-content div.cc-admin-register-user div.form-group.has-feedback input#firstName.form-control";

            string enterLast = "div.modal-content div.cc-admin-register-user div.form-group.has-feedback input#lastName.form-control";

            string enterEmail = "div.modal-content div.cc-admin-register-user div.form-group.has-feedback input#email.form-control";

            IWebElement usrSrchBox = webDriver.FindElement(By.CssSelector(usrSrchCss));

            usrSrchBox.SendKeys("3861ae01ee2e");

            utils.RandomPause(1);

            IWebElement registrationClick = webDriver.FindElement(By.CssSelector(usrRegLink));

            registrationClick.Click();

            IWebElement fnBox = webDriver.FindElement(By.CssSelector(enterFist));
            fnBox.SendKeys(fName);

            IWebElement lnBox = webDriver.FindElement(By.CssSelector(enterLast));
            lnBox.SendKeys(lName);

            IWebElement emBox = webDriver.FindElement(By.CssSelector(enterEmail));
            emBox.SendKeys(eMail);

            utils.RandomPause(3);

            var buttons = webDriver.FindElements(By.CssSelector("body.modal-open div div.fade.in.modal div.modal-dialog div.modal-content button"));

            utils.RandomPause(1.5);

            buttons[2].Click();

            utils.RandomPause(2);

            usrSrchBox.Clear();

            utils.PageIsReady(webDriver, utils, "", lName);

            usrSrchBox.SendKeys(lName);

            utils.RandomPause(2);

            studentNew[0] = usrLogInId;
            studentNew[1] = "123456";
            studentNew[2] = "";

            return webDriver;
        }

        /// <summary>
        /// Gets information for the first log on of new user.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="utils"></param>
        /// <param name="createdUser"></param>
        /// <returns>WebDriver</returns>
        public IWebDriver GetUsrUrl(IWebDriver webDriver, Utilities utils, ref string[] createdUser)
        {
            string pageText;
            
            string[] checkSum = new string[3];

            string lookForThis = "User is not registered";

            string filePathStart = Directory.GetCurrentDirectory() + "\\assets\\";

            string usrElementCss = "div.row div.col-md-11.col-xs-10 div.media div.media-body h6.pull-left";

            string manageUsrBtnCss = "div.cc-registered-user div.media.cc-registered-user-card div.media-body > div.pull-right button";

            string usrInviteLinkCss = "div.col-md-12.cc-admin-user-detail div#cc-user-detail.cc-user-detail.row div.col-md-6.col-xs-12 div div.row div.cc-user-detail-status div.cc-user-detail-status__registration-info div.optional.form-group.has-feedback.has-success textarea";

            var selUsrDisplayed = webDriver.FindElements(By.CssSelector(usrElementCss));

            selUsrDisplayed[0].Click();

            var usrMgmtBtn = webDriver.FindElement(By.CssSelector(manageUsrBtnCss));

            usrMgmtBtn.Click();

            pageText = webDriver.PageSource;

            utils.PageIsReady(webDriver, utils, pageText, lookForThis);

            var usrInviteUrl = webDriver.FindElement(By.CssSelector(usrInviteLinkCss));

            createdUser[2] = usrInviteUrl.Text;

            checkSum = WriteUsrFile(createdUser, "NewUserData.txt");

            Assert.AreEqual(checkSum, createdUser);

            File.Copy(filePathStart + "NewUserData.txt", filePathStart + "TargetUserData.txt");

            return webDriver;
        }

        /// <summary>
        /// Does the registration of a new user.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="utils"></param>
        /// <param name="newStudentData"></param>
        /// <param name="passCount"></param>
        /// <param name="failCount"></param>
        /// <returns>WebDriver</returns>
        public IWebDriver RegNewUsrViaUrl(IWebDriver webDriver, Utilities utils, ref string[] newStudentData, ref int passCount, ref int failCount)
        {
            string regButtonCss = "div.login-page div.row div.col-md-5.col-sm-12.col-xs-12 div.panel.panel-default li.list-group-item.cc-panel-item form div.form-group button.btn.btn-primary.cc-btn-sign-in.btn-confirm ";
            string createThisLoginId = newStudentData[0];
            string useThisPasswordVal = newStudentData[1];
            string invitationUrlToUse = newStudentData[2];

            utils.RandomPause(1);

            webDriver.Navigate().GoToUrl(invitationUrlToUse);

            utils.RandomPause(1);

            webDriver.FindElement(By.Id("Username")).SendKeys(createThisLoginId);

            webDriver.FindElement(By.Id("Security_Password")).SendKeys(useThisPasswordVal);

            webDriver.FindElement(By.Id("Security_ConfirmPassword")).SendKeys(useThisPasswordVal);

            utils.RandomPause(1);

            webDriver.FindElement(By.CssSelector(regButtonCss)).Click();

            utils.RandomPause(1);

            return webDriver;
        }

        /// <summary>
        /// Performs the very first login, to set 'Registered' in the db.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="utils"></param>
        /// <param name="regStudentNew"></param>
        /// <param name="passCount"></param>
        /// <param name="failCount"></param>
        /// <returns>Web Driver</returns>
        public IWebDriver FirstUsrLogIn(IWebDriver webDriver, Utilities utils, ref string[] regStudentNew, ref int passCount, ref int failCount)
        {
            utils.RandomPause(1);
            string pageText = string.Empty;
            string searchText = "You have not been enrolled in any courses";
            webDriver.FindElement(By.Id("Username")).SendKeys(regStudentNew[0]);
            webDriver.FindElement(By.Id("Password")).SendKeys(regStudentNew[1]);
            utils.RandomPause(2);
            webDriver.FindElement(By.ClassName("cc-btn-sign-in")).Click();
            utils.RandomPause();

            pageText = webDriver.PageSource.ToString();

            try
            {
                Assert.IsTrue(pageText.Contains(searchText));
                {
                    utils.MakeLogEntry("Student Account logged in");
                    utils.RandomPause(2);
                    passCount = ++passCount;
                    searchText = string.Empty;
                }
            }
            catch (Exception expText)
            {
                searchText = "Dashboard";
                if (pageText.Contains(searchText))
                {
                    utils.MakeLogEntry("Student Login was completed.");
                }
                else
                {
                    utils.MakeLogEntry("FAILED FAILED Tried to authenticate as a student something went really wrong");
                    utils.MakeLogEntry("Exception Code" + expText);
                    failCount = failCount++;
                    Assert.Fail();
                }
            }

            utils.RandomPause(1);

            return webDriver;
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
        public IWebDriver SignOff(IWebDriver webDriver, Utilities utils, UserData usrData, ref int passCount, ref int failCount)
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

        /// <summary>
        /// Writes the new user to a file
        /// </summary>
        /// <param name="newUsrName"></param>
        /// <param name="password"></param>
        /// <param name="createUserUrl"></param>
        /// <returns>String array of user details</returns>
        public string[] WriteUsrFile(string[] userDataToSave, string dataFile)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\assets\\" + dataFile;

            using (StreamWriter writeNewUser = new StreamWriter(filePath))
            {
                writeNewUser.WriteLine(userDataToSave[0]);
                writeNewUser.WriteLine(userDataToSave[1]);
                writeNewUser.WriteLine(userDataToSave[2]);
            }
            return userDataToSave;
        }


        public void FileCleanUp(string assetPath, string fileName)
        {
            string revoveThis = assetPath + fileName;
            bool isThere = File.Exists(revoveThis);
            if (isThere)
            {
                File.Delete(revoveThis);
            }
        }

    }
}

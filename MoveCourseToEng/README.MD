Primarily just a list of steps used to move a course into the engineering branch to be used for testing.

(1) Link to the GitHub path for our courses.  
https://github.com/woz-u/lms-curriculum

(2) There you will see many branches, 30 or more.

(3) Select the branch called "Master" and clone that to your local system.

(4) You now have most of what is available on our production LMS.  There may be some courses that are not listed in the Master Branch.  Unless it is a very specific course you need to test.  This is where I would start.

(5) As Example suppose you want to copy/move the course DS Big Data into the DEV / QA environment.  This requires you move the course from the Master Branch to the Engineering Branch.  

(6) Check out the "Master" branch from Git Hub.

(7) Create two temporary directories on your system in maybe the TEMP folder.

(8) In the folder, you created called, "courses" copy the course file, for example, if you wanted to move the Big Data course.
~ path/courses/DSG107 - Introduction to Big Data.md

(9) In the folder, you created called, "learning-modules" copy the entire contents of the learning module.
~ path/learning-modules/DS Big Data

*NOTE*  To verify the name of the directory, find the markdown file "Course Code ~ Name.MD" and it will have the name of the directory to move.

(10) Using either GitBASH, GitKraken, or your favorite tool to check out the Engineering branch from GitHub.

(11) I usually open up the first lesson and edit this with my initials on the page.  When it gets imported this gives me a quick easy way to verify this was the course I wanted to be loaded.

(12) Commit all changes and then push up to the Engineering Branch.  

(13) Unlucky ~ We leave this to Jenkins the butler.  After the push from your local machine to the repo.  That automatically triggers the Jenkins tool to push the latest Engineering Branch to the QA / DEV course version storage.  Watch for this in the Slack/eng-pr

(14) Log into the LMS in DEV or QA with an account that is an author role, and user-sec / lms.course.master claim. This claim is required for importing courses.

(15) In the top right corner of the top right box labeled "Courses" is a drop-down list.  This list is dynamically created.  You can type in the course code of the course you just updated, to make it easier to find on this list.  Select that course.

(16) If the course has never been imported you will see "Click here to import 'Course Code & title'".  Select that and watch for the notification.  If the course was successfully imported great!.  If it wasn't imported and there are errors.  They will be listed in the Notifications.  NOTE ~ I have found these error messages are brief, however pretty spot on with respect to why a new course import failed.

(17) If the course has already been imported, you will need to open it then select, "UPDATE" to load the latest version of the course.

(18) If the import was good, create a new section using the course you just imported.  

(19) If the import failed, read the warning messages carefully.  It usually gives a good clue about what caused the import failure.
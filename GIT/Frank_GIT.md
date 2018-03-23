/******************************************************************/
/*****************************************************************/
/*********   SCOTT's NOTES added and date coded    **************/
/***************************************************************/
/**************************************************************/

/*20180307*/

Putting Local files into a newly created repository  (repo)
(1) Files are local, you have created a project on your system and want to put it into source control. 
* Build and make sure it runs. 
* Create a README
* Run git init command 
       ~ git init
* NEXT check with the status command 
       ~ git status
* IT should respond with all files are untracked. 
* THEN add everything to your git 
       ~ git add . 
* CHECK again with the git status, this time it's going to show all files waiting for commit. 
* NOW we do a Git Commit Command with a comment 
      ~ git commit -m "Initial Commit of files blah blah blah..."
* IT will process this and perform the 'Git Snap Shot' of the files. 
* NOW all your files are locally tracked.  you don't have them put up in the repository, but changes are able to be rolled back. 

(2) Let's get these files up to the GitHUB repo.  
* FROM your personal Github account create a new repo, but do not initialize with any files.  
* AFTER completed ~ Copy the text from the clone link  it will be https :// stuff /NameYOUjustUSED
* OPEN GITBASH from there use CD LS ls-l etc to make sure you are in the correct directory. 
     ~ Example C:\MyLocalGitHub\NewCode is the path 
* Review:  you have code that runs; you have made it a GIT; all code is checked into local git:: AND you have a remote origin repo created. 
     ~ git remote add origin https://github.com/sbstemen/ExistingCodeToGit.git
* THAT will set the 'Origin' for this local git.  but we are not done. 
* NEXT verify that it's set  Enter this command, this will return the values configured for this repo
	 ~ $ git remote -v
     ~ origin  https://github.com/sbstemen/ExistingCodeToGit.git (fetch)
     ~ origin  https://github.com/sbstemen/ExistingCodeToGit.git (push)
* NOW you are finally ready to move bits, bytes, and nibbles up to the repo
     ~ git push origin master 
* THAT is the command that tells you to push your existing checked in code to the origin master branch 
* RETURNED code looks like this.  
   ~ Counting objects: 32, done.
    ~ Delta compression using up to 4 threads.
     ~ Compressing objects: 100% (27/27), done.
      ~ Writing objects: 100% (32/32), 27.93 KiB | 2.00 MiB/s, done.
       ~ Total 32 (delta 8), reused 0 (delta 0)
        ~ remote: Resolving deltas: 100% (8/8), done.
         ~ To https://github.com/sbstemen/ExistingCodeToGit.git
          ~  * [new branch]      master -> master

*** YOU NOW HAVE PLACED THE CODE FROM YOUR MACHINE TO THE DESERT ISLAND REPOSITORY OF GIT HUB!!  ***


* NOW LETS GET (PUN INTENDED) THE REMOTE REPOSITORY TO YOUR LOCAL MACHINE.  
* This process does somethings simultaneously that we did in the previous process. 
* When you 'Clone' a repo to your local page it will configure the origin automatically 
* It also initializes the target directory as a git directory and creates the .git files. 
* FROM GitHub collect the target Git you want to clone
  ~ EX: We want to get the 'OpenSolo' project repo Ardupilot-solo
  ~ clone path is ==> https://github.com/OpenSolo/ardupilot-solo.git 
  
 * TO CLONE 
   ~ $ git clone https://github.com/OpenSolo/ardupilot-solo.git aDirectoryICreatedAsTheTargetPath 
 * THAT will copy from the repo OPenSolo the git ArduPilot-Solo into your local path. 
 * IT will create the GIT directory and be ready to use. 
 * THIS will perform the GIT, INIT the directory, and put in a new directory from your current path. 
   ~ git clone https://github.com/codercamps/lms-automation.git FooBar
      ~ That will create the clone the (**) Branch into a directory 
	  

1 ~ Target Directory 
2 ~ From Git Bash { git clone http~foo~path } 
3 ~ You cloned the base or root directory 
4 ~ NOW from git bash do a check out  (*note1*) 
 ~~ git checkout feature/setup 
5 ~ NOW you should have the same stuff as what's in the git hub site /feature/setup






/******************************************************************/
/*****************************************************************/
/*********   Below is the original text from Frank   ************/
/***************************************************************/
/**************************************************************/
update your local branch with all the commits on the remote server

git pull origin <branch name>

e.g. git pull origin dev


create a new branch, run this command on the branch you want to create the feature from (usually dev)

git checkout -b <name of branch>

e.g. git checkout -b feature/harness-student-portal


checkout an existing branch, this essentially is used to switch between branches

git checkout <branch name>

e.g. git checkout dev


stage changes that you wish to commit

git add <file/dir>

this will stages all changes in your current directory

e.g. git add .


commit your stages changes this

git commit -m "comment for the commit goes here"


alternatively you can add and commit in one go, however this will not stage new or deleted files

git commit -a -m "comment goes here"


push your local commits to the remote repository

git push origin <branch name>

e.g. git push origin feature/harness-student-portal
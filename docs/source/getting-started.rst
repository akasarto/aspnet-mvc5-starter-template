===============
Getting Started
===============

Get the project up and running with a few simple steps.

Minimum Requirements
====================

* Windows 10+ machine
* Visual Studio 2017 Community v15.8.3+ or |vs_link|
* SQL Server Express LocalDB or |db_link|.

.. container:: Note

    Visual Studio 2017 includes the **SQL Server Express LocalDB**.

.. |vs_link| raw:: html

   <a href="https://visualstudio.microsoft.com" target="_blank">other edition of your choice</a>

.. |db_link| raw:: html

   <a href="https://www.microsoft.com/en-us/sql-server/sql-server-editions-express" target="_blank">other edition of your choice</a>

Running locally
===============

**Recommended**

* Open a new `cmd` or `powershell` console window.
* Navigate to the project root folder (where it was extracted or cloned).
* Execute the following command to setup the app: `app install` or `./app install`.

  - The command above will attempt to create the database on your local *LocalDb* instance.

* If necessary, change the _web.config_ connection string to point to your desired SQL Server.
* Open the `starterTemplateMVC5.sln` solution file under the `sources` folder.
* If necessary, set _App.UI.Mvc5_ as the startup project.
* Compile and you're good to go.
* Hit F5 to start.

**Manual (if the above fails for some reason)**

* Create a database named `starterTemplateMVC5`
* Execute the script under `/sources/Data.Tools.Migrator/SqlServerScripts/Create_Initial_Db_Structure.sql` to create the db objects.
* Execute the script under `/sources/Data.Tools.Migrator/SqlServerScripts/Create_Initial_SuperUser_Account.sql` to create the default user.
* If necessary, change the _web.config_ connection string to point to your SQL Server.
* Open the `starterTemplateMVC5.sln` solution file under the `sources` folder.
* If necessary, set _App.UI.Mvc5_ as the startup project.
* Compile and you're good to go.
* Hit F5 to start.
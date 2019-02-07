###############
Getting Started
###############

Get the project up and running with a few simple steps.

Minimum Requirements
====================

* Windows 10+ machine
* Visual Studio 2017 Community v15.8.3+ or |vs_link|
* SQL Server Express LocalDB or |db_link|.

*SQL Server Express LocalDB is included in Visual Studio 2017 editions*.

.. |vs_link| raw:: html

   <a href="https://visualstudio.microsoft.com" target="_blank">other edition of your choice</a>

.. |db_link| raw:: html

   <a href="https://www.microsoft.com/en-us/sql-server/sql-server-editions-express" target="_blank">other edition of your choice</a>

Running locally
===============

.. container:: Note

    Default credentials for the initial user:

    * **Username:** ``admin``
    * **Password:** ``password``

**Using Script (Recommended)**

* Open a new ``cmd`` or ``powershell`` console window.
* Navigate to the project root folder (where it was extracted or cloned).
* Execute the following command to setup the app: ``app install`` or ``./app install``.

  - The command above will perform the the following tasks:

    + Check for compatible PowerShell and .NET Framework versions.
    + Restore both server and client side dependent libraries (from *Nuget* and *LibMan*).
    + Compile the application after all required dependencies are properly restored.
    + Create the **starterTemplateMVC5** database on your **LocalDB** instance.

* If necessary, change the **web.config** connection string to point to your desired SQL Server edition.
* Open the ``starterTemplateMVC5.sln`` solution file under the ``sources`` folder.
* If necessary, set **App.UI.Mvc5** as the startup project.
* Hit F5 to start the application.

**Manual Setup (if the above fails for some reason)**

* Create a database named ``starterTemplateMVC5``
* Execute the script under ``/sources/Data.Tools.Migrator/SqlServerScripts/Create_Initial_Db_Structure.sql`` to create the db objects.
* Execute the script under ``/sources/Data.Tools.Migrator/SqlServerScripts/Create_Initial_SuperUser_Account.sql`` to create the default user.
* If necessary, change the **web.config** connection string to point to your SQL Server.
* Open the ``starterTemplateMVC5.sln`` solution file under the ``sources`` folder.
* If necessary, set **App.UI.Mvc5** as the startup project.
* Restore required *Nuget* and *LibMan* dependencies.
* Compile and you should be good to go.
* Hit F5 to start the application.

Optional Settings
=================

The configuration bellow will ensure your project will always start in the home page and that the II Express instance will not shutdown after stopping the debugger.

  .. image:: /_static/mvc_suggested_settings.png

  |

Solution .editorconfig
======================

Under the **0.Solution Items** solution folder, there is a global ``.editorconfig`` file that will ensure consistency between certain aspects of the code. This will apply to all users and will override their indivisual settings for this project.

More details can be found |editorconfig_link|.

.. |editorconfig_link| raw:: html

   <a href="https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options?view=vs-2017" target="_blank">here</a>
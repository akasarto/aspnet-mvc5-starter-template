################
Solution Details
################

Detailed information about the solution architecture, conventions, design choices and used tech.

Projects
========

============================  ===========
   Solution Folder / Project  Description
============================  ===========
`0.Solution Items`            This folder contains globally available scripts, docs and configuration files.
`1.Shared Libs`               This folder contains projects that are independent from the main model and could be event converted to nuget packages to be used in another projects. These projects provide several functionality such as email and sms messaging, storage, image processing and helful extensions.
`2.Domain`                    This folder contains the projects that can be considered the **core** of the onion architecture concept. This will contain the main code for the problem your project is solving.
`3.Data`                      This folder contains the projects that will handle the data your application will produce and/or consume.
`App.Identity`                This is the project that will handle the ASP.Net Identity functionality for the users. It should be self contained and has its own repository and services to handle the app identities.
`App.UI.Mvc5`                 This is the main *User Interface* project and will be the project that your end users will do most of their interaction with.
============================  ===========

Architecture
============

The solution was built following the **Onion Architecture**. The concept helps keet things organized and, as stated by the author himself:

.. container:: Note

    The overall philosophy of the Onion Architecture is to keep your business logic and model in the middle (Core) of your application and push your dependencies as far outward as possible.

Basically, it states that code from inner layers should not depend on code from outer layers.

Check the image below:

|

  .. image:: /_static/onion_architecture.png

|

Using the diagram above as an example, we can say that all code in the application can depend on code from the *core* layer, but code in the *infra* layer can **not** depend on code from *services* and *User Interface* layers.

For more information regarding this topic, please check the original definition post at `http://jeffreypalermo.com/blog/the-onion-architecture-part-1/ <http://jeffreypalermo.com/blog/the-onion-architecture-part-1/>`_.

Folder Conventions
==================

The application folders use a simple structure that has been proven to keep things handy and cleanly organized. Take the snippet bellow:

::

    folder
    ├── subfolder
    |   └── file.ext
    └── file.ext

Every major feature has its own folder. Files shared by that feature will be kept in the root folder and possible subfolders will follow the same rules. Taking *part* of the **Infrastructure** folder from the **App.UI.Mvc5** project, we have the following result:

::

    Infrastructure
    ├── Blobs
    │   ├── BlobService.cs
    │   ├── BlobServiceConfigs.cs
    │   ├── BlobUploadResult.cs
    │   └── IBlobService.cs
    ├── Cookies
    │   ├── GetCookie.cs
    │   └── SetCookie.cs
    ├── Realtime
    |   ├── Configs
    │   |   ├── HubActivator.cs
    │   |   ├── SignalRCamelCaseJsonResolver.cs
    │   |   └── UserIdProvider.cs
    |   ├── Hubs
    │   |   └── DatabusHub.cs
    │   ├── IRealtimeService.cs
    │   └── RealtimeService.cs
    ├── UrlExtensions
    │   ├── BlobThumbnail.cs
    │   └── GetHomeUrl.cs
    ├── AppAreas.cs
    ├── AppJsonResult.cs
    ├── AppJsonResult.cs
    ├── AppViewEngine.cs
    ├── AppWebViewPage.cs
    ├── AuthorizeAttribute.cs
    ├── DirectRouteProvider.cs
    └── SetLayout.cs

Namespace Conventions
=====================

Namespaces are defined keeping the folder structure above in mind, but they will go no farther then the first subfolder level for an specific project. Using the **App.UI.Mvc5** project as example, the namespaces will be as follows:

::

    App.UI.Mvc5
    ├── Controllers
    │   └── (Namespace: App.UI.Mvc5.Controllers)
    ├── Infrastructure
    |   ├── Configs
    │   |   └── (Namespace: App.UI.Mvc5.Infrastructure)
    │   └── (Namespace: App.UI.Mvc5.Infrastructure)
    ├── Models
    │   └── (Namespace: App.UI.Mvc5.Models)
    └── (Namespace: App.UI.Mvc5)

Tech Used and Third-Party Libraries
===================================

A list with the main tech and libraries that are used throughout the solution, for further information.

**Microsoft**

* Latest released Visual Studio Community edition or higher (https://www.visualstudio.com).
* ASP.Net MVC 5 (http://www.asp.net/mvc).
* ASP.Net Identity 2.0 (http://www.asp.net/identity).
* SQL Server Express and Tools (https://www.microsoft.com/en-us/sql-server/sql-server-editions-express/).
* PowerShell - for script execution and automation (https://docs.microsoft.com/en-us/powershell/scripting/overview).

**Third party libraries**

* Image Resizer **\**** - for local image manipulation (http://imageresizing.net/plugins/editions/free).
* Dapper .Net Micro ORM - for data manipulation (https://github.com/StackExchange/Dapper).
* Simple Injector - for IoC and Dependency injection (https://simpleinjector.org).
* Serilog - for logging (https://serilog.net).
* Json.NET - for json data manipulation (http://www.newtonsoft.com/json).
* FluentValidation - for client and server data validation (https://fluentvalidation.net).
* ValueInjecter - for class mapping (https://github.com/omuleanu/ValueInjecter).
* FluentMigrator - for database robust versioning and manipulation (https://fluentmigrator.github.io).

| \* *Free and paid version available.*

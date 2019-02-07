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

The proposed folder structure convention applies to any place that can be grouped by feature. The idea is to use as less subfolders as possible, keeping what is common in the root folder for that feature and what is specialized on its own subfolder structure (that follows the same pattern). Bellow we are using the **Views** from the UI project as an example.

| ``/Views``
|   ``/Alerts``
|   ``/Calendar``
|   ``/Contacts`` *(Groups all views)*
|     ``Form.cshtml``
|     ``Index.cshtml``
|     ``Manager.cshtml``
|     ``Modal.cshtml``
|   ``/Dashboard``
|   ``/Static`` *(Subfolders following the same structure pattern)*
|     ``/Forms``
|     ``/Tables``
|     ``/UIElements``
|     ``About.cshtml``
|     ``Dashboard.cshtml``
|     ``invoice.cshtml``
|     ``...``
|   ``/Users``
|   ``_Layout.cshtml`` *(Shared files live in the root folder.)*
|   ``_LayoutInternal.cshtml``
|   ``_ViewStart.cshtml``
|   ``...``

Namespace Conventions
=====================

When using the folder structure detailed above, some or maybe all files in that folder may be code classes. In this case, we use another convention that apply to namespaces. Consider the sample bellow:

| ``Sarto.Web.UI``
|   ``/Controllers``
|     ``/Contacts``
|       ``_ContactsController.cs`` *(Namespace: Sarto.Web.UI.Controllers)*
|     ``/Users``
|       ``_UsersController.cs``
|     ``BaseController.cs`` *(Namespace: Sarto.Web.UI.Controllers)*
|   ``/Models``
|     ``/Clients``
|       ``ModelX.cs`` *(Namespace: Sarto.Web.UI.Models)*
|       ``ModelY.cs``
|       ``ModelZ.cs``
|     ``ModelA.cs`` *(Namespace: Sarto.Web.UI.Models)*
|     ``ModelB.cs``
|     ``ModelC.cs``
| ``Initializer.cs`` *(Namespace: Sarto.Web.UI)*
| ``WebUIContext.cs``
| ``...``

Tech Used and Third-Party Libraries
===================================

**Microsoft**

* Visual Studio 2015 Communit or Higher (https://www.visualstudio.com/).
* ASP.Net MVC 5 (http://www.asp.net/mvc).
* ASP.Net Identity 2.0 (http://www.asp.net/identity).
* ASP.Net WebApi 2.0 (http://www.asp.net/web-api).
* SQL Server Express and Tools (http://downloadsqlserverexpress.com/).

**Third party libraries**

* Porto HTML Template **\*** (https://themeforest.net/item/porto-admin-responsive-html5-template/8539472).
* Image Resizer **\**** - for local image manipulation (http://imageresizing.net/plugins/editions/free).
* Dapper .Net Micro ORM - for data manipulation (https://github.com/StackExchange/dapper-dot-net).
* Simple Injector - for IoC and Dependency injection (https://simpleinjector.org).
* NLog - for logging (http://nlog-project.org).
* Json.NET - for json data manipulation (http://www.newtonsoft.com/json).
* FluentValidation - for client and server data validation (https://github.com/JeremySkinner/FluentValidation).
* ValueInjecter - for class mapping (https://github.com/omuleanu/ValueInjecter).

| \* *Paid license available.*
| \** *Free and paid version available.*

|
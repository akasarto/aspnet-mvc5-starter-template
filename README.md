# About
Asp.Net MVC 5 Starter Kit is a basic, clean, globalized and S.O.L.I.D template with all the necessary boilerplate is ready to go.

For more details and getting started, check our online documentation at https://mvc5-starter-template.readthedocs.org.

Some of the main features are:

- Bootstrap 4 based layout.
- Realtime ready with SignalR.
- Asp.Net Identity with role permissions.
- Dependency injection with SimpleInjector.
- Out of the box mailing system with support for native SMTP and MailGun Api.
- Out of the box dynamic image thumb generation with ImageResizer and Cloudinary.
- Client side libraries managed by the new Visual Studio Library Manager (LibMan).
- Globalization ready and by-user language, region and timezone definition.
- Error logging with Serilog.

And more to come...

## Default Username and Password

- admin
- password

## Pre Requirements

- Tooling and Environment
  - Windows 10+ machine
  - Visual Studio 2017 Community v15.8.3+.
- Clone or download the code from the _master_ branch.

## Running

- Recommended
  - Open a new `cmd` or `powershell` console window.
  - Navigate to the project root folder (where it was extracted or cloned).
  - Execute the following command to setup the app: `app install` or `./app install`.
    - The command above will attempt to create the database on your local *LocalDb* instance.
  - If necessary, change the _web.config_ connection string to point to your desired SQL Server.
  - Open the `starterTemplateMVC5.sln` solution file under the `sources` folder.
  - If necessary, set _App.UI.Mvc5_ as the startup project.
  - Compile and you're good to go.
  - Hit F5 to start.

- Manual (if the above fails for some reason)
  - Create a database named `starterTemplateMVC5`
  - Execute the script under `/sources/Data.Tools.Migrator/SqlServerScripts/Create_Initial_Db_Structure.sql` to create the db objects.
  - Execute the script under `/sources/Data.Tools.Migrator/SqlServerScripts/Create_Initial_SuperUser_Account.sql` to create the default user.
  - If necessary, change the _web.config_ connection string to point to your SQL Server.
  - Open the `starterTemplateMVC5.sln` solution file under the `sources` folder.
  - If necessary, set _App.UI.Mvc5_ as the startup project.
  - Compile and you're good to go.
  - Hit F5 to start.

## Recommended Layout Templates

- [Porto HTML5 Template](http://themeforest.net/item/porto-responsive-html5-template/4106987?ref=Sartor)
- [Porto Admin HTML5 Template](https://themeforest.net/item/porto-admin-responsive-html5-template/8539472?ref=Sartor)

## Screenshots

![Overview](/docs/ss1.png?raw=true "Overview")

## License

MIT License

Copyright (c) 2018 Sarto Research

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

## Donation

If you liked this work, please consider a small donation to help keeping it up.

[![PayPal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=V5RS2ZLZKG37E)

# About
This is a MVC5 starter kit application with all the initial (and boring) boilerplate already implemented. Some of the main features are:

- Bootstrap 4 based layout.
- Realtime ready with SignalR.
- Asp.Net Identity with role permissions.
- Dependency injection with SimpleInjector.
- Out of the box mailing system with support for native SMTP and MailGun Api.
- Out of the box dynamic image thumb generation with ImageResizer and Cloudinary.
- Globalization ready and by-user language, region and timezone definition.
- Error logging with Serilog.

And more to come...

## Running

- Db
  - Create a database named `skeletonMvc5`
  - Execute the script under `/sources/db-schemas/create_Db.sql` to create the db objects.
  - Execute the script under `/sources/db-schemas/create_Users.sql` to create the default user.
  - If necessary, change the _web.config_ connection string to point to your server.

- Web App
  - Get Visual Studio 2017 Community v15.5+.
  - Get the code from this repository _master_ branch.
  - Make sure nuget packages are set to auto restore.
  - Set _App.UI.Mvc5_ as the startup project.
  - Compile and you're good to go.
  - Hit F5 to start.

## Recommended Layout Templates

- [Porto HTML5 Template](http://themeforest.net/item/porto-responsive-html5-template/4106987?ref=Sartor)
- [Porto Admin HTML5 Template](https://themeforest.net/item/porto-admin-responsive-html5-template/8539472?ref=Sartor)

## Screenshots

![Overview](/media/ss1.png?raw=true "Overview")

## License

MIT License

Copyright (c) 2018 Thiago Alberto Schneider

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

## License

If this work was helpful to you, please consider a small donation to help keeping it up.  

[![PayPal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=V5RS2ZLZKG37E)

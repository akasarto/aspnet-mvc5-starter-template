# About
This is a MVC5 starter kit application with all the initial (and boring) boilerplate already implemented. Some of the main features are:

- Realtime ready with SignalR.
- Asp.Net Identity with role permissions.
- Dependency injection with SimpleInjector.
- Out of the box mailing system with support for native SMTP and MailGun Api.
- Out of the box dynamic image thumb generation with ImageResizer and Cloudinary.
- Globalization ready and by-user language, region and timezone definition.
- Error logging with Serilog.

And more to come...

## License

> This project is free for non-commercial use. To use it commercially, please purchase a one time license for only **$15**.  
>\
> [![PayPal](https://www.paypalobjects.com/en_US/i/btn/btn_buynowCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=TX5U3WZZDZYEQ)
>
> \* Price is in United States Dollar (USD).

## Running

- Db
  - Create a database named `skeletonMvc5`
  - Execute the script under `/sources/db-schemas/create_Db.sql` to create the db objects.
  - Execute the script under `/sources/db-schemas/create_Users.sql` to create the default user.
  - If necessary, change the _web.config_ connection string to point to your server.

- Web App
  - Get Visual Studio 2017 Community v15.5+.
  - `https://github.com/akasarto/skeleton-system-netfx-mvc5.git`.
  - Make sure nuget packages are set to auto restore.
  - Set _App.UI.Mvc5_ as the startup project.
  - Compile and you're good to go.
  - Hit F5 to start.

## Screenshots

![Overview](/media/ss1.png?raw=true "Overview")
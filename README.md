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

## License

>This project is free for non-commercial use. To use it commercially, please purchase a one time license* for only **$15**.  
>\
>[![PayPal](https://www.paypalobjects.com/en_US/i/btn/btn_buynowCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=TX5U3WZZDZYEQ)
>
> \* Licenses are per project.  
> \* Price is in United States Dollar (USD).  
> \* This project took a lot of effort. Please avoid piracy.  
> \* Payment receipt will be under the name _SCHNEIDER SYSTEMS LTDA - ME_.

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
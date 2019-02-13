###################
Client Side Scritps
###################

This solution does not use nuget packages to reference client side scripts and libraries. Instead, we use the newly available Library Manager (LibMan) that has become available since the release of Visual Studio 2017 Community v15.8.3+ or higher.

Referencing new libraries is pretty simple, just edit the ``libman.json`` file under the **App.UI.Mvc5** project root providing the library name and the destination to where it should be restored and you'll be good to go:

..  code-block:: json

    {
        "version": "1.0",
        "defaultProvider": "cdnjs",
        "libraries": [
            {
                "library": "jquery@3.3.1",
                "destination": "Assets/vendor/jquery"
            }
        ]
    }

You can browse available libraries from the https://cdnjs.com/ catalog or add your preferred source.

For more details, check the official docs at https://docs.microsoft.com/en-us/aspnet/core/client-side/libman.



|
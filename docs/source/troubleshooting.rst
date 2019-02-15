###############
Troubleshooting
###############

Possible problems that may occur during the development lifecycle.

Scripted Build Errors
=====================

In some specific cases, the command ``app.cmd install`` may fail. If you're facing problems with that, try the following steps:

* On your **development** machine, look for the ``Visual Studio Installer`` application:

  .. image:: /_static/vs_installer.png

  |

* Make sure that you have the **Nuget targets and build tasks** selected:

  .. image:: /_static/vs_build_tools.png

  |

  .. image:: /_static/vs_build_tools_targets.png

  |

Assembly mappings
=================

Specially after updating nuget packages, you may experience runtime errors like the following:

``Could not load file or assembly 'xxx' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)``

That is usually caused by assembly bindings that were not properly updated, along with the packages, in your **web.config** file, under the ``configuration/runtime/assemblyBinding`` node:

  .. image:: /_static/assembly_bindings.png

  |

You can manually fix that by comparing the package versions with the ones being redirected to or let **Visual Studio** handle it for you by doing the following steps:

* Completely delete the ``assemblyBinding`` node from the **web.config** file.

  .. image:: /_static/assembly_bindings_delete.png

  |

* Recompile the application using Visual Studio.

* Click on the compilation warning as instructed and click 'Yes' to the action box that is shown.

  .. image:: /_static/assembly_bindings_warning.png

  |

Your assembly bindings should now be pointing to the correct versions and the runtime errors will be gone.

For more details, check the official docs at https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/redirect-assembly-versions.

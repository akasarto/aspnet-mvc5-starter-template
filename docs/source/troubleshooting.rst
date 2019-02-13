###############
Troubleshooting
###############

Possible problems that may occur during the development lifecycle.

Scripted Build Errors
=====================

For some specific cases, automattic build from ``app.cmd install`` command may fail. The most common scenario for that, so far, was that the dev/server machine did not had the proper environment setup. If you facing problem building the application from script, try the following steps:

* On your **Windows 10** machine, look for the ``Visual Studio Installer`` application.

  .. image:: /_static/vs_installer.png

  |

* Make sure that at least the **Nuget targets and build tasks** are selected.

  .. image:: /_static/vs_build_tools.png

  |

  .. image:: /_static/vs_build_tools_targets.png

  |

Assembly mappings
=================

Specially after updating nuget packages, you may experience runtime errors like the following:

``Could not load file or assembly 'xxx' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)``

That is usually caused by assembly bindings that were not properly updated in the **web.config** file under ``configuration/runtime/assemblyBinding`` node:

  .. image:: /_static/assembly_bindings.png

  |

You can fix this by comparing the nuget package version installed with the one that the assembly is being redirected to, but the easiest way is to go through the following steps:

* Completely delete the ``assemblyBinding`` node from the **web.config** file.

  .. image:: /_static/assembly_bindings_delete.png

  |

* Recompile the application using Visual Studio.

* Click on the compilation warning as instructed and click 'Yes' to the action box that is shown.

  .. image:: /_static/assembly_bindings_warning.png

  |

Your assembly bindings should now pointing to the correct versions and the runtime errors will be fixed.

For more details, check the official docs at https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/redirect-assembly-versions.

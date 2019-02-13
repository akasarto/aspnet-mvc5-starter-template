###############
Troubleshooting
###############

Possible problems that may occur during the development lifecycle.

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

* Recompile the application using Visual Studio.

* Click on the compilation warning as instructed and click 'Yes' to the action box that is shown.

  .. image:: /_static/assembly_bindings_warning.png

* Your assembly bindings should now pointing to the correct versions and the runtime error will be fixed.



  |
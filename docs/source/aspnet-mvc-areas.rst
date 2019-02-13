#################
Asp.NET MVC Areas
#################

.. raw:: html

    <div style="text-align:center;">
        <iframe width="560" height="315" src="https://www.youtube.com/embed/cTqvS0tRCoU" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
    </div>

|

It is not very common to see areas being used in MVC applications, but when properly configured, they can be very handy. Providing good modularity options to the solution:

  .. image:: /_static/areas_top.png

  |

Each area can be considered a 'mini mvc' website inside the main app and need just a few adjustments to get up and running:

* Area name and namespaces
* Area controllers
* Views and models
* Client side scripts

The easiest way to create a new area is copying the ``Blank`` area and renaming the required classes as needed.

Areas **must** have their own base controller that inherits from the main base controller (``__BaseController.cs``). For convention, the area base controller is named ``__AreaBaseController.cs``.

Considering you may want to create a new area named ``Products``, your new area base controller will look like the following:

.. code-block:: csharp

    using App.UI.Mvc5.Controllers;
    using System.Web.Mvc;

    namespace App.UI.Mvc5.Areas.Products.Controllers
    {
        [RouteArea("Products", AreaPrefix = "products")]
        public abstract class __AreaBaseController : __BaseController
        {
        }
    }

Notice above that we also set the ``AreaPrefix`` in there to make sure all routes that bellong to the new area will start with ``procucts/``.

Another controller that is required is the ``_LandingController.cs``. The sole responsibility for this controller is to redirect the request to the primary controller of the area. It was designed like this so all areas can be called in a standardized way. The landing controller for the example above will look like the following:

.. code-block:: csharp

    using System.Web.Mvc;

    namespace App.UI.Mvc5.Areas.Products.Controllers
    {
        [RoutePrefix("")]
        public class _LandingController : __AreaBaseController
        {
            [HttpGet]
            [Route(Name = "Products_Landing_Index_Get")]
            public ActionResult Index() => RedirectToAction("Index", "Overview");
        }
    }

With that setup, it means that each time some request is made to the route ``.../products``, the request will be redirected to the ``Index`` action in the ``OverviewController.cs`` class:

.. code-block:: csharp

    using App.UI.Mvc5.Infrastructure;
    using App.UI.Mvc5.Models;
    using System.Web.Mvc;

    namespace App.UI.Mvc5.Areas.Products.Controllers
    {
        [RoutePrefix("overview")]
        [TrackMenuItem("products.overview")]
        public partial class OverviewController : __AreaBaseController
        {
            [Route(Name = "Products_Overview_Index_Get")]
            public ActionResult Index()
            {
                var model = new EmptyViewModel();

                return View(model);
            }
        }
    }

And for the final required step when setting up a new area, we must create a ``_MenuController.cs`` class as follows:

.. code-block:: csharp

    using App.UI.Mvc5.Models;
    using System.Web.Mvc;

    namespace App.UI.Mvc5.Areas.Products.Controllers
    {
        [RoutePrefix("menu")]
        public class _MenuController : __AreaBaseController
        {
            [Route("top-menu-item", Name = "Products_Menu_TopMenuItem")]
            public ActionResult TopMenuItem()
            {
                var model = new EmptyPartialViewModel();

                return PartialView(model);
            }
        }
    }

Notice that it will return a partial view named ``TopMenuItem.cshtml`` that will be available under the area ``Views`` folder:

.. code-block:: csharp

    @using App.UI.Mvc5.Areas.Products

    <!-- Page Contents -->

    <li class="nav-item @Menu.IfActiveItem("products.*", "active")">
        <a class="nav-link" href="@Url.Action("Index", "_Landing")">@(GetLocalizedString<AreaResources>("Products"))</a>
    </li>

The partial view can then be called anywhere in the main website to render the area menu entry (normally in the root ``TopMenu.cshtml`` file):

.. code-block:: csharp

    <ul class="nav navbar-nav mr-auto">

        <li class="nav-item @Menu.IfActiveItem("root.landing", "active", string.Empty)">
            <a class="nav-link" href="@Url.GetHomeUrl()">@GetLocalizedString("Home")</a>
        </li>

        @Html.Action("TopMenuItem", "_Menu", new { area = AppAreas.GetAreaName(Area.Features) })

        @Html.Action("TopMenuItem", "_Menu", new { area = AppAreas.GetAreaName(Area.Blank) })

        @Html.Action("TopMenuItem", "_Menu", new { area = AppAreas.GetAreaName(Area.Products) })

    </ul>

One last thing to notice is that, when using areas, all website links MUST know to which area the route is supposed to belong. To facilitate that process, the system provide a helper class named ``AppAreas.cs`` that can be found under the ``Infrastructure`` folder. Just add the new area name to the ``Areas`` enumerator and, when creating links, call the method as show above.

.. code-block:: csharp

    namespace App.UI.Mvc5.Infrastructure
    {
        public enum Area : int
        {
            Root,
            Blank,
            Features,
            Management,
            Users,
            Products
        }

        public class AppAreas
        {
            public static string GetAreaName(Area area)
            {
                if (area == Area.Root)
                {
                    return string.Empty;
                }

                return area.ToString();
            }
        }
    }



|
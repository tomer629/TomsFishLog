using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TomsFishLog.Controllers
{
    public class MapController : Controller
    {
        // GET: Map

        public PartialViewResult _mapPartial()
        {
            //todo map needs a full screen or at least large mode.
            return PartialView("_mapPartial");
        }

        public PartialViewResult _map_SelectFishCoords()
        {

            return PartialView("_mapPartial");
        }
    }
}
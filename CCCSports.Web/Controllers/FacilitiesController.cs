using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class FacilitiesController : BaseController
    {
        // GET: Facilities
        /// <summary>
        /// Returns the facilities home view with all facilites
        /// </summary>
        /// <returns></returns>
        public ActionResult FacilitiesHome()
        {
            return View();
        }

        /// <summary>
        /// Returns the Activity hall details view
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityHallDetails()
        {
            return View();
        }

        /// <summary>
        /// Returns the function room details view
        /// </summary>
        /// <returns></returns>
        public ActionResult FunctionRoomDetails()
        {
            return View();
        }


        /// <summary>
        /// Returns the swimming pool details view
        /// </summary>
        /// <returns></returns>
        public ActionResult SwimmingPoolDetails()
        {
            return View();
        }


        /// <summary>
        /// Returns the gym details view
        /// </summary>
        /// <returns></returns>
        public ActionResult GymDetails()
        {
            return View();
        }


        /// <summary>
        /// Returns the cafe details details view
        /// </summary>
        /// <returns></returns>
        public ActionResult CafeDetails()
        {
            return View();
        }



    }
}
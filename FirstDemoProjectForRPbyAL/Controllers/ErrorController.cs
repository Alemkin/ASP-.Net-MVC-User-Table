using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstDemoProjectForRPbyAL.Controllers
{
    /**
     * This is the Controller for handling Errors via
     * Http Status Codes
     * <author> Alexander Lemkin </author>
     */
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Error/BadRequest
        public ActionResult BadRequest()
        {
            return View();
        }

        // GET: /Error/InternalServerError
        public ActionResult InternalServerError()
        {
            return View();
        }

        // GET: /Error/NotFound
        public ActionResult NotFound()
        {
            return View();
        }
	}
}
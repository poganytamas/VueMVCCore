namespace VueMVCCore.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Net;
    using VueMVCCore.Models;

    public class HomeController : Controller
    {
        [Authorize(Constants.LoggedInPolicyName)]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            // Was this an API internal server error, or a generic one?
            if ((HttpContext.Request.Headers["Accept"]).First().Split(',')
       .Any(t => t.Equals("application/json", StringComparison.OrdinalIgnoreCase)))
                return StatusCode((int)HttpStatusCode.InternalServerError, new { feature?.Error?.Message });
            else
                return View(feature?.Error);
        }

        [AllowAnonymous]
        public IActionResult UnauthorizedRequest()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult APINotFound()
        {
            return NotFound();
        }
    }
}
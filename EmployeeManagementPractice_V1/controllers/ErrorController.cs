using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementPractice_V1.controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View("Error");
        }

        [Route("Error/{statuscode}")]
        public IActionResult StatusCodeHandler(int statuscode)
        {
            switch (statuscode)
            {
                case 404: ViewBag.ErrorMessage = "The page is not found";
                    var e = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    logger.LogError($"The Page has thrown exception {e.OriginalPath}");
                        break;
            }

            return View("NotFound");
        }
    }
}
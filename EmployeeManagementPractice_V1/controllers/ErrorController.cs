using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementPractice_V1.controllers
{
    public class ErrorController : Controller
    {
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
                        break;
            }

            return View("NotFound");
        }
    }
}
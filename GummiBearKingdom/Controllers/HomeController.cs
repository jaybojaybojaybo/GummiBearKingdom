﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GummiBearKingdom.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "If you need any assistance, please contact the departments below.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

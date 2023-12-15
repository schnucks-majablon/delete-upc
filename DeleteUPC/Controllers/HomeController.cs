using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeleteUPC.Models;

namespace DeleteUPC.Controllers
{
    public class HomeController : Controller
    {
       // [Authorize]
        public IActionResult DeleteUPC()
        {
            return View();
        }
    }
}

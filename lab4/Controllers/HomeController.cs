using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab4.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    public class HomeController : Controller
    {
      
       
        public IActionResult Index()
        {
            return View();
        }
        private readonly SchoolCommunityContext _context;
        public IActionResult Error()
        {
            return View();
        }
        public HomeController(SchoolCommunityContext context)
        {
            _context = context;
        }
    }
}

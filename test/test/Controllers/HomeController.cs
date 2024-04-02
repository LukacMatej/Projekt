using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using test.Models;
using UW.Shibboleth;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[Authorize]
        public IActionResult Index()
        {
            //var vm = new HomeViewModel();
            //var ident = (ClaimsIdentity)HttpContext.User.Identity;
            //
            //vm.NetID = ident.FindFirst(UWShibbolethClaimsType.UID).Value;
            //vm.wiscEduPVI = ident.FindFirst(UWShibbolethClaimsType.PVI).Value;
            //
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

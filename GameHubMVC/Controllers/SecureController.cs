using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameHubMVC.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult BeatTheBanker()
        {
            return View();
        }
    }
}
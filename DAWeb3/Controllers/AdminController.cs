using Microsoft.AspNetCore.Mvc;

namespace DAWeb3.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login", "Default");
            }
            return View();
        }

        public ActionResult RenderMenu()
        {
            return PartialView("_Navbar");
        }
    }
}

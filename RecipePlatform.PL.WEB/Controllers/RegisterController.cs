using Microsoft.AspNetCore.Mvc;

namespace RecipePlatform.PL.WEB.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}

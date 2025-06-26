using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipePlatform.PL.WEB.ViewModel;

namespace RecipePlatform.PL.WEB.Views.Account
{
    public class RegisterModel : PageModel
    {
        //[BindProperty]
        public Register Model;
        public void OnGet()
        {
        }
    }
}

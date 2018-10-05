using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class EditModel : PageModel
    {
        public EditModel()
        {
        }
        
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}

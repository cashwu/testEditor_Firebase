using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }

        public void OnGet()
        {
        }
    }
}
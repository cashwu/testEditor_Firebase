using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class Edit2Model : PageModel
    {
        public Edit2Model()
        {
        }

        public IActionResult OnGet()
        {
            if (TempData.ContainsKey("H"))
            {
               Html = TempData["H"].ToString();
            }
            if (TempData.ContainsKey("M"))
            {
               Md = TempData["M"].ToString();
            }
            return Page();
        }
        
        [BindProperty(Name = "editormd-image-file")]
        public IFormFile File { get; set; }
        
        [BindProperty(Name = "test-editormd-html-code")]
        public string Html { get; set; }
        
        [BindProperty(Name = "test-editormd-markdown-doc")]
        public string Md { get; set; }
        
        public IActionResult OnPostImg(long guid)
        {
            return new OkObjectResult(new
            {
                success = 1, // 0 表示上传失败，1 表示上传成功
                message = "Success",
                url = "https://i.imgur.com/89lzkTD.png" // 上传成功时才返回
            });
        }
        
        public IActionResult OnPost()
        {
            TempData["H"] = Html;
            TempData["M"] = Md;
            return RedirectToPage();
        }
    }
}
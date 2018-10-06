using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
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
        
        public IActionResult OnPost(List<IFormFile> image, string alt)
        {
            var r = new
            {
                data = new
                {
                    link = "https://i.imgur.com/89lzkTD.png"
                },
                success = true
            };
            
            // {
            //     "data":{"id":"89lzkTD","title":null,"description":null,"datetime":1538831904,"type":"image\/png","animated":false,"width":960,"height":403,"size":56162,"views":0,"bandwidth":0,"vote":null,"favorite":false,"nsfw":null,"section":null,
            //         "account_url":null,"account_id":0,"is_ad":false,"in_most_viral":false,"has_sound":false,"tags":[],"ad_type":0,"ad_url":"","in_gallery":false,"deletehash":"IgE4IqzQIAHsXxu","name":"",
            //         "link":"https:\/\/i.imgur.com\/89lzkTD.png"},
            //     "success":true,
            //     "status":200
            // } 
            return new OkObjectResult(r);
        }
    }
}

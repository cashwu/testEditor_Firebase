using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Firebase.Auth;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            ImgUrl = new List<string>();
        }
        
        public List<string> ImgUrl { get; set; }

        public async Task<IActionResult> OnPostAsync(List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return Page();
            }

            foreach (var file in files)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBDbJVPbvfQRe6IbD2K5yHSjr74_YbSJs8"));
                    var a = await auth.SignInWithEmailAndPasswordAsync("cash@cashwu.com", "E0F31815-1D25-4D98-80C4-629332F1B8F6");
                    var url = await new FirebaseStorage("website-cashwu.appspot.com", new FirebaseStorageOptions
                                    {
                                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                                        ThrowOnCancel = true
                                    })
                                    .Child("data")
                                    .Child(file.FileName)
                                    .PutAsync(ms);

                    ImgUrl.Add(url);
                }
            }

            return Page();
        }
    }
}
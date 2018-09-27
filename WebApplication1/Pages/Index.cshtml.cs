using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            ImgUrl = new List<string>();
        }

        public List<string> ImgUrl { get; set; }
        public Data FObject { get; set; }
        public string Key { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(""));
            var a = await auth.SignInWithEmailAndPasswordAsync("cash@cashwu.com", "");
            
            var firebaseClient = new FirebaseClient("https://.firebaseio.com/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken)
            });
            
            var data = await firebaseClient.Child("data")
                                           .OrderByKey()
                                           .LimitToFirst(1)
                                           .OnceAsync<Data>();
            
            FObject = data.FirstOrDefault()?.Object;
            Key = data.FirstOrDefault()?.Key;

            return Page();
        }

        public async Task<IActionResult> OnPostDataAsync()
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(""));
            var a = await auth.SignInWithEmailAndPasswordAsync("cash@cashwu.com", "");

            var firebaseClient = new FirebaseClient("https://.firebaseio.com/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken)
            });

            var obj = await firebaseClient.Child("data")
                                          .PostAsync(new Data
                                          {
                                              Name = "T",
                                              Url = "url"
                                          });
            FObject = obj.Object;
            Key = obj.Key;

            return Page();
        }

        public async Task<IActionResult> OnPostDataUpdateAsync()
        {
            Key = "-LNNfUTWxJF-Iuq8_Iqv";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(""));
            var a = await auth.SignInWithEmailAndPasswordAsync("cash@cashwu.com", "");

            var firebaseClient = new FirebaseClient("https://.firebaseio.com/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken)
            });

            await firebaseClient.Child("data")
                                .Child(Key)
                                .PutAsync(new
                                {
                                    Name = "T3",
                                    Url = "url3"
                                });

            return Page();
        }

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

                    var auth = new FirebaseAuthProvider(new FirebaseConfig(""));
                    var a = await auth.SignInWithEmailAndPasswordAsync("cash@cashwu.com", "");
                    var url = await new FirebaseStorage(".appspot.com", new FirebaseStorageOptions
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

    public class Data
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
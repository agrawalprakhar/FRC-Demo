using Firebase.Models;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using static Firebase.Program;

namespace Firebase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            //string serviceAccountFilePath = "C:/Users/admin/Downloads/fir-demo-53a64-firebase-adminsdk-hasa6-5f2a28253f.json"; // Update with the path to your service account JSON file
            //string serviceAccountJson = System.IO.File.ReadAllText(serviceAccountFilePath);

            //GoogleCredential googleCredential = GoogleCredential.FromJson(serviceAccountJson);
            //GoogleCredential scoped = googleCredential.CreateScoped("https://www.googleapis.com/auth/firebase.remoteconfig");
            //var token = scoped.UnderlyingCredential.GetAccessTokenForRequestAsync();
            //var accessToken = token.Result;

            //string projectId = "fir-demo-53a64";
            //string url = $"https://firebaseremoteconfig.googleapis.com/v1/projects/{projectId}/remoteConfig";

            //bool isButtonEnabled = true;

            //// Create an HTTP client
            //using (HttpClient client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            //    try
            //    {
            //        HttpResponseMessage response = await client.GetAsync(url);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            string jsonResponse = await response.Content.ReadAsStringAsync();
            //            var config = JsonConvert.DeserializeObject<RemoteConfigResponse>(jsonResponse);

            //            isButtonEnabled = bool.Parse(config.Parameters["btn_enable"].DefaultValue.Value);
            //            Console.WriteLine(isButtonEnabled);
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Failed to fetch Remote Config parameters. Status code: {response.StatusCode}");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"An error occurred: {ex.Message}");
            //    }
            //}

            //ViewData["IsButtonEnabled"] = isButtonEnabled;
            return View();
        }
        public async Task<IActionResult> GetRemoteConfig()
        {
            string serviceAccountFilePath = "C:/Users/admin/Downloads/fir-demo-53a64-firebase-adminsdk-hasa6-5f2a28253f.json"; // Update with the path to your service account JSON file
            string serviceAccountJson = System.IO.File.ReadAllText(serviceAccountFilePath);

            GoogleCredential googleCredential = GoogleCredential.FromJson(serviceAccountJson);
            GoogleCredential scoped = googleCredential.CreateScoped("https://www.googleapis.com/auth/firebase.remoteconfig");
            var token = await scoped.UnderlyingCredential.GetAccessTokenForRequestAsync();
            var accessToken = token;

            string projectId = "fir-demo-53a64";
            string url = $"https://firebaseremoteconfig.googleapis.com/v1/projects/{projectId}/remoteConfig";

           
            bool isButtonEnabled =false;
            // Create an HTTP client
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var config = JsonConvert.DeserializeObject<RemoteConfigResponse>(jsonResponse);

                        isButtonEnabled = bool.Parse(config.Parameters["btn_enable"].DefaultValue.Value);
                        Console.WriteLine(isButtonEnabled);
                        ViewData["IsButtonEnabled"] = isButtonEnabled;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch Remote Config parameters. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                  
                       Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return PartialView("_ButtonStatePartial");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
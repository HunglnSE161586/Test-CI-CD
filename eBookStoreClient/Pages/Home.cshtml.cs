using eBookStoreClient.OData;
using eBookStoreLib.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eBookStoreClient.Pages
{
    public class HomeModel : PageModel
    {
        private readonly HttpClient client;
        private string apiUrl = "";
        [BindProperty]
        public User User { get; set; }
        public User currentUser { get; set; }
        public HomeModel()
        {
            client = new HttpClient();
            //var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            //client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            User = null;
            string userData = HttpContext.Session.GetString("USER");
            if (userData == null)
                return RedirectToPage("/Index");
            currentUser = JsonSerializer.Deserialize<User>(userData);
            if (!currentUser.Role.RoleDesc.Equals("Admin"))
            {
                apiUrl = "https://localhost:7185/odata/Users?$filter=UserId eq " + currentUser.UserId;
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                //List<User> users = JsonSerializer.Deserialize<List<User>>(strData, option);
                //User = users.FirstOrDefault();
                var result= JsonSerializer.Deserialize<ODataResponse<User>>(strData, option);
                List<User> users = result.Value;
                User = users.FirstOrDefault();
            }

            //if(currentUser == null)
            //    return RedirectToPage("/Index");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string userData = HttpContext.Session.GetString("USER");
            currentUser = JsonSerializer.Deserialize<User>(userData);
            User.HireDate=DateTime.Now;
            
            apiUrl = "https://localhost:7185/odata/Users/"+User.UserId;
            var json = JsonSerializer.Serialize(User);
            HttpResponseMessage response = await client.PutAsync(
                apiUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            
            currentUser.EmailAddress = User.EmailAddress;
            currentUser.Source = User.Source;
            currentUser.FirstName = User.FirstName;
            currentUser.LastName= User.LastName;
            currentUser.MiddleName= User.MiddleName;
            HttpContext.Session.SetString("USER", JsonSerializer.Serialize(currentUser));
            return RedirectToPage("/Home");
        }
    }
}
//{ "UserId":1,
//"EmailAddress":"test1@gmail.com",
//"Password":"11111",
//"Source":"test source",
//"FirstName":"test",
//"MiddleName":"T",
//"LastName":"Tester",
//"RoleId":2,
//"PubId":2,
//"HireDate":"2023-10-06T00:00:00",
//"PublisherPub":null,
//"Role":null}
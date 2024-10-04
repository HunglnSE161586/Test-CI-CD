using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using eBookStoreLib.DataAccess;

namespace eBookStoreClient.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Message { get; set; }
        private readonly HttpClient client;
        private string apiUrl = "";
        public IndexModel()
        {            
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            apiUrl = "https://localhost:7185/Users/login";
            User requestUser=new User();
            requestUser.EmailAddress = Email;
            requestUser.Password = Password;
            var json = JsonSerializer.Serialize(requestUser);
            HttpResponseMessage response = await client.PostAsync(
                apiUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                string strData = await response.Content.ReadAsStringAsync();
                User result = JsonSerializer.Deserialize<User>(strData, option);
                if (result.EmailAddress!=null)
                {
                    HttpContext.Session.SetString("USER", JsonSerializer.Serialize(result));
                    return RedirectToPage("/Home");
                }
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }            
            Message = "Wrong email or password";
            return Page();
        }
    }
}

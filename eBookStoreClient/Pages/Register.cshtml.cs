using eBookStoreLib.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace eBookStoreClient.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string ConfirmPass { get; set; }
        private readonly HttpClient client;
        private string apiUrl = "";
        public RegisterModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!Password.Equals(ConfirmPass))
            {
                Message = "Password field and Confirm Password field must match";
                return Page();
            }
            apiUrl = "https://localhost:7185/odata/Users";
            User requestUser = new User();
            requestUser.EmailAddress = Email;
            requestUser.Password = Password;
            requestUser.RoleId = 2;
            requestUser.HireDate = DateTime.Now;
            var json = JsonSerializer.Serialize(requestUser);
            HttpResponseMessage response = await client.PostAsync(
                apiUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return RedirectToPage("/Index");
            return Page();
        }
    }
}

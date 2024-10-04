using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBookStoreLib.DataAccess;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace eBookStoreClient.Pages.PublisherPages
{
    public class CreatePublisherModel : PageModel
    {
        private readonly HttpClient client;
        private string apiUrl = "";

        public CreatePublisherModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IActionResult OnGet()
        {
            string userData = HttpContext.Session.GetString("USER");
            if (userData == null)
                return RedirectToPage("/Index");
            var currentUser = JsonSerializer.Deserialize<User>(userData);
            if (!currentUser.Role.RoleDesc.Equals("Admin"))
                return RedirectToPage("/Index");
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;
                
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            apiUrl = "https://localhost:7185/odata/Publishers";
            var json = JsonSerializer.Serialize(Publisher);
            HttpResponseMessage response = await client.PostAsync(
                apiUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            return RedirectToPage("/PublisherPages/PublisherList");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eBookStoreLib.DataAccess;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;
using eBookStoreClient.OData;

namespace eBookStoreClient.Pages.PublisherPages
{
    public class EditPublisherModel : PageModel
    {
        private readonly HttpClient client;
        private string apiUrl = "";

        public EditPublisherModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string userData = HttpContext.Session.GetString("USER");
            if (userData == null)
                return RedirectToPage("/Index");
            var currentUser = JsonSerializer.Deserialize<User>(userData);
            if (!currentUser.Role.RoleDesc.Equals("Admin"))
                return RedirectToPage("/Index");

            apiUrl = "https://localhost:7185/odata/Publishers?$filter=PubId eq "+id;
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Publisher> publishers = JsonSerializer.Deserialize<ODataResponse<Publisher>>(strData, option).Value;
            Publisher = publishers.FirstOrDefault();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            apiUrl = "https://localhost:7185/odata/Publishers/"+Publisher.PubId;
            var json = JsonSerializer.Serialize(Publisher);
            HttpResponseMessage response = await client.PutAsync(
                apiUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            return RedirectToPage("/PublisherPages/PublisherList");
        }       
    }
}

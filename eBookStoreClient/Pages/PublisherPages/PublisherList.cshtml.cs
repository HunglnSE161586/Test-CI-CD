using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eBookStoreLib.DataAccess;
using System.Net.Http.Headers;
using System.Text.Json;
using eBookStoreClient.OData;

namespace eBookStoreClient.Pages.PublisherPages
{
    public class PublisherListModel : PageModel
    {
        private readonly HttpClient client;
        private string apiUrl = "";

        public PublisherListModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string userData = HttpContext.Session.GetString("USER");
            if (userData == null)
                return RedirectToPage("/Index");
            var currentUser = JsonSerializer.Deserialize<User>(userData);
            if (!currentUser.Role.RoleDesc.Equals("Admin"))
                return RedirectToPage("/Index");
            apiUrl = "https://localhost:7185/odata/Publishers";
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                Publisher = JsonSerializer.Deserialize<ODataResponse<Publisher>>(strData, option).Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Page();
        }
    }
}

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

namespace eBookStoreClient.Pages.AuthorPages
{
    public class DeleteAuthorModel : PageModel
    {
        private readonly HttpClient client;
        private string apiUrl = "";

        public DeleteAuthorModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
      public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            string userData = HttpContext.Session.GetString("USER");
            if (userData == null)
                return RedirectToPage("/Index");
            var currentUser = JsonSerializer.Deserialize<User>(userData);
            if (!currentUser.Role.RoleDesc.Equals("Admin"))
                return RedirectToPage("/Index");

            apiUrl = "https://localhost:7185/odata/Authors?$filter=AuthorId eq " + id;
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Author> authors = JsonSerializer.Deserialize<ODataResponse<Author>>(strData, option).Value;
            Author = authors.FirstOrDefault();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            apiUrl = "https://localhost:7185/odata/Authors/" + id;
            HttpResponseMessage response = await client.DeleteAsync(
                apiUrl);
            return RedirectToPage("/AuthorPages/AuthorList");
        }
    }
}

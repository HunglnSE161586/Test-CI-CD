using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eBookStoreLib.DataAccess;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text;
using eBookStoreClient.OData;

namespace eBookStoreClient.Pages.BookPages
{
    public class DeleteBookModel : PageModel
    {
        private readonly HttpClient client;
        private string apiUrl = "";

        public DeleteBookModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
      public Book Book { get; set; } = default!;

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

            apiUrl = "https://localhost:7185/odata/Books?$filter=BookId eq " + id;
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Book> books = JsonSerializer.Deserialize<ODataResponse<Book>>(strData, option).Value;            
            Book = books.FirstOrDefault();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            apiUrl = "https://localhost:7185/odata/Books/"+id;            
            HttpResponseMessage response = await client.DeleteAsync(
                apiUrl);

            return RedirectToPage("/BookPages/BookList");
        }
    }
}

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

namespace eBookStoreClient.Pages.BookPages
{
    public class BookListModel : PageModel
    {
        private readonly HttpClient client;
        private string apiUrl = "";

        public BookListModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<Book> Book { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string userData = HttpContext.Session.GetString("USER");
            if (userData == null)
                return RedirectToPage("/Index");
            var currentUser = JsonSerializer.Deserialize<User>(userData);
            if(!currentUser.Role.RoleDesc.Equals("Admin"))
                return RedirectToPage("/Index");
            apiUrl = "https://localhost:7185/odata/Books";
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                var result = JsonSerializer.Deserialize<ODataResponse<Book>>(strData, option);
                Book = result.Value;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Page();
        }        
    }
}

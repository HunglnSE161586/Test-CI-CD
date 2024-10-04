using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository;
using eBookStoreLib.eBookStoreRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq.Expressions;

namespace eBookStoreWebAPI.Controllers
{
    //[Route("api/books")]
    //[ApiController]
    public class BooksController : ODataController
    {
        private IBookRepository bookRepository=new BookRepository();

        [EnableQuery]
        //[HttpGet]
        public IActionResult Get()
        {
            return Ok(bookRepository.Get().AsQueryable());
        }
        //[HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody]Book book)
        {
            try
            {
                bookRepository.Insert(book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //[HttpPut]
        [EnableQuery]
        public IActionResult Put(int key,[FromBody] Book book)
        {
            if (key != book.BookId) {
                return BadRequest();
            }
            try
            {
                bookRepository.Update(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //[HttpDelete]
        [EnableQuery]
        public IActionResult Delete(int key)
        {
            try
            {
                Book book = new Book();
                book.BookId= key;
                bookRepository.Delete(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}

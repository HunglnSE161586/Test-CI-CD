using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository;
using eBookStoreLib.eBookStoreRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    //[Route("api/authors")]
    //[ApiController]
    public class AuthorsController : ODataController
    {
        private IAuthorRepository authorRepository=new AuthorRepository();
        [EnableQuery]
        //[HttpGet]
        public IActionResult Get()
        {
            return Ok(authorRepository.Get().AsQueryable());
        }
        //[HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody] Author author)
        {
            try
            {
                authorRepository.Insert(author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //[HttpPut]
        [EnableQuery]
        public IActionResult Put(int key,[FromBody] Author author)
        {
            if (key != author.AuthorId)
            {
                return BadRequest();
            }
            try
            {
                authorRepository.Update(author);
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
                Author author=new Author();
                author.AuthorId= key;
                authorRepository.Delete(author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}

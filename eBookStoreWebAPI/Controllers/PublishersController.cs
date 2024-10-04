using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository;
using eBookStoreLib.eBookStoreRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    //[Route("api/publishers")]
    //[ApiController]
    public class PublishersController : ODataController
    {
        private IPublisherRepository publisherRepository=new PublisherRepository();
        [EnableQuery]
        //[HttpGet]
        public IActionResult Get()
        {
            return Ok(publisherRepository.Get().AsQueryable());
        }
        //[HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody] Publisher publisher)
        {
            try
            {
                publisherRepository.Insert(publisher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //[HttpPut]
        [EnableQuery]
        public IActionResult Put(int key,[FromBody] Publisher publisher)
        {
            if (key!=publisher.PubId)
            {
                return BadRequest();
            }
            try
            {
                publisherRepository.Update(publisher);
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
                Publisher publisher = new Publisher();
                publisher.PubId = key;
                publisherRepository.Delete(publisher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}

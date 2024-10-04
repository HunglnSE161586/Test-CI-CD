using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository;
using eBookStoreLib.eBookStoreRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    //[Route("api/users")]
    //[ApiController]
    public class UsersController : ODataController
    {
        private IUserRepository userRepository=new UserRepository();
        private IRoleRepository roleRepository=new RoleRepository();
        private IPublisherRepository publisherRepository=new PublisherRepository();
        private IConfiguration configuration;
        public UsersController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [EnableQuery]
        //[HttpGet]
        public IActionResult Get()
        {
            return Ok(userRepository.Get().AsQueryable());
        }
        //[HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                userRepository.Insert(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPost("Users/login")]
        [EnableQuery]
        public IActionResult Login([FromBody] User user)
        {
            User result;
            try
            {
                result=userRepository.Get(user.EmailAddress, user.Password);
                if (result == null)
                {
                    if (user.EmailAddress.Equals(configuration["Admin:Email"], StringComparison.OrdinalIgnoreCase) 
                        && user.Password.Equals(configuration["Admin:Password"]))
                        return Ok(new User {
                            EmailAddress=user.EmailAddress,
                            Role=new Role { RoleDesc="Admin"}
                        });
                    return NotFound();
                }
                if(result.PubId!=null)
                result.Publisher=publisherRepository.GetByID(result.PubId);
                result.Role = roleRepository.GetByID(result.RoleId);
                result.Password = null;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
        //[HttpPut]
        [EnableQuery]
        public IActionResult Put(int key,[FromBody] User user)
        {
            if (key != user.UserId)
            {
                return BadRequest();
            }
            try
            {
                userRepository.Update(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //[HttpDelete("id")]
        [EnableQuery]
        public IActionResult Delete(int key)
        {
            try
            {
                User user = new User();
                user.UserId = key;
                userRepository.Delete(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}

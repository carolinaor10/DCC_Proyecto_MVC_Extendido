using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var um = new UserManager();
                var lstUsers = um.RetrieveAll();
                return Ok(lstUsers);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveById/{userId}")]
        public ActionResult RetrieveById(int userId)
        {
            try
            {
                var um = new UserManager();
                var user = um.RetrieveUserById(new User { Id = userId });
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
         [Route("RetrieveByEmail")]
         public ActionResult RetrieveByEmail(string email)
         {
             try 
             {
                 var u = new User {Email = email};
                 var um = new UserManager();
                 var user = um.RetrieveUserByEmail(u);
                 return Ok(user);
             }
             catch (Exception ex)

             {
                 return StatusCode(500, ex.Message);
             }
         }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(User user)
        {
            try
            {
                var um = new UserManager();
                um.Create(user);
                return Ok(user);
            }
            catch (Exception ex)

            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]

        public ActionResult Update(User user)
        {
            try
            {
                var um = new UserManager();
                um.Update(user);
                return Ok(user);
            }
            catch (Exception ex)

            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]

        public ActionResult Delete(User user)
        {
            try
            {
                var um = new UserManager();
                um.Delete(user);
                return Ok(user);
            }
            catch (Exception ex)

            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

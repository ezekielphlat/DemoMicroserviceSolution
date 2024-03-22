using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserWebApi.Models;

namespace UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            this._userDbContext = userDbContext;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<User>> GetUsers()
        {
            return  _userDbContext.Users;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<User>> GetById(int userId)
        {
             var user = await _userDbContext.Users.FindAsync(userId);
            return Ok(user);
        }

        [HttpPost]

        public async Task<ActionResult<User>> CreateUser(User user)
        {
            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
              _userDbContext.Users.Update(user);
            await _userDbContext.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete("{userId:int}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var user = await _userDbContext.Users.FindAsync(userId);
            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
            return Ok(user);

        }

    }
}

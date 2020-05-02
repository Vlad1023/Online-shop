using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using asp_back.Models;
using System.Text.Json;
namespace asp_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository users;
        public UserController(IUserRepository users)
        {
            this.users = users;
        }
        [HttpGet("isLogged")]
        public IActionResult IsLogged()
        {
            var user = HttpContext.Session.GetString("user");
            if (user != null)
            {
                var userJson = JObject.Parse(user);
                return Ok(userJson.GetValue("Login").ToString());
            }
            return BadRequest("Войдите или зарегестрируйтесь");
        }
        [HttpGet("logOut")]
        public IActionResult logOut()
        {
            HttpContext.Session.Remove("user");
            return StatusCode(401);
        }
        [HttpPost("Login")]
        public IActionResult Login(JsonElement json)
        {
            var user = new User(json.GetProperty("Login").ToString(),json.GetProperty("Password").ToString());
            var userId = users.ifExistsGetId(user);
            if (userId != null)
            {
                HttpContext.Session.SetString("user", JsonSerializer.Serialize<User>(new User(Convert.ToInt32(userId),user.Login)));
                return Ok();
            }
            else
            {
                return BadRequest("Неправильный пароль или логин");
            }
        }
        [HttpPost("Register")]
        public IActionResult Registr(User user)
        {
            int? findUser = users.ifExistsGetId(user);
            if (findUser != null)
            {
                return BadRequest("Такой пользователь уже существует");
            }
            else
            {
                int userId = users.Create(user);
                HttpContext.Session.SetString( "user", JsonSerializer.Serialize<User>(new User(Convert.ToInt32(userId), user.Login)) );
                return Ok();
            }
        }
    }
}
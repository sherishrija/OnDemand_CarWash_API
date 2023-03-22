using Carwash.Models;
using Carwash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Carwash.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;
        public UserController(IUserRepository user)
        {
            _user = user;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _user.GetAllAsync(); return Ok(users);
        }
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetuserAsync")]
        public async Task<IActionResult> GetuserAsync(int id)
        {
            var users = await _user.GetAsync(id); if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> AddemployeeAsync(UserModel adduser)
        {
            var user = new Models.UserModel()
            {
                FirstName = adduser.FirstName,
                LastName = adduser.LastName,
                Email = adduser.Email,
                PhoneNo = adduser.PhoneNo,
                Password = adduser.Password,
                ConfirmPassword = adduser.ConfirmPassword,
                Address = adduser.Address,
                Role = adduser.Role,
                Status = adduser.Status
            };
           
            user = await _user.AddAsync(user);
            return Ok(new { message = "Register Successful" });
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var employeeid = await _user.DeleteAsync(id);
                if (employeeid == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UserModel updateuser)
        {
            try
            {
                var user = new Models.UserModel()
                {
                    FirstName = updateuser.FirstName,
                    LastName = updateuser.LastName,
                    Email = updateuser.Email,
                    PhoneNo = updateuser.PhoneNo,
                    Password = updateuser.Password,
                    ConfirmPassword = updateuser.ConfirmPassword,
                    Address = updateuser.Address,
                    Role = updateuser.Role,
                    Status = updateuser.Status
                };
                user = await _user.UpdateAsync(id, user);
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AddLogin([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest();
            }
          var user =  await _user.Login(login);
            if(user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            string Token = CreateJwt(user);
            return Ok(new {Token, message = "Login Successful" });


        }
        
          private string CreateJwt(UserModel user) 
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("shrijasherigmailcom");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role), 
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                
            Subject = identity, Expires = DateTime.Now.AddDays(1), SigningCredentials = credentials };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor); 
            return jwtTokenHandler.WriteToken(token); 
          
           }



    }
}
    



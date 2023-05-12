using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVManagementSystem.Data;
using CVManagementSystem.Models;
using System.Security.Cryptography;
using System.Text;
using System.Reflection.Metadata.Ecma335;

namespace CVManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CVContext _context;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UsersController(CVContext context)
        {
            _context = context;
        }

        [Route("login")]
        [HttpGet]
        public IResult GetUser(string email, string password)
        {

            if (_context.Users == null)
            {
                return Results.NotFound();
            }
            if (UserExistsByEmail(email))
            {

                var user = _context.Users.Where(e => e.Email == email).FirstOrDefault();
                if (VerifyPassword(password, user.HashValue, Convert.FromBase64String(user.SaltValue)))
                {
                    return Results.Ok(user);
                }
                else
                {
                    return Results.BadRequest("Wrong Password!");
                }
            }

            else
            {
                return Results.NotFound();
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IResult GetUser(int id)
        {
          if (_context.Users == null)
          {
              return Results.NotFound();
          }
            var activeStatus = _context.UserStatuses.Where(e => e.Name == "Active").FirstOrDefault().ID;
            var user = _context.Users.Where(x=>x.ID==id && x.StatusID == activeStatus);

            if (user == null)
            {
                return Results.NotFound("User not exists!");
            }

            return Results.Ok(user);
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IResult PutUser(int id, string password=null, string status = null)
        {
            if (!UserExists(id))
            {
                return Results.BadRequest("User not exists!");
            }
            else
            {
                var activeStatus = _context.UserStatuses.Where(e => e.Name == "Active").FirstOrDefault().ID;
                var user = _context.Users.Where(x => x.ID == id && x.StatusID==activeStatus).FirstOrDefault();
                byte[] saltValue;
                if (String.IsNullOrEmpty(password))
                {
                    user.HashValue = HashPasword(password, out saltValue);
                    user.SaltValue = Convert.ToBase64String(saltValue);
                }
                user.LastModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(status))
                {
                    var newStatus = _context.UserStatuses.Where(e => e.Name == status).FirstOrDefault();
                        if(newStatus!=null)
                        user.StatusID= newStatus.ID;
                }
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChangesAsync();
                return Results.Ok(user);
            }
            return Results.NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(String email, String password)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'CVContext.Users'  is null.");
          }
            if (UserExistsByEmail(email))
            {
                return Problem("User exists!");
            }
            byte[] saltValue;
            User user = new User
            {
                Email = email,
                HashValue = HashPasword(password, out saltValue),
                SaltValue = Convert.ToBase64String(saltValue),
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                RoleID = _context.Roles.Where(e => e.Name == "User").FirstOrDefault().ID,
                StatusID = _context.UserStatuses.Where(e => e.Name == "Active").FirstOrDefault().ID
            };
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        private bool UserExistsByEmail(string email)
        {
            var active = _context.UserStatuses.Where(e => e.Name == "Active").FirstOrDefault();
            return (_context.Users?.Any(e => e.Email == email && e.StatusID == active.ID)).GetValueOrDefault();
        }
        string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }
        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

    }
}

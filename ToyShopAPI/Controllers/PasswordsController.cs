#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyShopAPI.Data;
using ToyShopAPI.Models;

namespace ToyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PasswordsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Passwords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasswordsModel>>> GetPasswords()
        {
            return await _context.Passwords.ToListAsync();
        }

        // GET: api/Passwords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PasswordsModel>> GetPasswordsModel(int id)
        {
            var passwordsModel = await _context.Passwords.FindAsync(id);

            if (passwordsModel == null)
            {
                return NotFound();
            }

            return passwordsModel;
        }

        // PUT: api/Passwords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasswordsModel(int id, PasswordsModel passwordsModel)
        {
            if (id != passwordsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(passwordsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordsModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Passwords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PasswordsModel>> PostPasswordsModel(AddPasswordRequestModel addPasswordRequestModel)
        {

            var user = await _userManager.FindByIdAsync(addPasswordRequestModel.UserId);

            var passwordsModel = new PasswordsModel();

            passwordsModel.UserId = addPasswordRequestModel.UserId;

            passwordsModel.User = (UserModel)user;
            passwordsModel.WebsiteName = addPasswordRequestModel.WebsiteName;
            passwordsModel.Username = 
            passwordsModel.Password = addPasswordRequestModel.Password;
            

            _context.Passwords.Add(passwordsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasswordsModel", new { id = passwordsModel.Id }, passwordsModel);
        }

        // DELETE: api/Passwords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasswordsModel(int id)
        {
            var passwordsModel = await _context.Passwords.FindAsync(id);
            if (passwordsModel == null)
            {
                return NotFound();
            }

            _context.Passwords.Remove(passwordsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasswordsModelExists(int id)
        {
            return _context.Passwords.Any(e => e.Id == id);
        }
    }
}

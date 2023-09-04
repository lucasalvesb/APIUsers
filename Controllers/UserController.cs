using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using Models.Models;

namespace APIUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Buscar todos os usuários e os jogos atrelados a eles com paginação
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get(int page = 1, int pageSize = 10)
        {
            try
            {
                int skipCount = (page - 1) * pageSize;

                var users = await _context.Users
                    .Include(u => u.Games)
                    .Skip(skipCount)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving users: {ex.Message}");
            }
        }

        // Buscar os usuários (e seus jogos associados) pelo seu ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Games).FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return BadRequest("User not found.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the user: {ex.Message}");
            }
        }

        // Inserir um novo usuário
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            try
            {
                user.CreatedAt = DateTime.UtcNow;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(await _context.Users.Include(u => u.Games).ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while adding the user: {ex.Message}");
            }
        }

        // Atualizar informações de um novo usuário
        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {
            try
            {
                var dbUser = await _context.Users.FindAsync(request.Id);
                if (dbUser == null)
                    return BadRequest("User not found.");

                dbUser.Name = request.Name;
                dbUser.Email = request.Email;
                dbUser.Phone = request.Phone;
                dbUser.CPF = request.CPF;

                await _context.SaveChangesAsync();

                return Ok(await _context.Users.Include(u => u.Games).ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the user: {ex.Message}");
            }
        }

        // Delete um usuário por ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> Delete(int id)
        {
            try
            {
                var dbUser = await _context.Users.FindAsync(id);
                if (dbUser == null)
                    return BadRequest("User not found.");

                _context.Users.Remove(dbUser);
                await _context.SaveChangesAsync();

                return Ok(await _context.Users.Include(u => u.Games).ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the user: {ex.Message}");
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using Models.Models;

namespace APIUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameController(AppDbContext context)
        {
            _context = context;
        }

        // Buscar todos os jogos com paginação
        [HttpGet]
        public async Task<ActionResult<List<Game>>> Get(int page = 1, int pageSize = 10)
        {
            try
            {
                int skipCount = (page - 1) * pageSize;

                var games = await _context.Games
                    .Skip(skipCount)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving games: {ex.Message}");
            }
        }

        // Pegar jogo pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);
                if (game == null)
                    return BadRequest("Game not found.");
                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the game: {ex.Message}");
            }
        }

        // Inserir um novo jogo
        [HttpPost]
        public async Task<ActionResult<List<Game>>> AddGame(Game game)
        {
            try
            {
                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return Ok(await _context.Games.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while adding the game: {ex.Message}");
            }
        }

        // Atualizar um jogo existente pelo ID
        [HttpPut]
        public async Task<ActionResult<List<Game>>> UpdateGame(Game request)
        {
            try
            {
                var dbGame = await _context.Games.FindAsync(request.Id);
                if (dbGame == null)
                    return BadRequest("Game not found.");

                dbGame.GameName = request.GameName;
                dbGame.Category = request.Category;
                dbGame.Finished = request.Finished;
                dbGame.TotalHoursPlayed = request.TotalHoursPlayed;

                await _context.SaveChangesAsync();

                return Ok(await _context.Games.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the game: {ex.Message}");
            }
        }

        // Deletar um jogo pelo ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Game>>> Delete(int id)
        {
            try
            {
                var dbGame = await _context.Games.FindAsync(id);
                if (dbGame == null)
                    return BadRequest("Game not found.");

                _context.Games.Remove(dbGame);
                await _context.SaveChangesAsync();

                return Ok(await _context.Games.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the game: {ex.Message}");
            }
        }
    }
}
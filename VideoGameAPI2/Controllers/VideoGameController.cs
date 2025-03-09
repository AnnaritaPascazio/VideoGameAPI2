using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace VideoGameAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DebuggerDisplay($"{{{nameof(VideoGame)}(),nq}}")]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
        {
            new VideoGame
            {
                Id= 1,
                Title = "Spider-Man 2",
                Platform = "PS5",
                Developer = "Insomniac Games",
                Publisher = "Sony Interactive Entertainment"
            },
            new VideoGame
            {
                Id = 2,
                Title = "The legend of Zelda: Breath of the Wild",
                Platform = "Nintendo Switch",
                Developer = "Nintendo EPD",
                Publisher = "Nintendo"
            },
            new VideoGame
            {
                Id=3,
                Title="Cyberpunk 2077",
                Platform="PC",
                Developer="CD projekt Red",
                Publisher="CD Projekt"
            }
        };
        /* we can return a status code with an action result*/
        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);

        }
        [HttpGet("{id}")]
        public ActionResult<VideoGame> GetVideoGameById(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id); // this is a lambda expression : (g => g.Id == id)
            if (game is null)
                return NotFound();

            return Ok(game);
        }
        [HttpPost]
        public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest();

            newGame.Id = videoGames.Max(g => g.Id) + 1;
            videoGames.Add(newGame);
            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }
        [HttpPut("{id}")] // I => Interface 
        public IActionResult UpdateVideoGame(int id,VideoGame updatesGame)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            game.Title = updatesGame.Title;
            game.Platform = updatesGame.Platform;
            game.Developer = updatesGame.Developer;
            game.Publisher = updatesGame.Publisher;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVideoGame(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            videoGames.Remove(game);
            return NoContent();
        }
    }
}

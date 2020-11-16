using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameReview.Models;

namespace GameReview.Modules
{
    public class DbService
    {
        private readonly ApplicationDbContext _context;

        public DbService()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<int> GetGamesInDatabase()
        {
            return _context.Games
                .Select(gameInDb => gameInDb.GameIdentifier)
                .ToList();
        }

        public List<Review> GetReviews(int gameId)
        {
            return _context.Reviews
                .Where(r => r.GameId == gameId)
                .Include(r => r.User)
                .ToList();
        }

        public List<Platform> GetPlatforms(IEnumerable<int> platformIds)
        {
            return _context.Platforms
                .Where(p => platformIds.Contains(p.ApiPlatformId))
                .ToList();
        }
        
        //Gets the specified game's(by id) average rating by review scores
        public double GetAverageRating(int gameId)
        {
            return _context.Games
                    .SingleOrDefault(g => g.GameIdentifier == gameId)
                    .AverageRating;
        }

        public async Task<List<int>> GetTopGamesAsync()
        {
            return await _context.Games
                .OrderByDescending(r => r.AverageRating)
                .Select(g => g.GameIdentifier)
                .ToListAsync();
        }
    }
}
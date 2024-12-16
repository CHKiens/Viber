using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services
{
    public class MoodboardService : IMoodboardService
    {
        private readonly finsby_dk_db_viberContext _context;
        private readonly ILogger<MoodboardService> _logger;

        public MoodboardService(finsby_dk_db_viberContext context, ILogger<MoodboardService> logger)
        {
            _context = context;
            _logger = logger;
        }

        //CRUD
        public void CreateMoodboard(Moodboard moodboard)
        {
            if (moodboard == null)
            {
                _logger.LogWarning("Attempted to create a moodboard, but the moodboard object was null.");
                throw new ArgumentNullException(nameof(moodboard), "The moodboard object cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(moodboard.Title))
            {
                _logger.LogWarning("Attempted to create a moodboard, but the title was null or empty.");
                throw new ArgumentException("The moodboard must have a title.");
            }

            try
            {
                moodboard.DateOfCreation = DateTime.Now;
                _logger.LogInformation("Creating a new moodboard with title: {Title} for user ID: {UserId}", moodboard.Title, moodboard.UserId);
                
                _context.Moodboards.Add(moodboard);
                _context.SaveChanges();
                
                _logger.LogInformation("Moodboard created successfully with ID: {MoodboardId}", moodboard.MoodboardId);
                
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error occurred while creating a moodboard with title: {Title}", moodboard.Title);
                throw new Exception("An error occurred while saving the moodboard to the database. See inner exception for details.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating a moodboard with title: {Title}", moodboard.Title);
                throw new Exception("An unexpected error occurred while creating the moodboard.", ex);
            }
        }


        public Moodboard GetMoodboard(int moodboardId) 
        {
            return _context.Moodboards.FirstOrDefault(mb => mb.MoodboardId == moodboardId);
        }


        public List<Moodboard> GetMoodboardByUserId(int userId)
        {
            return _context.Moodboards
                .Where(mb => mb.UserId == userId)
                .ToList();
        }


        public Moodboard GetMoodboardAndCC(int moodboardId) {
            return _context.Moodboards
                .Include(mb => mb.ContentContainers)
                .Include(mb => mb.PrimaryTag)
                .FirstOrDefault(mb => mb.MoodboardId == moodboardId)!;
        }

        public void EditMoodboard(Moodboard moodboard)
        {
            _context.Update(moodboard);
            _context.SaveChanges();
        }
        

        public void UpdateMoodboard(Moodboard moodboard)
        {
            moodboard.UpdateDate = DateTime.Now; 
            _context.Moodboards.Update(moodboard);
            _context.SaveChanges();
        }



        public List<Moodboard> GetMoodboardsByPrimaryTagId(int primaryTagId, int limit = 14)
        {
            return _context.Moodboards
                .Where(mb => mb.PrimaryTagId == primaryTagId)
                .OrderByDescending(mb => mb.DateOfCreation)
                .Take(limit)
                .AsNoTracking()
                .ToList();
        }

        public void DeleteMoodboard(Moodboard moodboard)
        {
            _context.Remove(moodboard);
            _context.SaveChanges();
        }

        public List<Moodboard> GetMoodboardBySubTags(int subTagId, int limit = 8)
        {
            List<Moodboard> MoodboardBySubTag = _context.MoodboardSubTags
                .Where(mbst => mbst.SubtagId == subTagId)  
                .Select(mbst => mbst.Moodboard)
                .Take(limit)
                .ToList();
            
                return MoodboardBySubTag;
        }

        public List<Moodboard> SearchForMoodboards(string searchTerm)
        {
            return _context.Moodboards
                .Where(mb => mb.Title.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
        }

    }
}

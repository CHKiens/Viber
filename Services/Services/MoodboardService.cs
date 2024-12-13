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

        public MoodboardService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }

        //CRUD
        public void CreateMoodboard(Moodboard moodboard)
        {
            moodboard.DateOfCreation = DateTime.Now;
            _context.Moodboards.Add(moodboard);
            _context.SaveChanges();
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

        public void UpdateContainerList(Moodboard moodboard)
        {
            

        }

        public List<Moodboard> GetMoodboardsByPrimaryTagId(int primaryTagId, int limit = 14)
        {
            return _context.Moodboards
                .Where(mb => mb.PrimaryTagId == primaryTagId)
                .OrderByDescending(mb => mb.DateOfCreation)
                .Include(mb => mb.MoodboardSubTags)
                .ThenInclude(mbst => mbst.Subtag)
                .Take(limit)
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
                .Include(mbst => mbst.Moodboard)
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

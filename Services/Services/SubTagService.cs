using Microsoft.EntityFrameworkCore;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class SubTagService : ISubTagService{
        
        private readonly finsby_dk_db_viberContext _context;

        public SubTagService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }
        
        public List<SubTag> GetSubTags(int PrimaryTagId, int limit = 8)
        {
            var subtagIds = _context.MoodboardSubTags
                .Include(mst => mst.Subtag)
                .Where(mst => mst.Subtag.PrimaryTagId == PrimaryTagId)
                .GroupBy(x => x.SubtagId)
                .OrderByDescending(g => g.Count())
                .Take(limit)
                .Select(g => g.Key)
                .ToList();
            
                    
            List<SubTag> mostFrequentSubTags = new List<SubTag>();
            foreach (var subtagId in subtagIds)
            {
                mostFrequentSubTags.Add(GetSubTagById(subtagId));
            }
            
            return mostFrequentSubTags;
        }

        public SubTag GetSubTagById(int SubTagId)
        {
            return _context.SubTags.FirstOrDefault(st => st.SubTagId == SubTagId);
        }

    }
}

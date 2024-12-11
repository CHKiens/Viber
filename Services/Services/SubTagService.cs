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
        
        public List<SubTag> SearchForSubTags(string searchTerm)
        {
            return _context.SubTags
                .Where(st => st.Name.ToLower().Contains(searchTerm.ToLower()))
                .Include(st => st.MoodboardSubTags)
                .ThenInclude(mbst => mbst.Moodboard)
                .ToList();
        }

        public void SplitSubtagInput(string input, int primaryTagId, int moodboardId)
        {
            string[] substrings = input.Split(' ');
            foreach(string s in substrings)
            {
                CreateSubTag(s, primaryTagId, moodboardId);
            }
            
            
        }

        public void CreateSubTag(string tagName, int primaryTagId, int moodboardId)
        {
            // tjekker først hvis subtag allerede findes med samme navn+primarytag kombination
            var newSubTag = _context.SubTags
                .FirstOrDefault(s => s.Name == tagName && s.PrimaryTagId == primaryTagId);

            // laver nyt subtag hvis det ikke findes allerede
            if (newSubTag == null)
            {
                newSubTag = new SubTag
                {
                    Name = tagName,
                    PrimaryTagId = primaryTagId,
                };
                _context.SubTags.Add(newSubTag);
                _context.SaveChanges(); 
            }

            MoodboardSubTag moodboardSubTag = new MoodboardSubTag
            {
                MoodboardId = moodboardId,
                SubtagId = newSubTag.SubTagId
            };

            _context.MoodboardSubTags.Add(moodboardSubTag);
            _context.SaveChanges();
        }


    }
}

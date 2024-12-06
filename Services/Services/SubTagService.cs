using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class SubTagService : ISubTagService{
        private readonly finsby_dk_db_viberContext _context;

        public SubTagService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }
        
        public List<SubTag> GetSubTags() 
        { 
            List<SubTag> tagList = new List<SubTag>();
            foreach(var t in _context.SubTags)
            {
                tagList.Add(t);
            }
            return tagList;
        }
        
        public List<SubTag> GetSubTagsByPrimaryTagId(int primaryTagId, int limit = 5)
        {
            return _context.SubTags
                .Where(st => st.PrimaryTagId == primaryTagId)
                .Take(limit)
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

        public void CreateSubTag(string tagName, int primaryTagId, int mooodboardId) 
        {
            SubTag subTag = new SubTag()
            {
                Name = tagName,
                PrimaryTagId = primaryTagId,
            };
            if(!_context.SubTags. //hvis subtag ikke allerede findes, tilføjes den
            {
                _context.SubTags.Add(subTag);
                _context.SaveChanges();
            }
            MoodboardSubTag moodboardSubTag = new MoodboardSubTag()
            {
                MoodboardId = mooodboardId,
                SubtagId = _context.SubTags
                .FirstOrDefault(s => s.Name == tagName)
                .SubTagId
            };
            _context.MoodboardSubTags.Add(moodboardSubTag);
            _context.SaveChanges();
        }

    }
}

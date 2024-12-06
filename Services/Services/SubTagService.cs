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

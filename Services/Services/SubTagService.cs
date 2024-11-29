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

    }
}

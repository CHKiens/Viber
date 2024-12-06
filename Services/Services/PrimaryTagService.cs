using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class PrimaryTagService : IPrimaryTagService{
        private readonly finsby_dk_db_viberContext _context;

        public PrimaryTagService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }

        public List<PrimaryTag> GetPrimaryTags() 
        { 
            List<PrimaryTag> tagList = new List<PrimaryTag>();
            foreach(var t in _context.PrimaryTags)
            {
                tagList.Add(t);
            }
            return tagList;
        }
        public PrimaryTag GetPrimaryTag(int primarytagId)
        {
            return _context.PrimaryTags.FirstOrDefault(pt => pt.PrimaryTagId == primarytagId);
        }
    }
}

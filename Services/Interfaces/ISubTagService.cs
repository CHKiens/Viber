using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface ISubTagService
    {
        public List<SubTag> GetSubTags();

        public List<SubTag> GetSubTagsByPrimaryTagId(int primaryTagId, int limit = 5);

    }
}

using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface ISubTagService
    {
        public List<SubTag> GetSubTags(int PrimaryTagId, int limit = 8);

        public SubTag GetSubTagById(int SubTagId);

    }
}

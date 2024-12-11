using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface ISubTagService
    {
        public List<SubTag> GetSubTags(int PrimaryTagId, int limit = 8);

        public SubTag GetSubTagById(int SubTagId);

        public List<SubTag> SearchForSubTags(string searchTerm);

        public void SplitSubtagInput(string input, int primaryTagId, int moodboardId);

        public void CreateSubTag(string tagName, int primaryTagId, int moodboardId);

    }
}

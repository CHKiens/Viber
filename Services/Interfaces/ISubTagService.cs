using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface ISubTagService
    {
        public List<SubTag> GetSubTags();

        public List<SubTag> GetSubTagsByPrimaryTagId(int primaryTagId, int limit = 5);

        public void SplitSubtagInput(string input, int primaryTagId, int moodboardId);

        public void CreateSubTag(string tagName, int primaryTagId, int moodboardId);

    }
}

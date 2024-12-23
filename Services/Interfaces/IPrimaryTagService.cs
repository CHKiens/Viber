﻿using Viber.Models;

namespace Viber.Services.Interfaces {

    public interface IPrimaryTagService {

        public List<PrimaryTag> GetPrimaryTags();
        public PrimaryTag GetPrimaryTag(int primarytagId);
    }
}

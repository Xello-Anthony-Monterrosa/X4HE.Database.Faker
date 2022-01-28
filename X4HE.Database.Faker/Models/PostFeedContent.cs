using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class PostFeedContent
    {
        public int PostId { get; set; }
        public int FeedContentId { get; set; }

        public virtual FeedContent FeedContent { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}

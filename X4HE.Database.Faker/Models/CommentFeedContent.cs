using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class CommentFeedContent
    {
        public int CommentId { get; set; }
        public int FeedContentId { get; set; }

        public virtual Comment Comment { get; set; } = null!;
        public virtual FeedContent FeedContent { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class FeedContent
    {
        public int Id { get; set; }

        public virtual CommentFeedContent CommentFeedContent { get; set; } = null!;
        public virtual FeedContentText FeedContentText { get; set; } = null!;
        public virtual PostFeedContent PostFeedContent { get; set; } = null!;
    }
}

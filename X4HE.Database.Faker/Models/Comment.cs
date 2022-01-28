using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class Comment
    {
        public Comment()
        {
            CommentFeedContents = new HashSet<CommentFeedContent>();
            CommentReactions = new HashSet<CommentReaction>();
        }

        public int Id { get; set; }
        public virtual PostComment PostComment { get; set; } = null!;
        public virtual ICollection<CommentFeedContent> CommentFeedContents { get; set; }
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
    }
}

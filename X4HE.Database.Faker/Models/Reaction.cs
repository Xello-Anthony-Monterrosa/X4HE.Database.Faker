using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class Reaction
    {
        public int Id { get; set; }

        public virtual CommentReaction CommentReaction { get; set; } = null!;
        public virtual PostReaction PostReaction { get; set; } = null!;
        public virtual ReactionNameMap ReactionNameMap { get; set; } = null!;
    }
}

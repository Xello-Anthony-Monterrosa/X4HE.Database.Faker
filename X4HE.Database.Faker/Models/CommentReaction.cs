using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class CommentReaction
    {
        public int CommentId { get; set; }
        public int ReactionId { get; set; }

        public virtual Comment Comment { get; set; } = null!;
        public virtual Reaction Reaction { get; set; } = null!;
    }
}

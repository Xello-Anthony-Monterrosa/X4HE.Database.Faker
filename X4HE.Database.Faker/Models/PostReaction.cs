using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class PostReaction
    {
        public int PostId { get; set; }
        public int ReactionId { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual Reaction Reaction { get; set; } = null!;
    }
}

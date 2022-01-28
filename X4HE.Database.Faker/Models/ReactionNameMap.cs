using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class ReactionNameMap
    {
        public int ReactionId { get; set; }
        public int ReactionNameLookupId { get; set; }

        public virtual Reaction Reaction { get; set; } = null!;
        public virtual ReactionNameLookup ReactionNameLookup { get; set; } = null!;
    }
}

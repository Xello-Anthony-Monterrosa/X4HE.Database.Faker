using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class ReactionNameLookup
    {
        public ReactionNameLookup()
        {
            ReactionNameMap = new HashSet<ReactionNameMap>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ReactionNameMap> ReactionNameMap { get; set; }
    }
}

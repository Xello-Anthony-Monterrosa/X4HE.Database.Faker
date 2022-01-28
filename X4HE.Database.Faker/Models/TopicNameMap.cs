using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class TopicNameMap
    {
        public int TopicId { get; set; }
        public int TopicNameLookupId { get; set; }

        public virtual Topic Topic { get; set; } = null!;
        public virtual TopicNameLookup TopicNameLookup { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class TopicNameLookup
    {
        public TopicNameLookup()
        {
            TopicNameMap = new HashSet<TopicNameMap>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TopicNameMap> TopicNameMap { get; set; }
    }
}

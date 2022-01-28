using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class Topic
    {
        public int Id { get; set; }

        public virtual PostTopic PostTopic { get; set; } = null!;
        public virtual TopicNameMap TopicNameMap { get; set; } = null!;
    }
}

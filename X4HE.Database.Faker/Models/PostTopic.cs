using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class PostTopic
    {
        public int PostId { get; set; }
        public int TopicId { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual Topic Topic { get; set; } = null!;
    }
}

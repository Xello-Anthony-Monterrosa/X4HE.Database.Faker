using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class FeedContentText
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;

        public virtual FeedContent IdNavigation { get; set; } = null!;
    }
}

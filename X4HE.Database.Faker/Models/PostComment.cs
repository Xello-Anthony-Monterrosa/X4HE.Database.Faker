using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class PostComment
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}

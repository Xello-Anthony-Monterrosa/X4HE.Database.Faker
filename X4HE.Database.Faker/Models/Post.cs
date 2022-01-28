using System;
using System.Collections.Generic;

namespace X4HE.Database.Faker.Models
{
    public partial class Post
    {
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostFeedContents = new HashSet<PostFeedContent>();
            PostReactions = new HashSet<PostReaction>();
            PostTopics = new HashSet<PostTopic>();
        }

        public int Id { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostFeedContent> PostFeedContents { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
        public virtual ICollection<PostTopic> PostTopics { get; set; }
    }
}

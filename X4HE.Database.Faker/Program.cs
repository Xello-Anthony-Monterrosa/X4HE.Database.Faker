using System.Collections.Generic;

using Faker;
using X4HE.Database.Faker.Models;

namespace X4HE.Database.Faker
{
    public static class Program
    {
        public static void Main()
        {
            PopulateLookupTables();

            foreach(var postCount in Enumerable.Range(1, POST_COUNT))
            {
                Console.WriteLine($"Creating Post {postCount}.");
                GeneratePost();
            }
        }

        public static int GeneratePost()
        {
            var postId = CreatePost();

            foreach(var count in Enumerable.Range(1, RandomNumber.Next(MINIMUM_COMMENTS, MAXIMUM_COMMENTS)))
            {
                var commentId = GenerateComment();
                CreatePostComment(postId, commentId);
            }

            foreach(var count in Enumerable.Range(1, RandomNumber.Next(MINIMUM_REACTIONS, MAXIMUM_REACTIONS)))
            {
                var reactionId = GenerateReaction();
                CreatePostReaction(postId, reactionId);
            }

            foreach(var count in Enumerable.Range(1, RandomNumber.Next(MINIMUM_TOPICS, MAXIMUM_TOPICS)))
            {
                var topicId = GenerateTopic();
                CreatePostTopic(postId, topicId);
            }

            var contentId = GenerateFeedContent();
            CreatePostFeedContent(postId, contentId);

            return postId;
        }

        public static int GenerateComment()
        {
            var commentId = CreateComment();

            foreach(var count in Enumerable.Range(1, RandomNumber.Next(MINIMUM_REACTIONS, MAXIMUM_REACTIONS)))
            {
                var reactionId = GenerateReaction();
                CreateCommentReaction(commentId, reactionId);
            }

            var contentId = GenerateFeedContent();
            CreateCommentFeedContent(commentId, contentId);

            return commentId;
        }

        public static int GenerateFeedContent()
        {
            var contentId = CreateFeedContent();
            CreateFeedContentText(contentId);

            return contentId;
        }

        public static int GenerateReaction()
        {
            var reactionId = CreateReaction();
            CreateReactionNameMap(reactionId, RandomNumber.Next(1, REACTION_NAMES.Length));

            return reactionId;
        }

        public static int GenerateTopic()
        {
            var topicId = CreateTopic();
            CreateTopicNameMap(topicId, RandomNumber.Next(1, TOPIC_NAMES.Length));

            return topicId;
        }

        public static void CreateFeedContentText(int contentId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new FeedContentText()
                {
                    Id = contentId,
                    Text = Lorem.Paragraph()
                });
            });
        }

        public static void CreatePostComment(int postId, int commentId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new PostComment()
                {
                    PostId = postId,
                    CommentId = commentId
                });
            });
        }

        public static void CreateReactionNameMap(int reactionId, int reactionNameLookupId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new ReactionNameMap()
                {
                    ReactionId = reactionId,
                    ReactionNameLookupId = reactionNameLookupId
                });
            });
        }

        public static void CreateTopicNameMap(int topicId, int topicNameLookupId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new TopicNameMap()
                {
                    TopicId = topicId,
                    TopicNameLookupId = topicNameLookupId
                });
            });
        }

        public static int CreatePost()
        {
            CommitContextQuery(context =>
            {
                context.Add(new Post());
            });

            using(var context = new X4HEContext())
            {
                return context.Posts.OrderByDescending(context => context.Id).First().Id;
            }
        }

        public static int CreateComment()
        {
            CommitContextQuery(context =>
            {
                context.Add(new Comment());
            });

            using(var context = new X4HEContext())
            {
                return context.Comments.OrderByDescending(context => context.Id).First().Id;
            }
        }

        public static int CreateFeedContent()
        {
            CommitContextQuery(context =>
            {
                context.Add(new FeedContent());
            });

            using(var context = new X4HEContext())
            {
                return context.FeedContents.OrderByDescending(context => context.Id).First().Id;
            }
        }

        public static int CreateReaction()
        {
            CommitContextQuery(context =>
            {
                context.Add(new Reaction());
            });

            using(var context = new X4HEContext())
            {
                return context.Reactions.OrderByDescending(context => context.Id).First().Id;
            }
        }

        public static int CreateTopic()
        {
            CommitContextQuery(context =>
            {
                context.Add(new Topic());
            });

            using(var context = new X4HEContext())
            {
                return context.Topics.OrderByDescending(context => context.Id).First().Id;
            }
        }

        public static void CreatePostReaction(int postId, int reactionId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new PostReaction()
                {
                    PostId = postId,
                    ReactionId = reactionId
                });
            });
        }

        public static void CreateCommentReaction(int commentId, int reactionId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new CommentReaction()
                {
                    CommentId = commentId,
                    ReactionId = reactionId
                });
            });
        }

        public static void CreatePostTopic(int postId, int topicId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new PostTopic()
                {
                    PostId = postId,
                    TopicId = topicId
                });
            });
        }

        public static void CreatePostFeedContent(int postId, int feedContentId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new PostFeedContent()
                {
                    PostId = postId,
                    FeedContentId = feedContentId
                });
            });
        }

        public static void CreateCommentFeedContent(int commentId, int feedContentId)
        {
            CommitContextQuery(context =>
            {
                context.Add(new CommentFeedContent()
                {
                    CommentId = commentId,
                    FeedContentId = feedContentId
                });
            });
        }

        public static void PopulateLookupTables()
        {
            PopulateReactionNames();
            PopulateTopicNames();
        }

        public static void PopulateTopicNames()
        {
            foreach(var topicName in TOPIC_NAMES)
            {
                CreateTopicNameLookup(topicName);
            }
        }

        public static void PopulateReactionNames()
        {
            foreach(var reactionName in REACTION_NAMES)
            {
                CreateReactionNameLookup(reactionName);
            }
        }

        public static void CreateReactionNameLookup(string reactionName)
        {
            CommitContextQuery(context =>
            {
                context.Add(new ReactionNameLookup()
                {
                    Name = reactionName
                });
            });
        }

        public static void CreateTopicNameLookup(string topicName)
        {
            CommitContextQuery(context =>
            {
                context.Add(new TopicNameLookup()
                {
                    Name = topicName
                });
            });
        }

        private static void CommitContextQuery(Action<X4HEContext> changeToExecute)
        {
            using(var context = new X4HEContext())
            {
                changeToExecute(context);
                context.SaveChanges();
            }
        }

        private static string[] TOPIC_NAMES = new string[]
        {
            "Admissions",
            "Athletics",
            "Academics"
        };

        private static string[] REACTION_NAMES = new string[]
        {
            "Like",
            "Heart",
            "Wow"
        };

        private static int POST_COUNT = 1000;

        private static int MINIMUM_COMMENTS = 1;
        private static int MAXIMUM_COMMENTS = 3;

        private static int MINIMUM_TOPICS = 1;
        private static int MAXIMUM_TOPICS = 2;

        private static int MINIMUM_REACTIONS = 1;
        private static int MAXIMUM_REACTIONS = 10;
    }
}
using System.Collections.Generic;

using Faker;
using X4HE.Database.Faker.Models;

namespace X4HE.Database.Faker
{
    public static class Program
    {
        public static void Main()
        {
            using (var context = new X4HEContext())
            {
                PopulateReactionNames(context);
                PopulateTopicNames(context);
                context.SaveChanges();
            }

            foreach(var postCount in Enumerable.Range(1, POST_COUNT))
            {
                using(var context = new X4HEContext())
                {
                    Console.WriteLine($"Creating Post {postCount}.");
                    GeneratePost(context);
                    context.SaveChanges();
                }
            }
        }

        public static int GeneratePost(X4HEContext context)
        {
            var postId = CreatePost(context);

            var commentId1 = GenerateComment(context);
            CreatePostComment(context, postId, commentId1);

            var commentId2 = GenerateComment(context);
            CreatePostComment(context, postId, commentId2);

            foreach (var count in Enumerable.Range(1, RandomNumber.Next(MINIMUM_REACTIONS, MAXIMUM_REACTIONS)))
            {
                var reactionId = GenerateReaction(context);
                CreatePostReaction(context, postId, reactionId);
            }

            foreach (var count in Enumerable.Range(1, RandomNumber.Next(MINIMUM_TOPICS, MAXIMUM_TOPICS)))
            {
                var topicId = GenerateTopic(context);
                CreatePostTopic(context, postId, topicId);
            }

            var contentId = GenerateFeedContent(context);
            CreatePostFeedContent(context, postId, contentId);

            return postId;
        }

        public static int GenerateComment(X4HEContext context)
        {
            var commentId = CreateComment(context);

            foreach (var count in Enumerable.Range(1, RandomNumber.Next(MINIMUM_REACTIONS, MAXIMUM_REACTIONS)))
            {
                var reactionId = GenerateReaction(context);
                CreateCommentReaction(context, commentId, reactionId);
            }

            var contentId = GenerateFeedContent(context);
            CreateCommentFeedContent(context, commentId, contentId);

            return commentId;
        }

        public static int GenerateFeedContent(X4HEContext context)
        {
            var contentId = CreateFeedContent(context);
            CreateFeedContentText(context, contentId);

            return contentId;
        }

        public static void CreateFeedContentText(X4HEContext context, int contentId)
        {
            context.Add(new FeedContentText()
            {
                Id = contentId,
                Text = Lorem.Paragraph()
            });
            //context.SaveChanges();
        }

        public static int GenerateReaction(X4HEContext context)
        {
            var reactionId = CreateReaction(context);
            CreateReactionNameMap(context, reactionId, RandomNumber.Next(1, REACTION_NAMES.Length));

            return reactionId;
        }

        public static int GenerateTopic(X4HEContext context)
        {
            var topicId = CreateTopic(context);
            CreateTopicNameMap(context, topicId, RandomNumber.Next(1, TOPIC_NAMES.Length));

            return topicId;
        }

        public static void CreatePostComment(X4HEContext context, int postId, int commentId)
        {
            context.Add(new PostComment()
            {
                PostId = postId,
                CommentId = commentId
            });
            //context.SaveChanges();
        }

        public static void CreateReactionNameMap(X4HEContext context, int reactionId, int reactionNameLookupId)
        {
            context.Add(new ReactionNameMap()
            {
                ReactionId = reactionId,
                ReactionNameLookupId = reactionNameLookupId
            });
            //context.SaveChanges();
        }

        public static void CreateTopicNameMap(X4HEContext context, int reactionId, int topicNameLookupId)
        {
            context.Add(new TopicNameMap()
            {
                TopicId = reactionId,
                TopicNameLookupId = topicNameLookupId
            });
            //context.SaveChanges();
        }

        public static int CreatePost(X4HEContext context)
        {
            context.Add(new Post());
            //context.ChangeTracker.Clear();
            context.SaveChanges();

            return context.Posts.OrderByDescending(context => context.Id).First().Id;
            //return POST_INDEX++;
        }

        public static int CreateComment(X4HEContext context)
        {
            context.Add(new Comment());
            //context.ChangeTracker.Clear();
            context.SaveChanges();

            return context.Comments.OrderByDescending(context => context.Id).First().Id;
            //return COMMENT_INDEX++;
        }

        public static int CreateFeedContent(X4HEContext context)
        {
            context.Add(new FeedContent());
            //context.ChangeTracker.Clear();
            context.SaveChanges();

            return context.FeedContents.OrderByDescending(context => context.Id).First().Id;
            //return FEEDCONTENT_INDEX++;
        }

        public static int CreateReaction(X4HEContext context)
        {
            context.Add(new Reaction());
            //context.ChangeTracker.Clear();
            context.SaveChanges();

            return context.Reactions.OrderByDescending(context => context.Id).First().Id;
            //return REACTION_INDEX++;
        }

        public static int CreateTopic(X4HEContext context)
        {
            context.Add(new Topic());
            //context.ChangeTracker.Clear();
            context.SaveChanges();

            return context.Topics.OrderByDescending(context => context.Id).First().Id;
            //return TOPIC_INDEX++;
        }

        public static void CreatePostReaction(X4HEContext context, int postId, int reactionId)
        {
            context.Add(new PostReaction()
            {
                PostId = postId,
                ReactionId = reactionId
            });
            //context.SaveChanges();
        }

        public static void CreateCommentReaction(X4HEContext context, int commentId, int reactionId)
        {
            context.Add(new CommentReaction()
            {
                CommentId = commentId,
                ReactionId = reactionId
            });
            //context.SaveChanges();
        }

        public static void CreatePostTopic(X4HEContext context, int postId, int topicId)
        {
            context.Add(new PostTopic()
            {
                PostId = postId,
                TopicId = topicId
            });
            //context.SaveChanges();
        }

        public static void CreatePostFeedContent(X4HEContext context, int postId, int feedContentId)
        {
            context.Add(new PostFeedContent()
            {
                PostId = postId,
                FeedContentId = feedContentId
            });
            //context.SaveChanges();
        }

        public static void CreateCommentFeedContent(X4HEContext context, int commentId, int feedContentId)
        {
            context.Add(new CommentFeedContent()
            {
                CommentId = commentId,
                FeedContentId = feedContentId
            });
            //context.SaveChanges();
        }

        public static void PopulateTopicNames(X4HEContext context)
        {
            Console.WriteLine("Creating Topic Names.");

            foreach(var topicName in TOPIC_NAMES)
            {
                context.Add(new TopicNameLookup()
                {
                    Name = topicName
                });
            }
            //context.SaveChanges();

            Console.WriteLine("Completed Topic Names.");
        }

        public static void PopulateReactionNames(X4HEContext context)
        {
            Console.WriteLine("Creating Reaction Names.");

            foreach (var reactionName in REACTION_NAMES)
            {
                context.Add(new ReactionNameLookup()
                {
                    Name = reactionName
                });
            }
            //context.SaveChanges();

            Console.WriteLine("Completed Reaction Names.");
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

        private static int POST_INDEX = 0;
        private static int COMMENT_INDEX = 0;
        private static int REACTION_INDEX = 0;
        private static int TOPIC_INDEX = 0;
        private static int FEEDCONTENT_INDEX = 0;
    }
}
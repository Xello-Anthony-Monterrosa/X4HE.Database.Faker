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
                Console.WriteLine($"{postCount} : {POST_COUNT}.");
                GeneratePost();
            }

            using(var context = new X4HEContext())
            {
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("ENTITIES CREATED:");
                Console.WriteLine();

                Console.WriteLine($"Post: {context.Posts.Count()}");
                Console.WriteLine($"PostComment: {context.PostComments.Count()}");
                Console.WriteLine($"PostFeedContent: {context.PostFeedContents.Count()}");
                Console.WriteLine($"PostReaction: {context.PostReactions.Count()}");
                Console.WriteLine($"PostTopic: {context.PostTopics.Count()}");
                Console.WriteLine();

                Console.WriteLine($"Comment: {context.Comments.Count()}");
                Console.WriteLine($"CommentReaction: {context.CommentReactions.Count()}");
                Console.WriteLine($"CommentFeedContent: {context.CommentFeedContents.Count()}");
                Console.WriteLine();

                Console.WriteLine($"Reactions: {context.Reactions.Count()}");
                Console.WriteLine($"ReactionNameMap: {context.ReactionNameMaps.Count()}");
                Console.WriteLine($"ReactionNameLookup: {context.ReactionNameLookups.Count()}");
                Console.WriteLine();

                Console.WriteLine($"Topics: {context.Topics.Count()}");
                Console.WriteLine($"TopicNameMap: {context.TopicNameMaps.Count()}");
                Console.WriteLine($"TopicNameLookup: {context.TopicNameLookups.Count()}");
                Console.WriteLine();

                Console.WriteLine($"FeedContents: {context.FeedContents.Count()}");
                Console.WriteLine($"FeedContentText: {context.FeedContentTexts.Count()}");
                Console.WriteLine();
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
                var topicId = GenerateTopic(postId);
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

        public static int GenerateTopic(int postId)
        {
            var topicId = CreateTopic();

            // start at 1
            var topicNameLookupIds = GetTopicNameLookupIdsForPost(postId);

            // start at 0
            var unusedTopicNameLookupIds = Enumerable.Range(0, TOPIC_NAMES.Length).Except(topicNameLookupIds.Select(id => id - 1)).ToArray();

            // adjusting between 0-start and 1-start in RandomNumber call
            var pickedTopicIndex = RandomNumber.Next(0, unusedTopicNameLookupIds.Count() - 1);
            unusedTopicNameLookupIds = unusedTopicNameLookupIds.Select(id => id + 1).ToArray();

            CreateTopicNameMap(topicId, unusedTopicNameLookupIds[pickedTopicIndex]);

            return topicId;
        }

        private static int[] GetTopicNameLookupIdsForPost(int postId)
        {
            var topicNameLookupIds = new List<int>();
            using(var context = new X4HEContext())
            {
                var postTopics = context.PostTopics
                    .Where(pt => pt.PostId == postId);
                var topicNameMaps = context.TopicNameMaps.ToList();
                foreach(var postTopic in postTopics)
                {
                    topicNameLookupIds.Add(topicNameMaps.Where(tnm => tnm.TopicId == postTopic.TopicId).First().TopicNameLookupId);
                }
            }

            return topicNameLookupIds.ToArray();
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
            "Academics",
            "Student Life",
            "Student Aid"
        };

        private static string[] REACTION_NAMES = new string[]
        {
            "Like",
            "Heart",
            "Wow",
            "Bazinga",
            "Mid"
        };

        private static int POST_COUNT = 100000;

        private static int MINIMUM_COMMENTS = 0;
        private static int MAXIMUM_COMMENTS = 3;

        private static int MINIMUM_TOPICS = 0;
        private static int MAXIMUM_TOPICS = 5;

        private static int MINIMUM_REACTIONS = 0;
        private static int MAXIMUM_REACTIONS = 6;
    }
}
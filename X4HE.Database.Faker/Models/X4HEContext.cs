using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace X4HE.Database.Faker.Models
{
    public partial class X4HEContext : DbContext
    {
        public X4HEContext()
        {
        }

        public X4HEContext(DbContextOptions<X4HEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<CommentFeedContent> CommentFeedContents { get; set; } = null!;
        public virtual DbSet<CommentReaction> CommentReactions { get; set; } = null!;
        public virtual DbSet<FeedContent> FeedContents { get; set; } = null!;
        public virtual DbSet<FeedContentText> FeedContentTexts { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<PostFeedContent> PostFeedContents { get; set; } = null!;
        public virtual DbSet<PostReaction> PostReactions { get; set; } = null!;
        public virtual DbSet<PostTopic> PostTopics { get; set; } = null!;
        public virtual DbSet<Reaction> Reactions { get; set; } = null!;
        public virtual DbSet<ReactionNameLookup> ReactionNameLookups { get; set; } = null!;
        public virtual DbSet<ReactionNameMap> ReactionNameMaps { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<TopicNameLookup> TopicNameLookups { get; set; } = null!;
        public virtual DbSet<TopicNameMap> TopicNameMaps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=localhost;Database=X4HE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CommentFeedContent>(entity =>
            {
                entity.HasKey(e => new { e.CommentId, e.FeedContentId });

                entity.ToTable("CommentFeedContent");

                entity.HasIndex(e => e.FeedContentId, "UQ_CommentFeedContent_FeedContentID")
                    .IsUnique();

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.FeedContentId).HasColumnName("FeedContentID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentFeedContents)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentFeedContent_CommentID");

                entity.HasOne(d => d.FeedContent)
                    .WithOne(p => p.CommentFeedContent)
                    .HasForeignKey<CommentFeedContent>(d => d.FeedContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentFeedContent_FeedContentID");
            });

            modelBuilder.Entity<CommentReaction>(entity =>
            {
                entity.HasKey(e => new { e.CommentId, e.ReactionId });

                entity.ToTable("CommentReaction");

                entity.HasIndex(e => e.ReactionId, "UQ_CommentReaction_ReactionID")
                    .IsUnique();

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentReaction_CommentID");

                entity.HasOne(d => d.Reaction)
                    .WithOne(p => p.CommentReaction)
                    .HasForeignKey<CommentReaction>(d => d.ReactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentReaction_ReactionID");
            });

            modelBuilder.Entity<FeedContent>(entity =>
            {
                entity.ToTable("FeedContent");

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<FeedContentText>(entity =>
            {
                entity.ToTable("FeedContentText");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.FeedContentText)
                    .HasForeignKey<FeedContent>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedContentText_ID");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.CommentId });

                entity.ToTable("PostComment");

                entity.HasIndex(e => e.CommentId, "UQ_PostComment_CommentID")
                    .IsUnique();

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.HasOne(d => d.Comment)
                    .WithOne(p => p.PostComment)
                    .HasForeignKey<PostComment>(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComment_CommentID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComment_CPostID");
            });

            modelBuilder.Entity<PostFeedContent>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.FeedContentId });

                entity.ToTable("PostFeedContent");

                entity.HasIndex(e => e.FeedContentId, "UQ_PostFeedContent_FeedContentID")
                    .IsUnique();

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.FeedContentId).HasColumnName("FeedContentID");

                entity.HasOne(d => d.FeedContent)
                    .WithOne(p => p.PostFeedContent)
                    .HasForeignKey<PostFeedContent>(d => d.FeedContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostFeedContent_FeedContentID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostFeedContents)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostFeedContent_PostID");
            });

            modelBuilder.Entity<PostReaction>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.ReactionId });

                entity.ToTable("PostReaction");

                entity.HasIndex(e => e.ReactionId, "UQ_PostReaction_ReactionID")
                    .IsUnique();

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostReaction_PostID");

                entity.HasOne(d => d.Reaction)
                    .WithOne(p => p.PostReaction)
                    .HasForeignKey<PostReaction>(d => d.ReactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostReaction_ReactionID");
            });

            modelBuilder.Entity<PostTopic>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.TopicId });

                entity.ToTable("PostTopic");

                entity.HasIndex(e => e.TopicId, "UQ_PostTopic_TopicID")
                    .IsUnique();

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTopics)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTopic_PostID");

                entity.HasOne(d => d.Topic)
                    .WithOne(p => p.PostTopic)
                    .HasForeignKey<PostTopic>(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTopic_TopicID");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.ToTable("Reaction");

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ReactionNameLookup>(entity =>
            {
                entity.ToTable("ReactionNameLookup");

                entity.HasIndex(e => e.Name, "UQ_ReactionNameLookup_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(64);
            });

            modelBuilder.Entity<ReactionNameMap>(entity =>
            {
                entity.HasKey(e => new { e.ReactionId, e.ReactionNameLookupId });

                entity.ToTable("ReactionNameMap");

                entity.HasIndex(e => e.ReactionId, "UQ_ReactionNameMap_ReactionID")
                    .IsUnique();

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.ReactionNameLookupId).HasColumnName("ReactionNameLookupID");

                entity.HasOne(d => d.Reaction)
                    .WithOne(p => p.ReactionNameMap)
                    .HasForeignKey<ReactionNameMap>(d => d.ReactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReactionNameMap_ReactionID");

                entity.HasOne(d => d.ReactionNameLookup)
                    .WithMany(p => p.ReactionNameMap)
                    .HasForeignKey(d => d.ReactionNameLookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReactionNameMap_ReactionNameLookupID");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TopicNameLookup>(entity =>
            {
                entity.ToTable("TopicNameLookup");

                entity.HasIndex(e => e.Name, "UQ_TopicNameLookup_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(64);
            });

            modelBuilder.Entity<TopicNameMap>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.TopicNameLookupId });

                entity.ToTable("TopicNameMap");

                entity.HasIndex(e => e.TopicId, "UQ_TopicNameMap_TopicID")
                    .IsUnique();

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.Property(e => e.TopicNameLookupId).HasColumnName("TopicNameLookupID");

                entity.HasOne(d => d.Topic)
                    .WithOne(p => p.TopicNameMap)
                    .HasForeignKey<TopicNameMap>(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicNameMap_TopicID");

                entity.HasOne(d => d.TopicNameLookup)
                    .WithMany(p => p.TopicNameMap)
                    .HasForeignKey(d => d.TopicNameLookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicNameMap_TopicNameLookupID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
